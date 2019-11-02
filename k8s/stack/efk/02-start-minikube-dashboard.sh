# Start minikube cluster
echo -e "\nStart minikube with 4gb memory\n"
sudo minikube start --vm-driver none --memory='4000mb' /

# proxy to dashboard
echo -e "Luanchng dashboard as proxy LOCALHOST\n" /
sudo kubectl proxy --address='0.0.0.0' --disable-filter=true