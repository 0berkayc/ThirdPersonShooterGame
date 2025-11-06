using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;

public class CoverState : MovementBaseState
{
    private bool hasEnteredCover = false;

    Transform weaponHoldPoint;

    private MultiAimConstraint[] multiAimConstraints;
    private TwoBoneIKConstraint[] ikConstraints;
    private float constraintFadeDuration = 0.25f;

    public override void EnterState(MovementState state)
    {
        state.anim.SetTrigger("coverTrigger");
        state.anim.SetBool("isCovering", true);
        state.currentMoveSpeed = 0f;
        hasEnteredCover = false;

        // Rig bileşenlerini bul
        FindRigConstraints(state);

        // Smooth olarak devre dışı bırak
        state.StartCoroutine(ChangeAllRigWeights(0f, constraintFadeDuration));

        weaponHoldPoint = state.transform.Find("M4_Carbine");
    if (weaponHoldPoint != null)
    {
        weaponHoldPoint.localPosition = new Vector3(-0.045f, 0.01f, -0.04f);
        weaponHoldPoint.localRotation = Quaternion.Euler(34.6f, 124.1f, 176.4f);
    }
    }

    public override void UpdateState(MovementState state)
    {
        if (!IsNearCover(state))
        {
            ExitState(state, state.Idle);
            return;
        }

        if (!hasEnteredCover && state.anim.GetCurrentAnimatorStateInfo(0).IsName("Cover"))
        {
            hasEnteredCover = true;
        }

        if (hasEnteredCover)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            state.anim.SetFloat("hzInput", horizontal);
            state.anim.SetFloat("vtInput", vertical);

            if (Mathf.Abs(horizontal) > 0.1f)
            {
                Vector3 move = state.transform.right * horizontal * 1.5f * Time.deltaTime;
                state.controller.Move(move);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExitState(state, state.Idle);
        }
        
    }

    private void ExitState(MovementState state, MovementBaseState newState)
    {
        state.anim.SetBool("isCovering", false);
        state.StartCoroutine(ChangeAllRigWeights(1f, constraintFadeDuration));
        state.SwitchState(newState);
    }

    private bool IsNearCover(MovementState state)
    {
        return Physics.Raycast(
            state.transform.position + Vector3.up * 1f,
            state.transform.forward,
            1.2f,
            LayerMask.GetMask("Cover")
        );
    }

    private void FindRigConstraints(MovementState state)
    {
        multiAimConstraints = state.GetComponentsInChildren<MultiAimConstraint>(true);
        ikConstraints = state.GetComponentsInChildren<TwoBoneIKConstraint>(true);
    }

    private IEnumerator ChangeAllRigWeights(float targetWeight, float duration)
    {
        if ((multiAimConstraints == null || multiAimConstraints.Length == 0) &&
            (ikConstraints == null || ikConstraints.Length == 0))
            yield break;

        float elapsed = 0f;

        float[] startMulti = new float[multiAimConstraints.Length];
        float[] startIK = new float[ikConstraints.Length];

        for (int i = 0; i < multiAimConstraints.Length; i++)
            startMulti[i] = multiAimConstraints[i]?.weight ?? targetWeight;
        for (int i = 0; i < ikConstraints.Length; i++)
            startIK[i] = ikConstraints[i]?.weight ?? targetWeight;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            for (int i = 0; i < multiAimConstraints.Length; i++)
                if (multiAimConstraints[i] != null)
                    multiAimConstraints[i].weight = Mathf.Lerp(startMulti[i], targetWeight, t);

            for (int i = 0; i < ikConstraints.Length; i++)
                if (ikConstraints[i] != null)
                    ikConstraints[i].weight = Mathf.Lerp(startIK[i], targetWeight, t);

            yield return null;
        }

        // Son değerleri sabitle
        for (int i = 0; i < multiAimConstraints.Length; i++)
            if (multiAimConstraints[i] != null)
                multiAimConstraints[i].weight = targetWeight;

        for (int i = 0; i < ikConstraints.Length; i++)
            if (ikConstraints[i] != null)
                ikConstraints[i].weight = targetWeight;
    }
}