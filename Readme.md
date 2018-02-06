# My Portfolio

**Authors**: Luay Younus

## Overview
This is a website that I built with C# ASP.NET Core using Razor Pages.

#### Portfolio deployed on Azure

[![APIEndPoint](https://raw.githubusercontent.com/MidTermProject/Monster-Hunter-API/master/Resources/azure-logo.png?raw=true) ](http://luayyounus.azurewebsites.net/)

### Getting Started
The following are required to run the program locally.
- [Visual Studio 2017 Community with .NET Core 2.0 SDK](https://www.microsoft.com/net/core#windowscmd)
- [GitBash / Terminal](https://git-scm.com/downloads) or [GitHub Extension for Visual Studio](https://visualstudio.github.com)

1. Clone the repository to your local machine.
2. Cd into the application directory where the `Portfolio.sln` exist.
3. Open the application using `Open/Start Portfolio.sln`.
4) Once the App is opened, Right click on the application name from the `Solution Explorer Window` and select `Add` -> `New Item` -> find `ASP.NET Configuration File` and open add it to the project.
- Inside this file, change the Connection String to the following to connect to database
```css
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=PortfolioDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

5) Click `Tools` -> `NuGet Package Manager` -> `Package Manager Console` then run the following commands in the console.
```css
- Install-Package Microsoft.EntityFrameworkCore.Tools
- Add-Migration Initial
- Update-Database
```
6. Once the database is updated, you can Run the application by clicking on the Play button <img src="https://github.com/luayyounus/Lab02-Unit-Testing/blob/Lab02-Luay/WarCardGame/play-button.jpg" width="16">.

### Frameworks & Dependencies
- Entity Framework Core
- ASP.NET Core
- Azure
- Razor Pages
- Bootstrap
- .NET tag helpers
- Xunit