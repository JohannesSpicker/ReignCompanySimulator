using System.Collections.Generic;
using System.Linq;
using Reign.Companies;
using Sirenix.Utilities;
using TeppichsTurns.Iterators;

namespace Reign.TurnProgress
{
    public class ReignTurnIterator : CurrentTurnIterator<Company>
    {
        public ReignTurnIterator(List<Company> actors) : base(actors) { }

        public override Company GetNextActor()
        {
            if (currentTurn.IsNullOrEmpty())
                StartTurn();

            Company nextActor = currentTurn.First();
            currentTurn.Remove(nextActor);

            return nextActor;
        }

        protected override void StartTurn()
        {
            foreach (Company company in actors.Where(c => c.Sovereignty.Value < 1).ToList())
                RemoveActor(company);

            foreach (Company company in actors)
                company.ResetPools();

            base.StartTurn();
        }
    }
}