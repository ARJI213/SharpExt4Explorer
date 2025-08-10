namespace SharpExt4Explorer
{
    partial class ChangeModeDialog
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
        /// 
        private System.Windows.Forms.CheckBox checkBoxUserRead;
        private System.Windows.Forms.CheckBox checkBoxUserWrite;
        private System.Windows.Forms.CheckBox checkBoxUserExecute;
        private System.Windows.Forms.CheckBox checkBoxGroupRead;
        private System.Windows.Forms.CheckBox checkBoxGroupWrite;
        private System.Windows.Forms.CheckBox checkBoxGroupExecute;
        private System.Windows.Forms.CheckBox checkBoxOtherRead;
        private System.Windows.Forms.CheckBox checkBoxOtherWrite;
        private System.Windows.Forms.CheckBox checkBoxOtherExecute;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBoxUser;
        private System.Windows.Forms.GroupBox groupBoxGroup;
        private System.Windows.Forms.GroupBox groupBoxOther;

        private void InitializeComponent()
        {
            this.groupBoxUser = new System.Windows.Forms.GroupBox();
            this.checkBoxUserRead = new System.Windows.Forms.CheckBox();
            this.checkBoxUserWrite = new System.Windows.Forms.CheckBox();
            this.checkBoxUserExecute = new System.Windows.Forms.CheckBox();

            this.groupBoxGroup = new System.Windows.Forms.GroupBox();
            this.checkBoxGroupRead = new System.Windows.Forms.CheckBox();
            this.checkBoxGroupWrite = new System.Windows.Forms.CheckBox();
            this.checkBoxGroupExecute = new System.Windows.Forms.CheckBox();

            this.groupBoxOther = new System.Windows.Forms.GroupBox();
            this.checkBoxOtherRead = new System.Windows.Forms.CheckBox();
            this.checkBoxOtherWrite = new System.Windows.Forms.CheckBox();
            this.checkBoxOtherExecute = new System.Windows.Forms.CheckBox();

            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            // 
            // groupBoxUser
            // 
            this.groupBoxUser.Controls.Add(this.checkBoxUserRead);
            this.groupBoxUser.Controls.Add(this.checkBoxUserWrite);
            this.groupBoxUser.Controls.Add(this.checkBoxUserExecute);
            this.groupBoxUser.Location = new System.Drawing.Point(12, 12);
            this.groupBoxUser.Name = "groupBoxUser";
            this.groupBoxUser.Size = new System.Drawing.Size(180, 50);
            this.groupBoxUser.TabIndex = 0;
            this.groupBoxUser.TabStop = false;
            this.groupBoxUser.Text = "User Permissions";

            // 
            // checkBoxUserRead
            // 
            this.checkBoxUserRead.AutoSize = true;
            this.checkBoxUserRead.Location = new System.Drawing.Point(15, 20);
            this.checkBoxUserRead.Name = "checkBoxUserRead";
            this.checkBoxUserRead.Size = new System.Drawing.Size(45, 19);
            this.checkBoxUserRead.TabIndex = 0;
            this.checkBoxUserRead.Text = "Read";
            this.checkBoxUserRead.UseVisualStyleBackColor = true;

            // 
            // checkBoxUserWrite
            // 
            this.checkBoxUserWrite.AutoSize = true;
            this.checkBoxUserWrite.Location = new System.Drawing.Point(70, 20);
            this.checkBoxUserWrite.Name = "checkBoxUserWrite";
            this.checkBoxUserWrite.Size = new System.Drawing.Size(51, 19);
            this.checkBoxUserWrite.TabIndex = 1;
            this.checkBoxUserWrite.Text = "Write";
            this.checkBoxUserWrite.UseVisualStyleBackColor = true;

            // 
            // checkBoxUserExecute
            // 
            this.checkBoxUserExecute.AutoSize = true;
            this.checkBoxUserExecute.Location = new System.Drawing.Point(130, 20);
            this.checkBoxUserExecute.Name = "checkBoxUserExecute";
            this.checkBoxUserExecute.Size = new System.Drawing.Size(63, 19);
            this.checkBoxUserExecute.TabIndex = 2;
            this.checkBoxUserExecute.Text = "Execute";
            this.checkBoxUserExecute.UseVisualStyleBackColor = true;

            // 
            // groupBoxGroup
            // 
            this.groupBoxGroup.Controls.Add(this.checkBoxGroupRead);
            this.groupBoxGroup.Controls.Add(this.checkBoxGroupWrite);
            this.groupBoxGroup.Controls.Add(this.checkBoxGroupExecute);
            this.groupBoxGroup.Location = new System.Drawing.Point(12, 70);
            this.groupBoxGroup.Name = "groupBoxGroup";
            this.groupBoxGroup.Size = new System.Drawing.Size(180, 50);
            this.groupBoxGroup.TabIndex = 1;
            this.groupBoxGroup.TabStop = false;
            this.groupBoxGroup.Text = "Group Permissions";

            // 
            // checkBoxGroupRead
            // 
            this.checkBoxGroupRead.AutoSize = true;
            this.checkBoxGroupRead.Location = new System.Drawing.Point(15, 20);
            this.checkBoxGroupRead.Name = "checkBoxGroupRead";
            this.checkBoxGroupRead.Size = new System.Drawing.Size(45, 19);
            this.checkBoxGroupRead.TabIndex = 0;
            this.checkBoxGroupRead.Text = "Read";
            this.checkBoxGroupRead.UseVisualStyleBackColor = true;

            // 
            // checkBoxGroupWrite
            // 
            this.checkBoxGroupWrite.AutoSize = true;
            this.checkBoxGroupWrite.Location = new System.Drawing.Point(70, 20);
            this.checkBoxGroupWrite.Name = "checkBoxGroupWrite";
            this.checkBoxGroupWrite.Size = new System.Drawing.Size(51, 19);
            this.checkBoxGroupWrite.TabIndex = 1;
            this.checkBoxGroupWrite.Text = "Write";
            this.checkBoxGroupWrite.UseVisualStyleBackColor = true;

            // 
            // checkBoxGroupExecute
            // 
            this.checkBoxGroupExecute.AutoSize = true;
            this.checkBoxGroupExecute.Location = new System.Drawing.Point(130, 20);
            this.checkBoxGroupExecute.Name = "checkBoxGroupExecute";
            this.checkBoxGroupExecute.Size = new System.Drawing.Size(63, 19);
            this.checkBoxGroupExecute.TabIndex = 2;
            this.checkBoxGroupExecute.Text = "Execute";
            this.checkBoxGroupExecute.UseVisualStyleBackColor = true;

            // 
            // groupBoxOther
            // 
            this.groupBoxOther.Controls.Add(this.checkBoxOtherRead);
            this.groupBoxOther.Controls.Add(this.checkBoxOtherWrite);
            this.groupBoxOther.Controls.Add(this.checkBoxOtherExecute);
            this.groupBoxOther.Location = new System.Drawing.Point(12, 128);
            this.groupBoxOther.Name = "groupBoxOther";
            this.groupBoxOther.Size = new System.Drawing.Size(180, 50);
            this.groupBoxOther.TabIndex = 2;
            this.groupBoxOther.TabStop = false;
            this.groupBoxOther.Text = "Other Permissions";

            // 
            // checkBoxOtherRead
            // 
            this.checkBoxOtherRead.AutoSize = true;
            this.checkBoxOtherRead.Location = new System.Drawing.Point(15, 20);
            this.checkBoxOtherRead.Name = "checkBoxOtherRead";
            this.checkBoxOtherRead.Size = new System.Drawing.Size(45, 19);
            this.checkBoxOtherRead.TabIndex = 0;
            this.checkBoxOtherRead.Text = "Read";
            this.checkBoxOtherRead.UseVisualStyleBackColor = true;

            // 
            // checkBoxOtherWrite
            // 
            this.checkBoxOtherWrite.AutoSize = true;
            this.checkBoxOtherWrite.Location = new System.Drawing.Point(70, 20);
            this.checkBoxOtherWrite.Name = "checkBoxOtherWrite";
            this.checkBoxOtherWrite.Size = new System.Drawing.Size(51, 19);
            this.checkBoxOtherWrite.TabIndex = 1;
            this.checkBoxOtherWrite.Text = "Write";
            this.checkBoxOtherWrite.UseVisualStyleBackColor = true;

            // 
            // checkBoxOtherExecute
            // 
            this.checkBoxOtherExecute.AutoSize = true;
            this.checkBoxOtherExecute.Location = new System.Drawing.Point(130, 20);
            this.checkBoxOtherExecute.Name = "checkBoxOtherExecute";
            this.checkBoxOtherExecute.Size = new System.Drawing.Size(63, 19);
            this.checkBoxOtherExecute.TabIndex = 2;
            this.checkBoxOtherExecute.Text = "Execute";
            this.checkBoxOtherExecute.UseVisualStyleBackColor = true;

            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(40, 190);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(60, 27);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(110, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 27);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;

            // 
            // ChangeModeDialog
            // 
            this.ClientSize = new System.Drawing.Size(210, 230);
            this.Controls.Add(this.groupBoxUser);
            this.Controls.Add(this.groupBoxGroup);
            this.Controls.Add(this.groupBoxOther);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeModeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Change File Permissions";

            this.groupBoxUser.ResumeLayout(false);
            this.groupBoxUser.PerformLayout();
            this.groupBoxGroup.ResumeLayout(false);
            this.groupBoxGroup.PerformLayout();
            this.groupBoxOther.ResumeLayout(false);
            this.groupBoxOther.PerformLayout();
        }


        #endregion
    }
}