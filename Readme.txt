Build and run app
To built and run the application the easiest way is to install the latest Visual Studio IDE, the platform which I used to built the whole application. It provide various applications that supports the smooth running and debugging of the applications. Also install Microsoft SQL Server for the database management. 
You can download them through the following link.
https://visualstudio.microsoft.com/downloads/
https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16
Then Update the connection string of the database in the “appsettings.json” file.
If they show any error in database related, then you could delete the files in Data/Migrations and follow the below commands, in the Package Manager Console in the bottom of the screen.
Add-Migration InitialMigration;
Update-Database;
Then Open the project in Visual studio. Then built the solution. Then run the solution. Now the server is running. A browser will be opened automatically and we can use the system locally.
For all to work smoothly we want to install following packages in the NuGet Package Manager. For that
Load a project in Solution Explorer, and then select Project > Manage NuGet Packages.
The NuGet Package Manager window opens.
Then install the following packages.
Microsoft.EntityFrameworkCore.SqlServer              {6.0.9}  
Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore {6.0.8}  
Microsoft.AspNetCore.Identity.EntityFrameworkCore    {6.0.9}  
Microsoft.EntityFrameworkCore.Tools                  {6.0.9}  
Microsoft.VisualStudio.Web.CodeGeneration.Design     {6.0.9}  
Microsoft.AspNetCore.Identity.UI                     {6.0.9}  
