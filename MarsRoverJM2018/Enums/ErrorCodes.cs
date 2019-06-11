using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018.Enums
{
    /// <summary>
    /// Error code standardization for additional mission code response for unexpected behavior.
    /// </summary>
    [Flags]
    public enum ErrorCode
    {
        //Boundary avoidance
        Northern_Boundary = 1,
        Eastern_Boundary = 2,
        Southern_Boundary = 4,
        Western_Boundary = 8,
        
        //Collision Avoidance
        Unexpected_Object = 16,
        Command_Parse = 32,
        Unknown_Movement_Failure = 64,
        Unknown_Turn_Failure = 128,
        Unexpected_Drone = 256
    }
}
