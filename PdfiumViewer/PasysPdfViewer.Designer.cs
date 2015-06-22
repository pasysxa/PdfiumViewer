namespace PdfiumViewer
{
    partial class PasysPdfViewer
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.renderer = new PdfiumViewer.PdfRenderer();
            this.SuspendLayout();
            // 
            // renderer
            // 
            this.renderer.Cursor = System.Windows.Forms.Cursors.Default;
            this.renderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderer.Location = new System.Drawing.Point(0, 0);
            this.renderer.Name = "renderer";
            this.renderer.Page = 0;
            this.renderer.Size = new System.Drawing.Size(173, 154);
            this.renderer.TabIndex = 2;
            this.renderer.Text = "pdfRenderer1";
            this.renderer.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitHeight;
            // 
            // PasysPdfViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.renderer);
            this.Name = "PasysPdfViewer";
            this.Size = new System.Drawing.Size(173, 154);
            this.ResumeLayout(false);

        }

        #endregion

        private PdfRenderer renderer;
    }
}
