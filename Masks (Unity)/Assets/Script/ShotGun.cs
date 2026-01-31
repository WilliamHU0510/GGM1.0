using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    public int scatterNumber;
    public override void Shoot()
    {
        for(int i = 1; i <= scatterNumber; i++)//多次发射子弹
        {
            GameObject bulletGameObject = Instantiate(bullet);
            float scatterAngle = (Random.value * 2 - 1) * maxScatterAngle;
            Vector3 scatterFace;
            scatterFace.x = Mathf.Cos(Mathf.Atan(weaponDirection.y / weaponDirection.x) + Mathf.PI * BoolToInt(weaponDirection.x < 0) + scatterAngle);
            scatterFace.y = Mathf.Sin(Mathf.Atan(weaponDirection.y / weaponDirection.x) + Mathf.PI * BoolToInt(weaponDirection.x < 0) + scatterAngle);
            scatterFace.z = 0;
            bulletGameObject.GetComponent<Bullet>().Generate(scatterFace, launchPoint.transform.position, this.transform.rotation,enemyWeapon);
        }
        coolDown = coolDownTime;
        capNumber--;
    }
}
