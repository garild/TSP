#  Track Software Projects
` This project is something similar to Jira, but of course no as advanced like jira. The following functions will be available, such as:
  - create/update/close/reject task 
  - add remarks 
  - move task from project to other project 
  - basic authentication 
  - user privileges 
 Im gonna use EF or DacPac for e2e and integration test layer .
 For other stack like EFK, there will be log dashboard with resitricted privileges etc. ... more info upcomming soon!.`


#### This project was seperate for 2 application layers:
## This project was seperate for 2 application layers:
I. Backend - microservices - ks8
- [ ] **DDD** - Domain Driven Design architecture implementation
- [ ] **CQRS** - Command Query Responsibility Segregation command patern 
- [ ] **WebApi** - Service in .Net Core Framework
- [ ] **JWT Authorize** - Json Web Token authorization
- [x] **ElasticsearchSerilog** - this is required to user-friendly write log in kibana, log format. Log message has associated log level, which identifies how important/detailed the message 
- [ ] **Cookie Authentication ASP.NET Core**
- [ ] **NUnit Test** - integration,e2 and unit tests  
- [ ] **Docker** - containler services builder/publisher
- [x] **CI/D scripts** 
- [ ] **Swagger** - UI provides a display framework that reads an OpenAPI specification document and generates an interactive documentation website 

#### EFK Stack
- [x] Kibana - the complete Elastic Stack for **free** and start visualizing, analyzing, and exploring data. 
- [x] Elasticsearch -  RESTful search and analytics engine capable of addressing a growing number of use cases.
- [x] FluentD - log aggregat collector, catch every console logs in docker containers

### Database
- [ ] Redis ?
- [ ] MongoDB ?
- [ ] EF ?
- [ ] DacPac ?

II. Frontend - Angular 7 + Material

- https://material-components.github.io/material-components-web-catalog/#/

**CLI:**
1. npm install
2. ng build
3. ng serve -o


**Services:**
- [ ] AuthApi


**Docker Compose**

- run as powershell script ./app-develop-stack.ps1 
- alternative "docker-compose -apps-develope-compose.yml up --build -d 
