using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    public float maxHP;
    public float HP;
    public void Hit(float damage)
    {
        HP -= damage;
        if (gameObject.GetComponent<EnemyAction>() != null)
        {
            gameObject.GetComponent<EnemyAction>().idle = false;
        }
    }

    public void DeathCheck()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        DeathCheck();//死亡检测
    }
}
