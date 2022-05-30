public class PlayerSpawn : Simulation.Event<PlayerSpawn>
{
    readonly PlayerModel model = Simulation.GetModel<PlayerModel>();

    public override void Execute()
    {
        ResetPlayer();

        Simulation.Schedule<EnablePlayerInput>(2f);
    }

    void ResetPlayer()
    {
        model.playerController.Teleport(model.spawnPoint.position + new UnityEngine.Vector3(0, 3, 0));
        model.PlayerObject.SetActive(true);
        model.healthSystem.TriggerOnHealth();
    }
}
