﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfiumViewer.PasysDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pdfViewer.BaseSaveBitmapsPath = @"d:\testPdf";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (var form = new OpenFileDialog())
            {
                form.Filter = "PDF Files (*.pdf)|*.pdf|All Files (*.*)|*.*";
                form.RestoreDirectory = true;
                form.Title = "Open PDF File";

                if (form.ShowDialog(this) != DialogResult.OK)
                {
                    Dispose();
                    return;
                }

                textBox1.Text = form.FileName;
                pdfViewer.LoadFile(form.FileName);
            }
        }

        private void btnZoomPlus_Click(object sender, EventArgs e)
        {
            pdfViewer.Zoom = (double)(num.Value + 0.2m);
            num.Value += 0.2m;
        }

        private void btnZoomMinus_Click(object sender, EventArgs e)
        {
            pdfViewer.Zoom = (double)(num.Value - 0.2m);
            num.Value -= 0.2m;
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            pdfViewer.SaveAs();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            pdfViewer.Print();
        }

        private void btnPrintview_Click(object sender, EventArgs e)
        {
            pdfViewer.PrintPreview();
        }

        private void btnToBitmaps_Click(object sender, EventArgs e)
        {
            pdfViewer.ToBitmaps(0.8f);
        }

        private void btnFitWidth_Click(object sender, EventArgs e)
        {
            pdfViewer.FitPage(PdfViewerZoomMode.FitWidth);
        }

        private void btnFitHeight_Click(object sender, EventArgs e)
        {
            pdfViewer.FitPage(PdfViewerZoomMode.FitHeight);
        }

        private void btnRotate0_Click(object sender, EventArgs e)
        {
            pdfViewer.RotateType = RotateType.RotateNone;
        }

        private void btnRotate90_Click(object sender, EventArgs e)
        {
            pdfViewer.RotateType = RotateType.Rotate90;
        }

        private void btnRotate180_Click(object sender, EventArgs e)
        {
            pdfViewer.RotateType = RotateType.Rotate180;
        }

        private void btnRotate270_Click(object sender, EventArgs e)
        {
            pdfViewer.RotateType = RotateType.Rotate270;
        }
    }
}
