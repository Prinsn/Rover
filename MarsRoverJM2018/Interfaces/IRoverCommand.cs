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
    ///Interface representing a single drone's mission
    /// </summary>
    public interface IRoverCommand
    {
        Coordinant Start { get; }
        Directionality Heading { get; }
        string Mission { get; }
    }
}
