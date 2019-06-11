using MarsRoverJM2018.Enums;
using MarsRoverJM2018.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018.Framework
{
    public class ParsedRoverCommand : IRoverCommand
    {
        private const string MISSION_PARSABLE_CHARS = "LRM";
        public ParsedRoverCommand(string initializer, string mission)
        {
            var commands = initializer.Split(' ')
                .Select(z => z.Trim())
                .ToArray(); 

            if (commands.Length != 3)
            {
                Error = ErrorCode.Command_Parse;
                return;
            }

            if (mission.Any(c => !MISSION_PARSABLE_CHARS.Contains(c)))
            {
                Error = ErrorCode.Command_Parse;
                return;
            }

            Start = new Coordinant($"{commands[0]} {commands[1]}");
            Mission = mission;

            var heading = Heading;
            if(commands[2].Length != 1 || !Enum.TryParse(commands[2], out heading))
                Error = ErrorCode.Command_Parse;
            Heading = heading;
        }

        public Coordinant Start { get; private set; }

        public Directionality Heading { get; private set; }

        public string Mission { get; private set; }

        public ErrorCode? Error { get; private set;}
    }
}
