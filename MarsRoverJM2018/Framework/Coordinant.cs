using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018.Framework
{
    /// <summary>
    /// 2d coordinant wrapper
    /// </summary>
    public class Coordinant
    {
        public int xPos { get; set; }
        public int yPos { get; set; }

        /// <summary>
        /// NASA communication spec coordinant parsing constructor
        /// </summary>
        /// <param name="s">Two integers, space delimited</param>
        public Coordinant(string s)
        {
            var commands = s.Split(' ');
            xPos = int.Parse(commands[0]);
            yPos = int.Parse(commands[1]);
        }

        public Coordinant(int x, int y)
        {
            xPos = x;
            yPos = y;
        }

        public Coordinant Clone()
        {
            return new Coordinant(xPos, yPos);
        }

        public override bool Equals(object obj)
        {
            var coord = obj as Coordinant;
            if (coord == null)
                return false;

            return this.xPos == coord.xPos && this.yPos == coord.yPos;
        }

        public override int GetHashCode()
        {
            var hashCode = -478729987;
            hashCode = hashCode * -1521134295 + xPos.GetHashCode();
            hashCode = hashCode * -1521134295 + yPos.GetHashCode();
            return hashCode;
        }
    }
}
