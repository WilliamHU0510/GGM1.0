using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidPoint : MonoBehaviour
{
    // Start is called before the first frame update
 
   
    [SerializeField] private Transform objectA; // A对象的Transform

    void Update()
    {
        if (objectA != null)
        {
        
            transform.position = objectA.position;
        }
    }
}

