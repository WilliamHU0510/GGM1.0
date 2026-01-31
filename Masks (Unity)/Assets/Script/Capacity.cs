using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capacity : MonoBehaviour
{
    public Text capacity;
    void PrintCapacity()
    {
        capacity.text = gameObject.GetComponentInParent<Weapon>().capNumber + "/" + gameObject.GetComponentInParent<Weapon>().capacity;
    }
    // Start is called before the first frame update
    void Start()
    {
        capacity = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        PrintCapacity();//显示弹夹量
    }
}
