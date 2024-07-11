# DemoQA UI & API Automation Test Project

## Overview
These automation test projects are designed for testing DemoQA. They are built on .NET (C# is the main programming language) and utilize NUnit 3 and RestSharp. The project follows the BDD (Behavior Driven Development) approach using Specflow and Cucumber.

## Project Structure
The solution comprises three projects:

1. **Core**: Contains utilities for API interaction, configuration file management, status code verification, schema validation, and data storage for teardown processes.
2. **DemoQA.Service**: Contains request and response models, as well as `UserServices` for sending API requests.
3. **DemoQA.Test**: Contains the test steps, features, step definitions, test data, and configuration settings (`appsettings.json`).

## Development Tools
The project is set up using Visual Studio Code.

## Configuration File
The primary configuration file for this project is `appsettings.json`, which contains the application URL

## Running Tests
Before each test case, the token will be generated.

### Creating `user.json`

Create a `user.json` file in the `data` of your project to store your credentials. The format of the file should be as follows:

```user.json
{
   "Username": [Your Username],
   "Password": [Your Password],
   "Id": [Your Account Id]
}
```

### Using Visual Studio 2019
- Utilize the Test Explorer to select and run tests.

### Using Command Line
1. Install SpecFlow NUnit Adapter
   ```sh
   Install-Package SpecRun.SpecFlow
   ```
2. Restore all dependency packages:
   ```sh
   dotnet restore
   ```
3. Build the project:
   ```sh
   dotnet build
   ```
4. Run all tests:
   ```sh
   dotnet test
   ```
5. Run specific tests based on category:
   ```sh
   dotnet test --filter Category=GetUser
   ```
   (Replace `GetUser` with the desired test category.)
