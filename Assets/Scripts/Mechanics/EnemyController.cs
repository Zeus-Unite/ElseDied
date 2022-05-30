using UnityEngine;
using ZeusUnite.Monsters;

public class EnemyController : MonoBehaviour, IHealth
{
    readonly LevelModel levelmodel = Simulation.GetModel<LevelModel>();

    public UnitModel enemyModel;
    Animator anim;

    bool isDeath = false;
    bool walkRight;
    Vector3 lastPosition = Vector3.zero;

    void Awake()
    {
        anim = GetComponent<Animator>();
        transform.Rotate(0, 90, 0, Space.Self);
    }


    void Start()
    {
        enemyModel.HealthSystem.InitializeHealth(enemyModel.HealthSystem.Health * levelmodel.ActualLevel);
        anim.SetBool("Walk", true);
        walkRight = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount">Use Negative Amount to reduce Health</param>
    public void HandleHealth(int amount)
    {
        if (isDeath)
            return;

        enemyModel.HealthSystem.actualHealth += amount;

        if (enemyModel.HealthSystem.actualHealth <= 0)
        {
            //Unit Death
            isDeath = true;
            anim.enabled = false;
            Instantiate(enemyModel.BodyExplosionPrefab, this.transform.position, Quaternion.identity);
            Instantiate(enemyModel.ExplosionPrefab, this.transform.position, Quaternion.identity);
            EnemyDeath ed = Simulation.Schedule<EnemyDeath>();
            ed.spawnPosition = this.transform.position + new Vector3(0, 2, 0);
            ed.moneyAmount = enemyModel.GrantMoney;
            ed.scoreAmount = enemyModel.Experience;
            enemyModel.dieSound.Play(this.transform.position);
            RemoveEnemy();
            return;
        }

        enemyModel.HealthSystem.TriggerOnHealth();
    }

    public void RemoveEnemy()
    {
        SpawnController.Instance.RemoveEnemyFromList(this);
        Destroy(this.gameObject);
    }

    public void KillEnemy()
    {
        while (enemyModel.HealthSystem.actualHealth > 0) HandleHealth(-1);
    }

    void Update()
    {
        if (isDeath)
            return;

        EnemyWalking();
        IsEnemyStuck();

        lastPosition = transform.position;
    }

    void IsEnemyStuck()
    {
        if (lastPosition == transform.position)
            RotateEnemyAround();
    }

    void EnemyWalking()
    {
        if (walkRight)
        {

            transform.position += transform.forward * enemyModel.UnitSpeed * Time.deltaTime;
            return;
        }

        transform.position -= transform.forward * -enemyModel.UnitSpeed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isDeath)
            return;

        if (collision.transform.tag == "Wall" || collision.transform.tag == "Enemy")
        {
            //Turn Enemy around
            RotateEnemyAround();
        }
    }

    void RotateEnemyAround()
    {
        if (transform.rotation.y < 0)
        {
            walkRight = true;
            transform.Rotate(0, 180, 0, Space.World);
            return;
        }

        walkRight = false;
        transform.Rotate(0, -180, 0, Space.World);
    }
}
