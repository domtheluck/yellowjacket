FROM domtheluck/aspnetcore-node
ARG source
WORKDIR /app
EXPOSE 80
COPY $source .
ENTRYPOINT ["dotnet", "YellowJacket.Dashboard.dll"]
