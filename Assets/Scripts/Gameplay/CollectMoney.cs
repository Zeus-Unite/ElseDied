using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoney : Simulation.Event<CollectMoney>
{
    public int MoneyToCollect { get; set; }

    readonly ShopModel model = Simulation.GetModel<ShopModel>();

    public override void Execute()
    {
        model.OnGrantMoney(MoneyToCollect);
    }
}
