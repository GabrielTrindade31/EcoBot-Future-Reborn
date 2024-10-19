using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    private List<GameObject> projectiles = new List<GameObject>();
    [SerializeField] private int amountToPull;
    [SerializeField] private GameObject BulletPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < amountToPull; i++)
        {
            GameObject obj = Instantiate(BulletPrefab);
            obj.SetActive(false);
            projectiles.Add(obj);
        }
    }

    public GameObject GetProjectile()
    {
        for (int i = 0; i < projectiles.Count; i++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return projectiles[i];
            }
        }
        return null;
    }
}
