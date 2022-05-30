using System;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    readonly ShopModel shopmodel = Simulation.GetModel<ShopModel>();
    readonly PlayerModel playermodel = Simulation.GetModel<PlayerModel>();

    void OnEnable()
    {
        shopmodel.OnGrantMoney += GrantMoney;
    }

    void GrantMoney(int obj)
    {
        shopmodel.PlayerMoney += obj;
        shopmodel.OnBaseMoneyUpdate();
    }


    void Update()
    {
        if (Input.GetKeyDown(shopmodel.EnergyBuyKey))
        {
            BuyAmmo();
        }

        if (Input.GetKeyDown(shopmodel.DamageBuyKey))
        {
            BuyDamageUpgrade();
        }

        if (Input.GetKeyDown(shopmodel.ShotSpeedBuyKey))
        {
            BuyShotSpeedUpgrade();
        }


        if (Input.GetKeyDown(KeyCode.Alpha0))
            Time.timeScale++;

        if(Input.GetKeyDown(KeyCode.Alpha9))
            Time.timeScale--;
    }

    public void BuyDamageUpgrade()
    {
        if (!CheckIfEnoughMoney(shopmodel.DamagePrice))
        {
            //Not Enough Money
            return;
        }

        shopmodel.PlayerMoney -= shopmodel.DamagePrice;
        shopmodel.DamageLevel++;

        shopmodel.OnBaseMoneyUpdate();

    }

    public void BuyShotSpeedUpgrade()
    {
        if(shopmodel.ShotSpeedLevel >= shopmodel.ShotSpeedMaxLevel)
        {
            //Max Level Reached
            return;
        }

        if (!CheckIfEnoughMoney(shopmodel.ShotSpeedPrice))
        {
            //Not Enough Money
            return;
        }

        shopmodel.PlayerMoney -= shopmodel.DamagePrice;
        shopmodel.DamageLevel++;

        shopmodel.OnBaseMoneyUpdate();
    }

    public void BuyAmmo()
    {
        if (!CheckIfEnoughMoney(shopmodel.EnergyPrice))
        {
            //Not Enough Money
            Debug.Log("Not Enough Money");
            return;
        }

        playermodel.WeaponEnergy += shopmodel.EnergyAmount;
        shopmodel.PlayerMoney -= shopmodel.EnergyPrice;

        shopmodel.OnBaseMoneyUpdate();
        playermodel.OnWeaponEnergy();
    }

    bool CheckIfEnoughMoney(int price)
    {
        if (shopmodel.PlayerMoney >= price)
            return true;

        return false;
    }
}
