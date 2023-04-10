using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reign.Companies;
using UnityEngine;

namespace Reign.TurnProgress
{
    /// <summary>
    ///     This is the game loop
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private QualityDataHolder qualityDataHolder;
        [SerializeField] private AllTheCompanyDatas allTheCompanyDatas;

        private List<Company> companies = new();

        private bool gameIsRunning = true;

        private void Start()
        {
            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            foreach (var companyConfig in allTheCompanyDatas.companyAttributes)
                companies.Add(CompanyCreator.CreateCompany(companyConfig, qualityDataHolder));

            while (gameIsRunning)
            {
                //ask all the companies to do their turns until they're all finished.
                foreach (var company in companies)
                    yield return StartCoroutine(company.ProcessCompanyTurn());

                foreach (Company company in companies.Where(c => c.Sovereignty.Value < 1).ToList())
                {
                    //then clean up all dead companies
                }

                foreach (var company in companies) company.ResetPools();
            }
        }
    }
}