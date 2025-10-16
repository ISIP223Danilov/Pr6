using System;
using System.Collections.Generic;
using System.Threading;

namespace GIGACHADGAYDANGEROUS
{

    public class Item
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Item(string name, int attack = 0, int defense = 0)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }
    }

    public class Weapon : Item
    {
        public Weapon(string name, int attack) : base(name, attack, 0) { }
    }

    public class Armor : Item
    {
        public Armor(string name, int defense) : base(name, 0, defense) { }
    }
    
    public class Enemy
    {
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public double CritChance { get; set; }
        public double FreezeChance { get; set; }
        public bool IgnoreDefense { get; set; }

        public Enemy(string name, int hp, int attack, int defense,
                    double critChance = 0, double freezeChance = 0, bool ignoreDefense = false)
        {
            Name = name;
            MaxHP = hp;
            HP = hp;
            Attack = attack;
            Defense = defense;
            CritChance = critChance;
            FreezeChance = freezeChance;
            IgnoreDefense = ignoreDefense;
        }

        public bool IsAlive()
        {
            return HP > 0;
        }
    }
    
    public class Player
    {
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public Weapon Weapon { get; set; }
        public Armor Armor { get; set; }
        public bool Frozen { get; set; }

        public Player()
        {
            MaxHP = 100;
            HP = 100;
            Weapon = new Weapon("Кулаки", 5);
            Armor = new Armor("стринги", 2);
            Frozen = false;
        }

        public int TotalAttack => Weapon.Attack;
        public int TotalDefense => Armor.Defense;

        public void TakeDamage(int damage)
        {
            HP = Math.Max(0, HP - damage);
        }

        public void Heal()
        {
            HP = MaxHP;
        }

        public bool IsAlive()
        {
            return HP > 0;
        }
    }
    
    public class Game
    {
        private Player player;
        private int turnCount;
        private Random random;
        private List<Enemy> enemies;
        private List<Enemy> bosses;
        private List<Weapon> weapons;
        private List<Armor> armors;

        public Game()
        {
            player = new Player();
            turnCount = 0;
            random = new Random();

            InitializeEnemies();
            InitializeBosses();
            InitializeItems();
        }

        private void InitializeEnemies()
        {
            enemies = new List<Enemy>
            {
                new Enemy("Гоблин", 30, 8, 3, critChance: 0.2),
                new Enemy("Скелет", 25, 10, 2, ignoreDefense: true),
                new Enemy("Маг", 20, 12, 1, freezeChance: 0.25)
            };
        }

        private void InitializeBosses()
        {
            bosses = new List<Enemy>
            {
                new Enemy("ВВГ (Гоблин)", 60, 12, 4, critChance: 0.3),
                new Enemy("Ковальский (Скелет)", 63, 13, 3, ignoreDefense: true),
                new Enemy("Архимаг C++ (Маг)", 36, 19, 1, freezeChance: 0.35),
                new Enemy("Пестов С-- (Скелет)", 33, 18, 1, ignoreDefense: true, freezeChance: 0.4)
            };
        }

        private void InitializeItems()
        {
            weapons = new List<Weapon>
            {
                new Weapon("Деревянный меч", 8),
                new Weapon("Стальной меч", 15),
                new Weapon("Огненный посох", 20),
                new Weapon("Ледяной кинжал", 12),
                new Weapon("Топор воина", 18)
            };

            armors = new List<Armor>
            {
                new Armor("Кожаная броня", 5),
                new Armor("Кольчуга", 10),
                new Armor("Латные доспехи", 15),
                new Armor("Магические одежды", 8),
                new Armor("Драконий доспех", 20)
            };
        }

        public void RandomEvent()
        {
            turnCount++;

            Console.WriteLine($"\n=== Ход {turnCount} ===");
            Console.WriteLine($"Здоровье: {player.HP}/{player.MaxHP}");
            Console.WriteLine($"Оружие: {player.Weapon.Name} (Атака: {player.TotalAttack})");
            Console.WriteLine($"Доспехи: {player.Armor.Name} (Защита: {player.TotalDefense})");

            if (turnCount % 10 == 0)
            {
                var boss = bosses[random.Next(bosses.Count)];
                Console.WriteLine($"\n  Появляется босс: {boss.Name}!");
                Combat(boss);
                return;
            }

            if (random.NextDouble() < 0.5)
            {
                ChestEvent();
            }
            else
            {
                var enemy = enemies[random.Next(enemies.Count)];
                Console.WriteLine($"\n  Появляется враг: {enemy.Name}!");
                Combat(enemy);
            }
        }

        private void ChestEvent()
        {
            Console.WriteLine("\n Вы нашли сундук!");
            var itemTypes = new List<string> { "weapon", "armor", "potion" };
            var itemType = itemTypes[random.Next(itemTypes.Count)];

            if (itemType == "potion")
            {
                Console.WriteLine("Вы нашли лечебное зелье! Здоровье полностью восстановлено.");
                player.Heal();
            }
            else if (itemType == "weapon")
            {
                var newWeapon = weapons[random.Next(weapons.Count)];
                Console.WriteLine($"Вы нашли оружие: {newWeapon.Name} (Атака: {newWeapon.Attack})");
                Console.WriteLine($"Ваше текущее оружие: {player.Weapon.Name} (Атака: {player.TotalAttack})");

                Console.Write("Взять новое оружие? (д/н): ");
                var choice = Console.ReadLine()?.ToLower();

                if (choice == "д")
                {
                    player.Weapon = newWeapon;
                    Console.WriteLine("Вы экипировали новое оружие!");
                }
                else
                {
                    Console.WriteLine("Вы оставили оружие в сундуке.");
                }
            }
            else
            {
                var newArmor = armors[random.Next(armors.Count)];
                Console.WriteLine($"Вы нашли доспехи: {newArmor.Name} (Защита: {newArmor.Defense})");
                Console.WriteLine($"Ваши текущие доспехи: {player.Armor.Name} (Защита: {player.TotalDefense})");

                Console.Write("Взять новые доспехи? (д/н): ");
                var choice = Console.ReadLine()?.ToLower();

                if (choice == "д")
                {
                    player.Armor = newArmor;
                    Console.WriteLine("Вы экипировали новые доспехи!");
                }
                else
                {
                    Console.WriteLine("Вы оставили доспехи в сундуке.");
                }
            }
        }

        private void Combat(Enemy enemy)
        {
            var enemyHP = enemy.HP;
            var playerFrozen = false;
            var defenseActive = false;

            Console.WriteLine($"\nБой с {enemy.Name}!");
            Console.WriteLine($"Здоровье врага: {enemyHP}/{enemy.MaxHP}");

            while (enemyHP > 0 && player.IsAlive())
            {
                
                if (playerFrozen)
                {
                    Console.WriteLine(" Вы заморожены и пропускаете ход!");
                    playerFrozen = false;
                    defenseActive = false;
                }
                else
                {
                    
                    Console.WriteLine("\nВаш ход:");
                    Console.WriteLine("1 - Атаковать");
                    Console.WriteLine("2 - Защищаться");

                    Console.Write("Выберите действие: ");
                    var choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        var damage = player.TotalAttack;
                        enemyHP = Math.Max(0, enemyHP - damage);
                        Console.WriteLine($"Вы нанесли {damage} урона! Здоровье врага: {enemyHP}/{enemy.MaxHP}");
                        defenseActive = false;
                    }
                    else if (choice == "2")
                    {
                        Console.WriteLine("Вы готовитесь к защите...");
                        defenseActive = true;
                    }
                    else
                    {
                        Console.WriteLine("Неверный выбор, вы пропускаете ход!");
                        defenseActive = false;
                    }
                }

                
                if (enemyHP <= 0)
                {
                    Console.WriteLine($"\n Вы победили {enemy.Name}!");
                    break;
                }

                
                Console.WriteLine($"\nХод {enemy.Name}...");
                Thread.Sleep(1000);

                
                if (defenseActive)
                {
                    if (random.NextDouble() < 0.4) 
                    {
                        Console.WriteLine(" Вы увернулись от атаки!");
                        defenseActive = false;
                        continue;
                    }
                    else
                    {
                      
                        var blockPercent = random.NextDouble() * 0.3 + 0.7;
                        var damageReduction = (int)(player.TotalDefense * blockPercent);
                        Console.WriteLine($" Вы блокируете {damageReduction} урона!");
                    }
                }

               
                var enemyDamage = enemy.Attack;

                if (enemy.CritChance > 0 && random.NextDouble() < enemy.CritChance)
                {
                    enemyDamage *= 2;
                    Console.WriteLine(" Критический удар!");
                }

                int finalDamage;
                if (enemy.IgnoreDefense)
                {
                    finalDamage = enemyDamage;
                    Console.WriteLine(" Враг игнорирует вашу защиту!");
                }
                else if (defenseActive)
                {
                    var blockPercent = random.NextDouble() * 0.3 + 0.7;
                    var damageReduction = (int)(player.TotalDefense * blockPercent);
                    finalDamage = Math.Max(0, enemyDamage - damageReduction);
                    defenseActive = false;
                }
                else
                {
                    finalDamage = Math.Max(0, enemyDamage - player.TotalDefense);
                }

                
                if (finalDamage > 0)
                {
                    player.TakeDamage(finalDamage);
                    Console.WriteLine($"{enemy.Name} наносит вам {finalDamage} урона!");
                    Console.WriteLine($"Ваше здоровье: {player.HP}/{player.MaxHP}");
                }
                else
                {
                    Console.WriteLine("Враг не смог пробить вашу защиту!");
                }

                
                if (enemy.FreezeChance > 0 && random.NextDouble() < enemy.FreezeChance)
                {
                    playerFrozen = true;
                    Console.WriteLine(" Маг замораживает вас! Вы пропустите следующий ход.");
                }

                
                if (!player.IsAlive())
                {
                    Console.WriteLine("\n Вы погибли...");
                    break;
                }
            }

            if (player.IsAlive())
            {
                Console.WriteLine("Бой окончен!");
            }
        }

        public void Start()
        {
            Console.WriteLine("=== ТЕКСТОВАЯ ПОШАГОВАЯ ИГРА-РОГАЛИК ===");
            Console.WriteLine("Добро пожаловать в подземелье!");
            Console.WriteLine("На каждом ходу вас ждет сундук или враг.");
            Console.WriteLine("Каждые 10 ходов появляется босс!");
            Console.WriteLine("Удачи!\n");

            while (player.IsAlive())
            {
                RandomEvent();

                if (player.IsAlive())
                {
                    Console.Write("\nНажмите Enter для продолжения...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"\nИгра окончена! Вы прошли {turnCount} ходов.");
                    break;
                }
            }
        }
    }
}