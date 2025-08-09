namespace Ext4Explorer
{
    partial class DiskSelectorForm
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
            comboDisks = new ComboBox();
            btnOk = new Button();
            SuspendLayout();
            // 
            // comboDisks
            // 
            comboDisks.DropDownStyle = ComboBoxStyle.DropDownList;
            comboDisks.FormattingEnabled = true;
            comboDisks.Location = new Point(12, 33);
            comboDisks.Name = "comboDisks";
            comboDisks.Size = new Size(240, 23);
            comboDisks.TabIndex = 0;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(85, 92);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 1;
            btnOk.Text = "&OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // DiskSelectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(264, 143);
            Controls.Add(btnOk);
            Controls.Add(comboDisks);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DiskSelectorForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Disk";
            ResumeLayout(false);
        }

        #endregion

        private ComboBox comboDisks;
        private Button btnOk;
    }
}