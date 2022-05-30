public class DisablePlayerInput : Simulation.Event<DisablePlayerInput>
{
    PlayerModel model = Simulation.GetModel<PlayerModel>();

    public override void Execute()
    {
        var player = model.playerController;
        player.controlsDisabled = true;
    }
}
