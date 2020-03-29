using System.Drawing;

namespace MJBM
{
    public class HasScannedEnemy : BTNode
    {
        public HasScannedEnemy(BlackBoard _blackBoard)
        {
            blackBoard = _blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            if (blackBoard.LastScannedRobotEvent == null) { return BTNodeStatus.Failed; }
            else { blackBoard.Robot.SetAllColors(Color.LightGoldenrodYellow); return BTNodeStatus.Success; }
        }
    }
}
