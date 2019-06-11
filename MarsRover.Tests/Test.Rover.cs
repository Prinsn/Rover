using System;
using System.Collections.Generic;
using MarsRoverJM2018.Enums;
using MarsRoverJM2018.Framework;
using MarsRoverJM2018.Rovers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRover.Tests
{
    [TestClass]
    public class RoverTests
    {
        [TestMethod]
        public void TestRoverCollision()
        {
            var rover = new PessimisticRover(new ParsedRoverCommand("1 1 N", "MMM"));
            var rovers = new List<BaseRover>() { new PessimisticRover(new ParsedRoverCommand("1 3 N", "M")) };
            var bounds = new Coordinant(3, 3);
            ErrorCode? result = rover.DoCommand('M', bounds, rovers);
            Assert.IsNull(result);
            result = rover.DoCommand('M', bounds, rovers);
            Assert.IsTrue(result.HasValue);
            Assert.IsTrue(result.Value == ErrorCode.Unexpected_Drone);
        }

        #region boundary tests
        [TestMethod]
        public void TestNorthBound()
        {
            BoundaryTest('N', ErrorCode.Northern_Boundary);
        }

        /// <summary>
        /// and down
        /// </summary>
        [TestMethod]
        public void TestEastBound()
        {
            BoundaryTest('E', ErrorCode.Eastern_Boundary);
        }

        [TestMethod]
        public void TestSouthBound()
        {
            BoundaryTest('S', ErrorCode.Southern_Boundary);
        }

        [TestMethod]
        public void TestWestBound()
        {
            BoundaryTest('W', ErrorCode.Western_Boundary);
        }

        private void BoundaryTest(char heading, ErrorCode ec)
        {
            var rover = new PessimisticRover(new ParsedRoverCommand($"1 1 {heading}", "MMM"));
            var rovers = new List<BaseRover>();
            var bounds = new Coordinant(2, 2);
            ErrorCode? result = rover.DoCommand('M', bounds, rovers);
            Assert.IsNull(result);
            result = rover.DoCommand('M', bounds, rovers);
            Assert.IsTrue(result.HasValue);
            Assert.IsTrue(result.Value == ec);
        }
        #endregion

    }
}
