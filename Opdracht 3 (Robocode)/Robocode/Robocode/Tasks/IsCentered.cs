namespace MJBM
{
    public class IsCentered : BTNode
    {
        private int toleratedDistance;

        public IsCentered(BlackBoard _blackBoard, int _toleratedDistance = 0)
        {
            blackBoard = _blackBoard;
            toleratedDistance = _toleratedDistance;
        }

        public override BTNodeStatus Tick()
        {
            double _x = blackBoard.Robot.X;
            double _y = blackBoard.Robot.Y;
            double _centerX = blackBoard.Robot.BattleFieldWidth / 2;
            double _centerY = blackBoard.Robot.BattleFieldHeight / 2;

            if (_x > _centerX + toleratedDistance || _x < _centerX - toleratedDistance ||
                _y > _centerY + toleratedDistance || _y < _centerY - toleratedDistance)
            {
                return BTNodeStatus.Failed;
            }

            else
            {
                return BTNodeStatus.Success;
            }
        }
    }
}
