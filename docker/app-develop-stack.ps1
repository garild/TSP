#! /bin/sh
docker-compose -f apps-develope-compose.yml down
docker-compose -f apps-develope-compose.yml up --build $1