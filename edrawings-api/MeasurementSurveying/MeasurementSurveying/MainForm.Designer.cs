using eDrawings.Interop.EModelViewControl;

namespace CodeStack.Examples.eDrawingsApi
{
    partial class MainForm
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.ctrlEDrw = new CodeStack.Examples.eDrawingsApi.EDrawingsUserControl();
            this.btnCaptureMeasurement = new System.Windows.Forms.Button();
            this.txtMeasurements = new System.Windows.Forms.TextBox();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.txtFilePath, 0, 1);
            this.tlpMain.Controls.Add(this.btnOpen, 1, 1);
            this.tlpMain.Controls.Add(this.ctrlEDrw, 0, 0);
            this.tlpMain.Controls.Add(this.btnCaptureMeasurement, 2, 1);
            this.tlpMain.Controls.Add(this.txtMeasurements, 2, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(424, 327);
            this.tlpMain.TabIndex = 0;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(3, 302);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(197, 20);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(206, 301);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.OnOpen);
            // 
            // ctrlEDrw
            // 
            this.tlpMain.SetColumnSpan(this.ctrlEDrw, 2);
            this.ctrlEDrw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlEDrw.Location = new System.Drawing.Point(3, 3);
            this.ctrlEDrw.Name = "ctrlEDrw";
            this.ctrlEDrw.Size = new System.Drawing.Size(278, 292);
            this.ctrlEDrw.TabIndex = 2;
            this.ctrlEDrw.EDrawingsControlLoaded += new System.Action<eDrawings.Interop.EModelViewControl.EModelViewControl>(this.OnControlLoaded);
            // 
            // btnCaptureMeasurement
            // 
            this.btnCaptureMeasurement.Location = new System.Drawing.Point(287, 301);
            this.btnCaptureMeasurement.Name = "btnCaptureMeasurement";
            this.btnCaptureMeasurement.Size = new System.Drawing.Size(134, 23);
            this.btnCaptureMeasurement.TabIndex = 3;
            this.btnCaptureMeasurement.Text = "Capture Measurement";
            this.btnCaptureMeasurement.UseVisualStyleBackColor = true;
            this.btnCaptureMeasurement.Click += new System.EventHandler(this.OnCaptureMeasurement);
            // 
            // txtMeasurements
            // 
            this.txtMeasurements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMeasurements.Location = new System.Drawing.Point(287, 3);
            this.txtMeasurements.Multiline = true;
            this.txtMeasurements.Name = "txtMeasurements";
            this.txtMeasurements.Size = new System.Drawing.Size(134, 292);
            this.txtMeasurements.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 327);
            this.Controls.Add(this.tlpMain);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnOpen;
        private EDrawingsUserControl ctrlEDrw;
        private System.Windows.Forms.Button btnCaptureMeasurement;
        private System.Windows.Forms.TextBox txtMeasurements;
    }
}

