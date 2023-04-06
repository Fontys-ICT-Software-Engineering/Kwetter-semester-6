Afstudeer/Stage opdracht iT2: Full stack web development vue.js met .netcore API

Centralized Storage Solution



apiVersion: apps/v1
kind: Deployment
metadata:
  name: kweetservice-deployment
spec:
  replicas: 1
  selector:
    matchLabels:
      app: kweetservice
  template:
    metadata:
      labels:
        app: kweetservice
    spec:
      containers:
      - name: kweetservice
        image: fendamear/kweetservice:latest 
        resources:
          limits:
            memory: 512Mi
            cpu: "1"
