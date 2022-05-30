using System;

[System.Serializable]
public class HealthModel
{
    public int Health;

    public int actualHealth;
    public int maximumHealth;

    public void InitializeHealth(int _health = 0)
    {
        maximumHealth = _health != 0 ? _health : Health;
        actualHealth = maximumHealth;
        TriggerOnHealth();
    }

    public Action<int, int> OnHealth;

    public void TriggerOnHealth()
    {
        if (OnHealth != null)
            OnHealth(maximumHealth,actualHealth);

        //We could also Use Just One Line and Invoke OnHealth
        // OnHealth?.Invoke()
        //But i Personally  dont like the Invoke Method
    }
}
