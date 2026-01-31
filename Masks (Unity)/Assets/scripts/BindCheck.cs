using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindCheck : MonoBehaviour
{
    InputControl inputControl;
    public GameObject thisPlayer;

    private void Start()
    {
        inputControl = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputControl>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("1");
        if (thisPlayer.CompareTag("Player1"))
        {
            Debug.Log("2");
            if (other.CompareTag("Player2"))
            {
                inputControl.isInRange_1 = true;
            }
        }
        else if (thisPlayer.CompareTag("Player2"))
        {
            if (other.CompareTag("Player1"))
            {
                inputControl.isInRange_2 = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (thisPlayer.CompareTag("Player1"))
        {
            if (other.CompareTag("Player2"))
            {
                inputControl.isInRange_1 = false;
            }
        }
        else if (thisPlayer.CompareTag("Player2"))
        {
            if (other.CompareTag("Player1"))
            {
                inputControl.isInRange_2 = false;
            }
        }
    }

}
