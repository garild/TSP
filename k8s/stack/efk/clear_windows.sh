#! /bin/sh
green=`tput setaf 2`
reset=`tput sgr 0`
yellow=`tput setaf 3`

read -p "${yellow} Are you sure you want use this scripts ?? ${reset} (y/N)" CONFIRM

if [ "$CONFIRM" = "y" ]; then
echo  "${green} \n Deleteing all EFK stack components\n ${reset}"  

 kubectl delete clusterrole fluentd
 kubectl delete persistentvolume apps-logging-pv
 kubectl delete storageclass elastic-local-storage
 kubectl delete -n default service fluentd-svc
 kubectl delete -n default service kibana-svc
 kubectl delete -n default service elasticsearch-svc
 kubectl delete -n default configmap fluentd-config-map
 kubectl delete -n default configmap kibana-configmap
 kubectl delete -n default configmap elasticsearch-configmap
 kubectl delete -n default deployment kibana
 kubectl delete -n default daemonset fluentd
 kubectl delete -n default statefulset es-cluster 
 kubectl delete -n default secret elasticsearch-user-password
 kubectl delete storageclass local-storage
 kubectl delete -n default ingress kabina-ingress
 kubectl delete -n default secret $(  kubectl get -n default secret |  awk '{ print $1 }' | grep fluentd-)

echo  "${green} \n \n Done! \n \n ${reset}"  
else
 exit 0
fi