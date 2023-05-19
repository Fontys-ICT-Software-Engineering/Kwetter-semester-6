#!/bin/bash

kubectl delete deployment authservice-db
kubectl delete deployment profileservice-db
kubectl delete deployment kweetwriteservice-db
kubectl delete deployment mongodb

kubectl delete deployment rabbitmq

kubectl delete deployment authservice-deployment
kubectl delete deployment kweetreadservice-deployment
kubectl delete deployment kweetwriteservice-deployment
kubectl delete deployment profileservice-deployment

kubectl delete deployment mongo-express
kubectl delete deployment phpmyadmin
