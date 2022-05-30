using System;
using UnityEngine;

[System.Serializable]
public class PlayerModel
{
    public Transform PlayerObject;
    public UnityEngine.UI.Image DashFillImage;
    public PlayerController playerController;
    public HealthModel healthSystem;
    public Transform spawnPoint;

    public float playerSpeed = 2.0f;
    public float jumpHeight = 5.0f;
    public float gravityValue = -9.81f;
    public float head = 1f;
    public float dashTime = 1f;
    public float dashPower = 30f;

    [HideInInspector]public bool lastLookRight;

    public float WeaponEnergy;
    public Action OnWeaponEnergy;

    [Header("Audio")]
    public AudioShot shotSound;
    public AudioShot jumpSound;
    public AudioShot dieSound;
    public AudioShot dashSound;
    
}
