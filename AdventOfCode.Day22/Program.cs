using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCL.Extensions;

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

                var spellToCast = playerIsAttacking
                    ? playerSpellList[currentSpellIndex]
                    : null;

                SimulateRound(activeEffects, playerIsAttacking, currentFightStats, spellToCast);

                if (playerIsAttacking) //if the player just attacked, switch to the next spell to use
                    currentSpellIndex += 1;

                playerIsAttacking = !playerIsAttacking;
            }

            return currentFightStats.PlayerStats.HitPoints > 0;
        }

        private static void SimulateRound(List<Effect> activeEffects, bool playerIsAttacking, FightRoundResults currentStats, Spell spellToCast)
        {
            //start of round, apply effects
            foreach (var effect in activeEffects.ToList())
            {
                //has the effect expired?
                if (effect.ActiveTurns == 0)
                {
                    if (effect.OnEffectExpire != null)
                        effect.OnEffectExpire(currentStats);
                    activeEffects.Remove(effect);
                    continue;
                }

                //run action on round start if there is one
                if (effect.OnRoundStart != null)
                    effect.OnRoundStart(currentStats);

                //effect has been used this round, reduce turn use
                effect.ActiveTurns -= 1;
            }

            //is the boss dead because of effects?
            if (currentStats.BossStats.HitPoints <= 0)
            {
                return;
            }

            //start the attack phase
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

                if (spellToCast.Damage > 0) //if this spell is intended to attack, attack
                {
                    currentStats.BossStats.HitPoints = CalculateNewHitPointsAfterAttack(currentStats.BossStats.HitPoints, spellToCast.Damage, currentStats.BossStats.Armor);
                }

                if (spellToCast.Effect != null)
                {
                    activeEffects.Add(new Effect(spellToCast.Effect));

                    //if there is an action to run when the effect is casted, run it
                    if (spellToCast.Effect.OnEffectCast != null)
                    {
                        spellToCast.Effect.OnEffectCast(currentStats);
                    }
                }
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

        public override string ToString()
        {
            return "HP: {0}, Armor: {1}, Mana: {2}".FormatInline(HitPoints, Armor, Mana);
        }
    }

    public class BossStats : FighterStats
    {
        public int Attack { get; set; }

        public BossStats(BossStats copyStats)
            : base(copyStats)
        {
            Attack = copyStats.Attack;
        }

        public BossStats(int hp, int armor, int mana, int attack)
            : base(hp, armor, mana)
        {
            Attack = attack;
        }

        public override string ToString()
        {
            return base.ToString() + ", Attack: {0}".FormatInline(Attack);
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

        public override string ToString()
        {
            return "{0}, Mana: {1}, Damage: {2}, Healing: {3}, Effect: {4}"
                .FormatInline(Name, ManaCost, Damage, Healing, Effect != null);
        }
    }

    public class Effect
    {
        public Effect() { }

        public Effect(Effect copyEffect)
        {
            ActiveTurns = copyEffect.ActiveTurns;
            OnEffectCast = copyEffect.OnEffectCast;
            OnRoundStart = copyEffect.OnRoundStart;
            OnEffectExpire = copyEffect.OnEffectExpire;
        }

        public int ActiveTurns { get; set; }

        public Action<FightRoundResults> OnEffectCast { get; set; }
        public Action<FightRoundResults> OnRoundStart { get; set; }
        public Action<FightRoundResults> OnEffectExpire { get; set; }

        public override string ToString()
        {
            return "Turns Left: {0}".FormatInline(ActiveTurns);
        }
    }
}
