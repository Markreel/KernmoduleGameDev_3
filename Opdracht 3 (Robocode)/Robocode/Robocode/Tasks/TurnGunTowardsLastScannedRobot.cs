namespace MJBM
{
    public class TurnGunTowardsLastScannedRobot : BTNode
    {

        public TurnGunTowardsLastScannedRobot(BlackBoard blackBoard)
        {
            this.blackBoard = blackBoard;
        }

        public override BTNodeStatus Tick()
        {
            if (blackBoard.LastScannedRobotEvent != null)
            {
                double _absoluteBearing = blackBoard.Robot.Heading + blackBoard.LastScannedRobotEvent.Bearing;
                double _bearingFromGun = Robocode.Util.Utils.NormalRelativeAngleDegrees(_absoluteBearing - blackBoard.Robot.GunHeading);             
                
                blackBoard.Robot.TurnGunRight(_bearingFromGun);

                if(blackBoard.Robot.GunTurnRemaining == 0) { return BTNodeStatus.Success; }
                else { return BTNodeStatus.Running; }
            }
            else
            {
                return BTNodeStatus.Failed;
            }
        }
    }
}
