using System.Collections.Generic;
using TeppichsAttributes.Data;
using UnityEngine;

namespace Reign.TurnProgress
{
    [CreateAssetMenu(menuName = "Create AllTheCompanyDatas", fileName = "AllTheCompanyDatas", order = 0)]
    public class AllTheCompanyDatas : ScriptableObject
    {
        public List<AttributeConfig> companyAttributeConfigs = new();
    }
}