public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
{
    
    PlayerModel model = Simulation.GetModel<PlayerModel>();

    public override void Execute()
    {
        var player = model.playerController;
        player.controlsDisabled = false;
    }
}