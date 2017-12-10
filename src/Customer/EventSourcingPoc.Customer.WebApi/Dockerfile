FROM microsoft/aspnetcore:2.0.0-stretch

ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

WORKDIR /app
COPY ./publish ./
ENTRYPOINT ["dotnet", "EventSourcingPoc.Customer.WebApi.dll"]