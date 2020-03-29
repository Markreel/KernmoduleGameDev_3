namespace MJBM
{
    public enum BTNodeStatus { Failed, Running, Success }

    //elke node gaat inheriten van deze class (dat is wat we met abstract bedoelen)
    public abstract class BTNode
    {
        protected BlackBoard blackBoard;
        public abstract BTNodeStatus Tick();

    }
}
