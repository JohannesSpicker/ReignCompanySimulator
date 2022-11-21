namespace Code.Companies
{
    public class Company
    {
        private void ResetPools()
        {
        }

        private void Listen()
        {
            GeneralTurnMessages.OnStartOfTurn += ResetPools;
            GeneralTurnMessages.OnEndOfTurn += CheckForDeath;
        }

        private void Unlisten()
        {
            GeneralTurnMessages.OnStartOfTurn -= ResetPools;
            GeneralTurnMessages.OnEndOfTurn -= CheckForDeath;
        }

        private void CheckForDeath()
        {
        }
    }
}