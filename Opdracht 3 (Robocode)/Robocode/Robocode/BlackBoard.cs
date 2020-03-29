using Robocode;

namespace MJBM
{
    public class BlackBoard
    {
        public AdvancedRobot Robot;
        public ScannedRobotEvent LastScannedRobotEvent;
        public HitRobotEvent LastRammedRobotEvent;

        public string EnemyName;
        public double LastEnergyLevel;

        public bool IsLettingEnemyRegenerate;
    }
}