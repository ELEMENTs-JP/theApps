# theApp
The app is a business-oriented .NET Core 10 Blazor Server application (Windows build) that is generally compatible with all browsers. However, private applications can also be developed. The app is designed as an app designer.

![Lizenz](https://img.shields.io/badge/License-CRL-blue.svg)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=dotnet&logoColor=white)
[![Status](https://img.shields.io/badge/Status-Draft-orange)](https://github.com/ELEMENTs-JP/theApps)

> [!IMPORTANT]
> The software is still in an early stage of development. We do not recommend using the software in production environments. For example, security features are not yet fully implemented.

<img width="1408" height="890" alt="Taskliste" src="https://github.com/user-attachments/assets/398941b1-e20b-4dbc-b4d2-8f130d18f57a" />

## Key Features of This Application
- Dynamic Application Builder
- Default Apps and ItemTypes
- Page Designer (using Drag Drop)
- Drag-and-drop interfaces for business boards (eg. Kanban, Backlog, etc.)
- Global, local, and linked search
- Customizable item types
- Numerous controls for user input

## User Interfaces
The app includes a wide range of features that are continuously being expanded and refined. The current version is a pre-alpha release.
- Security (Login, Logoff, Register, Setup, Request new Password, etc.)
- Search (global search, local search in tables)
- Tables, Calendar, Images, Videos, Audio, Files, Checklist
- Multilingual support (German, English, French, Spanish) is not yet complete (in progress)

## Single-Client vs. Multi-Client System
This is a single-client system. However, upon request as part of a project, a multi-client system with multiple databases can be developed.

## Database
The application uses an SQLite database that is automatically created during setup. A separate database server is not required. This helps reduce costs.

## Item Editing
The interface for editing entries can be customized using code or the integrated designer (UI) or developed from scratch (code).

<img width="1475" height="1236" alt="Task" src="https://github.com/user-attachments/assets/a02b1419-b95f-4347-b165-9694594b7c14" />

## File Management
Uploaded files are stored in a separate FILES directory. It makes sense to move this directory to a separate location.

**External Libraries**

We use the following external libraries:
- Tabler.IO for graphical design
- Sortable.JS for drag-and-drop operations
- Apache eCharts for charting
- videojs for video streaming
- calendar and charts are from a separate open-source library via NuGet.

**System Requirements**

Currently, the following system requirements are needed. The system was developed and tested using these requirements.
- Microsoft Windows 11 Professional
- Microsoft Internet Information Server
- .net Core 10 (Blazor Server App)
- Disk space: Depends on how many files are stored in the system. The software itself is currently a maximum of 50 MB in size

**Installation and Setup**

Installation and setup are relatively simple.
- Copy the release files to a directory
- Create a website on IIS
- Link the website to the directory
- Access the website
- Run the setup

> [!NOTE]
> The screenshots were taken from the development environment.
> The people shown (e.g., avatar images) were generated using AI.

**Contact**

If you have any further questions or development needs, please visit the website theSTRIDEsPath.com
