# Microsoft Store Developer Command Line Interface (CLI)

## About
The Microsoft Store Developer Command Line Interface is a cross-platform (Windows, MacOS, Linux) CLI that helps developers access the Microsoft Store APIs, for both managed (MSIX), as well as unmanaged (MSI/EXE) applications. It helps developers by creating required online resources (credentials), as well as later setting up their application projects (UWPs, Win32s, Flutter, PWAs, Electron, React-Native, as well as many other types of Windows applications) to be ready to ship to the Microsoft Store, going from the initial steps of configuring the application's manifest, as well as the actual publishing of an MSIX or MSI/EXE.

## Installation
Note: The msstore-cli need to run on an X environmnet, for example it can't run on Linux server environment.

Download the proper install file from https://github.com/microsoft/msstore-cli/releases and follow instructions.

## Configuration
Note: if you run msstore command for the first time you can follow the instructions step by step to achieve the required items

For configuring msstore-cli you should provide four items:
- tenantId: 
    go to https://partner.microsoft.com/en-us/dashboard/account/v3/usermanagment and click "Add Azure AD Application" select "Manager(Windows)"
    Note: Logiin using your Azure AD credentials, not your MSA
- sellerId: 
    go to https://partner.microsoft.com/en-us/dashboard/account/v3/organization/legalinfo
- clientId, clientSecret:
    use msstore to create one or go to portal.azure.com > Active Directory
