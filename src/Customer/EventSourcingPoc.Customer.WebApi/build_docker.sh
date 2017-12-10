rm -r ./publish

dotnet restore
dotnet publish -c release -o ./publish
docker build . -t coilz/web-store-customer-api