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
}