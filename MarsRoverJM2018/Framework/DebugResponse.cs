using MarsRoverJM2018.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverJM2018.Framework
{
    /// <summary>
    /// Extended mission response to include error
    /// </summary>
    public class DebugResponse : MissionResponse
    {
        private ErrorCode _errorCode;

        public DebugResponse(Coordinant end, Directionality heading, ErrorCode errorCode)
            :base(end, heading)
        {
            _errorCode = errorCode;
        }

        public override string GetMissionResponse()
        {
            return $"{_end.xPos} {_end.yPos} {_heading} {(int)_errorCode}";
        }
    }
}
