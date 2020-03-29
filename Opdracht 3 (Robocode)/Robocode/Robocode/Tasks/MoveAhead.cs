namespace MJBM
{
    public class MoveAhead : BTNode
    {
        private int moveDistance;
        public MoveAhead(BlackBoard _blackBoard, int _movePixels)
        {
            blackBoard = _blackBoard;
            moveDistance = _movePixels;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.Robot.Ahead(moveDistance);
            return BTNodeStatus.Success;
        }
    }
}
