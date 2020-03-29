using System;
using System.Drawing;

namespace MJBM
{
    public class MoveToLastScanLocation : BTNode
    {
        public MoveToLastScanLocation(BlackBoard _blackBoard)
        {
            blackBoard = _blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            if (blackBoard.LastScannedRobotEvent != null)
            {
                if (blackBoard.Robot.TurnRemaining == 0 && Math.Abs(blackBoard.LastScannedRobotEvent.Bearing) > 3)
                {
                    blackBoard.Robot.SetTurnRight(blackBoard.LastScannedRobotEvent.Bearing);
                }
                else if (blackBoard.Robot.DistanceRemaining == 0)
                {
                    blackBoard.Robot.SetAhead(blackBoard.LastScannedRobotEvent.Distance);
                }

                if (blackBoard.LastScannedRobotEvent.Distance == 0) { blackBoard.LastScannedRobotEvent = null; return BTNodeStatus.Success; }

                blackBoard.Robot.SetAllColors(Color.BlueViolet);
                blackBoard.Robot.Execute();
                return BTNodeStatus.Running;
            }
            else
            {
                return BTNodeStatus.Failed;
            }
        }
    }
}
