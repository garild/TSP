#! /bin/bash

rm -r $(dirname $0)/packages
Projects=$(dirname $0)/src/infrastructure/*

for f in $Projects
do
    ProjectName="$(basename -- $f)"
    tput setaf 2;echo -e "\nRestoring project name $ProjectName\n"; tput sgr0;
	dotnet build -v q $f/$ProjectName.csproj -c Release  

done

Nuggets=$(dirname $0)/packages/*.nupkg

for f in $Nuggets
do
  NuggetName="$(basename -- $f)"
  tput setaf 2; echo -e "\nPushing package $NuggetName ...\n"; tput sgr0;
  dotnet nuget push $f -k oy2mzu7weg4mvjvvc5palw4msksnjjyerejn3ytmuvli2e -s https://api.nuget.org/v3/index.json

done