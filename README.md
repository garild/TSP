#  Track Software Projects
 This project is something similar to Jira, but of course no as advanced like jira. The following functions will be available, such as:
  - create/update/close/reject task 
  - add remarks 
  - move task from project to other project 
  - basic authentication 
  - user privileges 
 Im gonna use EF or DacPac for e2e and integration test layer .
 For other stack like EFK, there will be log dashboard with resitricted privileges etc. ... more info upcomming soon!.`


## I. Backend - microservices - ks8
- [ ] **DDD** - Domain Driven Design architecture implementation
- [x] **CQRS** - Command Query Responsibility Segregation command patern 
- [ ] **WebApi** - Service in .Net Core Framework
- [ ] **JWT Authorize** - Json Web Token authorization
- [x] **ElasticsearchSerilog** - this is required to user-friendly write log in kibana, log format. Log message has associated log level, which identifies how important/detailed the message 
- [ ] **Cookie Authentication ASP.NET Core**
- [ ] **NUnit Test** - integration,e2 and unit tests  
- [x] **Docker** - containler services builder/publisher
- [ ] **CI/D scripts** 
- [ ] **Swagger** - UI provides a display framework that reads an OpenAPI specification document and generates an interactive documentation website 

## EFK Stack
- [x] Kibana - Kibana is an open source data visualization plugin for Elasticsearch. It provides visualization capabilities on top of the content indexed on an Elasticsearch cluster. Users can create bar, line and scatter plots, or pie charts and maps on top of large volumes of data
- [x] Elasticsearch -  RESTful search and analytics engine capable of addressing a growing number of use cases.
- [x] FluentD - log aggregat collector, catch every console logs in docker containers

## Database
- [ ] Redis ?
- [ ] MongoDB ?
- [ ] EF ?
- [ ] DacPac ?

## II. Frontend - Angular 8 + Material

- https://material-components.github.io/material-components-web-catalog/#/

**CLI:**
```
1. npm install
2. ng build
3. ng serve -o --proxy-config proxy.conf.json 
```

## Backend stack
**Services:**
- [X] AuthApi

## Setup
**Docker Compose**

Run powershell script

` ./app-develop-stack.ps1 `

   or 
   
` docker-compose -f apps-develope-compose.yml up --build -d `
