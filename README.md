# SOLIDWORKS, SOLIDWORKS PDM, SOLIDWORKS Document Manager, eDrawings API examples

## Example projects for [SwEx Framework](https://www.codestack.net/labs/solidworks/swex/)

### Export Components
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/add-in/export-components/csharp)
[![VB.NET Source Code](https://img.shields.io/badge/src-VB.NET-blue.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/add-in/export-components/vb-net)

Add-in which allows quick exporting of the selected component in the assembly to one of neutral formats (IGES, STEP or Parasolid)

Export commands available in the menu, toolbar and the command tab box.

Commands are disabled unless component is selected.

Components are saved into the *Export* folder in the root assembly location and named the same as the source component.

### Insert Note
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/pmpage/InsertNote/csharp)

Add-in which allows inserting similar notes multipe times. Note can be inserted relative to the drawing sheet coordintae system or relative to the selected object coordinate.

User needs to specify the note text, size and offset in the property manager page. These options are preserved across the session.

### Convert Solid Body To Surface Body
[![VB.NET Source Code](https://img.shields.io/badge/src-VB.NET-blue.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/macro-feature\convert-solid-to-surface\vb-net)

Add-in allows converting selected solid body or bodies into the corresponding surface bodies.

Add-in utilizes macro feature so the converted body preserves parametric functionality.

### Create Geometry API
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/add-in/create-geometry-api)

Example demonstrates how to create API in SOLIDWORKS add-in and call it from the stand-alone application or macro. API allows to create cylindrical body with specified parameters.

### Distance Heat Map
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/add-in/distance-heat-map)

Add-in is using documents handler utility of the framework to setup custom model colorizer to display the heat map for distance of selected point.

### Face Indexer
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/add-in/face-indexer)

Demonstration of technique to call SOLIDWORKS add-in API from stand-alone application as in-process call. This improves the performance in 100s of times.

### Geometry Helper API (ROT)
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/add-in/geometry-helper-api-rot)

Demonstration of alternative approach to create SOLIDWORKS add-in API. Running object table (ROT) is used in this case. This technique enables the ability to call API without the need to reference SOLIDWORKS interop libraries.

### Issues Manager
[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/add-in/issues-manager/csharp)
[![VB.NET Source Code](https://img.shields.io/badge/src-VB.NET-blue.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/add-in/issues-manager/vb-net)

Demonstration of using of 3rd party storage store and streams to manage issues associated with documents. WPF interface is added to task pane allowing to view and edit issues.

### Insert Linked Geometry

[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/macro-feature/link-external-file/csharp)
[![VB.NET Source Code](https://img.shields.io/badge/src-VB.NET-blue.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/swex/macro-feature/link-external-file/vb-net)

Demonstration of using macro feature to create a geometry linked to external file with an ability to regenerate the geometry when the source file is updated.

## eDrawings API Examples

### Batch Export To PDF

[![VB.NET Source Code](https://img.shields.io/badge/src-VB.NET-blue.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/edrawings-api/BatchExportPdf)

Tool to export SOLIDWORKS drawings to PDF by printing them to PDF printer via eDrawings

### eDrawings Windows Forms Host

[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/edrawings-api/eDrawingsWinFormsHost)

Demonstration hosting of eDrawings control in the Windows Forms Application

### eDrawings Windows Presentation Foundation (WPF) Host

[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/edrawings-api/eDrawingsWpfHost)

Demonstration hosting of eDrawings control in the Windows Presentation Foundation (WPF)

### Measurement Surveying

[![C# Source Code](https://img.shields.io/badge/src-C%23-yellow.svg)](https://github.com/codestackdev/solidworks-api-examples/tree/master/edrawings-api/MeasurementSurveying)

Demonstration of eDrawings Markup API to collect the measurement data