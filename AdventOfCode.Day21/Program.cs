using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Common;
using TCL.Extensions;

namespace AdventOfCode.Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            var winningFights = ItemShop.EnumAllPlayerItemOptions()
                .Select(x => FightSimulator.CalculatePlayerStats(x))
                .Where(x => FightSimulator.SimulateFight(x, new PlayerStats { HitPoints = 104, Damage = 8, Armor = 1 }))
                .OrderBy(x => x.GoldSpentOnItems)
                .ToList();

            var part1Answer = winningFights.Min(x => x.GoldSpentOnItems);
        }
    }

    public static class FightSimulator
    {
        public static PlayerStats CalculatePlayerStats(List<Item> loadout)
        {
            return new PlayerStats()
            {
                Armor = loadout.Sum(x => x.Armor),
                Damage = loadout.Sum(x => x.Damage),
                HitPoints = 100, //player is always 100 HP
                GoldSpentOnItems = loadout.Sum(x => x.Cost),
                Items = loadout
            };
        }

        /// <summary>
        /// Returns true if the player wins the fight.
        /// </summary>
        /// <param name="playerStats"></param>
        /// <param name="bossStats"></param>
        /// <returns></returns>
        public static bool SimulateFight(PlayerStats playerStats, PlayerStats bossStats)
        {
            var hasSW = playerStats.Items.Select(x => x.Name).Contains("Shortsword");
            bool playerIsAttacking = true;

            while (playerStats.HitPoints > 0 && bossStats.HitPoints > 0)
            {
                if (playerIsAttacking)
                {
                    bossStats.HitPoints = CalculateNewHitPointsAfterAttack(bossStats.HitPoints, playerStats.Damage, bossStats.Armor);
                }
                else
                {
                    playerStats.HitPoints = CalculateNewHitPointsAfterAttack(playerStats.HitPoints, bossStats.Damage, playerStats.Armor);
                }

                playerIsAttacking = !playerIsAttacking;
            }

            return playerStats.HitPoints > 0;
        }

        public static int CalculateNewHitPointsAfterAttack(int startingHitPoints, int attackerAttackValue, int defenderArmorValue)
        {
            var netDamage = attackerAttackValue - defenderArmorValue;

            if (netDamage <= 0)
                netDamage = 1;

            var newHP = startingHitPoints - netDamage;
            return newHP;
        }
    }

    public static class ItemShop
    {
        public static IEnumerable<Item> AllShopItems()
        {
            yield return new Item { Name = "Dagger", Cost = 8, Damage = 4, Armor = 0, Type = ItemType.Weapon };
            yield return new Item { Name = "Shortsword", Cost = 10, Damage = 5, Armor = 0, Type = ItemType.Weapon };
            yield return new Item { Name = "Warhammer", Cost = 25, Damage = 6, Armor = 0, Type = ItemType.Weapon };
            yield return new Item { Name = "Longsword", Cost = 40, Damage = 7, Armor = 0, Type = ItemType.Weapon };
            yield return new Item { Name = "Greataxe", Cost = 74, Damage = 8, Armor = 0, Type = ItemType.Weapon };
            yield return new Item { Name = "Leather", Cost = 13, Damage = 0, Armor = 1, Type = ItemType.Armor };
            yield return new Item { Name = "Chainmail", Cost = 31, Damage = 0, Armor = 2, Type = ItemType.Armor };
            yield return new Item { Name = "Splintmail", Cost = 53, Damage = 0, Armor = 3, Type = ItemType.Armor };
            yield return new Item { Name = "Bandedmail", Cost = 75, Damage = 0, Armor = 4, Type = ItemType.Armor };
            yield return new Item { Name = "Platemail", Cost = 102, Damage = 0, Armor = 5, Type = ItemType.Armor };
            yield return new Item { Name = "Damage +1", Cost = 25, Damage = 1, Armor = 0, Type = ItemType.Ring };
            yield return new Item { Name = "Damage +2", Cost = 50, Damage = 2, Armor = 0, Type = ItemType.Ring };
            yield return new Item { Name = "Damage +3", Cost = 100, Damage = 3, Armor = 0, Type = ItemType.Ring };
            yield return new Item { Name = "Defense +1", Cost = 20, Damage = 0, Armor = 1, Type = ItemType.Ring };
            yield return new Item { Name = "Defense +2", Cost = 40, Damage = 0, Armor = 2, Type = ItemType.Ring };
            yield return new Item { Name = "Defense +3", Cost = 80, Damage = 0, Armor = 3, Type = ItemType.Ring };
        }

        public static IEnumerable<List<Item>> EnumAllPlayerItemOptions()
        {
            var weapons = AllShopItems().Where(x => x.Type == ItemType.Weapon);
            var armors = AllShopItems().Where(x => x.Type == ItemType.Armor);
            var rings = AllShopItems().Where(x => x.Type == ItemType.Ring);

            foreach (var weaponOption in weapons.GetKCombs(1)) //must be exactly 1 weapon
            {
                foreach (var armorNumber in Enumerable.Range(0, 2)) //armor can be either 0 or 1
                {
                    foreach (var armorOption in armors.GetKCombs(armorNumber))
                    {
                        foreach (var ringNumber in Enumerable.Range(0, 3)) //ring can be 0, 1, or 2
                        {
                            foreach (var ringOption in rings.GetKCombs(ringNumber))
                            {
                                var loadout = weaponOption
                                    .Union(armorOption)
                                    .Union(ringOption)
                                    .ToList();

                                yield return loadout;
                            }
                        }
                    }
                }
            }
        }
    }

    public class PlayerStats //this class is used by the boss as well
    {
        public int HitPoints { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }

        public int GoldSpentOnItems { get; set; }

        public List<Item> Items { get; set; }

        public PlayerStats()
        {
            Items = new List<Item>();
        }

        public override string ToString()
        {
            return "Gold: {0}, HP: {1}, Damage: {2}, Armor: {3} | ".FormatInline(GoldSpentOnItems, HitPoints, Damage, Armor)
                + Items.ToCSV(", ", x => x.Name);
        }
    }

    public class Item : IComparable, IComparable<Item>
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }

        public ItemType Type { get; set; }

        public int CompareTo(object obj)
        {
            return CompareTo((Item)obj);
        }

        public int CompareTo(Item other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Ring
    }
}
