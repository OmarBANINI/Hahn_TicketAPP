# Hahn_TicketApp

This is a .NET 8.0 web application built using Entity Framework Core, ASP.NET Core Identity, and OpenAPI (Swagger). Follow the steps below to set up and run the project using **Visual Studio**.

## Prerequisites

Before you can run the application, ensure you have the following installed:

- **Visual Studio 2022**
- **SQL Server** (or any other configured database)
- **Git**

## Setup Instructions

### 1. Clone the repository

1. Open **Visual Studio 2022**.
2. On the **start window**, select **Clone a repository**.
3. In the **Repository location** field, enter the GitHub URL for the project repository:

    ```bash
   https://github.com/OmarBANINI/Hahn_TicketApp.git
    ```

4. Choose a local path to clone the repository and click **Clone**.

### 2. Open the project

Once cloned, Visual Studio will automatically open the solution. If not, go to **File > Open > Project/Solution** and select the `.sln` file from the cloned repository.

### 3. Install dependencies

To install the project dependencies (NuGet packages):

1. In **Solution Explorer**, right-click on the solution or the project name.
2. Select **Restore NuGet Packages**. This will download and install all necessary dependencies defined in the `.csproj` file.

### 4. Update the database connection string

In **Solution Explorer**, open the `appsettings.json` file and update the connection string to point to your SQL Server instance. Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;User Id=YOUR_USER_ID;Password=YOUR_PASSWORD;Trusted_Connection=False;MultipleActiveResultSets=true"
  }
}
```
### 5. Apply Migrations to the Database

1. Open **Package Manager Console** in Visual Studio by going to **Tools > NuGet Package Manager > Package Manager Console**.
2. Run the following command to apply the migrations:

    ```bash
    Update-Database
    ```

### 6. Build the Solution

1. In **Solution Explorer**, right-click the solution or project and select **Build Solution** (or press `Ctrl+Shift+B`).
2. Ensure that there are no build errors.

### 7. Run the Application

1. In **Solution Explorer**, right-click the main project and choose **Set as Startup Project**.
2. Press **F5** or click on the **Start** button to run the application.

### 8. Test the Application

Once the application is running, you should see the **Swagger UI (OpenAPI)** where you can test the API endpoints.

