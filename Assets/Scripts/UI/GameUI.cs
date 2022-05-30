using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText = null;
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] TextMeshProUGUI highscoreText = null;
    [SerializeField] TextMeshProUGUI powerText = null;
    [SerializeField] TextMeshProUGUI enemiesRemainingText = null;

    [SerializeField] TextMeshProUGUI waveText = null;

    [SerializeField] Button energyBuyButton = null;
    [SerializeField] TextMeshProUGUI energyPriceText = null;
    [SerializeField] TextMeshProUGUI energyAmountText = null;
    [SerializeField] Button damageBuyButton = null;
    [SerializeField] TextMeshProUGUI damagePriceText = null;
    [SerializeField] TextMeshProUGUI damageLevelText = null;
    [SerializeField] Button shotSpeedBuyButton = null;
    [SerializeField] TextMeshProUGUI shotSpeedPriceText = null;
    [SerializeField] TextMeshProUGUI shotSpeedLevelText = null;

    readonly PlayerModel model = Simulation.GetModel<PlayerModel>();
    readonly LevelModel levelmodel = Simulation.GetModel<LevelModel>();
    readonly ShopModel shopmodel = Simulation.GetModel<ShopModel>();

    void Start()
    {
        RefreshPlayerMoneyText();
        RefreshWeaponEnergyText();
        RefreshWaveText();
        RefreshRemainingEnemiesText(0);

        highscoreText.text = GetHighscore().ToString();

        model.OnWeaponEnergy += RefreshWeaponEnergyText;
        levelmodel.OnNextWave += RefreshWaveText;
        levelmodel.OnScore += RefreshScoreText;
        shopmodel.OnBaseMoneyUpdate += RefreshPlayerMoneyText;

        SpawnController.Instance.RemainingEnemies += RefreshRemainingEnemiesText;
    }

    void RefreshScoreText()
    {
        scoreText.text = levelmodel.actualLevelScore.ToString();

        if (levelmodel.actualLevelScore > GetHighscore()) { 
            SetHighscore();}
    }

    void DisplayButtonPriceAndLevel()
    {
        energyPriceText.text = shopmodel.EnergyPrice.ToString();
        damagePriceText.text = shopmodel.DamagePrice.ToString();
        shotSpeedPriceText.text = shopmodel.ShotSpeedPrice.ToString();

        damageLevelText.text = shopmodel.DamageLevel.ToString();
        shotSpeedLevelText.text = shopmodel.ShotSpeedLevel.ToString();
        energyAmountText.text = shopmodel.EnergyAmount.ToString();
    }

    void RefreshPlayerMoneyText()
    {
        moneyText.text = (shopmodel.PlayerMoney).ToString();

        ResetShopButtons();
        DisplayButtonPriceAndLevel();
        MakeShopButtonsAvailable();
    }

    void MakeShopButtonsAvailable()
    {
        if (shopmodel.PlayerMoney >= shopmodel.EnergyPrice)
            energyBuyButton.interactable = true;

        if (shopmodel.PlayerMoney >= shopmodel.DamagePrice)
            damageBuyButton.interactable = true;

        if (shopmodel.ShotSpeedLevel < shopmodel.ShotSpeedMaxLevel)
        {
            if (shopmodel.PlayerMoney >= shopmodel.ShotSpeedPrice)
                shotSpeedBuyButton.interactable = true;

            return;
        }

        shotSpeedPriceText.text = "Max Level";
    }

    void ResetShopButtons()
    {
        energyBuyButton.interactable = false;
        damageBuyButton.interactable = false;
        shotSpeedBuyButton.interactable = false;
    }

    void RefreshRemainingEnemiesText(int obj)
    {
        if (obj == 0)
        {
            enemiesRemainingText.transform.parent.SetActive(false);
            return;
        }
        enemiesRemainingText.transform.parent.SetActive(true);
        enemiesRemainingText.text = obj.ToString();
    }

    void RefreshWaveText()
    {
        waveText.text = (levelmodel.WaveIndex).ToString();
    }

    void RefreshWeaponEnergyText()
    {
        powerText.text = model.WeaponEnergy.ToString();
    }

    void OnDisable()
    {
        model.OnWeaponEnergy -= RefreshWeaponEnergyText;
        levelmodel.OnNextWave -= RefreshWaveText;
        shopmodel.OnBaseMoneyUpdate -= RefreshPlayerMoneyText;

        if (SpawnController.Instance != null)
            SpawnController.Instance.RemainingEnemies -= RefreshRemainingEnemiesText;
    }


    int GetHighscore()
    {
        levelmodel.highScore = PlayerPrefs.GetInt("EASYIDLEHIGHSCORE", 0);
        return levelmodel.highScore;
    }

    void SetHighscore()
    {
        levelmodel.highScore = levelmodel.actualLevelScore;
        PlayerPrefs.SetInt("EASYIDLEHIGHSCORE", levelmodel.highScore);
        PlayerPrefs.Save();
    }

}

