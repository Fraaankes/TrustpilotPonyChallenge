## Welcome to the Trustpilot Pony challenge.

The solution consists of 2 frontend projects, a core project containing maze solving logic and a xUnit test project.
    
## Prerequisites
* Dotnet Core 5 (https://dotnet.microsoft.com/download/dotnet/5.0)
* Node / npm (https://nodejs.org/en/download/)

### Trustpilot.Pony.Console

A simple console app that will let the computer move and solve the maze on your behalf, leading to almost guaranteed victory (due to a bug in the Trustpilot API)

### Launch instructions

* Navigate to solution folder
* `cd Trustpilot.Pony.Console`
* `dotnet run`

Once won, there will be no additional output.

### Trustpilot.Pony.Web

A React web application that will let you interactively move around, as your player of choice.
But beware, the Domokun is right on your heels, and one wrong move may turn out fatal.

### Launch instructions
* Navigate to solution folder
* `cd Trustpilot.Pony.Web\ClientApp`
* `npm install` - If its the first time running it
* `npm run start` - This will launch the React dev server on `http://localhost:3000`
* In a separate commandpromt navigate to `\Trustpilot.Pony.Web`
* `dotnet run` - This will launch ASP.NET as the backend server, using Kestrel on `https://localhost:5001`
* Open a browser and navigate to `https://localhost:5001`

Alterntively use Visual Studio 16.8 or higher, and just press F5.

### Unit Tests
The C# test project is an xUnit test project, containing test data and unit tests.

Any future integration tests for web is intended to go into a different project.

### Launch instructions
* Navigate to solution folder
* `cd Trustpilot.Pony.Tests`
* `dotnet test`

For the React tests there is a separate npm command `npm run test` 