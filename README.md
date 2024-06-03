# Project Title

## Introduction

This project is an ASP.NET Core Web API that fetches and returns top stories from the Hacker News API.

## Prerequisites

- .NET 8.0 
- Visual Studio 2022

## Installation

1. Clone the repository
2. Open the solution in Visual Studio
3. Restore the NuGet packages

## Running the Application

1. Set the `HackerNews.Api` project as the startup project
2. Press `F5` to start debugging the application
3. Navigate to `https://localhost:<port>/api/HackerNews/TopStories?numberOfStories=<number>` in your web browser or Postman to fetch the top stories from the Hacker News API
4. or Navigate to '`https://localhost:<port>/swagger/index.html' and use swagger ui.

## Assumptions

- The Hacker News API is always available and returns data in the expected format

## Enhancements

- Add error handling for when the Hacker News API is unavailable or returns an error
- Add more sophisticated caching and API Keys.
## Built With

- ASP.NET Core 8.0
- Visual Studio 2022



