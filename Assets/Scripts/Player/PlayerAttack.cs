using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public const string AXE = "Axe";
    private WeaponManager weaponManager;
    private float fireRate = 15f;
    private float nextTimeToFire;
    private float damage = 20f;
    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }
    private void Update()
    {
        WeaponShoot();
    }
    private void WeaponShoot()
    {
        //if we have assault rifle
        if (weaponManager.GetCurrentSelectedWeapon().weaponFireType == WeaponFireType.Multiple)
        {
            //if we press and hold left mouse click 
            //if time is greater than the next time to fire
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                //BulletFire();
            }
        }
        //if we have a regular weapon thet shoots once
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (weaponManager.GetCurrentSelectedWeapon().tag == AXE)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }
                if (weaponManager.GetCurrentSelectedWeapon().weaponBulletType == WeaponBulletType.Bullet)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                    //BulletFire();
                }
                else
                {//we have an spear or bow 

                }
            }
        }
    }
}
