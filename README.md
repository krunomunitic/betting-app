# Betting App

Betting App is a simple app for betting.

## Installation and Launch

On mac (req install docker):

```bash
docker run -d -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=PASSWORD" -p 1433:1433 mcr.microsoft.com/mssql/server:2019-latest
git clone repo
cd betting-app
npm install ./clientapp
dotnet ef database update
dotnet run

```
## Prerequisites
- Node.js
- Npm
- ASP.NET core

## About


## Resources
[VueJS with Asp.Net Core 3.1 Web API Template](https://marketplace.visualstudio.com/items?itemName=alexandredotnet.netcorevuejs)