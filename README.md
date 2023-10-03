# BallastLane-TechnicalExercise

## Objective
The goal of this project is to build an exemplary project with the specifications provided on email.

## Compose sample application: ASP.NET with MS SQL server database
```
.
├── Presentation
│   ├── BallastLane.Presentation.WebAPI
├── Products
│   ├──Core
│   │   ├── BallastLane.Products.Application
│   │   ├── BallastLane.Products.Domain
|   └── Infraestructure
│   │   ├── BallastLane.Products.Infra.DataAccess.SQLServerADO
├── Security
│   ├──Core
│   │   ├── BallastLane.Security.Application
│   │   ├── BallastLane.Security.Domain
|   └── Infraestructure
│   │   ├── BallastLane.Security.Infra.DataAccess.SQLServerADO
└── DBScripts
    ├── data base scripts
```

### DBScripts
The database scripts contains the entire structure for creating database tables, stored procedures, and performing the initial data load.

### The code
This project's structure follows the principles of Clean Architecture, aiming to create a well-organized and maintainable system by clearly separating responsibilities. The architecture is designed with concentric layers, each serving a specific purpose, and dependency flow moving from the core to the periphery. I divided the project into DDD (Domain data driven), and we have two domains: Products and Security. Below is a breakdown of the project structure in line with these principles.

#### Domain projects 
Here we have these two projects: BallastLane.Security.Domain and BallastLane.Products.Domain.
This is the heart of the application. It houses the entities and business rules. It stands independently from other layers, avoiding dependencies on them. In this layer, domain models are defined, along with the main operations the application is designed to perform.

#### Application projects
Here we have these two projects: BallastLane.Security.Application and BallastLane.Products.Application.
Focused on application use cases, this layer orchestrates external actions. It relies on the domain layer and applies business rules to address user needs. Each use case can leverage domain services to achieve its objectives.

#### Infra.DataAccess.SQLServerADO projects
Here we have these two projects: BallastLane.Products.Infra.DataAccess.SQLServerADO and BallastLane.Security.Infra.DataAccess.SQLServerADO.
Implementations of Repository interfaces defined in the domain and use cases reside here. This layer handles technical aspects like database access implemented with ADO.Net. While it relies on both domain and use case layer, they remain independent of this layer.

####BallastLane.Presentation.WebAPI
The outermost layer interacts with the external world, handling HTTP requests. This layer depends on the use cases and infrastructure layers to execute its tasks.

### Tests
NUnit was used as the testing framework. Each layer has its own set of unit tests. The tests included in the "*.Infra.DataAccess.SQLServerADO.UnitTests" project are integration tests for the database access layer with the actual database. Therefore, it's necessary for the database to be up and running for these tests to be executed.

## Getting Started

This project was developed using Visual Studio 2022. The steps to initiate the project were set up to run in this environment.
To get started with the project, follow these steps:

1. Clone this repository.
2. Download and Install the SQL server or create a docker with the SQL server
3. Build and publish database scrips to SQL server
4. Set the startup project to "BallastLane.Presentation.WebAPI"
5. Adjust the SQL server connection string on "BallastLane.Presentation.WebAPI" "appsettings.json"
6. Run [f5] to start the application.

Visual Studio will launch the project's Swagger UI, where you can execute API functions through the "Try out" feature.

To test all functions, it's necessary to first use the "Login" function and then utilize the bearer token. The GetAllEsers function does not require login.

## User history for this project

### Create a New Product
#### Description:
As a product manager, I want to create a new product in the system so that I can add information about the products we are selling.
#### Acceptance Criteria:
1. I should be able to access the product creation functionality in the system.
2. When creating a product, I must provide the following information:
- Product name (mandatory).
- Product price (mandatory and must be a valid number).
- Product description (optional).
- After successfully creating a product, the system should display a confirmation message.
3. If I attempt to create a product without providing valid information, the system should display an error message.
4. The system should record the date and time of product creation.

### Update a Product
#### Description:
As a product manager, I want to update an existing product in the system so that I can modify information about our products when needed.
#### Acceptance Criteria:
1. I should be able to access the product update functionality in the system.
2. When updating a product, I must provide the following information:
- Product ID (mandatory, indicating which product to update).
- Updated product name (optional).
- Updated product price (optional and must be a valid number).
- Updated product description (optional).
3. After successfully updating a product, the system should display a confirmation message.
4. If I attempt to update a product without providing valid information or with an invalid product ID, the system should display an error message.
5. The system should record the date and time of the product update.

###  Delete a Product
#### Description:
As a product manager, I want to delete an existing product from the system so that I can remove products that are no longer available or relevant.
#### Acceptance Criteria:
1. I should be able to access the product deletion functionality in the system.
2. When deleting a product, I must provide the following information:
- Product ID (mandatory, indicating which product to delete).
3. After successfully deleting a product, the system should display a confirmation message.
4. If I attempt to delete a product with an invalid product ID or a product that doesn't exist, the system should display an error message.
5. The system should record the date and time of the product deletion.

### Retrieve All Products
#### Description:
As a user, I want to retrieve a list of all available products from the system so that I can view the complete catalog of products that the system offers.
#### Acceptance Criteria:
1. I should be able to access the product retrieval functionality in the system.
2. The system should provide an option to filter the list of products by:
- Product name (optional).
3. The system should allow pagination to retrieve a limited number of products per page (e.g., 10 products per page).
4. The system should return a list of products based on the applied filters and pagination.
5. The retrieved product list should include the following information for each product:
- Product ID.
- Product name.
- Product price.
- Product description.

### Retrieve Product by ID
#### Description:
As a user, I want to retrieve detailed information about a specific product from the system by providing its unique product ID. This will allow me to view specific details about a product.
#### Acceptance Criteria:
1. I should be able to access the product retrieval functionality in the system.
2. When retrieving a product, I must provide the following information:
- Product ID (mandatory, indicating which product to retrieve).
3. The system should return detailed information about the specified product, including:
- Product name.
- Product price.
- Product description.
4. If the specified product ID is not found or is invalid, the system should display an error message.
