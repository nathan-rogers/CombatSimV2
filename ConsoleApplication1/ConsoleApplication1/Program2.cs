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
        public Actor(string name, int hP)
        {
            this.Name = name;
            this.HP = hP;


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


        public virtual void Attack(Actor actor)
        {

        }
    }

    public class Enemy : Actor
    {
        public Enemy(string name, int hP)
            : base(name, hP)
        {

        }

        public override void Attack(Actor actor)
        {
            int damage = 0;
            //see if enemy attacks
            if (rand.Next(0, 100) < 80)
            {
                //enemy atk damage
                damage = rand.Next(10, 20);
                actor.HP = actor.HP - damage;
                Console.WriteLine("Repliant 1 did {0} damage!", damage);
            }
            //random enemy heal
            if (rand.Next(0, 100) < 15)
            {
                damage = rand.Next(0, 10);
                this.HP = HP + damage;
            }
        }
    }
    public class Player : Actor
    {
        Game enemyListGame = new Game();
        //player money for using supply drops
        public int Money { get; set; }
        public enum AttackType
        {
            MachineGun,
            Pistol,
            Katana,
            SupplyDrop
        }

        public Player(string name, int hP, int money)
            : base(name, hP)
        {
            this.Money = money;
        }

        public override void Attack(Actor actor)
        {
            // player damage variable

            int damage = 0;
            int selectedEnemy = SelectEnemy(enemyListGame.Enemies);
            if (selectedEnemy > 0 && selectedEnemy < enemyListGame.Enemies.Count())
            {


                switch (ChooseAttack())
                {
                    //if machine gun
                    case AttackType.MachineGun:
                        //if hits
                        if (rand.Next(0, 100) < 70)
                        {

                            damage = rand.Next(0, 40);
                            enemyListGame.Enemies[selectedEnemy - 1].HP -= damage;
                            Console.WriteLine("You did {0} damage with SMG!", damage);

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("You missed!");
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                        }
                        break;
                    //if pistol 
                    case AttackType.Pistol:
                        actor.HP = actor.HP - rand.Next(10, 25);
                        enemyListGame.Enemies[selectedEnemy - 1].HP -= damage;
                        break;
                    case AttackType.Katana:
                        if (rand.Next(0, 100) < 35)
                        {
                            damage = rand.Next(25, 100);
                            enemyListGame.Enemies[selectedEnemy - 1].HP -= damage;
                            Console.WriteLine("You did {0} damage with Katana!", damage);
                        }
                        break;
                    case AttackType.SupplyDrop:
                        if (Money > 0)
                        {
                            Money = Money - 5;
                            damage = rand.Next(10, 20);
                            this.HP = this.HP + damage;
                            Console.WriteLine("You healed for {0} HP!", damage);
                        }
                        else
                        {
                            Console.WriteLine("You do not have enough money. Try another selection.");
                            ChooseAttack();
                        }
                        break;
                }
            }

        }
        private AttackType ChooseAttack()
        {
            Console.WriteLine("Please select which target to attack: ");
            Game enemyGame = new Game();

            string attack = Console.ReadLine();
            switch (attack)
            {
                case "1":
                    return AttackType.MachineGun;
                case "2":
                    return AttackType.Pistol;
                case "3":
                    return AttackType.Katana;
                case "4":
                    return AttackType.SupplyDrop;
                default:
                    Console.WriteLine("Please enter an option 1-4: ");
                    break;
            }
            return AttackType.Pistol;
        }
        public int SelectEnemy(List<Enemy> enemies)
        {
            Console.WriteLine("Select enemy to attack: ");
            foreach (var baddie in enemies)
            {

            }
            int baddieSelector = int.Parse(Console.ReadLine());
            return baddieSelector;
        }
        public Actor ChooseTarget(List<Actor> listOfEnemies)
        {
            Player bob = player.;
            return bob;
        }
    }

    public class Game
    {
        public int citiesLost = 0;
        public int citiesCleared = 0;
        public int levelCounter = 1;
        public bool keepPlaying = true;
        public Player PlayerDude { get; set; }
        public List<Enemy> Enemies { get; set; }
        public Enemy BadGuy { get; set; }
        public int currentHealth = 100;
        public Game()
        {
            
            this.Enemies = new List<Enemy>();
            for (int i = 0; i < levelCounter; i++)
            {
                Enemies.Add(new Enemy("Replicant " + (i + 1), 50));
            }
            Console.Write("Please enter your name: ");
            string userName = Console.ReadLine();
            this.PlayerDude = new Player(userName, currentHealth, 50);

            

        }



        public void DisplayCombatInfo()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(@"

Replicants are infesting the city!   .......  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
1. Automatic Dispersion Pistol (ADP) ....... @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                     ....... @@@@@@@@@@ oo o@@@@o @@@@@@@@@oooo
2. 45 Caliber Anti-Matter Pistol     ....... @@@ @a0000000000000000a  a00000a00
                                     ....... @@@@ 0000000000000000000 000000000
3. Resupply Drop                     ........ @@ 0000 0000000000000000000000000
                                     ........ @@@ 0000 000000000000000000000000
                                     ........ @@@ 000 0000000000000000000000000
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
                        '--''        .      ,'     `00000a 00000000000000000000");
            HeadsUpDisplay();
            foreach (var baddie in Enemies)
            {
                Console.WriteLine(baddie.Name);
            }
            Console.WriteLine();
            Console.WriteLine(PlayerDude.Name);
            Console.WriteLine("Your HP IS {0} ", PlayerDude.HP);
            Console.WriteLine(PlayerDude.Money);

        }

        public void PlayGame()
        {
            while (keepPlaying == true)
            {
                while (this.PlayerDude.IsAlive && Enemies.Count() > 0)
                {

                    DisplayCombatInfo();
                    this.PlayerDude.Attack(this.BadGuy);
                    foreach (var baddie in Enemies)
                    {
                        baddie.Attack(this.PlayerDude);
                    }


                }
                if (this.PlayerDude.IsAlive)
                {
                    Console.WriteLine("You Win!!");
                    PlayAgain();
                }
                else if (this.BadGuy.IsAlive)
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
                    BadGuy.HP = 50;
                    if (PlayerDude.HP <= 0)
                    {

                        PlayerDude.HP = 100;
                    }

                    levelCounter++;
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
            //indentation count
            int hudReset = 0;

            //cities count and implants status
            //displays current level
            Console.WriteLine("                             Level: {0}\n", levelCounter);



            Console.WriteLine("                    Cities Cleared: {0}", citiesCleared);
            Console.WriteLine("                       Cities Lost: {0}\n", citiesLost);



            //scroll through and ad """"" to bullets hud
            for (int i = 0; i < PlayerDude.HP; i += 2)
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
            for (int i = 0; i < PlayerDude.Money; i += 2)
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
            Console.WriteLine("Bullets Left: {0}\n{1}", PlayerDude.HP, bulletHud);
            Console.WriteLine("Money Left: ${1}\n{0}", moneyHud, PlayerDude.Money);

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
