using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScore : Simulation.Event<CollectScore>
{
    public int GrantScore { get; set; }

    readonly LevelModel model = Simulation.GetModel<LevelModel>();

    public override void Execute()
    {
        model.actualLevelScore += GrantScore;
        model.OnScore();
    }
}
