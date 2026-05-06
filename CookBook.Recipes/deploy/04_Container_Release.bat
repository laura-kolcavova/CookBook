#docker run -it --rm  -d -p 8010:8010 --name cookbook-recipes-container --network cookbook-network cookbook-recipes-image

kubectl apply -f k8s/namespace.yaml
kubectl apply -f k8s/secrets.yaml
kubectl apply -f k8s/deployment.yaml
kubectl apply -f k8s/service.yaml

kubectl port-forward -n cookbook service/cookbook-recipes-api 8010:8010