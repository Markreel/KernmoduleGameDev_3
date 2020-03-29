using System;

namespace MJBM
{
    public class AllowRegeneration : BTNode
    {
        private double activationEnergyMinimum;
        private double regenerationLimit;
        private double ownEnergyLimit;

        public AllowRegeneration(BlackBoard _blackBoard, double _activationEnergyMinimum, double _regenerationLimit, double _ownEnergyLimit)
        {
            blackBoard = _blackBoard;
            activationEnergyMinimum = _activationEnergyMinimum;
            regenerationLimit = _regenerationLimit;
            ownEnergyLimit = _ownEnergyLimit;
        }

        public override BTNodeStatus Tick()
        {
            //Skip regeneration als we zelf niet genoeg energy hebben
            if (blackBoard.Robot.Energy < ownEnergyLimit) { blackBoard.IsLettingEnemyRegenerate = false; return BTNodeStatus.Success; }

            //Check of we een gescande robot hebben
            blackBoard.Robot.Scan();
            if (blackBoard.LastScannedRobotEvent != null)
            {
                if (blackBoard.Robot.TurnRemaining == 0 && Math.Abs(blackBoard.LastScannedRobotEvent.Bearing) > 3)
                {
                    blackBoard.Robot.SetTurnRight(blackBoard.LastScannedRobotEvent.Bearing);
                }

                if (blackBoard.IsLettingEnemyRegenerate)
                {
                    if (blackBoard.LastScannedRobotEvent.Energy > activationEnergyMinimum)
                    {
                        blackBoard.IsLettingEnemyRegenerate = false;
                        return BTNodeStatus.Success;
                    }
                    else { return BTNodeStatus.Running; }
                }

                else if (blackBoard.LastScannedRobotEvent.Energy < activationEnergyMinimum)
                {
                    blackBoard.IsLettingEnemyRegenerate = true;
                    return BTNodeStatus.Running;
                }

                else { return BTNodeStatus.Success; }
            }
            else
            {
                blackBoard.IsLettingEnemyRegenerate = false;
                return BTNodeStatus.Success;
            }
        }
    }
}
