kubectl apply -f ../k8s/namespace.yaml
kubectl apply -f ../k8s/ingress.yaml
kubectl apply -f ../k8s/secrets.yaml
kubectl apply -f ../k8s/deployment.yaml
kubectl apply -f ../k8s/service.yaml

kubectl port-forward -n cookbook service/cookbook-recipes-api 8010:8010