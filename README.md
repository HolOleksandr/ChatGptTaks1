# Inventory System

Inventory System is a web application designed to manage and maintain product inventory information. The application provides CRUD (Create, Read, Update, Delete) functionality for products, including managing product names, descriptions, and prices.

## Features

- Add new products with a name, description, and price.
- View a list of all the products in the inventory.
- Update product details, including name, description, and price.
- Delete products from the inventory.

## Getting Started

Follow these instructions to set up the project locally.

### Prerequisites

- .NET 5.0 or later
- Visual Studio 2019 or Visual Studio Code with C# extension
- Microsoft SQL Server (or another compatible database)

### Setup

1. Clone the repository.
2. Navigate to the solution folder.
3. Restore the .NET and NuGet packages: dotnet restore

4. Update the connection string in the `appsettings.json` file located in the `Inventory.API` project folder:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
  },
  // ...
}
Replace YOUR_SERVER, YOUR_DATABASE, YOUR_USER, and YOUR_PASSWORD with your database information.

5. Run the database migrations to create and update the necessary database schema:
        dotnet ef database update --project Inventory.DAL

## Running the Application

1. Ensure that the `Inventory.API` project is set as the startup project in Visual Studio or in your `launchSettings.json` file.

2. Start the application:

   - In Visual Studio: Press `F5` or click the "Start" button with the green triangle.
   - In Visual Studio Code: Press `F5`, or open a terminal and run:
     
     ```
     dotnet run --project Inventory.API
     ```
     
3. Once the application is running, open a web browser and navigate to `https://localhost:5001` (or the address displayed in the terminal).

4. You can now start using the Inventory System to manage your products.




## Feedback
This task was easy. 
I did task for 1,5 hour. 
Not all code was ready, I changed Repositories and UnitOfWork, also UnitTest style.
ChatGPT didn't understand how to format README file.
Got promts are: architecture, clean code and unit test writing.