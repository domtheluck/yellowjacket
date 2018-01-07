FROM microsoft/aspnetcore-build:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY src/YellowJacket.Dashboard/YellowJacket.Dashboard.csproj src/YellowJacket.Dashboard/
COPY src/YellowJacket.Common/YellowJacket.Common.csproj src/YellowJacket.Common/
COPY src/YellowJacket.Models/YellowJacket.Models.csproj src/YellowJacket.Models/
RUN dotnet restore
COPY . .
WORKDIR /src/src/YellowJacket.Dashboard
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YellowJacket.Dashboard.dll"]
