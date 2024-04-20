# CSharp.eShop.MicroService.net8

- [CSharp.eShop.MicroService.net8](#csharpeshopmicroservicenet8)
  - [01: Develop Catalog.API with Vertical slice architecture and CQRS](#01-develop-catalogapi-with-vertical-slice-architecture-and-cqrs)
    - [Implement CQRS abstractions Icommand into  vertical slice feature handler class](#implement-cqrs-abstractions-icommand-into--vertical-slice-feature-handler-class)
  - [99: Troubleshooting](#99-troubleshooting)

C# code repository - learning developing complex micro service.

## 01: Develop Catalog.API with Vertical slice architecture and CQRS

1. Created new repository on Git, ref. - Repo: [eShop.MicroService.net8](https://github.com/pandyamehul/eShop.MicroService.net8).

2. Cloned repo to local folder directlry using Git.clone command or using Visual studio clone option.

    ```cmd
    git clone https://github.com/pandyamehul/eShop.MicroService.net8.git
    ```

    OR

    ![Clone Catalog API](/img/20240420.1710.Clone.Catalog.API.png)
    ![Clone Catalog API](20240420.1710.Clone.Catalog.API.2.png)

    Clone repo to local folder on computer.

3. Open folder in visual studio and create new project using "Blank Solution" visual studio template.
4. Create new solution folder name "Services\Catalog". Commit code and push to remorte branch from visual studio.
5. Add new ASP.net core empty project for "Catalog.API" place under "\src\service\catalog" folder.
6. Setup application port. This can be done using Project properties --> navigate to debug section --> open debug launch profile or directly update the appsettings.json file.
7. Catalog API service will use http port 5000 and https port 5050.
8. To understand domain concept of Catalog micro service go through PPT page 113 to 123.
9. To understand technical concept of Catalog micro service go through PPT page 124 to 130.
10. Read through slide 149, to understand various topics.
11. Implemnted code as per course. Added CQRS interfaces and then Create product handlers, using MediatR library.
12. Seperated command and query using CQRS design pattern.
13. Created abstraction on MediatR using CQRS. Added generic implementation and building block IRequest --> IQuery --> IRequestHandler --> IQueryHandler and IRequest --> ICommand --> IRequestHandler --> ICommanHandler.

### Implement CQRS abstractions Icommand into  vertical slice feature handler class

## 99: Troubleshooting
