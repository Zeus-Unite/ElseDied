using UnityEngine;

public class PlayerPowerChange : Simulation.Event<PlayerPowerChange>
{
    public float PowerChange { get; set; }

    PlayerModel model = Simulation.GetModel<PlayerModel>();

    public override void Execute()
    {
        model.WeaponEnergy += PowerChange;
        model.OnWeaponEnergy();
    }
}
