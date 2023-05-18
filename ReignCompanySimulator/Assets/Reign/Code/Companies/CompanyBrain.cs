using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reign.Companies.CompanyActions;
using Reign.TurnProgress;
using TeppichsTools.Data;
using TeppichsTools.Math.Randomness;

namespace Reign.Companies
{
    public sealed class CompanyBrain
    {
        private readonly List<CompanyAction> actions = new()
        {
            new CompanyActionImproveTheCulture(),
            new CompanyActionRiseInStature(),
            new CompanyActionTrainAndLevyTroops(),
            new CompanyActionAttack()
        };

        private readonly Company company;

        private readonly CompanyAction doNothingAction = new DoNothingCompanyAction();

        public CompanyBrain(Company company) { this.company = company; }

        public IEnumerator DoAnAction()
        {
            if (CanDoSomething())
                yield return actions.Where(action => action.IsViable(company)).ToList().Shuffle().First()
                                    .ProcessAction(company,
                                                   GameManager.companies
                                                       [ThreadSafeRandom.ThisThreadsRandom.Next(GameManager.companies.Count)]);
            else
                yield return doNothingAction.ProcessAction(company, null);
        }

        public bool CanDoSomething() => actions.Any(action => action.IsViable(company));
    }
}