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
            this.pdfViewer = new PdfiumViewer.PasysPdfViewer();
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
            // pdfViewer
            // 
            this.pdfViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pdfViewer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pdfViewer.BaseSaveBitmapsPath = null;
            this.pdfViewer.Location = new System.Drawing.Point(12, 53);
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.Size = new System.Drawing.Size(1029, 624);
            this.pdfViewer.TabIndex = 3;
            this.pdfViewer.Zoom = 1D;
            this.pdfViewer.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitHeight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 689);
            this.Controls.Add(this.pdfViewer);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.num);
            this.Controls.Add(this.btnZoomPlus);
            this.Controls.Add(this.btnSelect);
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
    }
}

