using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ext4Explorer
{
    public enum MBRPartitionType : byte
    {
        Empty = 0x00,
        FAT12 = 0x01,
        FAT16_16MB = 0x04,
        Extended = 0x05,
        FAT16 = 0x06,
        NTFS_ExFAT = 0x07,
        FAT32_CHS = 0x0B,
        FAT32_LBA = 0x0C,
        Extended_LBA = 0x0F,

        LinuxSwap = 0x82,
        LinuxNative = 0x83,
        LinuxExtended = 0x85,
        LinuxLVM = 0x8E,

        FreeBSD = 0xA5,
        OpenBSD = 0xA6,
        MacOSX = 0xA8,
        MacOS_HFS = 0xAF,

        Unknown = 0xFF  // Optional: catch unrecognized types
    }

}
