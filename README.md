# event-sourcing-poc
For discovering event sourcing

## Preparation
In the docker-compose.yml change the ip-adres to your host ipadres

Run the below commands in
- customer-consoleUI
- shopping-consoleUI
- read models (to be imlpemented)
```
$ dotnet restore
$ dotnet publish -c release -o publish
```

Build the docker-containers for the consumer and the producer:
```
$ docker build . -t coilz/web-store-customer
$ docker build . -t coilz/web-store-shopping
$ docker build . -t coilz/web-store-readmodels
```

## Starting Kafka
From the root folder
```
$ docker-compose up -d
```

To stop the composition:
```
$ docker-compose down
```

## Running the various applications
Now execute in a different shell:
```
$ docker network ls
```
You will notice there is a new network created <foldername>_default. In my case it is eventsourcingpoc_default.

### Run customer-consoleUI:
```
$ docker run --rm --network="eventsourcingpoc_default" coilz/web-store-customer kafka my-topic
```
You can also add it to the compose file if you want

### Run shopping-consoleUI:
```
$ docker run --rm -it --network="eventsourcingpoc_default" coilz/web-store-shopping kafka my-topic
```
