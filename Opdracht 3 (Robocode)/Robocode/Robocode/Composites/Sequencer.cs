namespace MJBM
{
    /// <summary>
    /// Eigenlijk AND statement
    /// </summary>
    class Sequencer : BTNode
    {
        private BTNode[] inputNodes;
        public Sequencer(BlackBoard _blackBoard, params BTNode[] _input)
        {
            blackBoard = _blackBoard;
            inputNodes = _input;
        }

        public override BTNodeStatus Tick()
        {
            //Check alle subnodes
            foreach (BTNode _node in inputNodes)
            {
                BTNodeStatus _result = _node.Tick();
                switch (_result)
                {
                    case BTNodeStatus.Failed:
                    case BTNodeStatus.Running:
                        return _result;
                    case BTNodeStatus.Success:
                        continue;
                }
            }
            return BTNodeStatus.Success;
        }
    }
}
