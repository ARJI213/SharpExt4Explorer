using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Ext4Explorer
{
    public partial class DiskSelectorForm : Form
    {
        public DiskSelectorForm(IReadOnlyList<PhysicalDiskInfo> disk)
        {
            InitializeComponent();
            comboDisks.DataSource = disk;
        }

        public PhysicalDiskInfo SelectedDisk { get; private set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedDisk = (PhysicalDiskInfo)comboDisks.SelectedItem; Close();
        }
    }
}
