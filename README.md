# Customer Management System

## Description

This is a C# application for managing customers, orders, and items. It provides basic CRUD (Create, Read, Update, Delete) functionality for each of these entities. The application is built with a layered architecture, using a repository and unit of work pattern to interact with the database.

## Features

*   **Customer Management:**
    *   Add new customers
    *   Update existing customer information
    *   Delete customers
    *   Find customers by ID
*   **Order Management:**
    *   Create new orders for customers
    *   Update existing orders
    *   Delete orders
    *   Find orders by ID
*   **Item Management:**
    *   Add items to an order
    *   Update items in an order
    *   Delete items from an order
    *   Find items by ID

## Project Structure

The project is organized into the following layers:

*   **Controller:** Contains the main application logic and acts as an intermediary between the user interface and the data layer.
*   **Repository:** Implements the repository pattern to provide a generic way to access data.
*   **Unit of Work:** Implements the unit of work pattern to manage database transactions and ensure data consistency.
*   **Data:** Contains the data models (Customer, Order, Item) that represent the database entities.

## Setup and Usage

### Prerequisites

*   Visual Studio
*   .NET Framework
*   SQL Server (or another compatible database)

### Running the Application

1.  **Clone the repository:**
    ```sh
    git clone <repository-url>
    ```
2.  **Open the solution in Visual Studio:**
    *   Open the `CustomerManagement.sln` file.
3.  **Configure the database connection:**
    *   Update the connection string in the `App.config` file to point to your database.
4.  **Build the solution:**
    *   Build the project to restore the necessary NuGet packages.
5.  **Run the application:**
    *   Start the application from Visual Studio. The `Program.cs` file contains a `Main` method that demonstrates the basic functionality of the application.

## Example Usage

The `Program.cs` file includes example code that demonstrates how to use the controller to perform various operations, such as:

*   Creating and updating customers
*   Placing orders
*   Adding items to orders
*   Querying for customer orders

You can modify this file to experiment with the different features of the application.