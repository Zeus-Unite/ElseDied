using System.Collections.Generic;

[System.Serializable]
public class GameModel 
{
    public PowerSphere powerSpherePrefab;

    public List<WeaponSystem> weaponSystems = new List<WeaponSystem>();

    public List<UnityEngine.Transform> pickUpPrefabs = new List<UnityEngine.Transform>();

}

[System.Serializable]
public struct WeaponSystem
{
    public ProjectileController weaponPrefab;
    public float powerUsage;
    public int damage;
    public float shootingSpeed;

}