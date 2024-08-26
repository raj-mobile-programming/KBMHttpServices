**Introduction**
This assessment contains two projects one is KBMGrpcService and another one is KBMHttpService. These projects are developed on .Net Core 6.0 framework. 
KBMGrpcService: This is a GRPC project which has the services for Organization and Users. There are all CRUD operations included in the GRPC project. It includes Unit tests and integration tests.
KBMHttpService: This is a WebAPI project, which is consuming KBMGrpcService endpoints. This project includes all CRUD oprations of Organization and Users including association and disssociation of users with the organizations. 

**Project Setup**

1.Update the port number for https on which KBMGrpcService is running in KBMHttpService.Program.cs file.

2. Now run KBMHttpService project.

3. Now you are ready to use the endpoints for Organization and for Users for KBMHttpService

4. To run Unit Tests and Integration Tests
In visual studio Click>Tests > Test Explorer > Run All Tests

**Project Structure**

── KBMHttpService
   ├── Controllers
   │   ├── OrganizationsController.cs
   │   └── UsersController.cs
   ├── Models
   │   ├── OrganizationRequestModel.cs
   │   ├── UserRequestModel.cs
   │   └── (Response models)
   ├── Tests
   │   ├── UnitTests
   │   │   ├── OrganizationsControllerTests.cs
   │   │   └── UsersControllerTests.cs
   │   └── IntegrationTests
   │       ├── OrganizationsControllerIntegrationTests.cs
   │       └── UsersControllerIntegrationTests.cs
   └── Program.cs

