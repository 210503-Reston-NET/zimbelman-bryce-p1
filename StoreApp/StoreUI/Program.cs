using System;
using Serilog;
namespace StoreUI
{
    class Program
    {
        /// <summary>
        /// This is the main method, its the starting point of your application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("../logs/mochamomentlog.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            //call method that starts main user interface
            MenuFactory.GetMenu("main").Start();
        }
    }
}
