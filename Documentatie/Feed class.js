wachtwoord mysql database:
#3WR$G28ZGl


$ kubectl run -it --rm --image=mysql:8.0 --restart=Never mysql-client -- mysql -h mysql-clusterip-srv -password="#3WR$G28ZGl"
