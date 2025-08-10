using SharpExt4;
using SharpExt4Explorer;
using System;
using System.ComponentModel;
using System.Management;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Ext4Explorer
{
    public partial class Explorer : Form
    {

        private ExtDisk disk = null;
        private ExtFileSystem fs = null;
        private Partition Partition = null;

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        public Explorer()
        {
            InitializeComponent();
            LoadFolderIcon();
            listFiles.MouseUp += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    bool hasSelection = listFiles.SelectedItems.Count > 0;
                    bool clipboardHasFiles = Clipboard.ContainsFileDropList();

                    menuItemOpen.Enabled = hasSelection;
                    menuItemSaveAs.Enabled = hasSelection;
                    menuItemDelete.Enabled = hasSelection;
                    menuItemCopy.Enabled = hasSelection;

                    menuItemPaste.Enabled = clipboardHasFiles;

                    // Allow context menu if we can do anything
                    if (!hasSelection && !clipboardHasFiles)
                        listFiles.ContextMenuStrip = null;
                    else
                        listFiles.ContextMenuStrip = contextMenuFiles;
                }
            };

        }

        private void LoadFolderIcon()
        {
            IntPtr hIcon = ExtractIcon(IntPtr.Zero, "shell32.dll", 4);
            if (hIcon != IntPtr.Zero)
            {
                using (var icon = Icon.FromHandle(hIcon))
                {
                    imageListIcons.Images.Add("folder", (Icon)icon.Clone());
                }
                DestroyIcon(hIcon); // clean up unmanaged handle
            }
            else
            {
                // fallback
                imageListIcons.Images.Add("folder", SystemIcons.WinLogo.ToBitmap());
            }
        }

        /// <summary>
        ///  Clean up any resources being used.
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

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select EXT4 Disk Image";
                openFileDialog.Filter = "Image Files (*.img;*.iso)|*.img;*.iso|All Files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = openFileDialog.FileName;

                    var partitionDialog = new PartitionDialog(txtImagePath.Text);
                    if (partitionDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            disk = ExtDisk.Open(txtImagePath.Text);
                            fs = ExtFileSystem.Open(disk, partitionDialog.SelectedPartition);
                            btnRefresh_Click(sender, e);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Failed to open file system:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            treeFolders.BeginUpdate();
            treeFolders.Nodes.Clear();

            var rootNode = new TreeNode("/")
            {
                Tag = "/",
                ImageKey = "folder",
                SelectedImageKey = "folder"
            };

            // Add dummy child so it can be expanded
            rootNode.Nodes.Add(new TreeNode("Loading..."));
            treeFolders.Nodes.Add(rootNode);

            rootNode.Expand(); // Expand only top-level
            treeFolders.SelectedNode = rootNode;
            txtAddressBar.Text = "/";

            treeFolders.EndUpdate();
        }

        private void treeFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            var node = e.Node;

            // Skip if already loaded
            if (node.Nodes.Count == 1 && node.Nodes[0].Text == "Loading...")
            {
                node.Nodes.Clear();

                try
                {
                    string path = node.Tag.ToString();

                    foreach (var dirPath in fs.GetDirectories(path, "*", SearchOption.TopDirectoryOnly))
                    {
                        string dirName = dirPath.TrimEnd('/').Split('/').Last();

                        var childNode = new TreeNode(dirName)
                        {
                            Tag = dirPath,
                            ImageKey = "folder",
                            SelectedImageKey = "folder"
                        };

                        // Add dummy if it has children
                        if (fs.GetDirectories(dirPath, "*", SearchOption.TopDirectoryOnly).Any())
                        {
                            childNode.Nodes.Add(new TreeNode("Loading..."));
                        }

                        node.Nodes.Add(childNode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to expand folder:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedPath = e.Node.Tag.ToString();
            txtAddressBar.Text = selectedPath;

            listFiles.Items.Clear();

            try
            {
                foreach (var file in fs.GetFiles(selectedPath, "*", SearchOption.TopDirectoryOnly))
                {
                    string fileName = file.Substring(selectedPath.Length).Trim('/');
                    var item = new ListViewItem(fileName);

                    try
                    {
                        item.SubItems.Add(fs.GetFileLength(file).ToString("N0"));
                    }
                    catch
                    {
                        item.SubItems.Add("");
                    }

                    item.SubItems.Add(ChangeModeDialog.ModeToString(fs.GetMode(file)));
                    item.Tag = file;
                    listFiles.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading files: " + ex.Message);
            }
        }


        private void listFiles_DoubleClick(object sender, EventArgs e)
        {
            MenuItemOpen_Click(sender, e);
        }

        private string FormatSize(ulong bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }

        private void BuildFolderTree(TreeNode parentNode, string fullPath)
        {
            try
            {
                foreach (var dirPath in fs.GetDirectories(fullPath, "*", SearchOption.TopDirectoryOnly))
                {
                    string dirName = dirPath.TrimEnd('/').Split('/').Last();

                    var dirNode = new TreeNode(dirName)
                    {
                        Tag = dirPath
                    };

                    parentNode.Nodes.Add(dirNode);

                    // Recursive call using full absolute path
                    BuildFolderTree(dirNode, dirPath);
                }
            }
            catch
            {
                // You can optionally log/ignore inaccessible directories here
            }
        }

        private void listFiles_DragEnter(object sender, DragEventArgs e)
        {
            // Accept file drops (from outside) for copy
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listFiles_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string currentPath = txtAddressBar.Text;

            foreach (var filePath in files)
            {
                string fileName = Path.GetFileName(filePath);
                string destPath = currentPath.TrimEnd('/') + "/" + fileName;

                try
                {
                    using (var sourceStream = File.OpenRead(filePath))
                    using (var destStream = fs.OpenFile(destPath, FileMode.Create, FileAccess.Write))
                    {
                        sourceStream.CopyTo(destStream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to copy '{fileName}' into the image:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Refresh the file list to show new files
            treeFolders_AfterSelect(this, new TreeViewEventArgs(treeFolders.SelectedNode));
        }

        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0) return;

            var file = (string)listFiles.SelectedItems[0].Tag;
            var tempFile = Path.Combine(Path.GetTempPath(), Path.GetFileName(file));

            try
            {
                using (var stream = fs.OpenFile(file, FileMode.Open, FileAccess.Read))
                using (var outFile = File.Create(tempFile))
                {
                    stream.CopyTo(outFile);
                }

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = tempFile,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open file:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuItemSaveAs_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0) return;

            var file = (string)listFiles.SelectedItems[0].Tag;

            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Title = "Save File As";
                saveDialog.FileName = Path.GetFileName(file);

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var stream = fs.OpenFile(file, FileMode.Open, FileAccess.Read))
                        using (var outFile = File.Create(saveDialog.FileName))
                        {
                            stream.CopyTo(outFile);
                        }

                        MessageBox.Show("File saved to:\n" + saveDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to save file:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void MenuItemDelete_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0) return;

            var file = (string)listFiles.SelectedItems[0].Tag;

            if (MessageBox.Show($"Are you sure you want to delete:\n{file}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    fs.DeleteFile(file);

                    listFiles.Items.Remove(listFiles.SelectedItems[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete file:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listFiles_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0) return;

            // Prepare temporary files for drag-drop from virtual filesystem

            var tempFiles = new List<string>();

            try
            {
                foreach (ListViewItem item in listFiles.SelectedItems)
                {
                    string virtualFilePath = (string)item.Tag;
                    string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(virtualFilePath));

                    using (var inStream = fs.OpenFile(virtualFilePath, FileMode.Open, FileAccess.Read))
                    using (var outStream = File.Create(tempFilePath))
                    {
                        inStream.CopyTo(outStream);
                    }

                    tempFiles.Add(tempFilePath);
                }

                DataObject data = new DataObject(DataFormats.FileDrop, tempFiles.ToArray());

                listFiles.DoDragDrop(data, DragDropEffects.Copy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error preparing drag operation: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void listFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedFilesToClipboard();
                e.Handled = true;
            }
        }

        private void Explorer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteFilesFromClipboard();
                e.Handled = true;
            }
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            CopySelectedFilesToClipboard();
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            PasteFilesFromClipboard();
        }

        private void contextMenuListFiles_Opening(object sender, CancelEventArgs e)
        {
            bool hasSelection = listFiles.SelectedItems.Count > 0;
            menuItemOpen.Enabled = hasSelection;
            menuItemSaveAs.Enabled = hasSelection;
            menuItemDelete.Enabled = hasSelection;
            menuItemCopy.Enabled = hasSelection;

            // Check clipboard at the moment of opening
            menuItemPaste.Enabled = Clipboard.ContainsFileDropList();
        }

        private void CopySelectedFilesToClipboard()
        {
            if (listFiles.SelectedItems.Count == 0) return;

            var tempFiles = new List<string>();

            foreach (ListViewItem item in listFiles.SelectedItems)
            {
                string virtualFilePath = (string)item.Tag;
                string tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(virtualFilePath));

                try
                {
                    using (var inStream = fs.OpenFile(virtualFilePath, FileMode.Open, FileAccess.Read))
                    using (var outStream = File.Create(tempFilePath))
                    {
                        inStream.CopyTo(outStream);
                    }
                    tempFiles.Add(tempFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Copy failed: " + ex.Message);
                    return;
                }
            }

            DataObject data = new DataObject(DataFormats.FileDrop, tempFiles.ToArray());
            Clipboard.SetDataObject(data, true);
        }

        private void PasteFilesFromClipboard()
        {
            IDataObject data = Clipboard.GetDataObject();

            if (data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])data.GetData(DataFormats.FileDrop);
                string currentPath = txtAddressBar.Text;

                foreach (var filePath in files)
                {
                    string fileName = Path.GetFileName(filePath);
                    string destPath = currentPath.TrimEnd('/') + "/" + fileName;

                    if (fs.FileExists(destPath))
                    {
                        MessageBox.Show($"File '{fileName}' already exists. Paste skipped.", "Paste Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    try
                    {
                        using (var sourceStream = File.OpenRead(filePath))
                        using (var destStream = fs.OpenFile(destPath, FileMode.Create, FileAccess.Write))
                        {
                            sourceStream.CopyTo(destStream);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to paste '{fileName}':\n{ex.Message}", "Paste Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Refresh the file list to show pasted files
                treeFolders_AfterSelect(this, new TreeViewEventArgs(treeFolders.SelectedNode));
            }
            else
            {
                MessageBox.Show("Clipboard does not contain files.", "Paste Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            contextMenuLoad.Show(btnLoad, new Point(0, btnLoad.Height));
        }

        private void fromDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new DiskSelectorForm(GetAllPhysicalDisks()))
            {
                if (form.ShowDialog(this) == DialogResult.OK && form.SelectedDisk != null)
                {
                    txtImagePath.Text = form.SelectedDisk.ToString();
                    using (var partitionDialog = new PartitionDialog(form.SelectedDisk.DiskNumber))
                    {
                        if (partitionDialog.ShowDialog(this) == DialogResult.OK)
                        {
                            try
                            {
                                disk = ExtDisk.Open(form.SelectedDisk.DiskNumber);
                                fs = ExtFileSystem.Open(disk, partitionDialog.SelectedPartition);
                                btnRefresh_Click(sender, e);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Failed to load disk: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private List<PhysicalDiskInfo> GetAllPhysicalDisks()
        {
            var disks = new List<PhysicalDiskInfo>();

            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT DeviceID, Model, Size FROM Win32_DiskDrive"))
                {
                    foreach (ManagementObject disk in searcher.Get())
                    {
                        string deviceId = disk["DeviceID"]?.ToString() ?? "";
                        string model = disk["Model"]?.ToString()?.Trim() ?? "Unknown";
                        ulong size = 0;

                        if (disk["Size"] != null && ulong.TryParse(disk["Size"].ToString(), out var parsedSize))
                        {
                            size = parsedSize;
                        }

                        // Extract disk number from \\.\PHYSICALDRIVE0 ¡ú 0
                        int diskNumber = -1;
                        var match = System.Text.RegularExpressions.Regex.Match(deviceId, @"PHYSICALDRIVE(\d+)");
                        if (match.Success)
                        {
                            diskNumber = int.Parse(match.Groups[1].Value);
                        }

                        disks.Add(new PhysicalDiskInfo
                        {
                            DiskNumber = diskNumber,
                            DeviceID = deviceId,
                            Model = model,
                            Size = size
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve physical disks:\n\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return disks;
        }

        private void updateModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0) return;

            var selectedItem = listFiles.SelectedItems[0];
            var filePath = (string)selectedItem.Tag;

            try
            {
                var currentMode = fs.GetMode(filePath);
                var dialog = new ChangeModeDialog(currentMode);

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    fs.SetMode(filePath, dialog.FileMode);
                    MessageBox.Show($"Updated mode to {ChangeModeDialog.ModeToString(dialog.FileMode)}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh mode display
                    selectedItem.SubItems[2].Text = ChangeModeDialog.ModeToString(dialog.FileMode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to change mode: " + ex.Message);
            }
        }
    }
}