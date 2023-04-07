using System.Collections.Generic;
using System.Linq;
using Reign.Contests.Dice;

namespace Reign.Contests.Contests
{
    public abstract class Contest
    {
        public enum WinCondition
        {
            Height,
            Width
        }

        public List<Contestant> contestants = new();
        
        //does it make sense to have a list
        //i have 1 or 2 so far, active and inactive
        //dznamicContest could use multiple
        //ditto opposed actuallz
        //static is clearly just one, but group contests could be a thing
        
        //contra list: gotta call into that. maybe just use a property and gg.
        

        public Contestant activeContestant => contestants[0];
        
        public PassingCondition passingCondition;
        public WinCondition winCondition;
        public int penalties;

        public bool outcome;

        protected Contest(DicePool activeDicePool, PassingCondition passingCondition, WinCondition winCondition,
            int penalties)
        {
            this.passingCondition = passingCondition;
            this.penalties = penalties;
            this.winCondition = winCondition;
            
            this.contestants.Add(new Contestant(activeDicePool));
        }

        public abstract bool DetermineOutcome();
        public abstract void MakeRolls();

        protected static RolledDice RollDice(Contestant contestant, PassingCondition passingCondition,
            WinCondition winCondition, int penalties)
        {
            DicePool dicePool = contestant.dicePool;
            
            for (; 0 < penalties && 0 < dicePool.masterDice; penalties--)
                dicePool.masterDice--;

            if (1 < dicePool.masterDice)
            {
                dicePool.expertDice += dicePool.masterDice - 1;
                dicePool.masterDice = 1;
            }

            for (; 0 < penalties && 0 < dicePool.expertDice; penalties--)
                dicePool.expertDice--;

            for (; 0 < penalties && 0 < dicePool.dice; penalties--)
                dicePool.dice--;

            List<int> rolled = new();

            for (var i = 10; 0 < dicePool.expertDice && 0 < i; i--)
            {
                rolled.Add(i);
                dicePool.expertDice--;
            }

            rolled.AddRange(TeppichsDice.Dice.D10(dicePool.dice + dicePool.expertDice));

            var rolledDice = new RolledDice(rolled);

            if (0 < dicePool.masterDice)
                rolledDice.AddDie(FindBestMasterDieValue());

            return rolledDice;

            int FindBestMasterDieValue()
            {
                var alteredPassingCondition =
                    new PassingCondition(passingCondition.minHeight, passingCondition.minWidth - 1);

                if (winCondition == WinCondition.Height)
                {
                    List<int> candidates = new();

                    if (alteredPassingCondition.minWidth < 2 && rolledDice.TryGetHighestWaste(out var highestWaste))
                        candidates.Add(highestWaste);

                    if (rolledDice.TryGetHighestPassingSet(out var highestSet, alteredPassingCondition))
                        candidates.Add(highestSet.height);

                    if (candidates.Any())
                        return candidates.Max();
                }
                else
                {
                    if (rolledDice.TryGetWidestPassingSet(out var widestSet, alteredPassingCondition))
                        return widestSet.height;

                    if (alteredPassingCondition.minWidth < 2 && rolledDice.TryGetHighestWaste(out var highestWaste))
                        return highestWaste;
                }

                return 10;
            }
        }
    }
}