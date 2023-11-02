﻿namespace TownOfZuul
{
    public class Game
    {
        private Location? currentLocation;
        private readonly Stack<Location> previousLocations = new();
        int monthCounter = 1;
        int initialPopulation;
        double populationHealth;
        int foodStock;
        public Game()
        {
            CreateLocations();
            AdvanceMonth();
            monthCounter = 0;
            initialPopulation = 300;
            populationHealth = 30.0;
            foodStock = 10000;
        }

        private void CreateLocations()
        {
            Village? village = new();
            ElderHouse? elderHouse = new();
            Docks? docks = new();
            ResearchVessel? researchVessel = new();
            Ocean? ocean = new();
            Coast? coast = new();
            WastePlant? wastePlant = new();

            village.SetExits(null, docks, coast, elderHouse); // North, East, South, West

            docks.SetExits(researchVessel, ocean, null, village);

            elderHouse.SetExit("east", village);

            researchVessel.SetExit("south", docks);

            ocean.SetExit("west", docks);

            coast.SetExits(village, null, wastePlant, null);

            wastePlant.SetExit("north", coast);

            currentLocation = village;
        }

        public void Play()
        {
            Parser parser = new();

            PrintWelcome();

            bool continuePlaying = true;
            while (continuePlaying)
            {
                Console.WriteLine(currentLocation?.Name);
                Console.Write("> ");

                string? input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a command.");
                    continue;
                }

                Command? command = parser.GetCommand(input);

                if (command == null)
                {
                    Console.WriteLine("I don't know that command.");
                    continue;
                }

                switch(command.Name)
                {
                    case "look":
                        Console.WriteLine(currentLocation?.Description);
                        break;

                    case "back":
                        if (previousLocations.Count == 0)
                            Console.WriteLine("You can't go back from here!");
                        else
                        {
                            currentLocation = previousLocations.First();
                            previousLocations.Pop();
                        }
                        break;

                    case "north":
                    case "south":
                    case "east":
                    case "west":
                        Move(command.Name);
                        break;

                    case "quit":
                        continuePlaying = false;
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    case "talk":
                        Console.WriteLine(currentLocation?.Dialogue);
                        break;

                    case "info":
                        Console.WriteLine(currentLocation?.Information);
                        break;
                    case "sleep":
                        AdvanceMonth();
                        Console.WriteLine($"Your village advances to the {monthCounter} month. Your population health: {populationHealth}%.");
                        Console.WriteLine($"Your villages population: {initialPopulation}. Food stock:{foodStock}");
                        Console.WriteLine();
                        break;

                    default:
                        Console.WriteLine("I don't know what command");
                        break;
                }
            }

            Console.Clear();
        }

        private void Move(string direction)
        {
            if (currentLocation?.Exits.ContainsKey(direction) == true)
            {
                previousLocations.Push(currentLocation);
                currentLocation = currentLocation?.Exits[direction];
            }
            else
            {
                Console.WriteLine($"You can't go {direction}!");
            }
        }

        public void AdvanceMonth()
        {
            monthCounter++;

            //for now I made everything random just trying to make stuff I will improve the system as we go on.
            //if you feel like this way is not to good let me know how to improve it :)

            Random random = new Random();

            for (int monthCounter = 1; monthCounter <= 12; monthCounter++)
            {
                int populationChange = random.Next(-10, 11); // for now its ranndom change in population

                initialPopulation += populationChange;
                if (initialPopulation < 0)
                {
                    initialPopulation = 0;
                }

                double healthChange = random.Next(-5, 6); //for now its random change in population health

                populationHealth += healthChange;
                if (populationHealth < 0)
                {
                    populationHealth = 0;
                }
                else if (populationHealth > 100)
                {
                    populationHealth = 100;
                }

                int foodConsumption = initialPopulation * 30;

                int minValue = 5000;
                int maxValue = 10000;

                int foodStockChange = random.Next(minValue, maxValue);

                foodStock += foodStockChange;

                if (foodStock >= foodConsumption)
                {
                    foodStock -= foodConsumption;
                }
                else
                {
                    foodStock -= foodConsumption;
                }
            }
        }


        private static void PrintWelcome()
        {
            Console.WriteLine("Welcome to the Town Of Zuul!");
            Console.WriteLine("Town Of Zuul is a new, incredibly boring adventure game.");
            PrintHelp();
            Console.WriteLine();
        }

        private static void PrintHelp()
        {
            Console.WriteLine("You are lost. You are alone. You wander");
            Console.WriteLine("around the island.");
            Console.WriteLine();
            Console.WriteLine("Navigate by typing 'north', 'south', 'east', or 'west'.");
            Console.WriteLine("Type 'look' for more details.");
            Console.WriteLine("Type 'back' to go to the previous location.");
            Console.WriteLine("Type 'help' to print this message again.");
            Console.WriteLine("Type 'quit' to exit the game.");
            Console.WriteLine("Type 'talk' to have an interaction with NPC.");
            Console.WriteLine("Type 'info' to get more information from your current location.");
            Console.WriteLine("Type 'sleep' to advance to the next month.");
        }
    }
}
