using Reign.TurnProgress;
using TeppichsAttributes.Attributes;

namespace Reign.Companies
{
    public class Company
    {
        private AttributeContainer qualities;
        private QualityDataHolder  qualityDataHolder;

        public Company(AttributeContainer qualities, QualityDataHolder qualityDataHolder)
        {
            this.qualities         = qualities;
            this.qualityDataHolder = qualityDataHolder;
        }

        private void ResetPools() { }

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

        #endregion
    }
}