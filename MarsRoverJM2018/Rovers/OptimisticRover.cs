using MarsRoverJM2018.Enums;
using MarsRoverJM2018.Framework;
using MarsRoverJM2018.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018.Rovers
{
    /// <summary>
    /// Rover that will attempt to complete every action it can
    /// Error code will be the most recent error encountered
    /// </summary>
    public class OptimisticRover : BaseRover
    {
        public OptimisticRover(IRoverCommand init, bool debug = false) : base(init, debug)
        {
        }

        public override void ConductMission(IEnumerable<BaseRover> roverNetwork, Coordinant bounds)
        {
            for (var i = 0; i < this.Mission.Length; i++)
            {
                var result = DoCommand(this.Mission[i], bounds, roverNetwork);
                if (result.HasValue)
                    this.Error = result;
            }
        }
    }
}
