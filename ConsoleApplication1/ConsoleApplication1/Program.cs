using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.PlayGame();
        }
    }

    public abstract class Actor
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public bool IsAlive { get; set; }

        public Actor(string name, int hP)
        {
            this.Name = name;
            this.HP = hP;
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

        }
    }
    public class Player : Actor
    {
        public enum AttackType
        {
            MachineGun,
            Pistol,
            Katana,
            SupplyDrop
        }

        public Player(string name, int hP)
            : base(name, hP)
        {

        }

        public override void Attack(Actor actor)
        {

        }
        private AttackType ChooseAttack()
        {
            Console.WriteLine("Please choose a type of attack: ");

            return AttackType.Pistol;
        }
    }

    public class Game
    {
        public Player PlayerDude { get; set; }
        public Enemy BadGuy { get; set; }

        public Game()
        {

            this.PlayerDude = new Player("Bill", 100);
            this.BadGuy = new Enemy("Murray", 100);
        }
        public void DisplayCombatInfo()
        {

        }
        public void PlayGame()
        {
            while (this.PlayerDude.IsAlive && this.BadGuy.IsAlive)
            {
                DisplayCombatInfo();
                this.PlayerDude.Attack(this.BadGuy);
                this.BadGuy.Attack(this.PlayerDude);

            }
            if (this.PlayerDude.IsAlive)
            {
                Console.WriteLine("You Win!!");
                
            }
            else
            {
                Console.WriteLine("You Lose");
            }
        }
    }
}
