using MarsRoverJM2018.Enums;
using MarsRoverJM2018.Framework;
using MarsRoverJM2018.Interfaces;
using MarsRoverJM2018.Rovers;
using System.Collections.Generic;

namespace MarsRoverJM2018.RoverController
{
    /// <summary>
    /// Simulated rover command hub
    /// </summary>
    public class RoverMissionController
    {
        public List<BaseRover> RoverNetwork { get; private set; }
        public Coordinant MissionBounds { get; private set; }

        public RoverMissionController(IMissionCommand mission)
        {
            RoverNetwork = new List<BaseRover>();
            MissionBounds = mission.Bounds;
            var debug = (mission.MissionType & MissionType.Debug) > 0;
            var optimistic = (mission.MissionType & MissionType.Optimistic) > 0;
            foreach(var roverCommand in mission.RoverMissions)
            {
                BaseRover newRover;
                if (optimistic)
                    newRover = new OptimisticRover(roverCommand, debug);
                else
                    newRover = new PessimisticRover(roverCommand, debug);
                RoverNetwork.Add(newRover);
            }
        }

        public List<MissionResponse> ConductRoverMission()
        {
            var results = new List<MissionResponse>();
            foreach(var Rover in RoverNetwork)
            {
                Rover.ConductMission(RoverNetwork, MissionBounds);
                results.Add(Rover.Report());
            }

            return results;
        }
    }
}
