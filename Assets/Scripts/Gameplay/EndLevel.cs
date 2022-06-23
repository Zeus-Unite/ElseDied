public class EndLevel : Simulation.Event<EndLevel>
{
    readonly PlayerModel model = Simulation.GetModel<PlayerModel>();
    readonly BaseModel basemodel = Simulation.GetModel<BaseModel>();

    public bool forcedEnd = false;

    public override void Execute()
    {
        model.PlayerObject.SetActive(false);

        GameController.Instance.GameRunning = false;
        SpawnController.Instance.SpawnRunning = false;
        SpawnController.Instance.SpawnControlActive = false;

        SpawnController.Instance.ClearPlayField();

        if (!forcedEnd)
        {
            bool canContinue = true;

            if (basemodel.HealthSystem.actualHealth <= 0)
                canContinue = false;

            if (model.healthSystem.actualHealth <= 0)
                canContinue = false;


            GameController.Instance.mainUIController.EndGame(canContinue);
            return;
        }

        GameController.Instance.mainUIController.EndGame(false);
    }
}
