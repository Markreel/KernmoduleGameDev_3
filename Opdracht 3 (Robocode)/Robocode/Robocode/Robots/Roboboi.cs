using System;
using Robocode;
using System.Drawing;
namespace MJBM
{
    //dit is de strat
    //#1 rijd alleen in zn achteruit.
    //#2 hug walls
    //#3 skieten
    
     //heading is waar je heen kijkt met je body
     //bearing is hoek tussen jouw en de target (altijd ten opzichte van de heading)
     //bearing min verschil tussen de heading en gun heading.

    //http://robowiki.net/wiki/Energy_Management
    //The formula to calculated the damage to enemy robot for given firepower is 4 * power + max(0, 2 * (power - 1) ), where power is a value between 0.1 and 3. But usually, for some reasons, we do not really care about max(0, 2 * (power - 1) and just fire at power enemyEnergy / 4.

    public class Roboboi : AdvancedRobot
    {
        public BTNode BehaviourTree;
        public BlackBoard BlackBoard = new BlackBoard();

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            if (BlackBoard.EnemyName == null) { BlackBoard.EnemyName = evnt.Name; }
            BlackBoard.LastScannedRobotEvent = evnt;
        }

        public override void OnHitRobot(HitRobotEvent evnt)
        {
            if (BlackBoard.EnemyName == null) { BlackBoard.EnemyName = evnt.Name; }
            BlackBoard.LastRammedRobotEvent = evnt;
        }

        public override void Run()
        {
            #region Apply Paint-Job
            SetColors(Color.Orange, Color.Red, Color.Orchid, Color.LimeGreen, Color.HotPink);
            //SetAllColors(Color.Orange);
            #endregion

            #region Behaviour Tree setup
            BlackBoard.Robot = this;

            #region BEHAVIOUR 1 - "CENTER RAM"
            BTNode KeepCentered = new Selector(BlackBoard,
                new IsCentered(BlackBoard),
                new GoToCenter(BlackBoard)
                );

            BTNode CenterRam = new Sequencer(BlackBoard,
                KeepCentered,
                new MoveAhead(BlackBoard, -50),
                new MoveAhead(BlackBoard, 100),
                new MoveAhead(BlackBoard, -50)
                );
            #endregion

            #region BEHAVIOUR 2 - "HIT AND GUN"
            BTNode ScanEnemy = new Selector(BlackBoard,
                new HasScannedEnemy(BlackBoard),
                new Turn(BlackBoard, 360)
                );

            string[] _insults = new string[5];
            _insults[0] = "sukkel!";
            _insults[1] = "loser!";
            _insults[2] = "minkukel!";
            _insults[3] = "nozem!";
            _insults[4] = "slechte developer!";

            string[] _oneLiners = new string[5];
            _oneLiners[0] = "ik ga je verbrijzelen!";
            _oneLiners[1] = "jij gaat neer neef!";
            _oneLiners[2] = "ik ga je corona geven!";
            _oneLiners[3] = "kom maar door met die prijs!";
            _oneLiners[4] = "mark is de beste developer!";

            BTNode Taunt = new Selector(BlackBoard,
                new BTInvert(new HasEnergyChange(BlackBoard, 10)),
                new Taunt(BlackBoard, _insults, _oneLiners)
                );

            BTNode HitAndGun = new Sequencer(BlackBoard,
                Taunt,
                ScanEnemy,
                //new AllowRegeneration(BlackBoard, 5, 20, 30),
                new ShootOnRam(BlackBoard, Rules.MAX_BULLET_POWER, 4 * Rules.MAX_BULLET_POWER), //Wellicht zonder minimale energy (even testen wat beter is)
                new MoveToLastScanLocation(BlackBoard)
                );
            #endregion


            #endregion

            while (true)
            {
                BTNodeStatus _status = HitAndGun.Tick();
                //Out.WriteLine("Regenrating: " + BlackBoard.IsLettingEnemyRegenerate);
            }
        }
    }
}
