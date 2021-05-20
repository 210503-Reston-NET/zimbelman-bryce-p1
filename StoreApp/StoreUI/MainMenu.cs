using System;
using System.Collections.Generic;
using StoreModels;
using StoreBL;
using StoreDL;
using Serilog;
namespace StoreUI
{
    public class MainMenu : IMenu
    {
        private IMenu submenu;
        public void Start() {
            bool repeat = true;

            do
            {
                Console.WriteLine("Welcome to Mocha Moment!");
                Console.WriteLine("Are you a customer or employee?");
                Console.WriteLine("[1] Customer");
                Console.WriteLine("[2] Manager");
                Console.WriteLine("[0] Exit");

                // Receives input from user
                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        // Exit
                        Log.Information("Exit Program");
                        Console.WriteLine("Goodbye!");
                        repeat = false;
                        break;

                    case "1":
                        Log.Information("Customer Menu Selected");
                        submenu = MenuFactory.GetMenu("customer");
                        submenu.Start();
                        break;
                    
                    case "2":
                        Log.Information("Manager Menu Selected");
                        submenu = MenuFactory.GetMenu("manager");
                        submenu.Start();
                        break;

                    default:
                    // Invalid Input
                    Console.WriteLine("Please input a valid option");
                    break;
                }
            } while (repeat);
        }
    }
}