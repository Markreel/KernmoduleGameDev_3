namespace MJBM
{
    /// <summary>
    /// Runs all input nodes and returns success if an input node returns success (eigenlijk OR statement)
    /// </summary>
    class Selector : BTNode
    {
        private BTNode[] inputNodes;
        public Selector(BlackBoard _blackBoard, params BTNode[] _input)
        {
            blackBoard = _blackBoard;
            inputNodes = _input;
        }

        public override BTNodeStatus Tick()
        {
            foreach (BTNode _node in inputNodes)
            {
                BTNodeStatus _result = _node.Tick();
                switch (_result)
                {
                    case BTNodeStatus.Failed:
                        continue;
                    case BTNodeStatus.Running:
                        return BTNodeStatus.Running;
                    case BTNodeStatus.Success:
                        return BTNodeStatus.Success;
                }
            }
            return BTNodeStatus.Failed;
        }
    }
}
