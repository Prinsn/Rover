using MarsRoverJM2018.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018.Framework
{
    /// <summary>
    /// Used to report back mission result.
    /// </summary>
    public class MissionResponse
    {
        protected Coordinant _end;
        protected Directionality _heading;
        public ErrorCode? Error { get; set; }

        public MissionResponse(Coordinant end, Directionality heading)
        {
            _end = end;
            _heading = heading;
        }

        public virtual string GetMissionResponse()
        {
            return $"{_end.xPos} {_end.yPos} {_heading}";
        }

        public void Report()
        {
            Console.WriteLine(GetMissionResponse());
        }
    }
}
