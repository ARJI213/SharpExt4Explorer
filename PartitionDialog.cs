using SharpExt4;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ext4Explorer
{
    public partial class PartitionDialog : Form
    {
        public bool IsGPT { get; private set; }
        public Partition SelectedPartition { get; private set; }
        public IReadOnlyList<Partition> Partitions { get; private set; }
        private readonly string imageFile;
        private readonly Guid Linux = Guid.Parse("0FC63DAF-8483-4772-8E79-3D69D8477DE4");
        private readonly Guid Windows = Guid.Parse("EBD0A0A2-B9E5-4433-87C0-68B6B72699C7");
        const ulong SECTOR_SIZE = 512;
        private bool isImage;
        private int driveNumber;
        public PartitionDialog(string imageFile) : this()
        {
            this.imageFile = imageFile;
            this.isImage = true;
        }

        public PartitionDialog(int driveNumber) : this()
        {
            this.driveNumber = driveNumber;
            this.isImage = false;
        }

        private PartitionDialog()
        {
            InitializeComponent();
        }

        private void PartitionDialog_Load(object sender, EventArgs e)
        {
            var mbr = isImage ? DiskPartitionInfo.DiskPartitionInfo.ReadMbr().FromPath(imageFile) :
                DiskPartitionInfo.DiskPartitionInfo.ReadMbr().FromPhysicalDriveNumber(driveNumber);
            partitionList.Items.Clear();
            var i = 0;
            IsGPT = false;
            Partitions = new List<Partition>();

            foreach (var partition in mbr.Partitions)
            {
                var item = new ListViewItem(i.ToString());
                if (partition.PartitionType == 0xEE)
                {
                    IsGPT = true;
                    break;
                }
                var type = Enum.GetName(typeof(MBRPartitionType), partition.PartitionType);
                item.SubItems.Add(type);
                item.SubItems.Add(partition.Length.ToString());
                item.SubItems.Add(partition.IsActive.ToString());
                partitionList.Items.Add(item);
            }

            if (IsGPT)
            {
                var gpt = isImage
                    ? DiskPartitionInfo.DiskPartitionInfo.ReadGpt().Primary().FromPath(imageFile)
                    : DiskPartitionInfo.DiskPartitionInfo.ReadGpt().Primary().FromPhysicalDriveNumber(driveNumber);
                foreach (var partition in gpt.Partitions)
                {
                    var item = new ListViewItem(i.ToString());
                    i++;
                    if (partition.Type == Guid.Empty)
                        continue;

                    if (partition.Type == Linux)
                        item.SubItems.Add("Linux");
                    else if (partition.Type == Windows)
                        item.SubItems.Add("Windows");
                    else
                        item.SubItems.Add("Unknown");
                    ulong sizeBytes = (partition.LastLba - partition.FirstLba) * SECTOR_SIZE;
                    item.SubItems.Add($"{sizeBytes / (1024 * 1024)} MB");

                    item.SubItems.Add(partition.Name);
                    partitionList.Items.Add(item);
                }
                Partitions = gpt.Partitions.Select(x => new Partition() { Offset = x.FirstLba * SECTOR_SIZE, Size = (x.LastLba - x.FirstLba) * SECTOR_SIZE }).ToList();
            }
            else
                Partitions = mbr.Partitions.Select(x => new Partition() { Offset = x.FirstPartitionSector * SECTOR_SIZE, Size = x.Length * SECTOR_SIZE }).ToList();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (partitionList.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("Please select a partition before proceeding.", "Partition Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                int index = partitionList.SelectedIndices[0];
                SelectedPartition = Partitions[index];
            }

            base.OnClosing(e);
        }

        private void partitionList_DoubleClick(object sender, EventArgs e)
        {
            if (partitionList.SelectedIndices.Count > 0)
            {
                int index = partitionList.SelectedIndices[0];
                SelectedPartition = Partitions[index];

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
