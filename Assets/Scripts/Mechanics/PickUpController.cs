using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [SerializeField] PickUpModel pickUp;

    float activeTime = 0;


    void Update()
    {
        activeTime += Time.deltaTime;

        if (activeTime > pickUp.MaxSceneTime)
            Destroy(this.transform.parent.gameObject);
    }

    void DestroyObject()
    {
        if(pickUp.ParticleSystem != null)
            Instantiate(pickUp.ParticleSystem, this.transform.position,Quaternion.identity);


        Destroy(this.transform.parent.gameObject);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pickUp.Sound.Play(this.transform.position);
            
            if(pickUp.TypeToPickup == PickUpType.Money)
            {
                var money = Simulation.Schedule<CollectMoney>();
                money.MoneyToCollect = pickUp.Amount;

                DestroyObject();
                return;
            }

            if (pickUp.TypeToPickup == PickUpType.Score)
            {
                var score = Simulation.Schedule<CollectScore>();
                score.GrantScore = pickUp.Amount;

                DestroyObject();
                return;
            }

            if (pickUp.TypeToPickup == PickUpType.Energy)
            {
                var energy = Simulation.Schedule<CollectEnergy>();
                energy.EnergyToCollect = pickUp.Amount;

                DestroyObject();
                return;
            }
        }
    }
    // The OnTriggerEnter could be also a switch, but i dont see a necessarity with 3 If Statements to use a switch anmd produce a 3 Tab deep Code Structure
    //      switch (PickUpModel.typeToPickup)
    //      {
    //          case PickUpType.Money:
    //              break;
    //          case PickUpType.Energy:
    //              break;
    //          case PickUpType.Score:
    //              break;
    //      }
    //      DestroyObject();
    // Since we are also Triggering "DestroyObject" from all the States, it would be now really the Time to use a Switch so we dont have to Call 3 Times DestroyObject;
    // However if we hadn't DestroyObject the switch would be useless.
}