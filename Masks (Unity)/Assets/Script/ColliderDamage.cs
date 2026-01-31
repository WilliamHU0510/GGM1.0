using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamage : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float thisDamage = CalculateDamage();
            collision.gameObject.GetComponent<Attribute>().Hit(thisDamage);
        }
    }
    float CalculateDamage()
    {
        float damage;
        damage = Random.value * (maxDamage - minDamage) + minDamage;
        return damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
