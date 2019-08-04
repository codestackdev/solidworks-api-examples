namespace CodeStack.SwEx.AddIn.Examples.IssuesManager.Views
{
    partial class IssuesControlHost
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlHost = new System.Windows.Forms.Integration.ElementHost();
            this.ctrlIssues = new CodeStack.SwEx.AddIn.Examples.IssuesManager.Views.IssuesControl();
            this.SuspendLayout();
            // 
            // ctrlHost
            // 
            this.ctrlHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlHost.Location = new System.Drawing.Point(0, 0);
            this.ctrlHost.Name = "ctrlHost";
            this.ctrlHost.Size = new System.Drawing.Size(247, 223);
            this.ctrlHost.TabIndex = 0;
            this.ctrlHost.Text = "elementHost1";
            this.ctrlHost.Child = this.ctrlIssues;
            // 
            // IssuesControlHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlHost);
            this.Name = "IssuesControlHost";
            this.Size = new System.Drawing.Size(247, 223);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost ctrlHost;
        private IssuesControl ctrlIssues;
    }
}
