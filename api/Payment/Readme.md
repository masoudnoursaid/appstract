# Payment  [![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)

![version](https://img.shields.io/badge/version-1.0.0-blue)

# Introduction

The Payment microservice offers a comprehensive set of endpoints for seamless communication
and currency transfers, encompassing a wide array of the world's major fiat currencies,
as well as popular cryptocurrencies such as BTC, ETH, BNB, TRC, and various other
widely-used networks globally.

# Getting Started

Setup environment :

1. Follow the instructions in the ``.env.template`` file and create a new file named ```.env```.
   Place this file in the bin folder of your running project and root directory.
2. Execute the following commands to generate the ``.pfx`` certificate:

> dotnet dev-certs https -ep $certPath -p $password

3. Copy the ``cert.pfx`` file to the running project in presentation directory.
4. Open the ``.env`` file and update the cert path and password with the appropriate values.

# Migrations

1. Follow below command to create and apply a new migration :

```shell
cd src/Service/Migrations
dotnet-ef migrations add $MY_MIGRATION_NAME -o ..\Migrations\PaymentDbMigrations -s ..\Admin.Api\ -v
dotnet-ef database update -s ..\Admin.Api\ -v
```

2. Apply migration by running Migration project with arguments

```shell
cd src/Service/Migrations
dotnet run migrate #for apply all migrations to db
dotnet run digrate #first drop all migration and then update
```

# Seed

Seed percona to support ``Paypal``/``Billplz``/``Stripe``/``Mollie`` payment methods.

```shell
cd src/Service/Admin.Api
dotnet run seed
```

# Docker Support

1. **Install Docker**: If you haven't already, [install Docker](https://docs.docker.com/get-docker/) on your development
   machine.
2. Follow ``.env.template`` and add ``.env`` file to project root.
3. Run below command :

> docker compose up

# Test

1. Create ``.env`` file in your test project ``bin`` folder
2. Run ``dotnet test payment.sln``

# Sdk

To access the Payment SDK documentation, please refer to [docs](src/Sdk/Samples/Asp.Net/Readme.md)


