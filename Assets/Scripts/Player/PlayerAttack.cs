using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public const string AXE = "Axe";
    public const string Arrow = "Arrow";
    public const string LOOKROOT = "LookRoot";
    public const string ZOOMCAMERA = "FPCamera";
    public const string CROSSHAIR = "Crosshair";
    public const string ZOOMINANIM = "ZoomIn";
    public const string ZOOMOUTANIM = "ZoomOut";
    [SerializeField] private GameObject arrowPrefab, spearPrefab;
    [SerializeField] private Transform arrowAndBowStartPosition;
    private WeaponManager weaponManager;
    private Camera mainCamera;
    private GameObject crosshair;
    private Animator zoomCameraAnim;
    private float fireRate = 15f;
    private float nextTimeToFire;
    //private float damage = 20f;
    //private bool zoomed;
    private bool isAiming;
    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        zoomCameraAnim = transform.Find(LOOKROOT).transform.Find(ZOOMCAMERA).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(CROSSHAIR);
        mainCamera = Camera.main;
    }
    private void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
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
                BulletFired();
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
                    BulletFired();
                }
                else
                {//we have an spear or bow 
                    if (isAiming)
                    {
                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                        if (weaponManager.GetCurrentSelectedWeapon().weaponBulletType == WeaponBulletType.Arrow)
                        {
                            ThrowArrowOrSpear(true);
                        }
                        else if (weaponManager.GetCurrentSelectedWeapon().weaponBulletType == WeaponBulletType.Spear)
                        {
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            }
        }
    }
    private void ZoomInAndOut()
    {
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.Aim)
        {
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(ZOOMINANIM);
                crosshair.SetActive(false);
            }
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(ZOOMOUTANIM);
                crosshair.SetActive(true);
            }
        }
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SelfAim)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(true);
                isAiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(false);
                isAiming = false;
            }
        }
    }
    private void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.transform.position = arrowAndBowStartPosition.position;
            arrow.GetComponent<ArrowAndBow>().Launch(mainCamera);
        }
        else
        {
            if (!throwArrow)
            {
                GameObject spear = Instantiate(spearPrefab);
                spear.transform.position = arrowAndBowStartPosition.position;
                spear.GetComponent<ArrowAndBow>().Launch(mainCamera);
            }
        }
    }
    private void BulletFired()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            if (hit.transform)
            {

            }
        }
    }
}
