# Build Fluentd docker image
cd ../fluentd/images
echo -e "\n Building Fluentd docker image\n" 
cd ../fluentd/images && docker build -t fluentd-svc:local . && sleep 1 && cd ../../../ 
# Run elasticsearch
sudo kubectl apply -f ../elasticsearch/ && sleep 1 && sudo kubectl apply -f ../fluentd/state/ && sudo kubectl apply -f ../../kibana/