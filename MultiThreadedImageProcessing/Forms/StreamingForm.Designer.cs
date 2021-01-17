
namespace MultiThreadedImageProcessing.Forms
{
    partial class StreamingForm
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
            this.components = new System.ComponentModel.Container();
            this.StreamBox = new System.Windows.Forms.PictureBox();
            this.Commands = new IViewCustomControls.IViewContextMenuStrip();
            this.startStreamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowChangesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FPSTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.StreamBox)).BeginInit();
            this.Commands.SuspendLayout();
            this.SuspendLayout();
            // 
            // StreamBox
            // 
            this.StreamBox.BackColor = System.Drawing.Color.Silver;
            this.StreamBox.ContextMenuStrip = this.Commands;
            this.StreamBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StreamBox.Location = new System.Drawing.Point(0, 0);
            this.StreamBox.Name = "StreamBox";
            this.StreamBox.Size = new System.Drawing.Size(641, 456);
            this.StreamBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StreamBox.TabIndex = 1;
            this.StreamBox.TabStop = false;
            this.StreamBox.Paint += new System.Windows.Forms.PaintEventHandler(this.StreamBox_Paint);
            this.StreamBox.Resize += new System.EventHandler(this.StreamBox_Resize);
            // 
            // Commands
            // 
            this.Commands.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Commands.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Commands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startStreamToolStripMenuItem,
            this.ShowChangesToolStripMenuItem});
            this.Commands.Name = "Commands";
            this.Commands.Size = new System.Drawing.Size(153, 48);
            // 
            // startStreamToolStripMenuItem
            // 
            this.startStreamToolStripMenuItem.Name = "startStreamToolStripMenuItem";
            this.startStreamToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startStreamToolStripMenuItem.Text = "Start Stream";
            this.startStreamToolStripMenuItem.Click += new System.EventHandler(this.StartStreamToolStripMenuItem_Click);
            // 
            // ShowChangesToolStripMenuItem
            // 
            this.ShowChangesToolStripMenuItem.Name = "ShowChangesToolStripMenuItem";
            this.ShowChangesToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.ShowChangesToolStripMenuItem.Text = "Show Changes";
            this.ShowChangesToolStripMenuItem.Click += new System.EventHandler(this.ShowChangesToolStripMenuItem_Click);
            // 
            // FPSTimer
            // 
            this.FPSTimer.Enabled = true;
            this.FPSTimer.Interval = 1000;
            this.FPSTimer.Tick += new System.EventHandler(this.FPSTimer_Tick);
            // 
            // StreamingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 456);
            this.Controls.Add(this.StreamBox);
            this.DoubleBuffered = true;
            this.Name = "StreamingForm";
            this.Text = "StreamingForm";
            this.Load += new System.EventHandler(this.StreamingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.StreamBox)).EndInit();
            this.Commands.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox StreamBox;
        private System.Windows.Forms.Timer FPSTimer;
        private IViewCustomControls.IViewContextMenuStrip Commands;
        private System.Windows.Forms.ToolStripMenuItem startStreamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowChangesToolStripMenuItem;
    }
}