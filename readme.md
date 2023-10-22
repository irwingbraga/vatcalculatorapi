
# Vat Calculation API

This API allows users to calculate Net, Gross, and VAT amounts for purchases in Austria.

## Features

-   Calculates missing values (Net, Gross, VAT) based on provided input.
-   Supports Austrian VAT rates: 10%, 13%, 20%.
-   Validates input values and provides meaningful error messages.
-   Swagger UI for API documentation and testing.
-   Unit tests for ensuring functionality.
-   Clean architecture with MediatR, FluentValidation, and other best practices.

## Getting Started

## Prerequisites

-   [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
-   [Docker](https://www.docker.com/products/docker-desktop)

## Running the API Locally

### Using .NET CLI

1.  Navigate to the project root directory:
    
    `cd path/to/VatCalculatorApi` 
    
2.  Restore the required packages:
    
    `dotnet restore` 
    
3.  Build and run the project:
    
    `dotnet run` 
    
### Using Docker

1.  Pull the Docker image from Docker Hub:
    
    `docker pull irwingbraga/vatcalculatorapi:latest` 
    
2.  Run the Docker container:
    
    `docker run -p 5000:80 irwingbraga/vatcalculatorapi:latest`
    

After running the above commands, the API should be accessible at `http://localhost:5000`.

## API Documentation

After launching the application, you can access the Swagger UI documentation at `http://localhost:5000/swagger`.

## Running Tests

[![.NET Unit Tests](https://github.com/irwingbraga/vatcalculatorapi/actions/workflows/dotnet-test.yaml/badge.svg)](https://github.com/irwingbraga/vatcalculatorapi/actions/workflows/dotnet-test.yaml)

### Using .NET CLI

1.  Navigate to the tests directory:
    
    `cd path/to/VatCalculator.UnitTests` 
    
2.  Run the tests:
    
    `dotnet test` 
    

## Built With

-   .NET 7
-   MediatR
-   FluentValidation
-   xUnit, FluentAssertions, AutoFixture for testing

## Contributing

For contributing, please create a new branch, implement your feature or bug fix, and create a pull request to the `main` branch.

## License

This project is licensed under the MIT License.