using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKill : Simulation.Event<PlayerKill>
{
    PlayerModel model = Simulation.GetModel<PlayerModel>();

    public override void Execute()
    {
        Simulation.Schedule<DisablePlayerInput>();
        model.PlayerObject.SetActive(false);

        Simulation.Schedule<PlayerDeath>(2f);
    }
}

