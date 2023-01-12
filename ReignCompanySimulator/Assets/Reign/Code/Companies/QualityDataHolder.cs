using System;
using TeppichsAttributes.Data;
using UnityEngine;

namespace Reign.Companies
{
    [Serializable, CreateAssetMenu(menuName = "ReignCompanySimulator/Companies/QualityHolder", order = 2)]
    public class QualityDataHolder : ScriptableObject
    {
        [SerializeField] public AttributeData might;
        [SerializeField] public AttributeData treasure;
        [SerializeField] public AttributeData influence;
        [SerializeField] public AttributeData territory;
        [SerializeField] public AttributeData sovereignty;

        [SerializeField] public ResourceData remainingMight;
        [SerializeField] public ResourceData remainingTreasure;
        [SerializeField] public ResourceData remainingInfluence;
        [SerializeField] public ResourceData remainingTerritory;
        [SerializeField] public ResourceData remainingSovereignty;
    }
}