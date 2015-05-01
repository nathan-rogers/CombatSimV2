using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.PlayGame();

            Console.ReadKey();
        }
    }

    public abstract class Actor
    {
        public Random rand = new Random();
        public string Name { get; set; }
        public int HP { get; set; }
        public int Money { get; set; }
        public Actor(string name, int hP, int money)
        {
            this.Name = name;
            this.HP = hP;
            this.Money = money;


        }
        public bool IsAlive
        {
            get
            {

                if (this.HP > 0)
                {
                    return true;
                }
                return false;
            }
        }


        public virtual void Attack(List<Actor> actor)
        {

        }

    }

    public class Enemy : Actor
    {
        public Enemy(string name, int hP, int money)
            : base(name, hP, money)
        {

        }

        public override void Attack(List<Actor> actor)
        {
            if (this.HP > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                int damage = 0;
                //see if enemy attacks
                if (rand.Next(0, 100) < 80)
                {
                    //enemy atk damage
                    damage = rand.Next(2, 5);
                    actor[0].HP = actor[0].HP - damage;

                    Console.WriteLine("Replicant did {0} damage!", damage);

                }
                else
                {
                    Console.WriteLine("Replicant missed!!!");
                }
                //random enemy heal
                if (rand.Next(0, 100) < 80)
                {
                    damage = rand.Next(0, 10);
                    this.HP = HP + damage;
                    Console.WriteLine("Enemy healed for {0}", damage);
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
        }
    }
    public class Player : Actor
    {
        public int SuperWeaponNum { get; set; }
        int roundCounter = 1;
        public enum SuperWeapon
        {
            Katana,
            Bazooka,
            AirStrike,
            Nuke
        }
        public void Selector()
        {

            string select = Console.ReadLine();
            switch (select)
            {
                case "1":
                    SuperWeaponNum = (int)SuperWeapon.Katana;
                    break;
                case "2":
                    SuperWeaponNum = (int)SuperWeapon.Bazooka;
                    break;
                case "3":
                    SuperWeaponNum = (int)SuperWeapon.AirStrike;
                    break;
                case "4":
                    SuperWeaponNum = (int)SuperWeapon.Nuke;
                    break;
                default:
                    break;

            }
        }
        //player money for using supply drops
        public enum AttackType
        {
            MachineGun,
            Pistol,
            SuperWeapon,
            SupplyDrop
        }

        public Player(string name, int hP, int money, int super)
            : base(name, hP, money)
        {
            this.Money = money;
            this.SuperWeaponNum = super;
        }

        public override void Attack(List<Actor> actor)
        {
            // player damage variable

            roundCounter++;
            int damage = 0;
            int selectedEnemy = 0;
            while (selectedEnemy == 0)
            {
                Console.WriteLine("Select an enemy to attack: ");
                int.TryParse(Console.ReadLine(), out selectedEnemy);

            }
            while (selectedEnemy > actor.Count())
            {
                Console.WriteLine("There are not that many enemies. Try again: ");
                int.TryParse(Console.ReadLine(), out selectedEnemy);

            }
            while (actor[selectedEnemy - 1].HP <= 0 && selectedEnemy > actor.Count() && selectedEnemy <= 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Please select a different enemy. This enemy is already dead!");

                int.TryParse(Console.ReadLine(), out selectedEnemy);

                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }



            switch (ChooseAttack())
            {
                //if machine gun
                case AttackType.MachineGun:
                    //if hits

                    Console.WriteLine(@"
      THE ADP:
         Cost: 3 Bullets
Chance to Hit: 70
   Max Damage: 40");
                    if (this.HP - 3 > 0)
                    {
                        if (rand.Next(0, 100) < 70)
                        {
                            this.HP -= 3;
                            damage = rand.Next(20, 40);
                            actor[selectedEnemy - 1].HP -= damage;
                            Console.WriteLine("You did {0} damage with SMG!", damage);

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("You missed!");
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough HP!!!");
                    }
                    break;
                //if pistol 
                case AttackType.Pistol:
                    Console.WriteLine(@"
.45 Cal ANTI-MATTER PISTOL: 
                      Cost: 2 Bullets 
             Chance to Hit: 100 
                Max Damage: 25");

                    this.HP -= 2;
                    damage = rand.Next(10, 26);
                    actor[selectedEnemy - 1].HP -= damage;
                    Console.WriteLine("You did {0} damage with the pistol!!!", damage);
                    break;
                case AttackType.SuperWeapon:
                    if (SuperWeaponNum == 1)
                    {
                        {

                            if (rand.Next(0, 100) < 95)
                            {
                                damage = rand.Next(35, 70);
                                actor[selectedEnemy - 1].HP -= damage;
                                Console.WriteLine("You did {0} damage with Katana!", damage);
                            }
                        }
                    }
                    if (SuperWeaponNum == 2)
                    {
                        {
                            if (actor.Count() <= 2)
                            {
                                Console.WriteLine("Superweapon on lockdown. Cannot use yet.");
                            }
                            else
                            {
                                damage = rand.Next(20, 50);
                                int selectMany = rand.Next(0, actor.Count());
                                while (selectMany <= actor.Count() / 2)
                                {
                                    selectMany = rand.Next(0, actor.Count());
                                }
                                if (selectMany >= actor.Count() / 2)
                                {
                                    for (int i = 0; i < selectMany; i++)
                                    {
                                        actor[rand.Next(i, actor.Count())].HP -= damage;

                                        Console.WriteLine("You did {0} damage to {1} enemies!!!", damage, selectMany);
                                    }
                                }
                            }
                        }
                    }
                    if (SuperWeaponNum == 3)
                    {
                        if (actor.Count() <= 2)
                        {
                            Console.WriteLine("Superweapon on lockdown. Cannot use yet.");
                        }
                        else
                        {
                            damage = rand.Next(20, 50);
                            int selectMany = rand.Next(0, actor.Count());
                            while (selectMany <= actor.Count() / 2)
                            {
                                selectMany = rand.Next(0, actor.Count());
                            }
                            if (selectMany >= actor.Count() / 2)
                            {
                                for (int i = 0; i < selectMany; i++)
                                {
                                    actor[rand.Next(i, actor.Count())].HP -= damage;

                                    Console.WriteLine("You did {0} damage to {1} enemies!!!", damage, selectMany);
                                }
                            }
                        }
                    }
                    if (SuperWeaponNum == 4)
                    {
                        int countDown = roundCounter;
                        if (countDown > 10)
                        {
                            Console.WriteLine("You did 100% damage to {0} enemies!!!", actor.Count());
                            actor.Clear();
                            countDown -= 10;
                            roundCounter = 1;
                        }
                        else
                        {
                            Console.WriteLine("Nuclear missile will be ready to launch in T{0} combat rounds.", countDown - 10);
                        }

                    }
                    break;
                case AttackType.SupplyDrop:

                    int supplyDrop = 0;
                    int numOfDrops = 0;
                    int newBullets = 0;

                    Console.WriteLine("\nHow many supply drops would you like to call? \n       1 Drop: $5");
                    Console.WriteLine("   (Select: 5, 10, 15, 20, 25 etc.)");
                    int.TryParse(Console.ReadLine(), out supplyDrop);

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    //if input is a number
                    if (supplyDrop != 0)
                    {
                        //if they didn't type in too much money
                        if (supplyDrop % 5 == 0)
                        {

                            //and if input is divisible by 5
                            if (supplyDrop <= Money)
                            {
                                //subtract money
                                Money = Money - supplyDrop;
                                //total number of supply drops = input / 5
                                numOfDrops = supplyDrop / 5;

                                while (numOfDrops > 0)
                                {
                                    int freshBullets = rand.Next(5, 10);
                                    //random number of bullets awarded in bulk
                                    newBullets += freshBullets;
                                    numOfDrops--;
                                }

                                Console.WriteLine("\nYou purchased {0} bullets.", newBullets);
                                this.HP += newBullets;
                                newBullets = 0;

                            }
                            else if (supplyDrop > Money)
                            {
                                //you are cheating!
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("You don't have that kind of cash!");
                            }

                        }
                        //if input is too high

                        //if input is not divisible by 5
                        else if (supplyDrop % 5 != 0)
                        {
                            //5, 10, 15, are valid inputs only
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("That isn't in $5 increments.");
                        }

                        //bullets added to pool
                        //how many bullets are awarded
                    }
                    //subtract cost



                    break;

            }

        }
        private AttackType ChooseAttack()
        {
            Console.WriteLine("Please select an attack type: ");

            string attack = Console.ReadLine();
            switch (attack)
            {
                case "1":
                    return AttackType.MachineGun;
                case "2":
                    return AttackType.Pistol;
                case "3":
                    return AttackType.SuperWeapon;
                case "4":
                    return AttackType.SupplyDrop;
                default:
                    Console.WriteLine("Please enter an option 1-4: ");
                    break;
            }
            return AttackType.Pistol;
        }
        
    }

    public class Game
    {

        public int citiesLost = 0;
        public int citiesCleared = 0;
        public int levelCounter = 1;
        public bool keepPlaying = true;
        public Player PlayerDude { get; set; }
        public List<Actor> Enemies { get; set; }
        public List<Actor> Players { get; set; }
        public Enemy BadGuy { get; set; }
        public int currentHealth = 100;
        int killCount = 0;
        int previousKillCount = 0;
        public int number { get; set; }


        public Game()
        {

            Console.WindowHeight = 45;
            this.Enemies = new List<Actor>();
            for (int i = 0; i < levelCounter; i++)
            {
                Enemies.Add(new Enemy("Replicant " + (i + 1), 50, 0));
            }
            this.Players = new List<Actor>();


            Console.Write("Please enter your name: ");

            string userName = Console.ReadLine();
            Console.WriteLine("Please select your Super Weapon!");
            Console.WriteLine(@"
Select your Super Weapon: 
1. Katana (default): High damage to single target
2. Bazooka         : Low damage to over half targets, half targets removed from combat for 5 rounds
                     Usable at level 3
3. AirStrike       : High damage to over half targets, no secondary effect
                     Usable at level 3
4. Nuke            : Kills all targets, Useable every 10 rounds");
            int number = 0;
            while (number == 0)
            {
                Console.WriteLine("That isn't a valid option, try again!");
                int.TryParse(Console.ReadLine(), out number);
                
            }
            while (number > 4)
            {
                Console.WriteLine("That isn't a valid option, try again!");
                int.TryParse(Console.ReadLine(), out number);

            }
            for (int i = 0; i < 1; i++)
            {
                Players.Add(new Player(userName, 100, 50, number));
            }

        }



        public void DisplayCombatInfo()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(String.Format(@"
Bullets are your HP's! Each attack uses bullets so be careful!
You earn $25 for each enemy killed! Use $ to buy more ammo!

Replicants are infesting the city!   .......  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
1. Automatic Dispersion Pistol (ADP) ....... @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                     ....... @@@@@@@@@@ oo o@@@@o @@@@@@@@@oooo
2. 45 Caliber Anti-Matter Pistol     ....... @@@ @a0000000000000000a  a00000a00
                                     ....... @@@@ 0000000000000000000 000000000
3. Super Weapon  {0}                   ........ @@ 0000 0000000000000000000000000
                                     ........ @@@ 0000 000000000000000000000000
4. Supply Drop (5$)                  ........ @@@ 000 0000000000000000000000000
                                     ........ @@@ 000 0000000000000000000000000
                                     ......... @@@ 00 00000000000 00000 0000000
                                     ...... 00;.                 aaaaa a       
                                     .......0000 00000ta        /00000 000\  
                                     .......`000 000000000000mn000000 0000mn000
  ,-.__________________,======= ,    ....... 000 00000000000000000000 000000000
[ (   _  _  _ )_______)  \\\\\ ((t   ........ 00 00000000000000000000 000000000
  /================.-.______,--'_\   ......... 0 0000000000000000 00000 0000000
  \_,__,_________\     [ ]    /      ........... 0a  00000000000 0000000 000000
            \ (   )) )   o   (       ............ 000a00000000000mm   mm0000000
             \ \____/    \    \      ............ 0000 000000000000000000000000
              ' ===''\    \    \     ............  000000000000            0000
                      \    \    \    ......        a 000000000m0000000000000000
                       )____\   |    .....      ,' 00a 000000000          00000
                       ) __, __,'    ...      ,'   0000a 0000000000000000000000
                        '--''        .      ,'     `00000a 00000000000000000000", number));
            HeadsUpDisplay();
            foreach (var baddie in Enemies)
            {
                Console.WriteLine(baddie.Name);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(Players[0].Name);

        }

        public void PlayGame()
        {
            while (keepPlaying == true)
            {
                while (Players[0].IsAlive && Enemies.Any(x => x.IsAlive))
                {

                    DisplayCombatInfo();
                    Players[0].Attack(Enemies);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    //attack player
                    DisplayCombatInfo();
                    foreach (var baddies in Enemies)
                    {
                        baddies.Attack(Players);
                    }
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    DisplayCombatInfo();
                    killCount = Enemies.Where(x => x.HP == 0).Count();
                    if (killCount > previousKillCount)
                    {
                        Players[0].Money += (killCount - previousKillCount) * 25;
                        previousKillCount = killCount;
                    }


                }

                if (Players[0].IsAlive)
                {
                    Console.WriteLine("You Win!!");
                    PlayAgain();
                }
                else if (Enemies.Any(x => x.IsAlive))
                {
                    Console.WriteLine("You Lose");
                    PlayAgain();
                }
            }
        }

        public void PlayAgain()
        {
            Console.WriteLine("Time to save another city! Y/N");
            string continueGame = Console.ReadLine().ToUpper();
            switch (continueGame)
            {
                case "Y":

                    previousKillCount = 0;
                    killCount = 0;
                    if (Players[0].HP <= 0)
                    {

                        Players[0].HP = 100;
                    }
                    Enemies.Clear();

                    levelCounter++;
                    for (int i = 0; i < levelCounter; i++)
                    {
                        Enemies.Add(new Enemy(string.Format("Replicant {0}", i), 50, 0));
                    }

                    break;
                case "N":
                    Console.WriteLine("The end of the world was inevitable. ");
                    keepPlaying = false;
                    GameOverAnimation();
                    break;
                default:
                    DisplayCombatInfo();
                    Console.WriteLine("Please select either Y or N");
                    break;

            }

        }

        public void HeadsUpDisplay()
        {
            //keeps track of """"""
            string bulletHud = "";
            string moneyHud = "";
            string badGuy = null;
            string badGuyHP = null;
            //indentation count
            int hudReset = 0;

            //cities count and implants status
            //displays current level
            Console.WriteLine("                             Level: {0}\n", levelCounter);



            Console.WriteLine("                    Cities Cleared: {0}", citiesCleared);
            Console.WriteLine("                       Cities Lost: {0}\n", citiesLost);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < Enemies.Count(); i++)
            {
                badGuy = badGuy + string.Format("{0}.(⌐■_■)  ", i + 1);
                if (Enemies[i].HP <= 0)
                {
                    Enemies[i].HP = 0;
                }
                badGuyHP = badGuyHP + string.Format(" HP: {0}  ", Enemies[i].HP);
                hudReset++;
                if(hudReset == 7)
                {
                    badGuyHP = badGuyHP + "\n";
                    hudReset = 0;
                }


            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;


            //scroll through and ad """"" to bullets hud
            for (int i = 0; i < Players[0].HP; i += 2)
            {
                //add " to string
                bulletHud = bulletHud + "\"";
                //onlhy print 25 per line
                hudReset++;
                if (hudReset == 25)
                {
                    bulletHud = bulletHud + "\n";
                    hudReset = 0;
                }
            }
            //build money hud
            hudReset = 0;
            for (int i = 0; i < Players[0].Money; i += 2)
            {
                moneyHud = moneyHud + "\"";
                hudReset++;
                if (hudReset == 25)
                {
                    moneyHud = moneyHud + "\n";
                    hudReset = 0;
                }
            }
            hudReset = 0;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Replicants Remaining: " + badGuy);
            Console.WriteLine("Replicant Health    : {0}  ", badGuyHP);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Bullets Left: {0}\n{1}", Players[0].HP, bulletHud);
            Console.WriteLine("Money Left: ${1}\n{0}", moneyHud, Players[0].Money);


        }
        public void GameOverAnimation()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int loop = 0;
            while (loop < 10)
            {
                Console.Clear();


                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
 ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  
░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
 ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
      ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     
                                                     ░        
");
                Thread.Sleep(200);
                Console.Clear();


                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"
");
                Thread.Sleep(200);
                loop++;

            }
        }
    }
}
