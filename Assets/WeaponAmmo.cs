using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;

    public AudioClip magInSound;
    public AudioClip magOutSound;
    public AudioClip releaseSlideSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = clipSize;
    }

    // Update is called once per frame
    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            int ammoToReload = clipSize - currentAmmo;
            currentAmmo += ammoToReload;
            extraAmmo -= ammoToReload;
        }
        else if (extraAmmo > 0)
        {
            if (currentAmmo + extraAmmo > clipSize)
            {
                int leftOverAmmo = extraAmmo + currentAmmo - clipSize;
                currentAmmo = clipSize;
                extraAmmo = leftOverAmmo;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
