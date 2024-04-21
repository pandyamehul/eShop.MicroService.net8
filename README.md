# CSharp.eShop.MicroService.net8

- [CSharp.eShop.MicroService.net8](#csharpeshopmicroservicenet8)
  - [Section 01 to 05: Develop Catalog.API with Vertical slice architecture and CQRS](#section-01-to-05-develop-catalogapi-with-vertical-slice-architecture-and-cqrs)
    - [49: Implement CQRS abstractions Icommand into  vertical slice feature handler class](#49-implement-cqrs-abstractions-icommand-into--vertical-slice-feature-handler-class)
    - [50: Develop Create Product endpoint with minimal API and Cater](#50-develop-create-product-endpoint-with-minimal-api-and-cater)
    - [51,52: Develop POST endpoint with Carter implements ICarterModule with minimal API](#5152-develop-post-endpoint-with-carter-implements-icartermodule-with-minimal-api)
    - [53: Register MediatR, Carter and Mapster libraries to ASP.net dependency injection services](#53-register-mediatr-carter-and-mapster-libraries-to-aspnet-dependency-injection-services)
  - [99: Troubleshooting](#99-troubleshooting)

C# code repository - learning developing complex micro service.

## Section 01 to 05: Develop Catalog.API with Vertical slice architecture and CQRS

1. Created new repository on Git, ref. - Repo: [eShop.MicroService.net8](https://github.com/pandyamehul/eShop.MicroService.net8).

2. Cloned repo to local folder directlry using Git.clone command or using Visual studio clone option.

    ```cmd
    git clone https://github.com/pandyamehul/eShop.MicroService.net8.git
    ```

    OR

    ![Clone Catalog API](/img/20240420.1710.Clone.Catalog.API.png)
    ![Clone Catalog API](/img/20240420.1710.Clone.Catalog.API.2.png)

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

### 49: Implement CQRS abstractions Icommand into  vertical slice feature handler class

- Implement command handler for create product feature in catalog api.
- Refactor create product command to use ICommand abstraction instead of IRequest.
- Create product entity from command object. Updated CreateProductHandler.cs file under Handle method create new product entity.

### 50: Develop Create Product endpoint with minimal API and Cater

- Implement product endpoint as minimal api with carter library to expose http end point.
- CQRS and request pipeline - develop request and response object.
- Carter library extends the capability of ASP.net core minimal api. It provides more structured way to organize endpoint and simplify creation of http request handler. Ref. slide 155.
- added CreateRecordRequest and CreateRecordResponse objects in "CreateProductEndpoint" class and also reference of Carter library into building block.

### 51,52: Develop POST endpoint with Carter implements ICarterModule with minimal API

- Open CreateProductEndpoint and inherit ICaterModule interface.
- Implement "AddRoute" method in CreateProductEndpoint.
- Map request to create product command to pass to command handler. Use MediatR library and trigger MideateR command handler.
- In order to pass values from request to command object need mapping object and this is done using mapping library "Mapster".
- Mapster - is high performance and flexible mapping library that map objects. 
- Mapster is used to map request object to command object and response to result object.
- All common libraries and generic implementations are added to the building Block class library.
- Add "Mapster" library into Building block project under common class libraries.
- Moved some of repeated used nuget reference to global using. Namespace used many places are moved to "GlobalUsing" class file.
- Following code block added to product end point.

    ```c#
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async(CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        throw new NotImplementedException();
    }
    ```

### 53: Register MediatR, Carter and Mapster libraries to ASP.net dependency injection services

- Open program.cs file and add dependency injection services and configre the http request piepline.
- 

## 99: Troubleshooting
