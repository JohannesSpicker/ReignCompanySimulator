using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reign.Companies;
using TeppichsAttributes.Data;
using TeppichsTools.Behavior;
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

        public static float CompanyTickInSeconds => .00001f;

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
            {
                yield return StartCoroutine(turnIterator.GetNextActor().DoTurn());

                if (companies.Count(c => 0 < c.Sovereignty.Value) < 2)
                    gameIsRunning = false;
            }

            StringBuilder gameEndMessage = new("Game ended with remaining companies: ");

            foreach (Company company in companies)
                gameEndMessage.Append($"{company.name} ");

            Debug.Log(gameEndMessage);

            QuitHelper.Quit();
        }
    }
}