# Financial Management (Back-End)  
REST Api developed for assist in personal financial management, offering features suh as creating financial targets, entering expenses and revenues.

## Running locally with Docker
- In the ./FinancialManagement/docker, type the following command:
``` docker compose up -d ```
Wait for docker to upload all the dependencies and generate the tables in the database

### Using the project ###

- In browser, enter the following URL for open swagger interface
  (http://localhost:8080/swagger)
  
- In terminal, use the curl in some of the API routess
  (curl http://localhost:8080/"route")
  
  **That's why you need to create an account and pass the JWT token to use the other features** 

- For stop executing the project
  ```docker compose down ```
- For stop and delete the images docker genereated, use
  ``` docker compose down --rmi all ```
  
## Technologies
- Stack .NET (C#, Asp.NET, Entity Framework), Postgres for database and Docker for infrastructure

## API Endpoints
  Method  |       Route                Description
:------------:|:---------------------:|:---------------------------------------------------------------
  **POST**    |       /accounts       | Create account user 
  **POST**    |       /sign-in        | Sign-in user, generate token and refresh token
  **POST**    |   /refresh-token      | Use refresh token, for to maintain the user session
  **GET**     |    /revenue{id}       | Get revunue by Id
  **GET**     |    /revenues          | Get all revenues
 **POST**     |     /revenue          | Create a new revenue
  **PUT**     |    /revunue{id}       | Update a revenue by Id 
**DELETE**    |     /revenue{id}      | Delete a revenue by Id
 **GET**      |     /expense{id}      | Get expense by Id
 **GET**      |    /expenses          | Get all expenses
 **POST**     |    /expense           | Create a new expense
 **PUT**      |   /expense{id}        | Update a expense by Id
 **DELETE**   |   /expense{id}        | Delete a expense by Id
 **GET**      | /category-expense{id} | Get category by Id
 **GET**      | /categorys-expense    | Get all categorys
 **POST**     | /category-expense     | Create a new category
 **PUT**      | /category-expense{id} | Update a category
 **DELETE**   | /category-expense{id} | Delete a category
 **GET**      | /financial-target{id} | Get target by Id
 **GET**      |  /financial-targets   | Get all targets
 **POST**     |  /financial-target    | Create a new target
 **PUT**      | /financial-target{id} | Update target by Id
 **DELETE**   | /financial-target{id} | Delete target by Id


 
