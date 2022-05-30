using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyExplosion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("RemoveBodyParts");
    }

    IEnumerator RemoveBodyParts()
    {
        Transform[] bodyParts = GetComponentsInChildren<Transform>();

        List<Transform> parts = new List<Transform>();

        for (int i = 0; i < bodyParts.Length; i++)
        {
            parts.Add(bodyParts[i]);
        }

        bodyParts = null; //Is this really necessary?

        while (parts.Count > 0)
        {
            int removeIndex = Random.Range(0, parts.Count);
            Destroy(parts[removeIndex].gameObject);
            parts.RemoveAt(removeIndex);


            yield return new WaitForSeconds(Random.Range(3f, 5f));
        }

        DestroyObject();

        yield return null;
    }

    void DestroyObject()
    {
        StopAllCoroutines();
        Destroy(this.gameObject);
    }
}
