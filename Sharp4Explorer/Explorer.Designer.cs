
using SharpExt4;
using DiskPartitionInfo;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Ext4Explorer
{
    partial class Explorer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private System.Windows.Forms.TextBox txtImagePath;
        private System.Windows.Forms.TextBox txtAddressBar;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TreeView treeFolders;
        private System.Windows.Forms.ListView listFiles;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colType;
        private ImageList imageListIcons;
        private ContextMenuStrip contextMenuFiles;
        private ToolStripMenuItem menuItemOpen;
        private ToolStripMenuItem menuItemSaveAs;
        private ToolStripMenuItem menuItemDelete;
        private ToolStripMenuItem menuItemCopy;
        private ToolStripMenuItem menuItemPaste;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            txtImagePath = new TextBox();
            txtAddressBar = new TextBox();
            btnRefresh = new Button();
            splitContainerMain = new SplitContainer();
            treeFolders = new TreeView();
            imageListIcons = new ImageList(components);
            listFiles = new ListView();
            colName = new ColumnHeader();
            colSize = new ColumnHeader();
            colType = new ColumnHeader();
            contextMenuFiles = new ContextMenuStrip(components);
            menuItemOpen = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            menuItemCopy = new ToolStripMenuItem();
            menuItemPaste = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            menuItemSaveAs = new ToolStripMenuItem();
            menuItemDelete = new ToolStripMenuItem();
            btnLoad = new Button();
            contextMenuLoad = new ContextMenuStrip(components);
            fromImageToolStripMenuItem = new ToolStripMenuItem();
            fromDiskToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            contextMenuFiles.SuspendLayout();
            contextMenuLoad.SuspendLayout();
            SuspendLayout();
            // 
            // txtImagePath
            // 
            txtImagePath.Location = new Point(12, 12);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.PlaceholderText = "Enter path to ext4 image or disk number...";
            txtImagePath.Size = new Size(600, 23);
            txtImagePath.TabIndex = 0;
            // 
            // txtAddressBar
            // 
            txtAddressBar.Location = new Point(12, 45);
            txtAddressBar.Name = "txtAddressBar";
            txtAddressBar.PlaceholderText = "Current path...";
            txtAddressBar.ReadOnly = true;
            txtAddressBar.Size = new Size(600, 23);
            txtAddressBar.TabIndex = 2;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(620, 45);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 23);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // splitContainerMain
            // 
            splitContainerMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainerMain.Location = new Point(12, 80);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(treeFolders);
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(listFiles);
            splitContainerMain.Size = new Size(760, 360);
            splitContainerMain.SplitterDistance = 250;
            splitContainerMain.TabIndex = 4;
            // 
            // treeFolders
            // 
            treeFolders.Dock = DockStyle.Fill;
            treeFolders.HideSelection = false;
            treeFolders.ImageIndex = 0;
            treeFolders.ImageList = imageListIcons;
            treeFolders.Location = new Point(0, 0);
            treeFolders.Name = "treeFolders";
            treeFolders.SelectedImageIndex = 0;
            treeFolders.Size = new Size(250, 360);
            treeFolders.TabIndex = 0;
            treeFolders.BeforeExpand += treeFolders_BeforeExpand;
            treeFolders.AfterSelect += treeFolders_AfterSelect;
            // 
            // imageListIcons
            // 
            imageListIcons.ColorDepth = ColorDepth.Depth32Bit;
            imageListIcons.ImageSize = new Size(16, 16);
            imageListIcons.TransparentColor = Color.Transparent;
            // 
            // listFiles
            // 
            listFiles.AllowDrop = true;
            listFiles.Columns.AddRange(new ColumnHeader[] { colName, colSize, colType });
            listFiles.ContextMenuStrip = contextMenuFiles;
            listFiles.Dock = DockStyle.Fill;
            listFiles.FullRowSelect = true;
            listFiles.Location = new Point(0, 0);
            listFiles.Name = "listFiles";
            listFiles.Size = new Size(506, 360);
            listFiles.TabIndex = 0;
            listFiles.UseCompatibleStateImageBehavior = false;
            listFiles.View = View.Details;
            listFiles.ItemDrag += listFiles_ItemDrag;
            listFiles.DragDrop += listFiles_DragDrop;
            listFiles.DragEnter += listFiles_DragEnter;
            listFiles.DoubleClick += listFiles_DoubleClick;
            listFiles.KeyDown += listFiles_KeyDown;
            // 
            // colName
            // 
            colName.Text = "Name";
            colName.Width = 200;
            // 
            // colSize
            // 
            colSize.Text = "Size";
            colSize.Width = 80;
            // 
            // colType
            // 
            colType.Text = "Type";
            colType.Width = 100;
            // 
            // contextMenuFiles
            // 
            contextMenuFiles.Items.AddRange(new ToolStripItem[] { menuItemOpen, toolStripSeparator1, menuItemCopy, menuItemPaste, toolStripSeparator2, menuItemSaveAs, menuItemDelete });
            contextMenuFiles.Name = "contextMenuFiles";
            contextMenuFiles.Size = new Size(124, 126);
            contextMenuFiles.Opening += contextMenuListFiles_Opening;
            // 
            // menuItemOpen
            // 
            menuItemOpen.Name = "menuItemOpen";
            menuItemOpen.Size = new Size(123, 22);
            menuItemOpen.Text = "Open";
            menuItemOpen.Click += MenuItemOpen_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(120, 6);
            // 
            // menuItemCopy
            // 
            menuItemCopy.Name = "menuItemCopy";
            menuItemCopy.Size = new Size(123, 22);
            menuItemCopy.Text = "Copy";
            menuItemCopy.Click += menuItemCopy_Click;
            // 
            // menuItemPaste
            // 
            menuItemPaste.Name = "menuItemPaste";
            menuItemPaste.Size = new Size(123, 22);
            menuItemPaste.Text = "Paste";
            menuItemPaste.Click += menuItemPaste_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(120, 6);
            // 
            // menuItemSaveAs
            // 
            menuItemSaveAs.Name = "menuItemSaveAs";
            menuItemSaveAs.Size = new Size(123, 22);
            menuItemSaveAs.Text = "Save As...";
            menuItemSaveAs.Click += MenuItemSaveAs_Click;
            // 
            // menuItemDelete
            // 
            menuItemDelete.Name = "menuItemDelete";
            menuItemDelete.Size = new Size(123, 22);
            menuItemDelete.Text = "Delete";
            menuItemDelete.Click += MenuItemDelete_Click;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(625, 12);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(75, 23);
            btnLoad.TabIndex = 5;
            btnLoad.Text = "Load ...";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += loadBtn_Click;
            // 
            // contextMenuLoad
            // 
            contextMenuLoad.Items.AddRange(new ToolStripItem[] { fromImageToolStripMenuItem, fromDiskToolStripMenuItem });
            contextMenuLoad.Name = "contextMenuLoad";
            contextMenuLoad.Size = new Size(139, 48);
            // 
            // fromImageToolStripMenuItem
            // 
            fromImageToolStripMenuItem.Name = "fromImageToolStripMenuItem";
            fromImageToolStripMenuItem.Size = new Size(138, 22);
            fromImageToolStripMenuItem.Text = "From Image";
            fromImageToolStripMenuItem.Click += btnLoadImage_Click;
            // 
            // fromDiskToolStripMenuItem
            // 
            fromDiskToolStripMenuItem.Name = "fromDiskToolStripMenuItem";
            fromDiskToolStripMenuItem.Size = new Size(138, 22);
            fromDiskToolStripMenuItem.Text = "From Disk";
            fromDiskToolStripMenuItem.Click += fromDiskToolStripMenuItem_Click;
            // 
            // Explorer
            // 
            ClientSize = new Size(784, 461);
            Controls.Add(btnLoad);
            Controls.Add(txtImagePath);
            Controls.Add(txtAddressBar);
            Controls.Add(btnRefresh);
            Controls.Add(splitContainerMain);
            KeyPreview = true;
            Name = "Explorer";
            Text = "SharpExt4 Explorer";
            KeyDown += Explorer_KeyDown;
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            contextMenuFiles.ResumeLayout(false);
            contextMenuLoad.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private Button btnLoad;
        private ContextMenuStrip contextMenuLoad;
        private ToolStripMenuItem fromImageToolStripMenuItem;
        private ToolStripMenuItem fromDiskToolStripMenuItem;
    }
}
