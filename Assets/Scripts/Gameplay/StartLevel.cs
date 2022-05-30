
public class StartLevel : Simulation.Event<PlayerSpawn>
{
    readonly PlayerModel model = Simulation.GetModel<PlayerModel>();
    readonly BaseModel basemodel = Simulation.GetModel<BaseModel>();
    readonly LevelModel levelmodel = Simulation.GetModel<LevelModel>();
    readonly ShopModel shopmodel = Simulation.GetModel<ShopModel>();

    public bool IsNewStart = true;

    public override void Execute()
    {
        GameController.Instance.GameRunning = true;
        SpawnController.Instance.SpawnControlActive = true;

        Simulation.Schedule<PlayerSpawn>();
        Simulation.Schedule<StartWave>(4f);

        if (IsNewStart)
        {
            model.healthSystem.InitializeHealth();
            basemodel.HealthSystem.InitializeHealth();

            model.WeaponEnergy = 0;

            shopmodel.DamageLevel = 1;
            shopmodel.ShotSpeedLevel = 1;
            shopmodel.PlayerMoney = 50;


            levelmodel.actualLevelScore = 0;
            levelmodel.ActualLevel = 1;
            levelmodel.WaveIndex = 0;

            AddStartValues();
            return;
        }

        AddStartValues();

        levelmodel.ActualLevel++;
        levelmodel.WaveIndex = 0;
    }

    void AddStartValues()
    {
        var energy = Simulation.Schedule<CollectEnergy>();
        energy.EnergyToCollect = levelmodel.startEnergy * levelmodel.ActualLevel;

        var money = Simulation.Schedule<CollectMoney>();
        money.MoneyToCollect = levelmodel.startMoney * levelmodel.ActualLevel;
    }
}
