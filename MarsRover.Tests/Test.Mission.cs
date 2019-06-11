using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverJM2018;

namespace MarsRover.Tests
{
    /// <summary>
    /// Summary description for Test
    /// </summary>
    [TestClass]
    public class MissionTests
    {
        [TestMethod]
        public void SampleCodeTestMission()
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

            var results = MissionSimulator.Simulate(new[] { sampleMission.ToArray() });
            var result = results[0];
            var mission1 = result[0];
            var mission2 = result[1];
            Assert.AreEqual(mission1, "1 3 N");
            Assert.AreEqual(mission2, "5 1 E");
        }

        [TestMethod]
        public void PerimiterTest()
        {
            var move5 = "MMMMM";
            var move10 = move5 + move5;
            var start = "5 5 N";

            var sampleMission = new List<string>();

            //Bounds
            sampleMission.Add("10 10");

            //Rover 1
            sampleMission.Add("5 5 N");
            sampleMission.Add($"{move5}R{move5}R{move10}R{move10}R{move10}R{move5}R{move5}RR");

            var results = MissionSimulator.Simulate(new[] { sampleMission.ToArray() });
            var mission = results[0][0];
            
            Assert.AreEqual(mission, start);
        }
    }
}
