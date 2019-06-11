using MarsRoverJM2018.Enums;
using MarsRoverJM2018.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018.Interfaces
{
    /// <summary>
    /// Interface representing a full mission command instruction
    /// </summary>
    public interface IMissionCommand
    {
        Coordinant Bounds { get; }
        IEnumerable<IRoverCommand> RoverMissions { get; }
        MissionType MissionType { get; }
    }
}
