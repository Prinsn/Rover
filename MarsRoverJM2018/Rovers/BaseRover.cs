using MarsRoverJM2018.Enums;
using MarsRoverJM2018.Framework;
using MarsRoverJM2018.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Standard rover
/// </summary>
namespace MarsRoverJM2018.Rovers
{
    public abstract class BaseRover
    {
        /* Command Definitions */
        public const char LEFT = 'L';
        public const char RIGHT = 'R';
        public const char MOVE = 'M';

        /* Mission Parameters*/
        public Directionality Heading { get; private set; }
        public Coordinant Position {get; private set;}
        public string Mission { get; private set; }

        /* Internal Tracking Parameters */
        private bool _debug;
        protected ErrorCode? Error;

        public BaseRover(IRoverCommand init, bool debug = false)
        {
            Heading = init.Heading;
            Position = init.Start;
            Mission = init.Mission;
            _debug = debug;
        }

        public MissionResponse Report()
        {
            if (_debug && Error.HasValue)
                return new DebugResponse(Position, Heading, Error.Value);
            return new MissionResponse(Position, Heading);
        }

        abstract public void ConductMission(IEnumerable<BaseRover> roverNetwork, Coordinant bounds);

        public ErrorCode? DoCommand(char c, Coordinant bounds, IEnumerable<BaseRover> rovers)
        {
            var previousPosition = Position;

            switch (c)
            {
                case LEFT:
                    return TurnLeft();
                case RIGHT:
                    return TurnRight();
                case MOVE:
                    return Move(bounds, rovers);
                default:
                    return ErrorCode.Command_Parse;
            }
        }

        private ErrorCode? Move(Coordinant bounds, IEnumerable<BaseRover> rovers)
        {
            var previousPosition = Position.Clone();
            var predictedPosition = Position.Clone();
            //Predict Movement
            switch (Heading)
            {
                case Directionality.N:
                    predictedPosition.yPos++;
                    break;
                case Directionality.S:
                    predictedPosition.yPos--;
                    break;
                case Directionality.E:
                    predictedPosition.xPos++;
                    break;
                case Directionality.W:
                    predictedPosition.xPos--;
                    break;
            }

            //Validate Boundary
            if (predictedPosition.xPos > bounds.xPos)
                return ErrorCode.Eastern_Boundary;            
            if (predictedPosition.xPos < 0)
                return ErrorCode.Western_Boundary;
            if (predictedPosition.yPos > bounds.yPos)
                return ErrorCode.Northern_Boundary;
            if (predictedPosition.yPos < 0)
                return ErrorCode.Southern_Boundary;

            //Validate Objects
            if (rovers.Where(z => z != this).Any(r => r.Position.Equals(predictedPosition)))
                return ErrorCode.Unexpected_Drone;

            Position = predictedPosition;

            //Impossible, future proofing
            if (Position == previousPosition)
                return ErrorCode.Unknown_Movement_Failure;

            return null;
        }

        private ErrorCode? TurnLeft()
        {
            var previousHeading = Heading;
            //Rotate CCW
            var headingInt = (int)Heading - 1;
            if (headingInt < 0)
                headingInt = 3;
            Heading = (Directionality)headingInt;
            
            //Impossible, future proofing
            if (Heading == previousHeading)
                return ErrorCode.Unknown_Turn_Failure;
            return null;
        }

        private ErrorCode? TurnRight()
        {
            var previousHeading = Heading;
            //Rotate CW
            var headingInt = (int)Heading + 1;
            if (headingInt > 3)
                headingInt = 0;
            Heading = (Directionality)headingInt;

            //Impossible, future proofing
            if (Heading == previousHeading)
                return ErrorCode.Unknown_Turn_Failure;

            return null;
        }

    }
}
