# NotificationSystem

## This is my API implementation of a notification system that allows users to subscribe to supervisors, and receive notifications from them.

A few quick notes:

1. Security. I designed a system where when user creates account or logs in, they will receive a 1 hour Token. This token is checked and serves as an API key to access the different services.

2. Twilio integration. Used for sending the notifications, and injected into the controllers as a service.

3. I have no used Asp.net in a while, and wanted to do something with it again. For these types of challenges, should probalby specify the tech stack.

4. I am not currently enforcing rules for the hashed passwords, and the key generator is just a quick random alphanumerical generator that is not secure.

# Visual Studio 2019 Setup

This repo is for a Visual Studio 2019 solution. To install and run it requires installing Visual Studio 2019.

The API will run locally through IIS Express

Install Visual Studio 2019.

Open the Solution.

Run it.

## In Visual Studio's NuGet package manager, please install these packages:

1. Microsoft Entity Framework for Object-Database Mapper.
  
  1a. Microsoft.EntityFrameworkCore
  
  1b. Microsoft.EntityFrameworkCore.Design
  
  1c. Microsoft.EntityFrameworkCore.SqlServer
  
  1d. Microsoft.EntityFrameworkCore.Tools

2. Twilio

## Database setup

Update "sqlConnection" value in appsettings.json. By default I am using the built in mssqllocaldb. Could change it to sqllite, postgresql, etc.

Do database migration by running "Update-Database" in Package Manager Console.

## Secrets.cs setup

There should be a Secrets.cs file to hold API keys and other sensitive information.

It should have a Secrets class, with these three fields for the information needed for Twilio.

public static readonly string TWILIO_API_KEY = <key>;

public static readonly string TWILIO_SID = <SID>;
  
public static readonly string TWILIO_NUMBER = "+1xxxyyyzzzz";
  
## The API itself

On running IIS express (and fixing errors with certificates if needed), it should open up a Swagger UI page. The API can be interacted with this way.

I have also included Postman file to run it from there. Need to select disable SSH to work with IIS.
  
Workflow:
  
1. Create User and Supervisors. Save the Username and Tokens somewhere. Use real phone numbers or comment out the Twilio part.
  
2. Get the supervisors, save the supervisor UserIds for use later.
  
3. Subscribe to supervisor with UserName, Token, and supervisor's UserId.
  
4. Have a supervisor send a message by filling in UserName, Token, and message.
  
5. Receive SMS notifications through Twilio.
