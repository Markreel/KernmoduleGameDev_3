using System;
using System.Collections.Generic;
using System.Text;

namespace MJBM
{
    public class Shoot : BTNode
    {
        private float power;
        public Shoot(BlackBoard _blackBoard, float _power)
        {
            blackBoard = _blackBoard;
            power = _power;
        }

        public override BTNodeStatus Tick()
        {
            blackBoard.Robot.Fire(power);
            return BTNodeStatus.Success;
        }
    }
}
