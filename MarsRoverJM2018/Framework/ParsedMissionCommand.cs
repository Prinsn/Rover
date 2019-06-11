using MarsRoverJM2018.Enums;
using MarsRoverJM2018.Interfaces;
using MarsRoverJM2018.Rovers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018.Framework
{
    public class ParsedMissionCommand : IMissionCommand
    {
        public ParsedMissionCommand(string[] commands)
        {
            var commandLines = commands.Select(z => z.Trim()).ToArray();

            //One line header and 2 lines per drone command
            if (commandLines.Length < 3 ||commandLines.Length % 2 == 0)
            {
                Error = ErrorCode.Command_Parse;
                return;
            }

            try
            {
                Bounds = new Coordinant(commandLines[0]);
                var missions = new List<IRoverCommand>();
                for (var i = 1; i < commandLines.Length; i += 2)
                {
                    var newRoverMission = new ParsedRoverCommand(commandLines[i], commandLines[i + 1]);
                    if(newRoverMission.Error.HasValue)
                    {
                        this.Error = newRoverMission.Error;
                        return;
                    }

                    missions.Add(newRoverMission);
                }

                RoverMissions = missions;
            }
            catch (Exception)
            {
                Error = ErrorCode.Command_Parse;
            }
        }

        public Coordinant Bounds { get; private set; }

        public IEnumerable<IRoverCommand> RoverMissions { get; private set; }

        public MissionType MissionType { get; private set; } = MissionType.NoArgs;

        public ErrorCode? Error { get; private set; }
    }
}
