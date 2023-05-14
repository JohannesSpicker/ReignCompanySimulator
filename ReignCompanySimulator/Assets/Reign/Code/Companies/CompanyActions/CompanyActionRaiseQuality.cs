using System.Collections;
using Reign.Contests;
using Reign.Contests.Contests;
using Reign.Contests.Dice;
using Reign.TurnProgress;
using TeppichsAttributes.Attributes;
using UnityEngine;

namespace Reign.Companies.CompanyActions
{
    public abstract class CompanyActionRaiseQuality : CompanyActionWithCost
    {
        protected abstract Stat StatToImprove(Company company);

        public override IEnumerator ProcessAction(Company activeCompany, Company _)
        {
            StaticContest staticContest = new(new DicePool(GetActivePool(activeCompany)),
                                              new PassingCondition((int)StatToImprove(activeCompany).Value, 2),
                                              Contest.WinCondition.Height, 0);

            staticContest.MakeRolls();
            bool success = staticContest.DetermineOutcome();

            if (success)
                StatToImprove(activeCompany).AddToBaseValue(1);

            LogAction(activeCompany, "", success);

            yield return new WaitForSeconds(GameManager.CompanyTickInSeconds);
        }

        //TODO: also implement the temporary version
    }
}