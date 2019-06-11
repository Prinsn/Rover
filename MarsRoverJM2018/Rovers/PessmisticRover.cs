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
    /// Rover that will give up when encountering errors
    /// </summary>
    public class PessimisticRover : BaseRover
    {
        public PessimisticRover(IRoverCommand init, bool debug = false) : base(init, debug)
        {
        }

        public override void ConductMission(IEnumerable<BaseRover> roverNetwork, Coordinant bounds)
        {
#if DEBUG
            Console.WriteLine($"==BEGIN MISSION==");
#endif
            for (var i = 0; i < this.Mission.Length; i++)
            {
#if DEBUG
                var c = this.Mission[i];
                Console.WriteLine($"{c}: {Position.xPos} {Position.yPos} {Heading}");
#endif
                var result = DoCommand(this.Mission[i], bounds, roverNetwork);
                if (result.HasValue)
                {
                    this.Error = result;
                    break;
                }
            }

#if DEBUG
            Console.WriteLine($"==END MISSION==");
#endif
        }
    }
}
