using System;
using TeppichsAttributes.Data;
using UnityEngine;

namespace Reign.Companies
{
    [Serializable, CreateAssetMenu(menuName = "ReignCompanySimulator/Companies/QualityHolder", order = 2)]
    public class QualityDataHolder : ScriptableObject
    {
        [SerializeField] private AttributeData might;
        [SerializeField] private AttributeData treasure;
        [SerializeField] private AttributeData influence;
        [SerializeField] private AttributeData territory;
        [SerializeField] private AttributeData sovereignty;

        [SerializeField] private ResourceData remainingMight;
        [SerializeField] private ResourceData remainingTreasure;
        [SerializeField] private ResourceData remainingInfluence;
        [SerializeField] private ResourceData remainingTerritory;
        [SerializeField] private ResourceData remainingSovereignty;
    }
}