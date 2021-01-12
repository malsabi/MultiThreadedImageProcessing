
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
            this.StartStreamBtn = new System.Windows.Forms.Button();
            this.StreamBox = new System.Windows.Forms.PictureBox();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.FPSLabel = new System.Windows.Forms.Label();
            this.FPSTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.StreamBox)).BeginInit();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartStreamBtn
            // 
            this.StartStreamBtn.BackColor = System.Drawing.Color.DimGray;
            this.StartStreamBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.StartStreamBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartStreamBtn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartStreamBtn.ForeColor = System.Drawing.Color.White;
            this.StartStreamBtn.Location = new System.Drawing.Point(0, 0);
            this.StartStreamBtn.Name = "StartStreamBtn";
            this.StartStreamBtn.Size = new System.Drawing.Size(123, 57);
            this.StartStreamBtn.TabIndex = 0;
            this.StartStreamBtn.Text = "Start";
            this.StartStreamBtn.UseVisualStyleBackColor = false;
            this.StartStreamBtn.Click += new System.EventHandler(this.StartStreamBtn_Click);
            // 
            // StreamBox
            // 
            this.StreamBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.StreamBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StreamBox.Location = new System.Drawing.Point(0, 0);
            this.StreamBox.Name = "StreamBox";
            this.StreamBox.Size = new System.Drawing.Size(766, 485);
            this.StreamBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StreamBox.TabIndex = 1;
            this.StreamBox.TabStop = false;
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ControlPanel.Controls.Add(this.FPSLabel);
            this.ControlPanel.Controls.Add(this.StartStreamBtn);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ControlPanel.Location = new System.Drawing.Point(0, 485);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(766, 61);
            this.ControlPanel.TabIndex = 2;
            // 
            // FPSLabel
            // 
            this.FPSLabel.AutoSize = true;
            this.FPSLabel.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FPSLabel.Location = new System.Drawing.Point(144, 14);
            this.FPSLabel.Name = "FPSLabel";
            this.FPSLabel.Size = new System.Drawing.Size(73, 23);
            this.FPSLabel.TabIndex = 1;
            this.FPSLabel.Text = "FPS: 0";
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
            this.ClientSize = new System.Drawing.Size(766, 546);
            this.Controls.Add(this.StreamBox);
            this.Controls.Add(this.ControlPanel);
            this.DoubleBuffered = true;
            this.Name = "StreamingForm";
            this.Text = "StreamingForm";
            ((System.ComponentModel.ISupportInitialize)(this.StreamBox)).EndInit();
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartStreamBtn;
        private System.Windows.Forms.PictureBox StreamBox;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Label FPSLabel;
        private System.Windows.Forms.Timer FPSTimer;
    }
}