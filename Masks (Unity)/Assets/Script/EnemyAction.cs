using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    public float view;
    public float moveSpeed;
    public float shootCD;

    public bool moving;
    public bool attacking;

    public GameObject character;
    public bool idle = true;
    float shootCoolDown;
    public void TurnOver(bool ifLeft)
    {
        if (ifLeft)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public Vector3 Direction()
    {
        return (character.transform.position - this.transform.position);
    }
    public bool IdleCheck()
    {
        if (Vector2.SqrMagnitude(character.transform.position - this.transform.position) < view)
        {
            return false;
        }
        return true;
    }
    public void Shooting()
    {
        Weapon enemyWeapon;
        if (this.GetComponentInChildren<Weapon>() != null)
        {
            enemyWeapon = this.GetComponentInChildren<Weapon>();
            enemyWeapon.Shoot();
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        character = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if (idle)//处于非警戒状态时行为
        {
            idle = IdleCheck();//判断是否进入警戒状态
        }
        else//处于警戒状态时行为
        {
            if (Direction().x < 0)//转身判定
            {
                TurnOver(true);//左转
            }
            else
            {
                TurnOver(false);//右转
            }

            if (shootCoolDown < 0)
            {
                Shooting();//射击
                shootCoolDown = shootCD;
            }
            shootCoolDown -= Time.deltaTime;
        }
        
    }
    
}
