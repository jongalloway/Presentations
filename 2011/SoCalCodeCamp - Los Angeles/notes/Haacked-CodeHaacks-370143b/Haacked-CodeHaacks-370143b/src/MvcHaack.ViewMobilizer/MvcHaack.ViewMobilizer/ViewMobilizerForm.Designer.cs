namespace MvcHaack.ViewMobilizer {
    partial class ViewMobilizerForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.viewsCheckBoxList = new System.Windows.Forms.CheckedListBox();
            this.mobilizeButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // viewsCheckBoxList
            // 
            this.viewsCheckBoxList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewsCheckBoxList.FormattingEnabled = true;
            this.viewsCheckBoxList.Location = new System.Drawing.Point(5, 12);
            this.viewsCheckBoxList.Name = "viewsCheckBoxList";
            this.viewsCheckBoxList.Size = new System.Drawing.Size(322, 199);
            this.viewsCheckBoxList.TabIndex = 0;
            // 
            // mobilizeButton
            // 
            this.mobilizeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.mobilizeButton.Location = new System.Drawing.Point(252, 222);
            this.mobilizeButton.Name = "mobilizeButton";
            this.mobilizeButton.Size = new System.Drawing.Size(75, 23);
            this.mobilizeButton.TabIndex = 1;
            this.mobilizeButton.Text = "Mobilize!";
            this.mobilizeButton.UseVisualStyleBackColor = true;
            this.mobilizeButton.Click += new System.EventHandler(this.mobilizeButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(171, 222);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ViewMobilizerForm
            // 
            this.AcceptButton = this.mobilizeButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(339, 257);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.mobilizeButton);
            this.Controls.Add(this.viewsCheckBoxList);
            this.Name = "ViewMobilizerForm";
            this.Text = "ViewMobilizerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox viewsCheckBoxList;
        private System.Windows.Forms.Button mobilizeButton;
        private System.Windows.Forms.Button cancelButton;
    }
}