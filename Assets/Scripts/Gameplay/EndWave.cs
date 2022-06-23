public class EndWave : Simulation.Event<EndWave>
{
    readonly LevelModel levelmdel = Simulation.GetModel<LevelModel>();

    public override void Execute()
    {
        if (levelmdel.WaveIndex >= levelmdel.Waves.Count)
        {
            var sim = Simulation.Schedule<EndLevel>();
            sim.forcedEnd = false;

            return;
        }

        SpawnController.Instance.SpawnControlActive = false;

        GameController.Instance.mainUIController.EndWave();
    }
}

