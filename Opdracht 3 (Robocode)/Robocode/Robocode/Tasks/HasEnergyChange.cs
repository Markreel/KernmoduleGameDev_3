using System.Drawing;

namespace MJBM
{
    public class HasEnergyChange : BTNode
    {
        double changeAmount;

        public HasEnergyChange(BlackBoard _blackBoard, double _changeAmount)
        {
            blackBoard = _blackBoard;
            changeAmount = _changeAmount;
        }

        public override BTNodeStatus Tick()
        {
            if(blackBoard.Robot.Energy < blackBoard.LastEnergyLevel - changeAmount || blackBoard.LastEnergyLevel == 0)
            {
                blackBoard.Robot.SetAllColors(Color.LawnGreen);
                blackBoard.LastEnergyLevel = blackBoard.Robot.Energy;
                return BTNodeStatus.Success;
            }

            return BTNodeStatus.Failed;
        }
    }
}
