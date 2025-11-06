using Unity.VisualScripting;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] Transform recoilFollowPos;
    [SerializeField] float kickBackAmount = -1;
    [SerializeField] float kickBackSpeed = 10, returnSpeed = 20;
    float currentRecoilPosition, finalRecoilPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRecoilPosition = Mathf.Lerp(currentRecoilPosition, 0, returnSpeed * Time.deltaTime);
        finalRecoilPosition = Mathf.Lerp(finalRecoilPosition, currentRecoilPosition, kickBackSpeed * Time.deltaTime);
        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilPosition);
    }
    
    public void TriggerRecoil()
    {
        currentRecoilPosition += kickBackAmount;
    }
}
