using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject paralaxObject;
    private List<GameObject> objectCreated = new();

    public GameObject GetObject()
    {
        foreach (GameObject obj in objectCreated)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject c = Instantiate(paralaxObject);
        objectCreated.Add(c);
        return c;
    }


}
