public class StartWave : Simulation.Event<StartWave>
{
    readonly LevelModel levelmodel = Simulation.GetModel<LevelModel>();

    public override void Execute()
    {
        if (levelmodel.WaveIndex >= levelmodel.Waves.Count)
        {
            Simulation.Schedule<EndLevel>();
            return;
        }

        ActivateNextWave();
    }

    void ActivateNextWave()
    {
        SpawnController.Instance.SpawnControlActive = true;

        levelmodel.WaveIndex++;
        levelmodel.OnNextWave();

        SpawnController.Instance.StartWave();
    }
}
