FROM microsoft/dotnet:2-runtime-jessie
WORKDIR /app
COPY ./publish/ ./
ENTRYPOINT ["dotnet", "EventSourcingPoc.Customer.ConsoleUI.dll"]