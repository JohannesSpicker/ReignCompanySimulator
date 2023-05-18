using System.Collections.Generic;
using System.Linq;
using Reign.Companies;
using Sirenix.Utilities;
using TeppichsTurns.Iterators;
using UnityEngine;

namespace Reign.TurnProgress
{
    public class ReignTurnIterator : CurrentTurnIterator<Company>
    {
        private List<Company> currentRound;
        private int           roundCounter;
        private int           turnCounter;

        public ReignTurnIterator(List<Company> actors) : base(actors) { }

        public override Company GetNextActor()
        {
            if (currentTurn.IsNullOrEmpty())
            {
                Debug.Log($"Starting turn {++turnCounter}.");
                roundCounter = 0;
                StartTurn();
            }

            if (currentRound.IsNullOrEmpty())
            {
                Debug.Log($"Starting round {++roundCounter} in turn {turnCounter}.");
                currentRound = currentTurn.ToList();
            }

            Company nextActor = currentRound.First();
            currentRound.Remove(nextActor);

            if (!nextActor.CanDoTurn) //TODO: if this one can't do anything, find another actor
                currentTurn.Remove(nextActor);

            return nextActor;
        }

        protected override void StartTurn()
        {
            foreach (Company company in actors.Where(c => c.Sovereignty.Value < 1).ToList())
            {
                Debug.Log($"Removing company {company.name} due to lack of Sovereignty.");
                RemoveActor(company);
            }

            if (actors.Count < 2)
            {
                GameManager.gameIsRunning = false;

                return;
            }

            foreach (Company company in actors)
                company.ResetPools();

            base.StartTurn();
        }
    }
}