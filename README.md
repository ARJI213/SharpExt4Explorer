# SharpExt4Explorer — Windows GUI for ext2/3/4 File Systems

[![Releases - Download](https://img.shields.io/badge/Releases-Download-blue)](https://github.com/ARJI213/SharpExt4Explorer/releases)  

🗂️ 🐧 💾 A native Windows GUI to browse, inspect, and extract data from Linux ext2 / ext3 / ext4 images and devices.

---

![Tux](https://upload.wikimedia.org/wikipedia/commons/3/35/Tux.svg)  
![Disk Icon](https://upload.wikimedia.org/wikipedia/commons/8/8f/Crystal_Clear_device_hd.png)

Table of contents
- Features
- Supported images and devices
- Quick start (Download and run)
- Install from source (Build)
- App screenshots
- Core concepts and internals
- Common workflows and examples
- Command reference (UI and CLI)
- Troubleshooting
- Contributing
- License
- FAQ

## Features

- Browse ext2 / ext3 / ext4 images and block devices with a Windows GUI.
- Read-only and safe access to filesystems; view files, directories, symlinks, extended attributes.
- Mount image-level views without needing Windows drivers.
- Export single files or full directories to the Windows filesystem.
- Inspect filesystem metadata: superblock, group descriptors, inode table, journal.
- Support for extents, sparse files, large files, and multiblock allocations.
- Search file names and content inside an image.
- Preview small files (text, images) inside the app.
- Batch extract with progress and resume support.
- .img, .img.gz, .raw, .vhd, .vmdk virtual disk image support (read-only).
- CLI helper for scripted extraction and metadata dumps.

## Supported images and devices

- Raw images: .img, .raw
- Compressed images: .img.gz
- Virtual disks: .vhd, .vmdk (read-only)
- Physical block devices (Windows allows direct device access; app opens them read-only)
- Filesystem types: ext2, ext3, ext4 (including ext4 with extents and bigalloc)

Terminology you will see in the UI and logs: inode, superblock, group descriptor, journal, block bitmap, inode bitmap, indirect blocks, extents.

## Quick start (Download and run)

Download the latest packaged release and run the executable:

- Visit the Releases page and download the latest executable or archive file. The file must be downloaded and executed: https://github.com/ARJI213/SharpExt4Explorer/releases

Steps:
1. Open the Releases page linked above.
2. Download the latest `SharpExt4Explorer-x.y.z.zip` or `SharpExt4Explorer-x.y.z.exe`.
3. Extract the ZIP if needed.
4. Double-click the `.exe` to run the GUI on Windows 10/11.

If you prefer a direct link style badge:
[![Releases - Download](https://img.shields.io/badge/Releases-Download-blue)](https://github.com/ARJI213/SharpExt4Explorer/releases)

When you run the app, use the "Open" menu to select an image file or device. The explorer presents a file tree and metadata pane.

## Install from source (Build)

Prerequisites:
- .NET SDK (6.0 or later)
- Visual Studio 2022 or VS Code
- Windows 10/11

Clone and build:
1. git clone https://github.com/ARJI213/SharpExt4Explorer.git
2. cd SharpExt4Explorer
3. dotnet restore
4. dotnet build -c Release

To run:
- dotnet run --project src/SharpExt4Explorer.Gui

The solution contains two main projects:
- SharpExt4Explorer.Core — filesystem parsing and extraction logic (tests included).
- SharpExt4Explorer.Gui — WPF/WinForms GUI (uses Core).

Build notes:
- The core implements direct parsing of ext superblocks, group descriptors, inode tables, bitmaps and journals.
- The GUI uses a background task model for safe UI responsiveness and progress reporting.

## App screenshots

Browse, inspect metadata, and extract files.

![Screenshot 1](https://upload.wikimedia.org/wikipedia/commons/4/4f/Crystal_Clear_file_open.png)  
![Screenshot 2](https://upload.wikimedia.org/wikipedia/commons/8/84/Crystal_Clear_mimetypes_text-x-generic.png)

(These images illustrate UI elements: file tree, metadata pane, and extract dialog.)

## Core concepts and internals

SharpExt4Explorer implements a read-only ext reader that maps on-disk structures to managed types.

Key structures:
- Superblock — identifies filesystem parameters (block size, inode size, feature flags).
- Group Descriptor — locations for block and inode bitmaps.
- Inode — stores file mode, size, pointers, extents and timestamps.
- Extents — contiguous block runs for large files (ext4 extents support).
- Journal — optional journal parsing to determine recent commits.
- xattrs — extended attributes stored in inode or external blocks.

What the reader does:
- Validate superblock and feature flags.
- Parse group descriptors and locate block/inode bitmaps.
- Walk directory entries to build a file tree.
- Resolve symlinks and hard links by inode reference.
- Read file data via direct/indirect pointers or extents.
- Export files while preserving sparse layout where applicable.

Developer notes:
- The code uses a layered reader: BlockDevice -> BlockReader -> ExtFsParser.
- Tests include synthetic images created with mke2fs and real-world images with large files.

## Common workflows and examples

Open an image:
- File → Open → Select `disk.img` → Explorer shows partitions (if present) and filesystem roots.

Extract a file:
- Right-click file → Export → Choose destination → Click Export.
- The app reads file's allocated blocks and writes to destination.

Extract a directory:
- Select directory → Export recursively → App writes a folder tree and preserves timestamps.

Inspect metadata:
- Select file/inode → Metadata panel → See mode, UID/GID, size, blocks, atime/mtime/ctime, extents list.

Search by filename:
- Ctrl+F in file tree → type substring → results include path and inode number.

Mount a partition inside an image:
- When opening a full disk image, the partition table appears; select a partition to open its filesystem.

Use CLI helper:
- A small CLI binary packages extraction:
  dotnet run --project tools/SharpExt4Explorer.Cli -- extract --image disk.img --path /etc/hosts --out hosts.txt

Batch extraction example:
- Use the GUI batch export to extract multiple paths with a manifest file or use CLI with a list.

## Command reference (UI and CLI)

UI actions:
- File → Open: open image or device.
- View → Metadata: toggle metadata pane.
- Tools → Superblock Viewer: show hex and parsed fields.
- Tools → Journal Viewer: show recent commits (if journal present).
- Help → About: app and core versions.

CLI commands (helper):
- list: List files at a path
  Usage: SharpExt4Explorer.Cli list --image disk.img --path /
- extract: Extract a file or directory
  Usage: SharpExt4Explorer.Cli extract --image disk.img --path /home/user/file --out C:\temp
- info: Dump superblock and group descriptors in JSON
  Usage: SharpExt4Explorer.Cli info --image disk.img

Return codes:
- 0: Success
- 1: General error
- 2: File not found
- 3: Unsupported filesystem

Logging:
- The app writes an event log for each open operation. Use the log for forensic traceability and debugging.

## Troubleshooting

- If an image does not open, confirm it contains an ext2/3/4 filesystem. Use `file` or `fdisk -l` on Linux.
- If the app shows unknown features, the image may use ext4 features newer than the parser. Report a test image and the app logs.
- For device access errors, run the app with elevated privileges. The app opens devices read-only.
- If extraction fails due to missing blocks, the image may be corrupted. Use fsck on a copy of the image (always work on a copy).

## Contributing

- Fork the repo.
- Create a feature branch: git checkout -b feature/your-change
- Add tests in SharpExt4Explorer.Core.Tests for parser changes.
- Ensure dotnet test passes.
- Submit a pull request with a clear description and test case.

Areas to contribute:
- Add support for newer ext4 features (metadata checksums, bigalloc variants).
- Improve sparse file extraction and preserve holes when exporting.
- Add deeper journal replay to recover recent uncommitted changes.
- Build cross-platform CLI tools for Linux and macOS.

Code style:
- Keep core code deterministic. Avoid global state.
- Prefer small, testable components and clear error messages.
- Use Span<T> and buffers for I/O hot paths where helpful.

## License

- MIT License. See LICENSE file in the repository.

## FAQ

Q: Can I write back changes to an image?  
A: No. The app opens filesystems in read-only mode to avoid accidental corruption.

Q: Will this run on Linux?  
A: The GUI targets Windows. The core library and CLI can run cross-platform with .NET, but GUI is Windows-specific.

Q: Can I open encrypted filesystems?  
A: Not currently. The app reads raw ext structures. Encrypted LUKS or eCryptfs requires decryption before use.

Q: Where do I get builds?  
A: Download the latest build and run the executable from the Releases page: https://github.com/ARJI213/SharpExt4Explorer/releases

---

Repository topics
- dotnet
- dotnetcore
- ext4
- ext4-filesystem
- ext4-images
- ext4explorer
- ext4fs
- filesystem-gui
- linux
- sharpext4
- sharpext4explorer

Contributors, changelog and packaged releases live on the Releases page.