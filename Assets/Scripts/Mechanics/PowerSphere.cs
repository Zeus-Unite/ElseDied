using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSphere : MonoBehaviour
{
    readonly PlayerModel model = Simulation.GetModel<PlayerModel>();

    // Update is called once per frame
    void Update()
    {
        if (!model.PlayerObject.gameObject.activeSelf)
            Destroy(this.gameObject);

        this.transform.LookAt(model.PlayerObject.position);
        this.transform.Translate(Vector3.forward * 15 * Time.deltaTime, Space.Self);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var x = Simulation.Schedule<PlayerPowerChange>();
            x.PowerChange = Random.Range(1,4);
            Destroy(this.gameObject);
        }
    }

}
