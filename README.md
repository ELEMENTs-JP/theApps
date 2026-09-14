# theApp
The app is a business-oriented .NET Core 10 Blazor Server application (Windows build) that is generally compatible with all browsers. However, private applications can also be developed. The app is designed as an app designer.

<img width="1337" height="840" alt="image" src="https://github.com/user-attachments/assets/74c482aa-39bc-4e60-b777-0915a6e368d6" />

## Features
The app includes a wide range of features that are continuously being expanded and refined. The current version is a pre-alpha release.
- Security (Login, Logoff, Register, Setup, Request new Password, etc.)
- Search (global search, local search in tables)
- Tables, Calendar, Images, Videos, Audio, Files, Checklist
- Multilingual support (German, English, French, Spanish) is not yet complete (in progress)

## Single-Client vs. Multi-Client System
This is a single-client system. However, upon request as part of a project, a multi-client system with multiple databases can be developed.

## Item Editing
The interface for editing entries can be customized using code or the integrated designer (UI) or developed from scratch (code).

<img width="1600" height="1297" alt="image" src="https://github.com/user-attachments/assets/150f8905-da39-455a-a5b4-c5a09ce8f717" />

**External Libraries**

We use the following external libraries:
- Tabler.IO for graphical design
- Sortable.JS for drag-and-drop operations
- Apache eCharts for charting
- videojs for video streaming

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
- 
**Note**
  
The system is not currently a fully-fledged business management system. It is an application builder that can create various applications based on specific data record types. The navigation shown in the screenshot is an example of the types of applications that can be designed.
