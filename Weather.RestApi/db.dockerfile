# Use the official SQL Server image
FROM mcr.microsoft.com/mssql/server:2022-latest
ENV MSSQL_PID=Express
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P455w0r7!

# Add optional environment variable for database name
ENV MSSQL_DBNAME=Weather