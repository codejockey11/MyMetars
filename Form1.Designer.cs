namespace MyMetars
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxStation = new System.Windows.Forms.TextBox();
            this.buttonGetMetar = new System.Windows.Forms.Button();
            this.textBoxReport = new System.Windows.Forms.RichTextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelUTC = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxStation
            // 
            this.textBoxStation.Location = new System.Drawing.Point(131, 13);
            this.textBoxStation.Name = "textBoxStation";
            this.textBoxStation.Size = new System.Drawing.Size(60, 20);
            this.textBoxStation.TabIndex = 0;
            this.textBoxStation.TextChanged += new System.EventHandler(this.textBoxStation_TextChanged);
            // 
            // buttonGetMetar
            // 
            this.buttonGetMetar.Location = new System.Drawing.Point(197, 11);
            this.buttonGetMetar.Name = "buttonGetMetar";
            this.buttonGetMetar.Size = new System.Drawing.Size(75, 23);
            this.buttonGetMetar.TabIndex = 1;
            this.buttonGetMetar.Text = "Get Metar";
            this.buttonGetMetar.UseVisualStyleBackColor = true;
            this.buttonGetMetar.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // textBoxReport
            // 
            this.textBoxReport.AcceptsTab = true;
            this.textBoxReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReport.Location = new System.Drawing.Point(13, 40);
            this.textBoxReport.Name = "textBoxReport";
            this.textBoxReport.ReadOnly = true;
            this.textBoxReport.Size = new System.Drawing.Size(759, 397);
            this.textBoxReport.TabIndex = 2;
            this.textBoxReport.TabStop = false;
            this.textBoxReport.Text = "";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 440);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(74, 17);
            this.toolStripStatusLabel.Text = "Enter Station";
            // 
            // labelUTC
            // 
            this.labelUTC.AutoSize = true;
            this.labelUTC.Location = new System.Drawing.Point(12, 16);
            this.labelUTC.Name = "labelUTC";
            this.labelUTC.Size = new System.Drawing.Size(56, 13);
            this.labelUTC.TabIndex = 2;
            this.labelUTC.Text = "Date Time";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.labelUTC);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.textBoxReport);
            this.Controls.Add(this.buttonGetMetar);
            this.Controls.Add(this.textBoxStation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.Text = "MyMetars";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxStation;
        private System.Windows.Forms.Button buttonGetMetar;
        private System.Windows.Forms.RichTextBox textBoxReport;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Label labelUTC;
    }
}
