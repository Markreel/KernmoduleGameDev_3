using System;
using System.Drawing;

namespace MJBM
{
    public class Taunt : BTNode
    {
        private string[] insults;
        private string[] oneLiners;

        public Taunt(BlackBoard _blackBoard, string[] _insults, string[] _oneLiners)
        {
            blackBoard = _blackBoard;
            insults = _insults;
            oneLiners = _oneLiners;
        }

        public override BTNodeStatus Tick()
        {
            string _randomInsult = insults[(int)RandomRange(0, insults.Length)];
            string _randomOneLiner = oneLiners[(int)RandomRange(0, oneLiners.Length)];
            double _randomInteger = RandomRange(0, 2);

            string _outputString = "";

            if (blackBoard.EnemyName == null || _randomInteger == 0) { _outputString = _randomOneLiner; }
            else { _outputString = blackBoard.EnemyName + " is een " + _randomInsult; }

            blackBoard.Robot.Out.WriteLine("Roboboi: " + _outputString);
            blackBoard.Robot.SetAllColors(Color.AntiqueWhite);

            return BTNodeStatus.Success;
        }

        private double RandomRange(double _minimum, double _maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (_maximum - _minimum) + _minimum;
        }
    }
}
