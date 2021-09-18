# NotificationSystem

## This is my API implementation of a notification system that allows users to subscribe to supervisors, and receive notifications from them.

A few quick notes:

1. Security. I designed a system where when user creates account or logs in, they will receive a 1 hour Token. This token is checked and serves as an API key to access the different services.

2. Twilio integration. Used for sending the notifications, and injected into the controllers as a service.

3. I have no used Asp.net in a while, and wanted to do something with it again. For these types of challenges, should probalby specify the tech stack.

4. I am not currently enforcing rules for the hashed passwords, and the key generator is just a quick random alphanumerical generator that is not secure.

## Visual Studio 2019 Setup

This repo is for a Visual Studio 2019 solution. To install and run it requires installing Visual Studio 2019.

## In NuGet package manager, please install these packages:

1. Microsoft Entity Framework for Object-Database Mapper.
  
  1a. Microsoft.EntityFrameworkCore
  
  1b. Microsoft.EntityFrameworkCore.Design
  
  1c. Microsoft.EntityFrameworkCore.SqlServer
  
  1d. Microsoft.EntityFrameworkCore.Tools

2. Twilio
