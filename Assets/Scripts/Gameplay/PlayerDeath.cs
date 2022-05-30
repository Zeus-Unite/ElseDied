public class PlayerDeath : Simulation.Event<PlayerDeath>
{
    PlayerModel model = Simulation.GetModel<PlayerModel>();

    public override void Execute()
    {
        Simulation.Schedule<PlayerSpawn>(2f);

    }
}