namespace PdfiumViewer.PasysDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnZoomMinus = new System.Windows.Forms.Button();
            this.btnZoomPlus = new System.Windows.Forms.Button();
            this.num = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintview = new System.Windows.Forms.Button();
            this.btnToBitmaps = new System.Windows.Forms.Button();
            this.btnFitWidth = new System.Windows.Forms.Button();
            this.btnFitHeight = new System.Windows.Forms.Button();
            this.pdfViewer = new PdfiumViewer.PasysPdfViewer();
            this.btnRotate0 = new System.Windows.Forms.Button();
            this.btnRotate90 = new System.Windows.Forms.Button();
            this.btnRotate180 = new System.Windows.Forms.Button();
            this.btnRotate270 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num)).BeginInit();
            this.SuspendLayout();
            // 
            // btnZoomMinus
            // 
            this.btnZoomMinus.Location = new System.Drawing.Point(520, 10);
            this.btnZoomMinus.Name = "btnZoomMinus";
            this.btnZoomMinus.Size = new System.Drawing.Size(75, 23);
            this.btnZoomMinus.TabIndex = 0;
            this.btnZoomMinus.Text = "Zoom-";
            this.btnZoomMinus.UseVisualStyleBackColor = true;
            this.btnZoomMinus.Click += new System.EventHandler(this.btnZoomMinus_Click);
            // 
            // btnZoomPlus
            // 
            this.btnZoomPlus.Location = new System.Drawing.Point(369, 10);
            this.btnZoomPlus.Name = "btnZoomPlus";
            this.btnZoomPlus.Size = new System.Drawing.Size(75, 23);
            this.btnZoomPlus.TabIndex = 0;
            this.btnZoomPlus.Text = "Zoom+";
            this.btnZoomPlus.UseVisualStyleBackColor = true;
            this.btnZoomPlus.Click += new System.EventHandler(this.btnZoomPlus_Click);
            // 
            // num
            // 
            this.num.DecimalPlaces = 1;
            this.num.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.num.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num.Location = new System.Drawing.Point(451, 12);
            this.num.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.num.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.num.Name = "num";
            this.num.Size = new System.Drawing.Size(63, 21);
            this.num.TabIndex = 1;
            this.num.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 10F);
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(270, 21);
            this.textBox1.TabIndex = 2;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(288, 10);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "選択・・・";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(601, 10);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAs.TabIndex = 0;
            this.btnSaveAs.Text = "SaveAs...";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(682, 10);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintview
            // 
            this.btnPrintview.Location = new System.Drawing.Point(763, 10);
            this.btnPrintview.Name = "btnPrintview";
            this.btnPrintview.Size = new System.Drawing.Size(75, 23);
            this.btnPrintview.TabIndex = 0;
            this.btnPrintview.Text = "Printview";
            this.btnPrintview.UseVisualStyleBackColor = true;
            this.btnPrintview.Click += new System.EventHandler(this.btnPrintview_Click);
            // 
            // btnToBitmaps
            // 
            this.btnToBitmaps.Location = new System.Drawing.Point(844, 10);
            this.btnToBitmaps.Name = "btnToBitmaps";
            this.btnToBitmaps.Size = new System.Drawing.Size(75, 23);
            this.btnToBitmaps.TabIndex = 0;
            this.btnToBitmaps.Text = "ToBitmaps";
            this.btnToBitmaps.UseVisualStyleBackColor = true;
            this.btnToBitmaps.Click += new System.EventHandler(this.btnToBitmaps_Click);
            // 
            // btnFitWidth
            // 
            this.btnFitWidth.Location = new System.Drawing.Point(925, 10);
            this.btnFitWidth.Name = "btnFitWidth";
            this.btnFitWidth.Size = new System.Drawing.Size(75, 23);
            this.btnFitWidth.TabIndex = 0;
            this.btnFitWidth.Text = "FitWidth";
            this.btnFitWidth.UseVisualStyleBackColor = true;
            this.btnFitWidth.Click += new System.EventHandler(this.btnFitWidth_Click);
            // 
            // btnFitHeight
            // 
            this.btnFitHeight.Location = new System.Drawing.Point(1006, 10);
            this.btnFitHeight.Name = "btnFitHeight";
            this.btnFitHeight.Size = new System.Drawing.Size(75, 23);
            this.btnFitHeight.TabIndex = 0;
            this.btnFitHeight.Text = "FitHeight";
            this.btnFitHeight.UseVisualStyleBackColor = true;
            this.btnFitHeight.Click += new System.EventHandler(this.btnFitHeight_Click);
            // 
            // pdfViewer
            // 
            this.pdfViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfViewer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pdfViewer.BaseSaveBitmapsPath = null;
            this.pdfViewer.Location = new System.Drawing.Point(12, 63);
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.Size = new System.Drawing.Size(1171, 643);
            this.pdfViewer.TabIndex = 3;
            this.pdfViewer.Zoom = 1.728D;
            this.pdfViewer.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitHeight;
            // 
            // btnRotate0
            // 
            this.btnRotate0.Location = new System.Drawing.Point(763, 36);
            this.btnRotate0.Name = "btnRotate0";
            this.btnRotate0.Size = new System.Drawing.Size(75, 23);
            this.btnRotate0.TabIndex = 0;
            this.btnRotate0.Text = "Rotate0";
            this.btnRotate0.UseVisualStyleBackColor = true;
            this.btnRotate0.Click += new System.EventHandler(this.btnRotate0_Click);
            // 
            // btnRotate90
            // 
            this.btnRotate90.Location = new System.Drawing.Point(844, 36);
            this.btnRotate90.Name = "btnRotate90";
            this.btnRotate90.Size = new System.Drawing.Size(75, 23);
            this.btnRotate90.TabIndex = 0;
            this.btnRotate90.Text = "Rotate90";
            this.btnRotate90.UseVisualStyleBackColor = true;
            this.btnRotate90.Click += new System.EventHandler(this.btnRotate90_Click);
            // 
            // btnRotate180
            // 
            this.btnRotate180.Location = new System.Drawing.Point(925, 36);
            this.btnRotate180.Name = "btnRotate180";
            this.btnRotate180.Size = new System.Drawing.Size(75, 23);
            this.btnRotate180.TabIndex = 0;
            this.btnRotate180.Text = "Rotate180";
            this.btnRotate180.UseVisualStyleBackColor = true;
            this.btnRotate180.Click += new System.EventHandler(this.btnRotate180_Click);
            // 
            // btnRotate270
            // 
            this.btnRotate270.Location = new System.Drawing.Point(1006, 36);
            this.btnRotate270.Name = "btnRotate270";
            this.btnRotate270.Size = new System.Drawing.Size(75, 23);
            this.btnRotate270.TabIndex = 0;
            this.btnRotate270.Text = "Rotate270";
            this.btnRotate270.UseVisualStyleBackColor = true;
            this.btnRotate270.Click += new System.EventHandler(this.btnRotate270_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 718);
            this.Controls.Add(this.pdfViewer);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.num);
            this.Controls.Add(this.btnZoomPlus);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnRotate270);
            this.Controls.Add(this.btnRotate180);
            this.Controls.Add(this.btnRotate90);
            this.Controls.Add(this.btnRotate0);
            this.Controls.Add(this.btnFitHeight);
            this.Controls.Add(this.btnFitWidth);
            this.Controls.Add(this.btnToBitmaps);
            this.Controls.Add(this.btnPrintview);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnZoomMinus);
            this.Name = "Form1";
            this.Text = "PasysDemo";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnZoomMinus;
        private System.Windows.Forms.Button btnZoomPlus;
        private System.Windows.Forms.NumericUpDown num;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintview;
        private System.Windows.Forms.Button btnToBitmaps;
        private PasysPdfViewer pdfViewer;
        private System.Windows.Forms.Button btnFitWidth;
        private System.Windows.Forms.Button btnFitHeight;
        private System.Windows.Forms.Button btnRotate0;
        private System.Windows.Forms.Button btnRotate90;
        private System.Windows.Forms.Button btnRotate180;
        private System.Windows.Forms.Button btnRotate270;
    }
}

