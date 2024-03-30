# Raftelis-Technical-Interview

## Introduction
This web application is designed to display and manage property listings. It provides features for sorting and viewing property details, including a link to the property's location on Google Maps.

## Features
- View a list of properties with details such as PIN, address, owner, market value, sale date, and sale price.
- Sort properties by various criteria including PIN, address, owner, market value, sale date, and sale price.
- Direct links to Google Maps for each property address.
- Responsive design for optimal viewing on various devices and screen sizes.

## Technologies Used
- ASP.NET Core Razor Pages
- Bootstrap for styling
- Google Maps API for map links

## Setup and Running

### Running from the Command Line
To run this project locally from the command line, follow these steps:

1. Ensure you have [.NET 5.0 SDK](https://dotnet.microsoft.com/download) or later installed.
2. Clone the repository to your local machine.
3. Navigate to the project directory in your terminal or command prompt.
4. Run `dotnet restore` to restore all the necessary NuGet packages.
5. Run `dotnet run` to start the application.
6. Open your web browser and navigate to the URL indicated in your terminal.

### Running in Visual Studio
To run this project in Visual Studio, follow these steps:

1. Open Visual Studio.
2. From the File menu, select Open > Project/Solution.
3. Navigate to the directory where you cloned the project and open the solution file (`.sln`).
4. Once the project is open in Visual Studio, you can run it by pressing `F5` or clicking the "Start" button in the toolbar. Visual Studio should automatically open your default web browser and navigate to the app's home page.

## Data
The application uses a text file (`Parcels.txt`) located in the `Data` folder as its data source. Ensure this file is correctly formatted and contains the data you wish to display.

## Customization
You can customize the look and feel of the application by modifying the CSS files located in the `wwwroot/css` folder.

## Acknowledgments
This project was developed as part of an interview process for Raftelis. I'd like to thank the Raftelis team for providing me with this opportunity.
