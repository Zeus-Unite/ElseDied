public class CollectEnergy : Simulation.Event<CollectEnergy>
{
    public int EnergyToCollect { get; set; }

    readonly PlayerModel model = Simulation.GetModel<PlayerModel>();

    public override void Execute()
    {
        model.WeaponEnergy += EnergyToCollect;
        model.OnWeaponEnergy();
    }
}
