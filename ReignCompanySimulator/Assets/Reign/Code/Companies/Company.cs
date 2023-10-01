using System.Collections;
using Reign.TurnProgress;
using TeppichsAttributes.Attributes;
using TeppichsTurns.Actors;

namespace Reign.Companies
{
    public class Company : IComparableActor<Company>
    {
        private readonly AttributeContainer qualities;
        private readonly QualityDataHolder  qualityDataHolder;

        private CompanyBrain brain;
        public  string       name = "Some Company";

        public Company(AttributeContainer qualities, QualityDataHolder qualityDataHolder)
        {
            this.qualities         = qualities;
            this.qualityDataHolder = qualityDataHolder;
            brain                  = new CompanyBrain(this);
        }

        private void Subscribe()
        {
            GeneralTurnMessages.OnStartOfTurn += ResetPools;
            GeneralTurnMessages.OnEndOfTurn   += CheckForDeath;
        }

        private void Unsubscribe()
        {
            GeneralTurnMessages.OnStartOfTurn -= ResetPools;
            GeneralTurnMessages.OnEndOfTurn   -= CheckForDeath;
        }

        private void CheckForDeath() { }

        #region Turn

        public bool CanDoTurn => brain.CanDoSomething();

        public IEnumerator DoTurn()
        {
            yield return brain.DoAnAction();
        }

        public int CompareTo(Company other) => Sovereignty.Value.CompareTo(other.Sovereignty.Value);

        public void ResetPools()
        {
            RemainingMight.SetTo(Might.Value);
            RemainingTreasure.SetTo(Treasure.Value);
            RemainingInfluence.SetTo(Influence.Value);
            RemainingTerritory.SetTo(Territory.Value);
            RemainingSovereignty.SetTo(Sovereignty.Value);
        }

        #endregion

        #region Quality Accessors

        public Stat Might       => qualities.GetStat(qualityDataHolder.might);
        public Stat Treasure    => qualities.GetStat(qualityDataHolder.treasure);
        public Stat Influence   => qualities.GetStat(qualityDataHolder.influence);
        public Stat Territory   => qualities.GetStat(qualityDataHolder.territory);
        public Stat Sovereignty => qualities.GetStat(qualityDataHolder.sovereignty);

        public Resource RemainingMight       => qualities.GetResource(qualityDataHolder.remainingMight);
        public Resource RemainingTreasure    => qualities.GetResource(qualityDataHolder.remainingTreasure);
        public Resource RemainingInfluence   => qualities.GetResource(qualityDataHolder.remainingInfluence);
        public Resource RemainingTerritory   => qualities.GetResource(qualityDataHolder.remainingTerritory);
        public Resource RemainingSovereignty => qualities.GetResource(qualityDataHolder.remainingSovereignty);

        public int UseMight()       => UseResource(RemainingMight);
        public int UseTreasure()    => UseResource(RemainingTreasure);
        public int UseInfluence()   => UseResource(RemainingInfluence);
        public int UseTerritory()   => UseResource(RemainingTerritory);
        public int UseSovereignty() => UseResource(RemainingSovereignty);

        private int UseResource(Resource resource)
        {
            if (resource.Value <= 0)
                return 0;

            int remaining = (int)resource.Value;
            resource.Spend(1);

            return remaining;
        }

        #endregion
    }
}