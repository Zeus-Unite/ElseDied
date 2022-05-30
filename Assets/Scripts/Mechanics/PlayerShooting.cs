using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShooting : MonoBehaviour
{
    float fireTimer = 0;
    readonly GameModel gameModel = Simulation.GetModel<GameModel>();
    readonly PlayerModel playerModel = Simulation.GetModel<PlayerModel>();
    readonly ShopModel shopModel = Simulation.GetModel<ShopModel>();

    float CalculateNextShotTime => gameModel.weaponSystems[0].shootingSpeed - (shopModel.ShotSpeedLevel * .02f);
    int CalculateWeaponDamage => gameModel.weaponSystems[0].damage + (shopModel.DamageLevel * gameModel.weaponSystems[0].damage);


    void Update()
    {
        fireTimer -= Time.deltaTime;

        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (Input.GetButton("Fire1") && fireTimer <= 0 && playerModel.WeaponEnergy > 0)
        {
            CreateBulletPlayerFront();
            FinishShotSequenze();
        }

        void CreateBulletPlayerFront()
        {
            Vector3 spawnPos = playerModel.lastLookRight == true ? new Vector2(1.135f, .55f) : new Vector2(-1.135f, .55f);
            spawnPos += (Vector3)this.transform.localPosition;

            ProjectileController pj = Instantiate(gameModel.weaponSystems[0].weaponPrefab, spawnPos, Quaternion.identity);
            pj.SetupProjectile(playerModel.lastLookRight, -CalculateWeaponDamage);
        }

        void FinishShotSequenze()
        {
            playerModel.shotSound.Play(this.transform.position);
            var powerChange = Simulation.Schedule<PlayerPowerChange>();
            powerChange.PowerChange = -gameModel.weaponSystems[0].powerUsage;
            fireTimer = CalculateNextShotTime;
        }
    }


    #region If Else Update Example
    // Some Issues are that many of the Youtube Tutorials will look like this. So Bad Coding Style is really a trend.
    // this is a really poor approach, its like im slapping Down Code Lines after Code Lines.
    // It is repetive and 4 Tabs deep for unecessary Reasons. The Code is very Noisy and theres so much going on even its the same whats happening.
    // Think, Before!
    //void IfElseUpdate()
    //{
    //    fireTimer -= Time.deltaTime;

    //    if (Input.GetButton("Fire1") && fireTimer <= 0 && playerModel.WeaponEnergy > 0)
    //    {
    //        if (!playerModel.lastLookRight)
    //        {
    //            //Face Left
    //            Vector3 spawnPos = new Vector2(-1.135f, -.30f);
    //            spawnPos += (Vector3)this.transform.localPosition;
    //            Projectile pj = Instantiate(gameModel.weaponSystems[0].weaponPrefab, spawnPos, Quaternion.identity);
    //            pj.SetupProjectile(true);
    //        }
    //        else if (playerModel.lastLookRight)
    //        {
    //            //Face Right
    //            Vector2 spawnPos = new Vector2(1.135f, -.30f);
    //            spawnPos += (Vector2)this.transform.localPosition;
    //            Projectile pj = Instantiate(gameModel.weaponSystems[0].weaponPrefab, spawnPos, Quaternion.identity);
    //            pj.SetupProjectile(false);
    //        }

    //        playerModel.shotSound.Play(this.transform.position);
    //        var powerChange = Simulation.Schedule<PlayerPowerChange>();
    //        powerChange.PowerChange = -gameModel.weaponSystems[0].powerUsage;
    //        fireTimer = playerModel.shootingSpeed;
    //    }
    //}
    #endregion
}



