using Reign.TurnProgress;
using TeppichsAttributes.Attributes;

namespace Reign.Companies
{
    public class Company
    {
        private AttributeContainer qualities;

        public Company(AttributeContainer qualities) { this.qualities = qualities; }

        private void ResetPools() { }

        private void Listen()
        {
            GeneralTurnMessages.OnStartOfTurn += ResetPools;
            GeneralTurnMessages.OnEndOfTurn   += CheckForDeath;
        }

        private void Unlisten()
        {
            GeneralTurnMessages.OnStartOfTurn -= ResetPools;
            GeneralTurnMessages.OnEndOfTurn   -= CheckForDeath;
        }

        private void CheckForDeath() { }
    }
}