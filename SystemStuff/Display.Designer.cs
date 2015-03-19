namespace SystemStuff
{
    partial class Display
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
            this._trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.LogList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // _trayIcon
            // 
            this._trayIcon.Text = "Sound Player";
            this._trayIcon.Visible = true;
            // 
            // LogList
            // 
            this.LogList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LogList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogList.FormattingEnabled = true;
            this.LogList.Location = new System.Drawing.Point(0, 0);
            this.LogList.Name = "LogList";
            this.LogList.Size = new System.Drawing.Size(284, 261);
            this.LogList.TabIndex = 0;
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.LogList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.Name = "Display";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon _trayIcon;
        private System.Windows.Forms.ListBox LogList;
    }
}