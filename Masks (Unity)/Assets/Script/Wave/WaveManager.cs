using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] sprawnPoints;
    private void Start()
    {
        if (sprawnPoints.Length == 0)
        {
            Debug.LogError("Can not find enemy spawn point, please check it!");
            return;
        }

    }

   
}
