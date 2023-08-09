

# Project Template


Here we have some code to generate Client SDK and use a mechanism for handling errors.
I should mention, if you're seeing, that they are not following the Clean Architecture approach, so feel free to change everything with your own aspects.


# SimpleApi
If you take a look at the project, you did see a project with the name `SimpleApi`.
As a matter of fact, it is not really a project you need. It is just a sample to see how our error handling and Client SDK are working.


If you open it up, you will see an endpoint that is connected to a request named `GetInfoRequest`. It is a simple calculation of age to get your birth year and say how old you are.


# Error handling
Actually, `ErrorHandling` is big concern to introduce. but I will explain some concepts:


## HandlerCode
If you open the `HandlerCode` you can see we have an enum with the following structure:
```
public enum HandlerCode
{
    GetInfo = 11_999
}
```


Each handler code contains 2 parts.
- Area name (accounting, payment, etc)
- Handler number: it will start from 999 decress to down


And if you open `GetInfoRequest` you can see it mentioned using an attribute named `HandlerCode`


```
[HandlerCode(HandlerCode.GetInfo)]
public record GetInfoRequest(int BirthYear) : IRequest<Response<GetInfoDto>>;
```


## Error Code
If you open the `GetInfoErrorCodes` you can see we have an enum as following:


```
public enum GetInfoErrorCodes
{
    [ErrorType(BackendErrorType.BusinessLogic)]
    AgeIsLessThan20 = 11_999_101
}
```



In the first part, `Enum` for each handler, you should have an enum that starts with that handler's name and ends with `ErrorCodes`


Each error code contain 3 part like this 00_000_000


First 5 numbers comming from error `HandlerCode` and last 3 number is your error number so it is start from 101 and incress desending.


## Common Error Code
Some of the errors should handle in each handler call, such as identity or session expireation.
If you remember, I told you error codes contain 3 parts. The first two parts are determined by the handler and the area, and the final three parts can be filled using Common Error Codes.
Plase open the `CommonErrorCode`. You can have 3 ranges of error.
- 600: ApplicationFailure
- 700: Database errors
- 800: Errors that relate to client


## Pipeline behaviors
In the previous session, I told we some common errors. you can use pipeline behaviors to catch up on the common errors.
I think better to open up one sample we created named `CommonErrorsPipelineBehavior` to learn how some common error codes like 804 will work.


Remember, many of the common error codes are not captured anywhere; they are specific codes that relate to one of our projects. and I don't remove it to let you know you can implement it for your project.


### Validation and throw error


Please open `GetInfoValidation` you can see we have rule that telling you should input the year that grater than 1990.
In the `CommonErrorsPipelineBehavior` you can see we have a mechanism that maps the requests to a validation file, then it will do validation and throw an error code if it is an invalid request.


But in our handler, I also throw an error when the age is less than 20.


```
if (age < 20)
{
      return Task.FromResult<Response<GetInfoDto>>(GetInfoErrorCodes.AgeIsLessThan20);
}
```
So here you can see the usage of a specific error code.


# To Do
- Please implement the `IpService` and add its dependencies.
  - We have some interceptors in `Infrastructure.Interceptors`, so you need to configure them in your `DbContext`.
- In the `Infrastructure` layer, I did install `Pomelo.EntityFrameworkCore.MySql` because i need `Microsoft.EntityFrameworkCore.Diagnostics.DbConnectionInterceptor` and it's just exist in a relational implementation for EF, Please remove them if you do not need them and install the right package.
  - Please 