using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : EnemyAction
{
    public float rushTime;
    public float rushSpeed;
    public float jumpTime;
    public float jumpExcursion;
    public float scatterNumber;
    public float coolDown;
    public GameObject bullet;
    public float maxScatterAngle;
    int skillIn = 0;
    float skillTimeNow = 0;
    Vector3 aim = new Vector3(0, 0, 0);
    // Update is called once per frame
    void Skill()
    {
        if (skillTimeNow > 0)
        {
            SkillSettlement();
        }
        else
        {
            if (skillIn == 0)
            {
                SkillChoose();
            }
            else
            {
                skillIn = 0;
                skillTimeNow = coolDown;
            }
        }
    }
    void SkillChoose()
    {
        int lastSkill = skillIn;
        while (skillIn == lastSkill)
        {
            skillIn = Random.Range(1, 4);
        }
        switch (skillIn)
        {
            case 1:
                skillTimeNow = rushTime;
                if(character.transform.position.x - this.transform.position.x > 0)
                {
                    aim = new Vector2(rushSpeed * rushTime, 0);
                }
                else
                {
                    aim = new Vector2(-rushSpeed * rushTime, 0);
                }
                break;
            case 2:
                skillTimeNow = jumpTime;
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().gravityScale /2 * 9.8f * jumpTime);
                aim = character.transform.position - this.transform.position + new Vector3((2 * Random.value - 1) * jumpExcursion, 0, 0);
                break;
            case 3:
                Shoot();
                skillTimeNow = 0;
                break;
        }
    }
    void SkillSettlement()
    {
        skillTimeNow -= Time.deltaTime;
        switch (skillIn)
        {
            case 0:
                break;
            case 1:
                Move(rushTime);
                break;
            case 2:
                Move(jumpTime);
                break;
            case 3:
                break;
        }
        
    }
    void Move(float time)
    {
        Vector2 move;
        move = aim / time * Time.deltaTime;
        this.transform.position = this.transform.position + new Vector3(move.x, 0, 0);
    }
    void Shoot()
    {
        for (int i = 1; i <= scatterNumber; i++)
        {
            GameObject bulletGameObject = Instantiate(bullet);
            float scatterAngle = (Random.value * 2 - 1) * maxScatterAngle;
            Vector3 scatterFace;
            scatterFace.x = Mathf.Cos(scatterAngle);
            scatterFace.y = Mathf.Sin(scatterAngle);
            scatterFace.z = 0;
            bulletGameObject.GetComponent<Bullet>().Generate(scatterFace, this.transform.position, this.transform.rotation, true);
        }
    }
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
            
            Skill();
            
        }

    }
}
