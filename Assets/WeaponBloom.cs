using UnityEngine;

public class WeaponBloom : MonoBehaviour
{

    [SerializeField] float defaultBloomAngle = 3;
    [SerializeField] float walkBloomMultiplier = 1.5f;
    [SerializeField] float crouchBloomMultiplier = 0.5f;
    [SerializeField] float sprintBloomMultiplier = 2f;
    [SerializeField] float adsBloomMultiplier = 0.5f;

    MovementState movement;
    AimStateManager aiming;

    float currentBloom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponentInParent<MovementState>();
        aiming = GetComponentInParent<AimStateManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public Vector3 BloomAngle(Transform barrelPos)
    {
        if (movement.currentState == movement.Idle)
        {
            currentBloom = defaultBloomAngle;
        }
        else if (movement.currentState == movement.Walk)
        {
            currentBloom = defaultBloomAngle * walkBloomMultiplier;
        }
        else if (movement.currentState == movement.Crouch)
        {
            if (movement.moveDirection.magnitude == 0)
            {
                currentBloom = defaultBloomAngle * crouchBloomMultiplier;
            }
            else
            {
                currentBloom = defaultBloomAngle * crouchBloomMultiplier * walkBloomMultiplier;
            }
            currentBloom = defaultBloomAngle * crouchBloomMultiplier;
        }
        else if (movement.currentState == movement.Run)
        {
            currentBloom = defaultBloomAngle * sprintBloomMultiplier;
        }

        if (aiming.currentState == aiming.Aim)
        {
            currentBloom *= adsBloomMultiplier;
        }
        
        float randomX = Random.Range(-currentBloom, currentBloom);
        float randomY = Random.Range(-currentBloom, currentBloom);
        float randomZ = Random.Range(-currentBloom, currentBloom);

        Vector3 randomRotation = new Vector3(randomX, randomY, randomZ);
        return barrelPos.localEulerAngles + randomRotation;
    }
}
