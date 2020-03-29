using System;

namespace MJBM
{
    public class GoToCenter : BTNode
    {
        public GoToCenter(BlackBoard _blackboard)
        {
            blackBoard = _blackboard;
        }

        public override BTNodeStatus Tick()
        {
            double _robotX = blackBoard.Robot.X;
            double _robotY = blackBoard.Robot.Y;

            double _centerX = blackBoard.Robot.BattleFieldWidth / 2;
            double _centerY = blackBoard.Robot.BattleFieldHeight / 2;

            double _targetRadians = Math.Atan2(_centerX - _robotX, _centerY - _robotY);
            double _targetDegrees = (_targetRadians * (180d / Math.PI) + 360d) % 360d;


            double _targetDistance = Math.Sqrt(Math.Pow(_robotX - _centerX, 2) + Math.Pow(_robotY - _centerY, 2));

            if (blackBoard.Robot.Heading != _targetDegrees)
            {
                blackBoard.Robot.Out.WriteLine("Turning! | remaining: " + blackBoard.Robot.TurnRemaining + " | degrees: " + _targetDegrees);
                if(blackBoard.Robot.TurnRemaining == 0) { blackBoard.Robot.SetTurnRight(_targetDegrees - blackBoard.Robot.Heading); }
                blackBoard.Robot.Execute();
                return BTNodeStatus.Running;
            }
            else if(_robotX != _centerX && _robotY != _centerY)
            {
                if(blackBoard.Robot.DistanceRemaining == 0) { blackBoard.Robot.SetAhead(_targetDistance); }
                blackBoard.Robot.Execute();
                return BTNodeStatus.Running; 
            }
            else
            {
                return BTNodeStatus.Success;
            }
        }
    }
}
