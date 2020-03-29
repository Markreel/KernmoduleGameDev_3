using System.Drawing;

namespace MJBM
{
    public class ShootOnRam : BTNode
    {
        private double power;
        private double minimumEnemyEnergy;

        public ShootOnRam(BlackBoard _blackBoard, double _power, double _minimumEnemyEnergy = 0)
        {
            blackBoard = _blackBoard;
            power = _power;
            minimumEnemyEnergy = _minimumEnemyEnergy;
        }

        public override BTNodeStatus Tick()
        {
            if(blackBoard.LastRammedRobotEvent != null)
            {
                if (blackBoard.LastRammedRobotEvent.Energy > minimumEnemyEnergy) { blackBoard.Robot.Fire(power); }
                blackBoard.LastRammedRobotEvent = null;
                blackBoard.Robot.SetAllColors(Color.Beige);
            }

            return BTNodeStatus.Success;
        }
    }
}
