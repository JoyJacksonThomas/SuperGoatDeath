using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{

    public MovementPattern mMovementPattern;
    public GunMotor mGunMotor;
    private GameObject mPlayer;

    public Health mHealth;

    public float mRotationSpeed;
    public bool mShouldBeFiring = false;

    public bool mPaused = false;

    void Start()
    {
        mGunMotor = GetComponent<GunMotor>();
        mPlayer = GameObject.Find("Player");
    }

    void Update()
    {
        if (!mPaused)
        {
            if (mShouldBeFiring && mHealth.mCurrentHealth >= 0)
            {
                Aim();
                mGunMotor.Fire();
            }
        }
    }

    void Aim()
    {
        if (mMovementPattern != null)
        {
            if (mMovementPattern.aimingType == MovementPattern.AimingType.SPINNING)
            {
                transform.Rotate(Vector3.forward * mMovementPattern.data[mMovementPattern.mCurrentTargetIndex].rotationSpeed * Time.deltaTime);
            }
            else if (mMovementPattern.aimingType == MovementPattern.AimingType.FACING_FORWARD)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (mMovementPattern.aimingType == MovementPattern.AimingType.LOOK_AT_PLAYER)
            {
                Vector2 facingDirection = mPlayer.transform.position - transform.position;
                float dir = Mathf.Atan2(facingDirection.y, facingDirection.x);
                transform.rotation = Quaternion.Euler(0, 0, dir * Mathf.Rad2Deg + 90);
            }
        }
    }
}
