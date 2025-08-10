using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpExt4Explorer
{
    public partial class ChangeModeDialog : Form
    {
        public static string ModeToString(uint mode)
        {
            char fileType = '-';

            if ((mode & 0xF000) == 0x4000) fileType = 'd'; // Directory
            else if ((mode & 0xF000) == 0x8000) fileType = '-'; // Regular file
            else if ((mode & 0xF000) == 0xA000) fileType = 'l'; // Symlink
            else if ((mode & 0xF000) == 0x6000) fileType = 'b'; // Block device
            else if ((mode & 0xF000) == 0x2000) fileType = 'c'; // Char device
            else if ((mode & 0xF000) == 0x1000) fileType = 'p'; // FIFO
            else if ((mode & 0xF000) == 0xC000) fileType = 's'; // Socket

            string perms = "";

            int[] shifts = { 6, 3, 0 }; // User, Group, Others
            foreach (int shift in shifts)
            {
                perms += ((mode >> shift) & 0b100) != 0 ? 'r' : '-';
                perms += ((mode >> shift) & 0b010) != 0 ? 'w' : '-';
                perms += ((mode >> shift) & 0b001) != 0 ? 'x' : '-';
            }

            // Handle special bits
            if ((mode & 0x0800) != 0) // Setuid
                perms = perms.Substring(0, 2) + (perms[2] == 'x' ? 's' : 'S') + perms.Substring(3);
            if ((mode & 0x0400) != 0) // Setgid
                perms = perms.Substring(0, 5) + (perms[5] == 'x' ? 's' : 'S') + perms.Substring(6);
            if ((mode & 0x0200) != 0) // Sticky
                perms = perms.Substring(0, 8) + (perms[8] == 'x' ? 't' : 'T');

            return fileType + perms;
        }

        public static string ToSymbolic(int mode)
        {
            char[] result = new char[9];
            int[] masks = { 0x100, 0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01 };
            char[] chars = { 'r', 'w', 'x' };

            for (int i = 0; i < 9; i++)
                result[i] = (mode & masks[i]) != 0 ? chars[i % 3] : '-';

            return new string(result);
        }

        public uint FileMode { get; private set; }

        public ChangeModeDialog(uint currentMode)
        {
            InitializeComponent();
            LoadMode(currentMode);
        }

        private void LoadMode(uint mode)
        {
            // User permissions
            checkBoxUserRead.Checked = (mode & 0x100) != 0;
            checkBoxUserWrite.Checked = (mode & 0x80) != 0;
            checkBoxUserExecute.Checked = (mode & 0x40) != 0;

            // Group permissions
            checkBoxGroupRead.Checked = (mode & 0x20) != 0;
            checkBoxGroupWrite.Checked = (mode & 0x10) != 0;
            checkBoxGroupExecute.Checked = (mode & 0x08) != 0;

            // Other permissions
            checkBoxOtherRead.Checked = (mode & 0x04) != 0;
            checkBoxOtherWrite.Checked = (mode & 0x02) != 0;
            checkBoxOtherExecute.Checked = (mode & 0x01) != 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FileMode = 0;

            if (checkBoxUserRead.Checked) FileMode |= 0x100;
            if (checkBoxUserWrite.Checked) FileMode |= 0x80;
            if (checkBoxUserExecute.Checked) FileMode |= 0x40;

            if (checkBoxGroupRead.Checked) FileMode |= 0x20;
            if (checkBoxGroupWrite.Checked) FileMode |= 0x10;
            if (checkBoxGroupExecute.Checked) FileMode |= 0x08;

            if (checkBoxOtherRead.Checked) FileMode |= 0x04;
            if (checkBoxOtherWrite.Checked) FileMode |= 0x02;
            if (checkBoxOtherExecute.Checked) FileMode |= 0x01;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
