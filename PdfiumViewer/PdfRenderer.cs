﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PdfiumViewer
{
    /// <summary>
    /// Control to render PDF documents.
    /// </summary>
    public class PdfRenderer : PanningZoomingScrollControl
    {
        private static readonly Padding PageMargin = new Padding(4);

        private double _width;
        private double _height;
        private double _maxWidth;
        private double _maxHeight;
        private bool _disposed;
        private double _scaleFactor;
        private ShadeBorder _shadeBorder = new ShadeBorder();
        private int _suspendPaintCount;
        private PdfDocument _document;
        private ToolTip _toolTip;
        private PdfViewerZoomMode _zoomMode;
        private RotateType _rotateType;
        private bool _pageCacheValid;
        private readonly List<PageCache> _pageCache = new List<PageCache>();
        private int _visiblePageStart;
        private int _visiblePageEnd;
        private PdfPageLink _cachedLink;
        private DragState _dragState;

        /// <summary>
        /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
        /// </summary>
        /// 
        /// <returns>
        /// true if the user can give the focus to the control using the TAB key; otherwise, false. The default is true.Note:This property will always return true for an instance of the <see cref="T:System.Windows.Forms.Form"/> class.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [DefaultValue(true)]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }

        public int Page
        {
            get
            {
                if (_document == null || !_pageCacheValid)
                    return 0;

                int top = -DisplayRectangle.Top + (int)(ClientSize.Height * Zoom) / 2;

                for (int page = 0; page < _document.PageSizes.Count; page++)
                {
                    var pageCache = _pageCache[page].OuterBounds;
                    if (top >= pageCache.Top && top < pageCache.Bottom)
                        return page;
                }

                return _document.PageCount - 1;
            }
            set
            {
                if (_document == null)
                {
                    SetDisplayRectLocation(new Point(0, 0));
                }
                else
                {
                    int page = Math.Min(Math.Max(value, 0), _document.PageCount - 1);

                    SetDisplayRectLocation(new Point(0, -_pageCache[page].OuterBounds.Top));
                }
            }
        }

        /// <summary>
        /// Gets or sets the way the document should be zoomed initially.
        /// </summary>
        public PdfViewerZoomMode ZoomMode
        {
            get { return _zoomMode; }
            set
            {
                _zoomMode = value;
                PerformLayout();
            }
        }

        public RotateType RotateType
        {
            get { return _rotateType; }
            set
            {
                _width = 0;
                _height = 0;
                _maxWidth = 0;
                _maxHeight = 0;
                switch (value)
                {
                    case PdfiumViewer.RotateType.RotateNone:
                    case PdfiumViewer.RotateType.Rotate180:
                        foreach (var size in _document.PageSizes)
                        {
                            _width += size.Width;
                            _height += size.Height;
                            _maxWidth = Math.Max(size.Width, _maxWidth);
                            _maxHeight = Math.Max(size.Height, _maxHeight);
                        }
                        break;
                    case PdfiumViewer.RotateType.Rotate90:
                    case PdfiumViewer.RotateType.Rotate270:
                        foreach (var size in _document.PageSizes)
                        {
                            _height += size.Width;
                            _width += size.Height;
                            _maxWidth = Math.Max(size.Height, _maxWidth);
                            _maxHeight = Math.Max(size.Width, _maxHeight);
                        }
                        break;
                }
                _rotateType = value;
                PerformLayout();
                Invalidate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the PdfRenderer class.
        /// </summary>
        public PdfRenderer()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);

            TabStop = true;

            _toolTip = new ToolTip();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Layout"/> event.
        /// </summary>
        /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs"/> that contains the event data. </param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            UpdateScrollbars();
        }

        protected override void OnZoomChanged(EventArgs e)
        {
            base.OnZoomChanged(e);

            UpdateScrollbars();
        }

        /// <summary>
        /// Load a <see cref="PdfDocument"/> into the control.
        /// </summary>
        /// <param name="document">Document to load.</param>
        public void Load(PdfDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (document.PageCount == 0)
                throw new ArgumentException("Document does not contain any pages", "document");

            _document = document;

            SetDisplayRectLocation(new Point(0, 0));

            _height = 0;
            _maxWidth = 0;
            _maxHeight = 0;

            foreach (var size in _document.PageSizes)
            {
                _width += size.Width;
                _height += size.Height;
                _maxWidth = Math.Max(size.Width, _maxWidth);
                _maxHeight = Math.Max(size.Height, _maxHeight);
            }

            UpdateScrollbars();

            Invalidate();
        }

        private void UpdateScrollbars()
        {
            if (_document == null)
                return;

            UpdateScaleFactor(ScrollBars.Both);

            var bounds = GetScrollClientArea(ScrollBars.Both);

            var documentSize = GetDocumentBounds().Size;

            bool horizontalVisible = documentSize.Width > bounds.Width;

            if (!horizontalVisible)
            {
                UpdateScaleFactor(ScrollBars.Vertical);

                documentSize = GetDocumentBounds().Size;
            }

            _suspendPaintCount++;

            try
            {
                SetDisplaySize(documentSize);
            }
            finally
            {
                _suspendPaintCount--;
            }

            RebuildPageCache();
        }

        private void RebuildPageCache()
        {
            if (_document == null || _suspendPaintCount > 0)
                return;

            _pageCacheValid = true;

            int maxWidth = 0;
            int leftOffset = 0;
            //Pasys^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^Added
            switch (_rotateType)
            {
                case PdfiumViewer.RotateType.RotateNone:
                case PdfiumViewer.RotateType.Rotate180:
                    maxWidth = (int)(_maxWidth * _scaleFactor) + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                    leftOffset = -maxWidth / 2;
                    break;
                case PdfiumViewer.RotateType.Rotate90:
                case PdfiumViewer.RotateType.Rotate270:
                    maxWidth = (int)(_maxHeight * _scaleFactor) + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                    leftOffset = -maxWidth / 2;
                    break;
            }
            //int maxWidth = (int)(_maxWidth * _scaleFactor) + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
            //int leftOffset = -maxWidth / 2;
            //Pasys^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^Added

            int offset = 0;

            for (int page = 0; page < _document.PageSizes.Count; page++)
            {
                var size = _document.PageSizes[page];
                double height = 0;
                double fullHeight = 0;
                double width = 0;
                double maxFullWidth = 0;
                double fullWidth = 0;
                double thisLeftOffset = 0;
                //Pasys^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^Added
                switch (_rotateType)
                {
                    case PdfiumViewer.RotateType.RotateNone:
                    case PdfiumViewer.RotateType.Rotate180:
                        height = size.Height * _scaleFactor;
                        fullHeight = height + ShadeBorder.Size.Vertical + PageMargin.Vertical;
                        width = size.Width * _scaleFactor;
                        maxFullWidth = _maxWidth * _scaleFactor + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                        fullWidth = width + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                        thisLeftOffset = leftOffset + (maxFullWidth - fullWidth) / 2d;
                        break;
                    case PdfiumViewer.RotateType.Rotate90:
                    case PdfiumViewer.RotateType.Rotate270:
                        height = (int)(size.Width * _scaleFactor);
                        fullHeight = height + ShadeBorder.Size.Vertical + PageMargin.Vertical;

                        width = (int)(size.Height * _scaleFactor);
                        maxFullWidth = (int)(_maxHeight * _scaleFactor) + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                        fullWidth = width + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                        thisLeftOffset = leftOffset + (maxFullWidth - fullWidth) / 2d;
                        break;
                }
                //int height = (int)(size.Height * _scaleFactor);
                //int fullHeight = height + ShadeBorder.Size.Vertical + PageMargin.Vertical;
                //int width = (int)(size.Width * _scaleFactor);
                //int maxFullWidth = (int)(_maxWidth * _scaleFactor) + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                //int fullWidth = width + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                //int thisLeftOffset = leftOffset + (maxFullWidth - fullWidth) / 2;
                //Pasys^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^Added

                while (_pageCache.Count <= page)
                {
                    _pageCache.Add(new PageCache());
                }

                var pageCache = _pageCache[page];

                pageCache.Links = null;
                pageCache.Bounds = new Rectangle(
                    (int)thisLeftOffset + ShadeBorder.Size.Left + PageMargin.Left,
                    offset + ShadeBorder.Size.Top + PageMargin.Top,
                    (int)width,
                    (int)height
                );
                pageCache.OuterBounds = new Rectangle(
                    (int)thisLeftOffset,
                    offset,
                    (int)width + ShadeBorder.Size.Horizontal + PageMargin.Horizontal,
                    (int)height + ShadeBorder.Size.Vertical + PageMargin.Vertical
                );

                offset += (int)fullHeight;
            }
        }

        private PdfPageLinks GetPageLinks(int page)
        {
            var pageCache = _pageCache[page];
            if (pageCache.Links == null)
                pageCache.Links = _document.GetPageLinks(page, pageCache.Bounds.Size);

            return pageCache.Links;
        }

        private Rectangle GetScrollClientArea()
        {
            ScrollBars scrollBarsVisible;

            if (HScroll && VScroll)
                scrollBarsVisible = ScrollBars.Both;
            else if (HScroll)
                scrollBarsVisible = ScrollBars.Horizontal;
            else if (VScroll)
                scrollBarsVisible = ScrollBars.Vertical;
            else
                scrollBarsVisible = ScrollBars.None;

            return GetScrollClientArea(scrollBarsVisible);
        }

        private Rectangle GetScrollClientArea(ScrollBars scrollbars)
        {
            return new Rectangle(
                0,
                0,
                scrollbars == ScrollBars.Vertical || scrollbars == ScrollBars.Both ? Width - SystemInformation.VerticalScrollBarWidth : Width,
                scrollbars == ScrollBars.Horizontal || scrollbars == ScrollBars.Both ? Height - SystemInformation.HorizontalScrollBarHeight : Height
            );
        }

        private void UpdateScaleFactor(ScrollBars scrollBars)
        {
            var bounds = GetScrollClientArea(scrollBars);

            // Scale factor determines what we need to multiply the dimensions
            // of the metafile with to get the size in the control.

            if (ZoomMode == PdfViewerZoomMode.FitHeight)
            {
                int height = bounds.Height - ShadeBorder.Size.Vertical - PageMargin.Vertical;

                _scaleFactor = ((double)height / _maxHeight) * Zoom;
            }
            else
            {
                int width = bounds.Width - ShadeBorder.Size.Horizontal - PageMargin.Horizontal;

                _scaleFactor = ((double)width / _maxWidth) * Zoom;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_document == null || _suspendPaintCount > 0)
                return;

            var bounds = GetScrollClientArea();
            int maxWidth = (int)(_maxWidth * _scaleFactor) + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
            int leftOffset = (HScroll ? DisplayRectangle.X : (bounds.Width - maxWidth) / 2) + maxWidth / 2;
            int topOffset = VScroll ? DisplayRectangle.Y : 0;

            using (var brush = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(brush, e.ClipRectangle);
            }

            _visiblePageStart = -1;
            _visiblePageEnd = -1;

            for (int page = 0; page < _document.PageSizes.Count; page++)
            {
                var pageCache = _pageCache[page];
                var rectangle = pageCache.OuterBounds;
                rectangle.Offset(leftOffset, topOffset);

                if (_visiblePageStart == -1 && rectangle.Bottom >= 0)
                    _visiblePageStart = page;
                if (_visiblePageEnd == -1 && rectangle.Top > bounds.Height)
                    _visiblePageEnd = page - 1;

                if (e.ClipRectangle.IntersectsWith(rectangle))
                {
                    var pageBounds = pageCache.Bounds;
                    pageBounds.Offset(leftOffset, topOffset);

                    e.Graphics.FillRectangle(Brushes.White, pageBounds);

                    DrawPageImage(e.Graphics, page, pageBounds);

                    _shadeBorder.Draw(e.Graphics, pageBounds);
                    var pageOutBounds = pageCache.OuterBounds;
                    pageOutBounds.Offset(leftOffset, topOffset);
                    e.Graphics.DrawRectangle(Pens.Blue, pageOutBounds);
                }
            }

            if (_visiblePageStart == -1)
                _visiblePageStart = 0;
            if (_visiblePageEnd == -1)
                _visiblePageEnd = _document.PageCount - 1;
        }

        private void DrawPageImage(Graphics graphics, int page, Rectangle pageBounds)
        {
            _document.Render(page, graphics, graphics.DpiX, graphics.DpiY, pageBounds, (int)_rotateType, false);
        }

        protected override Rectangle GetDocumentBounds()
        {

            //Pasys^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^Added
            int height = 0;
            int width = 0;
            switch (_rotateType)
            {
                case PdfiumViewer.RotateType.RotateNone:
                case PdfiumViewer.RotateType.Rotate180:
                    height = (int)(_height * _scaleFactor + (ShadeBorder.Size.Vertical + PageMargin.Vertical) * _document.PageCount);
                    width = (int)(_maxWidth * _scaleFactor + ShadeBorder.Size.Horizontal + PageMargin.Horizontal);
                    break;
                case PdfiumViewer.RotateType.Rotate90:
                case PdfiumViewer.RotateType.Rotate270:
                    height = (int)(_height * _scaleFactor + (ShadeBorder.Size.Vertical + PageMargin.Vertical) * _document.PageCount);
                    width = (int)(_maxWidth * _scaleFactor + ShadeBorder.Size.Horizontal + PageMargin.Horizontal);
                    break;
            }

            //int height = (int)(_height * _scaleFactor + (ShadeBorder.Size.Vertical + PageMargin.Vertical) * _document.PageCount);
            //int width = (int)(_maxWidth * _scaleFactor + ShadeBorder.Size.Horizontal + PageMargin.Horizontal);
            //Pasys^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^Added

            var center = new Point(
                DisplayRectangle.Width / 2,
                DisplayRectangle.Height / 2
            );

            if (
                DisplayRectangle.Width > ClientSize.Width ||
                DisplayRectangle.Height > ClientSize.Height
            )
            {
                center.X += DisplayRectangle.Left;
                center.Y += DisplayRectangle.Top;
            }

            return new Rectangle(
                center.X - width / 2,
                center.Y - height / 2,
                width,
                height
            );
        }

        protected override void OnSetCursor(SetCursorEventArgs e)
        {
            _cachedLink = null;

            if (_pageCacheValid)
            {
                var bounds = GetScrollClientArea();
                int maxWidth = (int)(_maxWidth * _scaleFactor) + ShadeBorder.Size.Horizontal + PageMargin.Horizontal;
                int leftOffset = (HScroll ? DisplayRectangle.X : (bounds.Width - maxWidth) / 2) + maxWidth / 2;

                var displayLocation = DisplayRectangle.Location;

                var location = new Point(
                    e.Location.X,
                    e.Location.Y - displayLocation.Y
                );

                for (int page = _visiblePageStart; page <= _visiblePageEnd; page++)
                {
                    var links = GetPageLinks(page);

                    var pageLocation = location;
                    var pageBounds = _pageCache[page].Bounds;
                    pageLocation.X -= leftOffset + pageBounds.Left;
                    pageLocation.Y -= pageBounds.Top;

                    foreach (var link in links.Links)
                    {
                        if (link.Bounds.Contains(pageLocation))
                        {
                            _cachedLink = link;
                            e.Cursor = Cursors.Hand;
                            return;
                        }
                    }
                }
            }

            base.OnSetCursor(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            _dragState = null;

            if (_cachedLink != null)
            {
                _dragState = new DragState
                {
                    Link = _cachedLink,
                    Location = e.Location
                };
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (_dragState == null)
                return;

            int dx = Math.Abs(e.Location.X - _dragState.Location.X);
            int dy = Math.Abs(e.Location.Y - _dragState.Location.Y);

            var link = _dragState.Link;
            _dragState = null;

            if (link == null)
                return;

            if (dx <= SystemInformation.DragSize.Width && dy <= SystemInformation.DragSize.Height)
            {
                if (link.TargetPage.HasValue)
                    Page = link.TargetPage.Value;

                if (link.Uri != null)
                {
                    try
                    {
                        Process.Start(link.Uri);
                    }
                    catch
                    {
                        // Some browsers (Firefox) will cause an exception to
                        // be thrown (when it auto-updates).
                    }
                }
            }
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control"/> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        protected override void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                if (_shadeBorder != null)
                {
                    _shadeBorder.Dispose();
                    _shadeBorder = null;
                }

                if (_toolTip != null)
                {
                    _toolTip.Dispose();
                    _toolTip = null;
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }

        private class PageCache
        {
            public PdfPageLinks Links { get; set; }
            public Rectangle Bounds { get; set; }
            public Rectangle OuterBounds { get; set; }
        }

        private class DragState
        {
            public PdfPageLink Link { get; set; }
            public Point Location { get; set; }
        }
    }
}
