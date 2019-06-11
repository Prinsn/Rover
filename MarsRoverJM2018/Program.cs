using MarsRoverJM2018.Framework;
using MarsRoverJM2018.RoverController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018
{
    class Program
    {
        /// <summary>
        /// Conduct rover mission from instructions
        /// </summary>
        /// <param name="args">Either an array of commands, or semicolon delimited as a single string</param>
        static void Main(string[] args)
        {
            var results = MissionSimulator.Simulate(new []{ args.ParseMissionData()});
            //Keep console window open
            Console.ReadLine();
        }
    }
}
