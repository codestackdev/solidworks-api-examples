using SolidWorks.Interop.swconst;

namespace CodeStack
{
    partial class SelectionForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selBox = new SelectionBox();
            this.pnlLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.pnlLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // selBox
            // 
            this.selBox.BackColor = System.Drawing.Color.LightBlue;
            this.selBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selBox.Filter = new SolidWorks.Interop.swconst.swSelectType_e[] {
        SolidWorks.Interop.swconst.swSelectType_e.swSelFACES,
        SolidWorks.Interop.swconst.swSelectType_e.swSelDATUMPLANES};
            this.selBox.ForeColor = System.Drawing.Color.White;
            this.selBox.FormattingEnabled = true;
            this.selBox.ItemHeight = 16;
            this.selBox.Location = new System.Drawing.Point(3, 3);
            this.selBox.Mark = -1;
            this.selBox.Name = "selBox";
            this.selBox.Selection = null;
            this.selBox.Size = new System.Drawing.Size(276, 50);
            this.selBox.TabIndex = 0;
            // 
            // pnlLayout
            // 
            this.pnlLayout.ColumnCount = 1;
            this.pnlLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlLayout.Controls.Add(this.selBox, 0, 0);
            this.pnlLayout.Controls.Add(this.lblMsg, 0, 1);
            this.pnlLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLayout.Location = new System.Drawing.Point(0, 0);
            this.pnlLayout.Name = "pnlLayout";
            this.pnlLayout.RowCount = 2;
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlLayout.Size = new System.Drawing.Size(282, 255);
            this.pnlLayout.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMsg.Location = new System.Drawing.Point(3, 56);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(276, 199);
            this.lblMsg.TabIndex = 1;
            this.lblMsg.Text = "Select objects in the SOLIDWORKS graphics view";
            this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 255);
            this.Controls.Add(this.pnlLayout);
            this.Name = "SelectionForm";
            this.Text = "SelectionForm";
            this.pnlLayout.ResumeLayout(false);
            this.pnlLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SelectionBox selBox;
        private System.Windows.Forms.TableLayoutPanel pnlLayout;
        private System.Windows.Forms.Label lblMsg;
    }
}