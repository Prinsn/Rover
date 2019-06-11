using MarsRoverJM2018.Framework;
using MarsRoverJM2018.RoverController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018
{
    public static class MissionSimulator
    {
        /// <summary>
        /// Conduct rover mission from instructions
        /// </summary>
        /// <param name="args">Either an array of commands, or semicolon delimited as a single string</param>
        public static string[] ParseMissionData(this string[] args)
        {
            //No input, handle direct input
            if (args.Length == 0)
            {
                Console.WriteLine("No input detected, please supply full mission command, semicolon (;) delimited.");
                var userInputString = Console.ReadLine();
                args = new[] { userInputString };
            }

            //Handle for single string input vs line set
            if (args.Length == 1)
            {
                if (args[0] == "DEMO")
                {
                    args = GetTestSimulation().ToArray();
                }
                else
                {
                    args = args[0]
                        .Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(z => z.Trim())
                        .ToArray();
                }
            }

            return args;
        }

        private static List<string> GetTestSimulation()
        {
            var sampleMission = new List<string>();

            //Bounds
            sampleMission.Add("5 5");

            //Rover 1
            sampleMission.Add("1 2 N");
            sampleMission.Add("LMLMLMLMM");

            //Rover 2
            sampleMission.Add("3 3 E");
            sampleMission.Add("MMRMMRMRRM");

            return sampleMission;
        }

        public static List<List<string>> Simulate(string[][] missions)
        {
            List<List<string>> returnSet = new List<List<string>>();

            Action<string, int> ReportFailure = (s, i) => Console.WriteLine($"Mission {i} Failed: {s}");
            Action<int, int, string> ReportSuccess = (m, r, i) => Console.WriteLine($"Mission {m} Rover {r} Pos: {i}");

            for (var missionNo = 0; missionNo < missions.Length; missionNo++)
            {
                var results = new List<string>();
                try
                {
                    Console.WriteLine($"{Environment.NewLine} \\* Mission {missionNo} Start");
                    var mission = new ParsedMissionCommand(missions[missionNo]);
                    if (mission.Error.HasValue)
                    {
                        ReportFailure(mission.Error.ToString(), missionNo);
                        continue;
                    }
                    var debug = (mission.MissionType & Enums.MissionType.Debug) > 0;
                    var missionControl = new RoverMissionController(mission);
                    var missionResults = missionControl.ConductRoverMission();
                    var counter = 0;
                    foreach (var result in missionResults)
                    {
                        var response = result.GetMissionResponse();
                        ReportSuccess(missionNo, counter++, response);
                        results.Add(response);
                    }
                }
                catch (Exception ex)
                {
                    ReportFailure(ex.Message, missionNo);
                }
                finally
                {
                    returnSet.Add(results);
                    Console.WriteLine($"\\* Mission {missionNo} End {Environment.NewLine}");
                }
            }

            return returnSet;
        }
    }
}
