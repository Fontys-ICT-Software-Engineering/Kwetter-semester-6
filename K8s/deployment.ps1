#!/bin/bash

# creating the database services first

kubectl apply -f .\Authservice\authservice-db-deployment.yaml
kubectl apply -f .\KweetReadService\mongodb-deployment.yaml
kubectl apply -f .\KweetWriteService\KweetWriteservice-db-deployment.yaml
kubectl apply -f .\ProfileService\ProfileService-db-deployment.yaml

Start-Sleep -Seconds 35

# creating the event bus

kubectl apply -f .\Overig\rabbitmq-deployment.yaml

Start-Sleep -Seconds 30

# Creation of the microservices itself

# creation of the messagebus queus
kubectl apply -f .\ProfileService\Profileservice-deployment.yaml
kubectl apply -f .\KweetReadService\kweetread-deployment.yaml

Start-Sleep -Seconds 10

kubectl apply -f .\Authservice\authservice-deployment.yaml
kubectl apply -f .\KweetWriteService\kweetwriteService-deployment.yaml

# adding of management tools

kubectl apply -f .\Overig\mongo-express-deployment.yaml
kubectl apply -f .\Overig\phpmyadmin-deployment.yaml

# configuring ingress controller

kubectl apply -f .\ingress\ingress-srv.yaml
