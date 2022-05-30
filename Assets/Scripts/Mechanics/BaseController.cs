using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour, IHealth
{
    readonly BaseModel basemodel = Simulation.GetModel<BaseModel>();

    public void HandleHealth(int amount)
    {
        basemodel.HealthSystem.actualHealth += amount;
        basemodel.HealthSystem.TriggerOnHealth();

        if (basemodel.HealthSystem.actualHealth <= 0)
        {
            //Unit Death
            Simulation.Schedule<EndLevel>();
            return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Enemy")
        {
            HandleHealth(-1);

            other.TryGetComponent(out EnemyController eo);

            if(eo != null)
                eo.RemoveEnemy();
        }
    }
}
