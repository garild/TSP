#! /bin/sh
green=`tput setaf 2`
reset=`tput sgr 0`
yellow=`tput setaf 3`

echo  "${green} \nRunning EFK stack rollout\n ${reset}"  

echo  "${yellow} \n Deploying Elasticsearch cluster setup\n ${reset}"  
sudo kubectl apply -f ../elasticsearch/
sleep 8


echo  "${yellow} \n Deploying Fluentd Deamon Set \n ${reset}"  
sudo kubectl apply -f ../fluentd/
sleep 2

echo  "${yellow} \n Deploying Kibana service \n ${reset}"  
sudo kubectl apply -f ../kibana/
sleep 2

echo  "${green} \n Checkout EFK stack status \n ${reset}"  
sudo kubectl get svc | grep -i svc
echo  "${green} \n Done! \n \n${reset}"  