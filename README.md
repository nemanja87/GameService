# SimpleGame Microservices

## Description

This repository contains three microservices for a simple game built using .NET. Each microservice performs a specific role in the system, and they communicate via HTTP.

### Microservices:

1. **GameService**: Handles the core game logic (e.g., Rock-Paper-Scissors) and interacts with the other services.
2. **ComputerService**: Provides the computer's choice in the game.
3. **ScoreboardService**: Manages the game results, keeping track of the last 10 games.

## Table of Contents

1. [Technologies Used](#technologies-used)
2. [Microservices](#microservices)
3. [Prerequisites](#prerequisites)
4. [Running the Project Locally](#running-the-project-locally)
5. [Testing](#testing)
6. [Contributing](#contributing)
7. [License](#license)

## Technologies Used

- **.NET 8**
- **MediatR** for CQRS implementation
- **Moq** for unit testing
- **Swagger** for API documentation
- **Docker** for containerization
- **xUnit** for unit tests

## Microservices

### GameService
The `GameService` is the core service that allows players to play the game by comparing their choice to the computer's choice. The game logic is implemented using `IGameLogicService`, and the service also interacts with `ComputerService` to get the computer's move and `ScoreboardService` to record the game result.

### ComputerService
The `ComputerService` randomly selects a game move (e.g., Rock, Paper, Scissors, etc.) for the computer. It provides this choice to the `GameService` whenever a game is played.

### ScoreboardService
The `ScoreboardService` manages the game results. It keeps track of the last 10 games and stores the results in memory (future work can include adding persistent storage).

## Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/) (if you plan to run the services inside containers)

## Running the Project Locally

### Step 1: Clone the Repository
``bash
git clone https://github.com/nemanja87/SimpleGame.git
cd SimpleGame

### Step 2:Solution setup
Make sure that all three services are up and running.
Adjust appSettings with the proper localhost values if needed.
Use `GameService` and its `play` endpoint to initiate the game. (this service makes http calls to both `Computer` and `Scoreboard` services)
Results of 10 last games should be visible when calling `last-results` endpoint of `ScoreboardService`