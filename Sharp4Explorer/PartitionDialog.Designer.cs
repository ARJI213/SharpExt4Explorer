namespace Ext4Explorer
{
    partial class PartitionDialog
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
            ok = new Button();
            cancel = new Button();
            partitionList = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            SuspendLayout();
            // 
            // ok
            // 
            ok.DialogResult = DialogResult.OK;
            ok.FlatStyle = FlatStyle.System;
            ok.Location = new Point(61, 147);
            ok.Name = "ok";
            ok.Size = new Size(75, 23);
            ok.TabIndex = 0;
            ok.Text = "&OK";
            ok.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            cancel.DialogResult = DialogResult.Cancel;
            cancel.Location = new Point(178, 147);
            cancel.Name = "cancel";
            cancel.Size = new Size(75, 23);
            cancel.TabIndex = 1;
            cancel.Text = "&Cancel";
            cancel.UseVisualStyleBackColor = true;
            // 
            // partitionList
            // 
            partitionList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            partitionList.FullRowSelect = true;
            partitionList.GridLines = true;
            partitionList.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            partitionList.Location = new Point(12, 12);
            partitionList.Name = "partitionList";
            partitionList.Size = new Size(288, 117);
            partitionList.TabIndex = 2;
            partitionList.UseCompatibleStateImageBehavior = false;
            partitionList.View = View.Details;
            partitionList.DoubleClick += partitionList_DoubleClick;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "ID";
            columnHeader1.Width = 40;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Type";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Size";
            columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Name / Active";
            // 
            // PartitionDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(312, 182);
            Controls.Add(partitionList);
            Controls.Add(cancel);
            Controls.Add(ok);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PartitionDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "PartitionDialog";
            Load += PartitionDialog_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button ok;
        private Button cancel;
        private ListView partitionList;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
    }
}