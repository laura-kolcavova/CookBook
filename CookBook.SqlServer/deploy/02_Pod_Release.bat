kubectl apply -f ../k8s/namespace.yaml
kubectl apply -f ../k8s/secrets.yaml
kubectl apply -f ../k8s/statefulset.yaml
kubectl apply -f ../k8s/service.yaml

kubectl port-forward -n cookbook service/cookbook-sql-server 8000:1433