# Level5 Resource Editor

### Overview

Level5 Resource Editor is a tool tool for viewing and editing Level-5 RES.bin resource files from Nintendo 3DS games. 
You can find the RES.bin files in the .xc archives. These archives typically contain 3D models, animations, ...

<img width="1920" height="1032" alt="image" src="https://github.com/user-attachments/assets/2fa76f35-b898-4de9-9b4a-bc36e581c10e" />

### What is a RES.bin file?

A RES.bin file acts as a resource name table that contains references to all resources used by other files within the same archive. For example:
- Image names
- Mesh names
- Material names
- Animation names
- And more...

Other files in the archive can access these resource names using the CRC32 string table stored in the RES.bin file.

## File Format Support

The tool supports two versions of the RES format:

### XRES (Legacy Format)
- Used in Level-5 games **before 2011**
- Older resource structure
- Fully supported for Scene3D resources

### RES (Modern Format)
- Used in Level-5 games **after 2011**
- Updated resource structure
- Fully supported for Scene3D resources

## Scene Types

RES and XRES files are divided into two main categories:

### Scene3D
Scene3D resources are used for 3D environment-related content, including:
- 3D models archives
- Animation archives
- Character models
- Environmental assets

**Status:** Fully supported

### Scene2D
Scene2D resources are used in menu-related archives and UI elements.

**Status:** Not currently supported (planned for future releases)

## Dll used

This project is built using:

- **ImaginationGUI** - Custom WPF-based GUI framework
- **[StudioElevenLib](https://github.com/Tiniifan/StudioElevenLib)** - C# library for handling Level-5 Nintendo 3DS file formats
- **[Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)** - C# library for handling .json (JSONs are used as application resources to manage translation)

## Installation

1. Download the latest release from the [Releases](https://github.com/Tiniifan/Level5ResourceEditor/releases) page
2. Extract the archive
3. Run `Level5ResourceEditor.exe`

## Usage

### Opening a File
1. Click the **Open** button in the title bar
2. Navigate to your RES.bin file (typically found in extracted .xc archives)
3. Select the file and click Open

### Viewing Resources
- **Material Items** - Located in the left panel under "Material Items"
- **Node Items** - Located in the left panel under "Node Items"
- Click on any type to view its associated elements in the center panel
- Select an element to view its properties in the right panel

### Editing Resources
- **Add Element** - Select a resource type, go to the last row of the main datagrid and type the name then press enter to create a new element
- **Delete Element** - Select an element in the list, then use the Delete key from the keyboard
- **Modify Properties** - Edit values directly in the properties panel (right panel)

### Saving Changes
1. Click the **Save** button in the title bar
2. Choose the output format (RES or XRES)
3. Select the scene type (Scene3D or Scene2D)
4. Specify the magic identifier if needed
5. Choose the output path and save

### Language Selection
Click the language dropdown in the title bar to change the interface language. Your selection will be saved for future sessions.

## Disclaimer

**Educational Purpose**: This project is developed for educational and research purposes only.

**Work in Progress**: This tool may contain bugs or incomplete features. The documentation for RES/XRES file formats is still being researched and written. Some resource types may not be fully supported or may behave unexpectedly. Please report any issues you encounter.
