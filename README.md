# GridWorld Pathfinding Simulation

## Overview
This project simulates a character navigating through a grid world. The character uses a heuristic function that combined with randomness to choose the most probable next move towards a predefined goal. It also incorporates a mechanism to recognize and avoid making invalid moves. The simulation is visualized in the console.

## Features
* Heuristic based pathfinding using Manhattan Distance.
* Handling invalid moves by reducing their probability in future iterations.
* Simple console visualization of the map and character movement.
* Configuration loading from an external text file.
* Dead-end detection and backtracking.

## Usage
1. Configure the map, start position, and goal position in the map_config.txt file.
2. Run the program.
3. Observe the character's movements towards the goal on the console.
4. To exit the program, press space; to continue running and launch a new simulation, press any key other than space.
   
## Project Structure
* Program.cs - Entry point for the application. Handles main simulation loop.
* IAgent.cs - Interface serves as a blueprint for agent behavior in the simulation. It can be implemented by another class with a performant AI or machine learning agent.
* Character.cs - Defines the logic for character movement and decision-making.
* Vector2.cs - Utility class for storing grid coordinates and calculating distances.
* ConfigLoader.cs - Handles loading of configuration from external files.
* Configuration.cs - Data class for storing loaded configuration.

## License
This project is open-source and available under the MIT License.
