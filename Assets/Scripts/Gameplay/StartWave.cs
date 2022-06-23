public class StartWave : Simulation.Event<StartWave>
{
    readonly LevelModel levelmodel = Simulation.GetModel<LevelModel>();

    public override void Execute()
    {
        if (levelmodel.WaveIndex >= levelmodel.Waves.Count)
        {
            var sim = Simulation.Schedule<EndLevel>();
            sim.forcedEnd = false;
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
