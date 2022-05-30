using UnityEngine;

[System.Serializable]
public class ShopModel 
{
    public KeyCode EnergyBuyKey;
    public int EnergyPrice;
    public int EnergyAmount;

    public KeyCode DamageBuyKey;
    public int DamagePrice;
    public int DamageLevel;

    public KeyCode ShotSpeedBuyKey;
    public int ShotSpeedPrice;
    public int ShotSpeedLevel;
    public int ShotSpeedMaxLevel;

    [HideInInspector]public int PlayerMoney;
    public System.Action OnBaseMoneyUpdate;
    public System.Action<int> OnGrantMoney;
}
