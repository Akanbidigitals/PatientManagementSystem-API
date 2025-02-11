# Patient Management System API
The Patient Management System API is a robust and scalable solution designed to manage patient information efficiently. This API provides endpoints to create, retrieve, update, and delete patient records, ensuring seamless integration with healthcare systems. Built with a focus on clean architecture, validation, and extensibility, this project is ideal for managing patient data in a secure and organized manner.

## Features
### 1. Patient Management:

* Create Patient: Add new patient records with detailed information such as name, email, date of birth, phone number, and address.

* Retrieve Patients: Fetch all patients or a specific patient by their unique ID.

* Update Patient: Modify existing patient records.

* Delete Patient: Soft delete patient records (mark as deleted without permanent removal).

### 2. Validation:
   *  Ensures data integrity with robust validation rules for patient information.

* Validates email format, phone number length, and date of birth format (YYYY-MM-DD).

   * Prevents future dates from being used as dates of birth.

### 3. Soft Delete:

* Patients are not permanently deleted but marked as IsDeleted = true for data retention and audit purposes.

###  4. Extensible Design:

* Built with clean architecture principles, making it easy to extend or modify functionality.

* Uses dependency injection for repository patterns, ensuring testability and maintainability.

### 5. Error Handling:

* Proper error responses for invalid requests or missing data.

* Custom Error-Exception methods was created for `NotFoundException()` and `AlreadyExistsException()`.

* Uses `ErroHandlingMiddleware.cs` in the middleware folder to register the above error exceptions.

* Registered the error handling middleware in the `program.cs` class as a Custom middleware in our request and response pipeline.

### 6. Logging Injection:

* Each repository class receives an instance of ILogger<T> via dependency injection, where T is the repository class.


### 7. RESTful API:

* Follows REST conventions for clear and predictable endpoints.

* Supports JSON for request and response payloads. 




## Technologies Used
* .NET: Core framework for building the API.

* FluentValidation: For robust and customizable validation rules.

* RESTful Principles: For designing clean and intuitive API endpoints.

* Dependency Injection: For managing dependencies and promoting testability.

* Serilog: For logging configuration

* Repository Pattern: For abstracting data access logic.

## PatienceController
### **1. Create a Patient** 
**Endpoint: `POST /api/patients`**
```json
 {
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "dateOfBirth": "1990-01-01",
    "phoneNumber": "12345678901",
    "address": "123 Main St"
}

```
### Success Response
*  **StatusCode:** `200 OK`
*  **Response Body:**
```json
{
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "dateOfBirth": "1990-01-01",
    "phoneNumber": "12345678901",
    "address": "123 Main St",
    "registeredAt": "01-10-2023",
    "isDeleted": false,
    "patientRecords": []
}
```
### Errors Responses
 * **Validation Error:**
   *  **StatusCode:** `400 Bad Request`
   *  **Response Body:**
   ```json
   {
    "errors": {
        "firstName": ["First name is required."],
        "email": ["A valid email address is required."]
    }
    } 
    ```
### Server Error
 * Success Response
   *  **StatusCode:** `500 Internal Server Error`
   *  **Response Body:**
   ```json
   {
    "message": "An error occurred while processing your request."
    }
    ```
---
### **2. Get All Patients**

**Endpoint: `GET /api/patients`**
### Success Response
*  **StatusCode:** `200 OK`
 *  **Response Body:**
```json
[
    {
        "id": 1,
        "firstName": "John",
        "lastName": "Doe",
        "email": "john.doe@example.com",
        "dateOfBirth": "1990-01-01",
        "phoneNumber": "12345678901",
        "address": "123 Main St",
        "registeredAt": "01-10-2023",
        "isDeleted": false,
        "patientRecords": []
    },
    {
        "id": 2,
        "firstName": "Jane",
        "lastName": "Smith",
        "email": "jane.smith@example.com",
        "dateOfBirth": "1985-05-15",
        "phoneNumber": "09876543210",
        "address": "456 Elm St",
        "registeredAt": "01-10-2023",
        "isDeleted": false,
        "patientRecords": []
    }
]
```
### Errors Responses
 * **No Patient Found:**
   *  **StatusCode:** `400 Bad Request`
    *  **Response Body:**
    ```json 
    {
    "message": "No patients found."
    }
    ```
    ---
### **3. Get Patient by ID**

**Endpoint: `GET /api/patients`**
### Success Response
*  **StatusCode:** `200 OK`
 *  **Response Body:**
 ```json
 {
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "dateOfBirth": "1990-01-01",
    "phoneNumber": "12345678901",
    "address": "123 Main St",
    "registeredAt": "01-10-2023",
    "isDeleted": false,
    "patientRecords": []
}
```
### Errors Responses
 * **Patient Not Found:**
   *  **StatusCode:** `404 Not Found`
    *  **Response Body:**
    ```json
    {
    "message": "Patient with ID 1 not found."
    }
    ```
    ---
### **4. Update Patient by ID**

**Endpoint: `PACTH /api/patients`**
* Request Body
```json
{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "dateOfBirth": "1990-01-01",
    "phoneNumber": "12345678901",
    "address": "123 Main St"
}
```
### Success Response
*  **StatusCode:** `200 OK`
 *  **Response Body:**
 ```json
 {
    "id": 1,
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "dateOfBirth": "1990-01-01",
    "phoneNumber": "12345678901",
    "address": "123 Main St",
    "updatedAt": "02-10-2023",
    "registeredAt": "01-10-2023",
    "isDeleted": false,
    "patientRecords": []
}
```
### Errors Responses
 * **Patient Not Found:**
   *  **StatusCode:** `404 Not Found`
    *  **Response Body:**
    ```json
    {
    "message": "Patient with ID 1 not found."
    }
    ```
 * **Validation Error:**
   *  **StatusCode:** `400 Bad Request`
   *  **Response Body:**
   ```json  
   {
    "errors": {
        "firstName": ["First name is required."],
        "email": ["A valid email address is required."]
              }
    } 
    ```
    ---
### **4. Delete Patient by ID**

**Endpoint: `DELETE /api/patients`**
#### **Success Response**
  * **Status Code: `204 No Content`**
  * **Response Body:** None

#### **Error Responses**
* **Patient Not Found:**
  * **Status Code: `404 Not Found`**
  * **Response Body:**
  ```c#
  {
    "message": "Patient with ID 1 not found."
  }
  ```

## PatientRecordController 

### **1. Create a Patient Record** 
**Endpoint: `POST /api/patients/{patientId}/records`** 
* **Request Body:**
```json
{
    "diagnosis": "Common Cold",
    "treatment": "Rest and hydration"
} 
```
### Success Response
* **Status Code:** `200 OK`

* **Response Body:**
```json
{
    "id": 1,
    "diagnosis": "Common Cold",
    "treatment": "Rest and hydration",
    "createdAt": "02-10-2023",
    "patientId": 1
}
``` 
###  Errors Responses
 * **Patient Not Found:**
   *  **StatusCode:** `404 Not Found`
    *  **Response Body:**
    ```json
    {
    "message": "Patient with ID 1 not found."
    }
    ```
    ### Errors Responses
 * **Validation Error:**
   *  **StatusCode:** `400 Bad Request`
   *  **Response Body:**
   ```json
   {
    "errors": {
        "diagnosis": ["Diagnosis is required."]
              }
   }
   ```
   ---
   ### **2. Get All Records for a Patient** 
**Endpoint: `GET /api/patients/{patientId}/records`** 
### Success Response
* **Status: `200 OK`**
* **Response Body:**
```json
[
    {
        "id": 1,
        "diagnosis": "Common Cold",
        "treatment": "Rest and hydration",
        "createdAt": "02-10-2023",
        "patientId": 1
    }
]
```
   ### Errors Responses
 * **No Records Found:**
   *  **StatusCode:** `400 Bad Request`
   *  **Response Body:**
   ```json
   {
    "message": "No records found for patient with ID 1."
   }
   ```
    ---
### **2. Update a Patient Record** 
**Endpoint: `PUT /api/patients/{patientId}/records/{recordId}`** 
* **Response Body:**
```json 
{
    "diagnosis": "Flu",
    "treatment": "Antiviral medication"
}
```
### Success Response
* **Status: `200 OK`**
* **Response Body:**
```json 
{
    "id": 1,
    "diagnosis": "Flu",
    "treatment": "Antiviral medication",
    "updatedAt": "02-10-2023",
    "createdAt": "02-10-2023",
    "patientId": 1
}
```
   ### Errors Responses
 * **No Records Found:**
   *  **StatusCode:** `400 Bad Request`
   *  **Response Body:**
   ```json
   {
    "message": "No records found for patient with ID 1."
   }
   ```


# Docker Setup
The API is containerized using Docker for easy deployment and consistency across environments. Below are the steps to set up and run the API using Docker.

#### Dockerfile
The Dockerfile defines the environment and steps to build and run the API.
```Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-nanoserver-1809 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PatientManagementSystem-API.csproj", "."]
RUN dotnet restore "./PatientManagementSystem-API.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./PatientManagementSystem-API.csproj" -c %BUILD_CONFIGURATION% -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientManagementSystem-API.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientManagementSystem-API.dll"]
```

## Steps to Run with Docker

### 1. Building and Running the Docker Container
To build and run the API using Docker, follow these steps:

#### **Build the Docker Image**
```sh
docker build -t patient-management-api .
```

#### **Run the Container**
```bash
docker run -d -p 8080:8080 --name patient-api patient-management-api
```

### 2. Debugging with Visual Studio
1. Open **Visual Studio** and set **Docker** as the startup project.
2. Press `F5` to start debugging inside the container.
3. Set breakpoints to debug API requests.

### 3. Checking Running Containers
To list running containers:
```sh
docker ps
```

To remove all stopped containers and unused images:
```sh
docker system prune -a
```


# Design Thought Process
#### Clean Architecture
The API is designed using Clean Architecture, which emphasizes separation of concerns and independence of layers. The key principles followed are:

#### 1. **Domain Layer:**

* Contains the core business logic and entities (e.g., Patient, PatientRecord).

* Independent of external frameworks or databases.

#### 2. **Application Layer:**

* Implements use cases and application-specific logic.

* Defines interfaces for repositories and validators.

#### 3. **Infrastructure Layer:**
* Implements repository interfaces and interacts with the database.

* Handles external dependencies like logging and validation.

#### 3. **Presentation Layer:**
* Contains controllers and DTOs for handling HTTP requests and responses.

* Validates input and maps data to domain models.



# Contact Information

#### Name: **Munirudeen Akanbi**

#### Email: **Adsdesigntech@gmail.com**

#### GitHub: https://github.com/Akanbidigitals?tab=repositories
