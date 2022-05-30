using UnityEngine;
using UnityEngine.UI;

public class HealthCanvas : MonoBehaviour
{
    Image fillerImage;

    EnemyController enemyBehaviour;
    BaseController baseBehaviour;


    void Awake()
    {
        fillerImage = this.transform.Find("Canvas").Find("HealthBar").Find("HealthFiller").GetComponent<Image>();

        enemyBehaviour = GetComponent<EnemyController>();
        baseBehaviour = GetComponent<BaseController>();
    }

    void OnEnable()
    {
        if (enemyBehaviour != null)
        {
            enemyBehaviour.enemyModel.HealthSystem.OnHealth += OnHealth;
            return;
        }

        if(baseBehaviour != null)
        {
            GameController.Instance.basemodel.HealthSystem.OnHealth += OnHealth;
            return;
        }

        GameController.Instance.model.healthSystem.OnHealth += OnHealth;

    }

    void OnHealth(int maxHealth, int minHealth)
    {
        if (fillerImage == null)
        {
            Debug.LogWarning("Something Went Wrong Updating Enemy Health Interface");
            return;
        }

        float healthNormalized = (float)((float)minHealth / (float)maxHealth);
        fillerImage.fillAmount = healthNormalized;
    }

    void OnDisable()
    {
        if (enemyBehaviour != null)
        {
            enemyBehaviour.enemyModel.HealthSystem.OnHealth -= OnHealth;
            return;
        }

        if (baseBehaviour != null && GameController.Instance != null)
        {
            GameController.Instance.basemodel.HealthSystem.OnHealth -= OnHealth;
            return;
        }

        if(GameController.Instance != null)
             GameController.Instance.model.healthSystem.OnHealth -= OnHealth;
    }
}
