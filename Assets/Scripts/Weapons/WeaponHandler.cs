using UnityEngine;
public enum WeaponAim
{
    None,
    SelfAim,
    Aim
}
public enum WeaponFireType
{
    Single,
    Multiple
}
public enum WeaponBulletType
{
    Bullet,
    Arrow,
    Spear,
    None
}
public class WeaponHandler : MonoBehaviour
{
    public const string SHOOT = "Shoot";
    public const string AIM = "Aim";
    private Animator animator;
    public WeaponAim weaponAim;
    public WeaponFireType weaponFireType;
    public WeaponBulletType weaponBulletType;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private AudioSource shootSound, reloadSound;
    [SerializeField] private GameObject attackPoint;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ShootAnimation()
    {
        animator.SetTrigger(SHOOT);
    }
    public void Aim(bool canAim)
    {
        animator.SetBool(AIM, canAim);
    }
    private void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }
    private void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }
    private void PlayShootSound()
    {
        shootSound.Play();
    }
    private void PlayReloadSound()
    {
        reloadSound.Play();
    }
    private void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    private void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
