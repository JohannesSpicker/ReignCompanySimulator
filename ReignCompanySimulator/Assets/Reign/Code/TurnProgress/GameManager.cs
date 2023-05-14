using System.Collections;
using System.Collections.Generic;
using Reign.Companies;
using TeppichsAttributes.Data;
using UnityEngine;

namespace Reign.TurnProgress
{
    /// <summary>
    ///     This is the game loop
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static bool          gameIsRunning = true;
        public static List<Company> companies     = new();

        [SerializeField] private QualityDataHolder  qualityDataHolder;
        [SerializeField] private AllTheCompanyDatas allTheCompanyDatas;

        private ReignTurnIterator turnIterator;

        public static QualityDataHolder QualityDataHolder { get; private set; }

        public static float CompanyTickInSeconds => 1f;

        private void Awake() => QualityDataHolder = qualityDataHolder;

        private void Start()
        {
            foreach (AttributeConfig companyConfig in allTheCompanyDatas.companyAttributeConfigs)
            {
                Company company = CompanyCreator.CreateCompany(companyConfig, qualityDataHolder);
                companies.Add(company);
            }

            turnIterator = new ReignTurnIterator(companies);

            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            while (gameIsRunning && Application.isPlaying)
                yield return StartCoroutine(turnIterator.GetNextActor().DoTurn());
        }
    }
}