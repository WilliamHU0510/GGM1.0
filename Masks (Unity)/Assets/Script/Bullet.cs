using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //子弹参数
    public float bulletSpeed;
    public float maxExistTime;
    public float minDamage;
    public float maxDamage;
    public bool penetrate;
    public bool melee;
    public bool enemyBullet;
    Vector3 bulletVector;
    float existTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && !melee)//撞墙
        {
            Destroy(gameObject);
        }
        if (!enemyBullet)//友方子弹
        {
            if (collision.tag == "Enemy")
            {
                float thisDamage = CalculateDamage();
                collision.gameObject.GetComponent<Attribute>().Hit(thisDamage);
                if (!penetrate && !melee)
                {
                    Destroy(gameObject);
                }
            }
        }
        else//敌方子弹
        {
            if (collision.tag == "Player")
            {
                float thisDamage = CalculateDamage();
                collision.gameObject.GetComponent<Attribute>().Hit(thisDamage);
                if (!penetrate && !melee)
                {
                    Destroy(gameObject);
                }
            }
        }
        if (melee)//近战武器清除子弹
        {
            if (collision.gameObject.GetComponent<Bullet>() != null)//碰到子弹
            {
                if (enemyBullet != collision.gameObject.GetComponent<Bullet>().enemyBullet)//非友方子弹
                {
                    if (!collision.gameObject.GetComponent<Bullet>().melee)//碰撞到非近战子弹
                    {
                        print("叮！");
                        Destroy(collision.gameObject);
                    }
                }
            }
            
        }
    }
    float CalculateDamage()
    {
        float damage = Random.value * (maxDamage - minDamage) + minDamage;
        return damage;
    }
    private void Awake()
    {
        existTime = 0;
    }
    // Start is called before the first frame update
    public void Generate(Vector3 direction,Vector3 shootPosition, Quaternion rotation,bool isEnemyBullet)//生成子弹时确定位置、方向和旋转，以及是否为敌方子弹
    {
        this.transform.position = shootPosition;
        bulletVector = bulletSpeed * direction.normalized;
        this.transform.rotation = rotation;
        enemyBullet = isEnemyBullet;
    }
    void ReachMaxTime()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = this.transform.position + bulletVector * Time.deltaTime;
        existTime += Time.deltaTime;
        if (existTime >= maxExistTime)//存在时间达到最大时执行
        {
            ReachMaxTime();
        }
    }
}
