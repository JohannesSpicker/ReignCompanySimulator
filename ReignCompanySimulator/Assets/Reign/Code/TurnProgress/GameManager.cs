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
        [SerializeField] private QualityDataHolder  qualityDataHolder;
        [SerializeField] private AllTheCompanyDatas allTheCompanyDatas;

        private List<Company> companies = new();

        private bool gameIsRunning = true;

        private ReignTurnIterator turnIterator;

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