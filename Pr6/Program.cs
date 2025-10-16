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

    }