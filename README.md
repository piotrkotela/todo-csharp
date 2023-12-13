### Prerequisites (for M1 Macs)

- Docker
- Azure Data Studio
- .NET SDK

### Setting up a local MS SQL database

1. Pull MS SQL database image by executing the below command:

   `docker pull mcr.microsoft.com/azure-sql-edge`

2. Run the container

   `docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge'`

3. Connect to the database by using Azure Data Studio, enter the following connection detals:

   - **Server**: localhost,1433
   - **Authentication** type: SQL Login
   - **User**: sa
   - **Password**: The password you set for MSSQL_SA_PASSWORD.

   Password is whatever was set in this variable `MSSQL_SA_PASSWORD`, in our case it is `yourStrong(!)Password` and it is the password used in `appsettings.json`

4. Create a test database with the name of your choice. Remeber to replace the name in app `appsettings.json` (it's `Database=MyProjectDB` by default)

### Local Development

Run `dotnet restore` and then `dotnet run` to run the project locally. Remember that you need MS SQL Docker container to be running.

### Testing

All the test are in TodoCSHarp.Tests project, execute `dotnet test` from TodoCSHarp.Tests folder to run the tests

### Other

If you are using VSCode, install C# Dev Kit to see syntax highlighting and other useful stuff
