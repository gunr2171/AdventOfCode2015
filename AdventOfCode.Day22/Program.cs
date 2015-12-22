using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day22
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public static class Processor
    {
        public static bool SimulateFight(FighterStats initialPlayerStats, BossStats initialBossStats, List<Spell> playerSpellList)
        {
            throw new NotImplementedException();

            var activeEffects = new List<Effect>();
            bool playerIsAttacking = true;
            var currentSpellIndex = 0;

            var currentFightStats = new FightRoundResults() { PlayerStats = new FighterStats(initialPlayerStats), BossStats = new BossStats(initialBossStats) };

            while (currentFightStats.PlayerStats.HitPoints > 0 && currentFightStats.BossStats.HitPoints > 0)
            {
                //if it's the player's turn, check if you ran out of spells
                if (playerIsAttacking && currentSpellIndex == playerSpellList.Count)
                {
                    //player can't cast a spell, dies
                    currentFightStats.PlayerStats.HitPoints = -1;
                    break;
                }

                SimulateRound(activeEffects, playerIsAttacking, currentFightStats, playerSpellList[currentSpellIndex]);
                currentSpellIndex += 1;
            }
        }

        private static void SimulateRound(List<Effect> activeEffects, bool playerIsAttacking, FightRoundResults currentStats, Spell spellToCast)
        {
            //start of round, apply effects
            foreach (var effect in activeEffects)
            {
                throw new NotImplementedException();
            }

            if (playerIsAttacking)
            {
                //can you cast the spell?
                if (spellToCast.ManaCost > currentStats.PlayerStats.Mana)
                {
                    //you don't have enough to cast the spell, die
                    currentStats.PlayerStats.HitPoints = -1;
                    return;
                }

                //cast the spell
                currentStats.PlayerStats.Mana -= spellToCast.ManaCost;
                currentStats.PlayerStats.HitPoints += spellToCast.Healing;
                currentStats.BossStats.HitPoints = CalculateNewHitPointsAfterAttack(currentStats.BossStats.HitPoints, spellToCast.Damage, currentStats.BossStats.Armor);
            }
            else
            {
                currentStats.PlayerStats.HitPoints = CalculateNewHitPointsAfterAttack(currentStats.PlayerStats.HitPoints, currentStats.BossStats.Attack, currentStats.PlayerStats.Armor);
            }
        }

        public static int CalculateNewHitPointsAfterAttack(int startingHitPoints, int attackerAttackValue, int defenderArmorValue)
        {
            var netDamage = attackerAttackValue - defenderArmorValue;

            if (netDamage <= 0)
                netDamage = 1;

            var newHP = startingHitPoints - netDamage;
            return newHP;
        }

        public static IEnumerable<Spell> EnumAllSpells()
        {
            yield return new Spell() { Name = "Magic Missile", ManaCost = 53, Damage = 4 };
            yield return new Spell() { Name = "Drain", ManaCost = 73, Damage = 2, Healing = 2 };

            yield return new Spell()
            {
                Name = "Shield",
                ManaCost = 113,
                Effect = new Effect
                {
                    ActiveTurns = 6,
                    OnEffectCast = (cs) => cs.PlayerStats.Armor += 7,
                    OnRoundStart = null, //nothing to do on round start
                    OnEffectExpire = (cs) => cs.PlayerStats.Armor -= 7
                }
            };
            yield return new Spell()
            {
                Name = "Poison",
                ManaCost = 173,
                Effect = new Effect
                {
                    ActiveTurns = 6,
                    OnEffectCast = null,
                    OnRoundStart = (cs) => cs.BossStats.HitPoints = Processor.CalculateNewHitPointsAfterAttack(cs.BossStats.HitPoints, 3, cs.BossStats.Armor),
                    OnEffectExpire = null,
                }
            };
            yield return new Spell()
            {
                Name = "Recharge",
                ManaCost = 229,
                Effect = new Effect
                {
                    ActiveTurns = 5,
                    OnEffectCast = null,
                    OnRoundStart = (cs) => cs.PlayerStats.Mana += 101,
                    OnEffectExpire = null,
                }
            };
        }
    }

    public class FighterStats
    {
        public int HitPoints { get; set; }
        public int Armor { get; set; }
        public int Mana { get; set; }

        public FighterStats() { }

        public FighterStats(FighterStats copyStats)
        {
            HitPoints = copyStats.HitPoints;
            Armor = copyStats.Armor;
            Mana = copyStats.Mana;
        }

        public FighterStats(int hp, int armor, int mana)
        {
            HitPoints = hp;
            Armor = armor;
            Mana = mana;
        }
    }

    public class BossStats : FighterStats
    {
        public int Attack { get; set; }

        public BossStats(BossStats copyStats):base(copyStats)
        {
            Attack = copyStats.Attack;
        }

        public BossStats(int hp, int armor, int mana, int attack)
        {
            Attack = attack;
        }
    }
    
    public class FightRoundResults
    {
        public FighterStats PlayerStats { get; set; }
        public BossStats BossStats { get; set; }
    }

    public class Spell
    {
        public string Name { get; set; }
        public int ManaCost { get; set; }
        public int Damage { get; set; }
        public int Healing { get; set; }

        public Effect Effect { get; set; }
    }

    public class Effect
    {
        public int ActiveTurns { get; set; }

        public Action<FightRoundResults> OnEffectCast { get; set; }
        public Action<FightRoundResults> OnRoundStart { get; set; }
        public Action<FightRoundResults> OnEffectExpire { get; set; }
    }
}
