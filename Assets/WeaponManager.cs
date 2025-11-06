using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;
    [SerializeField] bool semiAuto;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barellPos;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    AimStateManager aim;

    [SerializeField] AudioClip gunShot;
    AudioSource audioSource;
    WeaponAmmo weaponAmmo;
    WeaponBloom bloom;

    ActionStateManager actions;
    WeaponRecoil recoil;

    Light muzzleFlashLight;
    ParticleSystem muzzleFlashParticles;
    float lightIntensity;
    [SerializeField] float lightReturnSpeed = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        recoil = GetComponent<WeaponRecoil>();
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        weaponAmmo = GetComponent<WeaponAmmo>();
        bloom = GetComponent<WeaponBloom>();
        actions = GetComponentInParent<ActionStateManager>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        lightIntensity = muzzleFlashLight.intensity;
        muzzleFlashLight.intensity = 0;
        muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        if (muzzleFlashParticles == null)
    {
        Debug.LogError("HATA: Muzzle Flash Particle System bulunamadı! Silah objesi altında mevcut olduğundan emin olun.");
    }
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire())
        {
            Fire();
        }
        Debug.Log("Current Ammo:" + weaponAmmo.currentAmmo);
        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime); 
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate)
        {
            return false;
        }
        if (weaponAmmo.currentAmmo == 0)
        {
            return false;
        }
        if (actions.currentState == actions.Reload)
        {
            return false;
        }
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0))
        {
            return true;
        }
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }
        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barellPos.LookAt(aim.aimPos);
        barellPos.localEulerAngles = bloom.BloomAngle(barellPos);

        audioSource.PlayOneShot(gunShot);
        recoil.TriggerRecoil();
        TriggerMuzzleFlash();
        weaponAmmo.currentAmmo--;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barellPos.position, barellPos.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barellPos.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
    
    void TriggerMuzzleFlash()
    {
        muzzleFlashParticles.Play();
        muzzleFlashLight.intensity = lightIntensity;
    }
}
