using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponAmmo : MonoBehaviour
{
    [Header("Ammo Settings")]
    public int clipSize = 30;
    public int extraAmmo = 90;
    public int currentAmmo;

    [Header("Audio Clips")]
    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;
    private AudioSource audioSource;

    [Header("UI Reference")]
    public TextMeshProUGUI ammoText; // HUD üzerindeki AmmoText objesi

    void Start()
    {
        currentAmmo = clipSize;
        audioSource = GetComponent<AudioSource>();
        UpdateAmmoUI();
    }

    void Update()
    {
        // Test için reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        UpdateAmmoUI();
    }

    public void Reload()
    {
        if (extraAmmo <= 0 || currentAmmo == clipSize)
            return;

        // Ses oynatma sırası (örnek animasyon zamanlaması)
        StartCoroutine(ReloadSequence());
    }

    private System.Collections.IEnumerator ReloadSequence()
    {
        // Magazin çıkar
        if (audioSource && magOutSound)
            audioSource.PlayOneShot(magOutSound);
        yield return new WaitForSeconds(0.5f);

        // Magazin tak
        if (audioSource && magInSound)
            audioSource.PlayOneShot(magInSound);
        yield return new WaitForSeconds(0.5f);

        // Sürgü bırakma
        if (audioSource && releaseSlideSound)
            audioSource.PlayOneShot(releaseSlideSound);

        // Mermiyi güncelle
        int ammoNeeded = clipSize - currentAmmo;
        int ammoToLoad = Mathf.Min(ammoNeeded, extraAmmo);
        currentAmmo += ammoToLoad;
        extraAmmo -= ammoToLoad;

        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Ammo: {currentAmmo} / {extraAmmo}";
        }
    }
}