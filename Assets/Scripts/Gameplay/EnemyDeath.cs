using UnityEngine;

public class EnemyDeath : Simulation.Event<EnemyDeath>
{
    public int moneyAmount { get; set; }
    public int scoreAmount { get; set; }
    public Vector3 spawnPosition { get; set; }

    readonly GameModel model = Simulation.GetModel<GameModel>();
    readonly ShopModel shopmodel = Simulation.GetModel<ShopModel>();

    public override void Execute()
    {
        PowerSphere powerSphere = (PowerSphere)MonoBehaviour.Instantiate(model.powerSpherePrefab, spawnPosition, Quaternion.identity);

        var collectMoney = Simulation.Schedule<CollectMoney>();
        collectMoney.MoneyToCollect = moneyAmount;

        var score = Simulation.Schedule<CollectScore>();
        score.GrantScore = scoreAmount;
    }
}
