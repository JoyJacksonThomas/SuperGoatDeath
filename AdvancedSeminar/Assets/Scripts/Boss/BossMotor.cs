using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMotor : MonoBehaviour
{

    public BossPhase[] mBossPhases;
    public int mCurrentPhase = 0;
    public int mCurrentSubPhase = 0;


    public MovementPattern mMovementPattern;
    public GunMotor mGunMotor;
    private BossBulletManager mBulletManager;
    private Health mHealth;
    private Rigidbody2D mRigidbody2D;
    private Animator mAnimator;

    public float mTargetRadius = .1f;
    public float mSlowRadius = 1f;
    public float mTimeToTarget = 1f;
    public float mMaxAcceleration = 2f;

    public bool mPaused = false;


    void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
        mBulletManager = GameObject.Find("BossBulletSpawn").GetComponent<BossBulletManager>();
        mGunMotor = GameObject.Find("BossBulletSpawn").GetComponent<GunMotor>();
        mHealth = GetComponent<Health>();

        mMovementPattern = mBossPhases[mCurrentPhase].data[mCurrentSubPhase].movementPattern;
        mGunMotor.mGunData = mBossPhases[mCurrentPhase].data[mCurrentSubPhase].gunData;
        mGunMotor.initializeGunTimers();
        mAnimator = GameObject.Find("TouhouBossSprite").GetComponent<Animator>();

        mBulletManager.mHealth = mHealth;
    }

    void Update()
    {

        mBulletManager.mPaused = mPaused;
        if (!mPaused)
        {
            mBulletManager.mMovementPattern = mMovementPattern;
            moveToPoints();
            switchPhases();
            switchSubPhases();
        }
    }

    void moveToPoints()
    {
        MovementPattern currentPattern = mMovementPattern;
        Vector2 diff = currentPattern.data[currentPattern.mCurrentTargetIndex].target - (Vector2)transform.position;
        if (Mathf.Abs(diff.magnitude) <= mTargetRadius)
        {
            currentPattern.mCurrentTargetIndex++;
            currentPattern.mCurrentTargetIndex %= currentPattern.data.Length;
            mBulletManager.mShouldBeFiring = currentPattern.data[currentPattern.mCurrentTargetIndex].shootOnArrive;
            mRigidbody2D.velocity = Vector2.zero;
            return;
        }

        float targetSpeed = 0;
        if (diff.magnitude > mSlowRadius)
        {
            targetSpeed = currentPattern.data[currentPattern.mCurrentTargetIndex].speedToTarget;
        }
        else
        {
            targetSpeed = currentPattern.data[currentPattern.mCurrentTargetIndex].speedToTarget * diff.magnitude / mTimeToTarget;
        }

        Vector2 targetVelocity = diff;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        Vector2 acceleration = targetVelocity - mRigidbody2D.velocity;
        acceleration /= mTimeToTarget;

        if (acceleration.magnitude > mMaxAcceleration)
        {
            acceleration.Normalize();
            acceleration *= mMaxAcceleration;
        }

        mRigidbody2D.AddForce(acceleration);

        mBulletManager.mShouldBeFiring = currentPattern.data[currentPattern.mCurrentTargetIndex].shootOnTravel;
        return;

    }

    public void switchPhases()
    {
        if (mHealth.mHealthDepleted == true)
        {
            if (mCurrentPhase + 1 < mBossPhases.Length)
            {
                mHealth.mCurrentHealth = mHealth.mStartHealth;
                mHealth.mHealthDepleted = false;
                mCurrentPhase++;
                mCurrentPhase %= mBossPhases.Length;
                mCurrentSubPhase = 0;
                mMovementPattern = mBossPhases[mCurrentPhase].data[mCurrentSubPhase].movementPattern;
                mGunMotor.mGunData = mBossPhases[mCurrentPhase].data[mCurrentSubPhase].gunData;
                mGunMotor.initializeGunTimers();
            }
            else
            {
                mAnimator.SetBool("Dead", true);
            }
        }
    }

    void switchSubPhases()
    {
        if (mCurrentSubPhase + 1 < mBossPhases[mCurrentPhase].data.Length)
        {
            if (((float)mHealth.mCurrentHealth / mHealth.mStartHealth) * 100 < mBossPhases[mCurrentPhase].data[mCurrentSubPhase + 1].healthPercent)
            {
                mCurrentSubPhase++;
                mCurrentSubPhase %= mBossPhases[mCurrentPhase].data.Length;
                mMovementPattern = mBossPhases[mCurrentPhase].data[mCurrentSubPhase].movementPattern;
                mGunMotor.mGunData = mBossPhases[mCurrentPhase].data[mCurrentSubPhase].gunData;
                mGunMotor.initializeGunTimers();
            }
        }

    }
}
