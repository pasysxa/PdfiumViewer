using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PdfiumViewer
{
    /// <summary>
    /// Control to host PDF documents with support for printing.
    /// </summary>
    public partial class PasysPdfViewer : UserControl
    {
        private PdfDocument _document;

        public PasysPdfViewer()
        {
            InitializeComponent();
            DefaultPrintMode = PdfPrintMode.CutMargin;
        }

        /// <summary>
        /// Gets or sets the PDF document.
        /// </summary>
        protected PdfDocument Document
        {
            get { return _document; }
            set
            {
                if (_document != value)
                {
                    _document = value;

                    if (_document != null)
                        renderer.Load(_document);
                }
            }
        }

        /// <summary>
        /// Gets or sets the default print mode.
        /// </summary>
        [DefaultValue(PdfPrintMode.CutMargin)]
        public PdfPrintMode DefaultPrintMode { get; set; }

        /// <summary>
        /// Gets or sets the way the document should be zoomed initially.
        /// </summary>
        public PdfViewerZoomMode ZoomMode
        {
            get { return renderer.ZoomMode; }
            set { renderer.ZoomMode = value; }
        }

        public double Zoom
        {
            get
            {
                return renderer.Zoom;
            }
            set
            {
                renderer.ZoomFactor = 1;
                renderer.Zoom = renderer.ZoomFactor * value;
            }
        }

        private string _fileName = string.Empty;
        public void LoadFile(string fileName)
        {
            this.Document = PdfDocument.Load(fileName);
            _fileName = fileName;
        }

        public void SaveAs()
        {
            using (var form = new SaveFileDialog())
            {
                form.DefaultExt = ".pdf";
                form.Filter = Properties.Resources.SaveAsFilter;
                form.RestoreDirectory = true;
                form.Title = Properties.Resources.SaveAsTitle;

                if (form.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    try
                    {
                        _document.Save(form.FileName);
                    }
                    catch
                    {
                        MessageBox.Show(
                            FindForm(),
                            Properties.Resources.SaveAsFailedText,
                            Properties.Resources.SaveAsFailedTitle,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
        }

        public void Print()
        {
            if (Document.PageCount > 0)
            {
                using (var printDocument = Document.CreatePrintDocument(DefaultPrintMode))
                {
                    try
                    {
                        printDocument.Print();
                    }
                    catch
                    {
                    }
                }
            }
        }

        public void PrintPreview()
        {
            using (var form = new PrintDialog())
            {
                using (var document = Document.CreatePrintDocument(DefaultPrintMode))
                {
                    form.AllowSomePages = true;
                    form.Document = document;
                    form.UseEXDialog = true;
                    form.Document.PrinterSettings.FromPage = 1;
                    form.Document.PrinterSettings.ToPage = Document.PageCount;

                    this.BeginInvoke(new Action(() =>
                    {
                        if (form.ShowDialog(FindForm()) == DialogResult.OK)
                        {
                            try
                            {
                                if (form.Document.PrinterSettings.FromPage <= Document.PageCount)
                                    form.Document.Print();
                            }
                            catch
                            {
                                // Ignore exceptions; the printer dialog should take care of this.
                            }
                        }
                    }));
                }
            }
        }

        /// <summary>
        /// 幅もしくは高さに合わせる
        /// </summary>
        /// <param name="zoomMode"></param>
        public void FitPage(PdfViewerZoomMode zoomMode)
        {
            int page = renderer.Page;
            renderer.ZoomMode = zoomMode;
            renderer.Zoom = 1;
            renderer.Page = page;
        }

        public string BaseSaveBitmapsPath
        {
            get;
            set;
        }

        public void Clear()
        {
            Document.Dispose();
            _fileName = string.Empty;
        }

        /// <summary>
        /// イメージの生成
        /// </summary>
        /// <param name="reduceRate">イメージの縮小率</param>
        public void ToBitmaps(float reduceRate)
        {
            if (reduceRate < 0.1 || reduceRate > 1)
            {
                new ArgumentException("0.1~1間の値を設定しかないです");
            }

            if (string.IsNullOrEmpty(BaseSaveBitmapsPath) || !Directory.Exists(BaseSaveBitmapsPath))
            {
                new ArgumentException("指定したディレクトリは存在しません。");
            }

            var folderName = Path.GetRandomFileName();
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(Path.Combine(BaseSaveBitmapsPath, folderName));
            }

            try
            {
                var fileName = Path.GetFileNameWithoutExtension(_fileName);
                for (int i = 0; i < Document.PageCount; i++)
                {
                    var pageSize = Document.PageSizes[i].ToSize();
                    var imageWidth = (int)(pageSize.Width * reduceRate);
                    var imageHeight = (int)(pageSize.Height * reduceRate);

                    using (var image = Document.Render(
                            i, imageWidth, imageHeight, 96, 96, false))
                    {
                        image.Save(Path.Combine(BaseSaveBitmapsPath, folderName, string.Format("{0}_{1}.png", fileName, i)));
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// イメージの生成
        /// </summary>
        /// <param name="imageSize">イメージのサイズ</param>
        public void ToBitmaps(Size imageSize)
        {
            if (string.IsNullOrEmpty(BaseSaveBitmapsPath) || !Directory.Exists(BaseSaveBitmapsPath))
            {
                new ArgumentException("指定したディレクトリは存在しません。");
            }

            if (imageSize.Width <= 0 || imageSize.Height <= 0)
            {
                new ArgumentException("0以下の数字を指定できません。");
            }

            var folderName = Path.GetRandomFileName();
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(Path.Combine(BaseSaveBitmapsPath, folderName));
            }

            try
            {
                var fileName = Path.GetFileNameWithoutExtension(_fileName);
                for (int i = 0; i < Document.PageCount; i++)
                {
                    var pageSize = Document.PageSizes[i].ToSize();
                    var imageWidth = imageSize.Width;
                    var imageHeight = imageSize.Height;

                    if (imageWidth > pageSize.Width)
                    {
                        imageWidth = pageSize.Width;
                    }
                    if (imageHeight > pageSize.Height)
                    {
                        imageHeight = pageSize.Height;
                    }

                    using (var image = Document.Render(
                            i, imageWidth, imageHeight, 96, 96, false))
                    {
                        image.Save(Path.Combine(BaseSaveBitmapsPath, folderName, string.Format("{0}_{1}.png", fileName, i)));
                    }
                }
            }
            catch
            {

            }
        }
    }
}
