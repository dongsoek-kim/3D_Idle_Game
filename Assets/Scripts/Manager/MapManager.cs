using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class MapManager : MonoBehaviour
{
    private static MapManager instance;
    public MapSpawner mapSpawner;
    public static MapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MapManager>();

                if (instance == null)
                {
                    GameObject go = new GameObject("MapManager");
                    instance = go.AddComponent<MapManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        mapSpawner = GetComponent<MapSpawner>();
    }

    void Start()
    {
        mapSpawner.StartGame();
    }
}
