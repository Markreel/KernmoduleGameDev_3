using System.Drawing;

namespace MJBM
{
    public class Turn : BTNode
    {
        private float degrees;

        public Turn(BlackBoard blackBoard, float degrees)
        {
            this.degrees = degrees;
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            if(blackBoard.Robot.GunTurnRemaining == 0) { blackBoard.Robot.SetTurnGunRight(degrees); }
            blackBoard.Robot.SetAllColors(Color.MediumPurple);
            blackBoard.Robot.Execute();

            return BTNodeStatus.Running;
        }
    }
}
