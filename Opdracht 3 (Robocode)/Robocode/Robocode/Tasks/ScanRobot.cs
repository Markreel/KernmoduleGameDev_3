namespace MJBM
{
    public class ScanRobot : BTNode
    {
        private float scanDegrees;
        public ScanRobot(BlackBoard _blackBoard, float _scanDegrees)
        {
            blackBoard = _blackBoard;
            scanDegrees = _scanDegrees;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.Robot.TurnRadarLeft(scanDegrees);
            return BTNodeStatus.Success;
        }
    }
}
