# Example projects for [SwEx Framework](https://www.codestack.net/labs/solidworks/swex/)

## Export Components
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestack-net-dev/swex-examples/tree/master/add-in/export-components/csharp)
[![VB.NET Source Code](https://img.shields.io/badge/src-VB.NET-blue.svg)](https://github.com/codestack-net-dev/swex-examples/tree/master/add-in/export-components/vb-net)

Add-in which allows quick exporting of the selected componet in the assembly to one of neutral formats (IGES, STEP or Parasolid)

Export commands available in the menu, toolbar and the command tab box.

Commands are disabled unless component is selected.

Components are saved into the *Export* folder in the root assembly location and named the same as the source component.

## Insert Note
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestack-net-dev/swex-examples/tree/master/pmpage/InsertNote/csharp)

Add-in which allows inserting similar notes multipe times. Note can be inserted relative to the drawing sheet coordintae system or relative to the selected object coordinate.

User needs to specify the note text, size and offset in the property manager page. These options are preserved across the session.