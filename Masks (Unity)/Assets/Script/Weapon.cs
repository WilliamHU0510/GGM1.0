using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //射击参数
    public GameObject bullet;
    public GameObject launchPoint;
    public float maxScatterAngle;

    //弹夹参数
    public int capacity;
    public int capNumber;
    
    //CD参数
    public float coolDownTime;
    public float reloadTime;
    public float coolDown;

    //敌方武器参数
    public bool enemyWeapon;
    
    float reload;
    bool reloading;
    public Vector3 weaponDirection;
    public int BoolToInt(bool expression)
    {
        if (expression) return 1;
        return 0;
    }

    void Face()//枪口转向
    {
        float angle;
        if (weaponDirection.x != 0) angle = Mathf.Atan(weaponDirection.y / weaponDirection.x) * 180 / Mathf.PI;
        else angle = Mathf.Sign(weaponDirection.y) * 90;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    Vector3 Direction()//确定枪口方向
    {
        Vector3 mousePosOnScreen = Input.mousePosition;
        mousePosOnScreen.z = 0;
        Vector3 mousePosInWorld = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        return (mousePosInWorld - this.transform.position);
    }
    Vector3 EnemyDirection()//敌人确定枪口方向
    {
        GameObject character = GameObject.Find("Character");
        Vector3 characterPosition = character.transform.position;
        return (characterPosition - this.transform.position);
    }
    //Shoot module
    public bool ShootAble()//是否可以射击
    {
        if (capNumber <= 0) return false;
        if (coolDown > 0 || reload > 0)
        {
            return false;
        }
        return true;
    }
    void CoolDown()//减少CD
    {
        coolDown -= Time.deltaTime;
        reload -= Time.deltaTime;
        if (reload <= 0)
        {
            reloading = false;
        }
    }
    public virtual void Shoot()//发射子弹
    {
        GameObject bulletGameObject = Instantiate(bullet);
        float scatterAngle = (Random.value * 2 - 1) * maxScatterAngle;
        Vector3 scatterFace;
        scatterFace.x = Mathf.Cos(Mathf.Atan(weaponDirection.y / weaponDirection.x) + Mathf.PI * BoolToInt(weaponDirection.x < 0) + scatterAngle);
        scatterFace.y = Mathf.Sin(Mathf.Atan(weaponDirection.y / weaponDirection.x) + Mathf.PI * BoolToInt(weaponDirection.x < 0) + scatterAngle);
        scatterFace.z = 0;
        bulletGameObject.GetComponent<Bullet>().Generate(scatterFace, launchPoint.transform.position, this.transform.rotation,enemyWeapon);
        coolDown = coolDownTime;
        capNumber--;
    }
    //Reload module
    public void Reload()//换弹
    {
        if (!reloading)
        {
            capNumber = capacity;
            reload = reloadTime;
            reloading = true;
        }
    }
    public void ShowingReloading()//显示Reloading图标
    {
        if (reloading)
        {
            GameObject.Find("ReloadingSign").GetComponent<Reloading>().showingReloading = true;
        }
        else
        {
            GameObject.Find("ReloadingSign").GetComponent<Reloading>().showingReloading = false;
        }
    }
    private void Start()
    {
        maxScatterAngle = maxScatterAngle * Mathf.PI / 180;
        capNumber = capacity;
    }
    // Update is called once per frame
    void Update()
    {
        if (!enemyWeapon)//玩家武器
        {
            launchPoint = transform.Find("LaunchPoint").gameObject;//确定枪口位置
            weaponDirection = Direction();//确定枪朝向
            weaponDirection.z = 0;
            Face();//调整枪朝向
            bool shootable = ShootAble();//确定是否可射击
            if (shootable && Input.GetMouseButton(0))
            {
                Shoot();
            }//射击
            if (Input.GetKeyDown(KeyCode.R) || capNumber <= 0)
            {
                Reload();
            }//换子弹
            CoolDown();//减少CD
            ShowingReloading();//显示换弹中
        }
        else//敌人武器
        {
            if (!gameObject.transform.parent.GetComponent<EnemyAction>().idle)//判断是否处于警戒状态
            {
                launchPoint = transform.Find("LaunchPoint").gameObject;//确定枪口位置
                weaponDirection = EnemyDirection();//确定枪朝向
                weaponDirection.z = 0;
                Face();//调整枪朝向
            }
            
        }
    }
}
