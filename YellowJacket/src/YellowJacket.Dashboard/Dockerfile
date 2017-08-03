FROM yellowjacket/dotnet-node:1.1.2-runtime
ARG source
WORKDIR /app
EXPOSE 80
COPY $source .
ENTRYPOINT ["dotnet", "YellowJacket.Dashboard.dll"]
