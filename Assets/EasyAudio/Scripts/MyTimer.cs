using System;
using UnityEngine;
/// <summary>
/// Create a Timer on a MonoBehaviour GameObject to Instantiate in World
/// After Time runs out the defined Action will be called.
/// We Only call the Create Timer Function everything else is private inside the Class
/// ** Check https://www.stussegames.com for more Information about MyTimer Class **
/// </summary>
public class MyTimer
{
    /// <summary>
    /// We Call this Function from other Scripts to Create a Timer at Runtime
    /// </summary>
    /// <param name="action">The Action that gets Performed after the Time runs out.</param>
    /// <param name="timer">The amount of Time that has to pass until the Timer gets Triggered</param>
    /// <returns></returns>
    public static MyTimer CreateTimer(Action action, float timer)
    {
        GameObject obj = new GameObject("TimerObject", typeof(MonoBehaviourHook));
        MyTimer myTimer = new MyTimer(obj, action, timer);
        obj.GetComponent<MonoBehaviourHook>().OnUpdate = myTimer.UpdateTimer;

        return myTimer;
    }

    GameObject gameObject;

    float timer;
    Action OnTimeAction;

    MyTimer(GameObject gameObject, Action action, float timer)
    {
        this.gameObject = gameObject;
        this.timer = timer;
        this.OnTimeAction = action;
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        if (timer > 0)
            return;

        OnTimeAction();
        DestroyItSelf();
    }

    void DestroyItSelf()
    {
        GameObject.Destroy(gameObject);
    }

    class MonoBehaviourHook : MonoBehaviour
    {
        public Action OnUpdate;

        void Update()
        {
            if (OnUpdate != null) OnUpdate();
        }
    }
}