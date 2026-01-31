using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;
    public GameObject weapon4;
    // Start is called before the first frame update
    void ChangeWeapon(int number)
    {
        switch (number)
        {
            case 1:
                weapon1.SetActive(true);
                weapon2.SetActive(false);
                weapon3.SetActive(false);
                weapon4.SetActive(false);
                break;
            case 2:
                weapon1.SetActive(false);
                weapon2.SetActive(true);
                weapon3.SetActive(false);
                weapon4.SetActive(false);
                break;
            case 3:
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                weapon3.SetActive(true);
                weapon4.SetActive(false);
                break;
            case 4:
                weapon1.SetActive(false);
                weapon2.SetActive(false);
                weapon3.SetActive(false);
                weapon4.SetActive(true);
                break;
        }
    }
    void Start()
    {
        ChangeWeapon(1);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeWeapon(4);
        }
    }
}
