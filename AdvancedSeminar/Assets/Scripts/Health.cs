using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int mStartHealth = 100;
    public int mCurrentHealth;
    public bool mHealthDepleted = false;

    public GameObject mHealthBar;
    public float mStartHealthBarScale;
    public float mCurrentHealthBarScale;

    public bool mInvincible = false;
    public int mNumInvincibilityFrames = 40;
    public Color invincibilityColor;
    private int mCurrentInvincibilityFrame = 40;
    public Vector2 mStartEndHealthBar = new Vector2(-.43f, -9.8f);
    private float mIncrementValue = 0;

    public int timesHit = 0;

    private CameraMotor cameraMotor;
    public float shakeDur = 0;
    public float shakeMag = 0;

    private void Start()
    {
        mCurrentHealth = mStartHealth;
        mIncrementValue = (mStartEndHealthBar.x - mStartEndHealthBar.y) / (float)mStartHealth;
        mCurrentInvincibilityFrame = mNumInvincibilityFrames;
        cameraMotor = GameObject.Find("Main Camera").GetComponent<CameraMotor>();
    }

    void Update()
    {
        if (mCurrentHealth <= 0)
        {
            mHealthDepleted = true;
        }
        if (mHealthBar)
        {
            float healthBarX = mHealthBar.transform.position.x;
            float healthBarY = Mathf.Lerp(mHealthBar.transform.position.y, mStartEndHealthBar.x - ((float)mStartHealth - (float)mCurrentHealth) * mIncrementValue, .2f);
            float healthBarZ = mHealthBar.transform.position.z;
            mHealthBar.transform.position = new Vector3(healthBarX, healthBarY, healthBarZ);
            if (mInvincible && gameObject.name == "Player")
            {
                mCurrentInvincibilityFrame--;
                if (mCurrentInvincibilityFrame < 0)
                {
                    mInvincible = false;
                    mCurrentInvincibilityFrame = mNumInvincibilityFrames;
                    GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }

    }

    public void damage(int damageToTake)
    {
        if (mInvincible == false)
        {
            mCurrentHealth -= damageToTake;
            if (gameObject.name == "Player")
            {
                AudioManager.instance.playSound("PlayerHurt");
                mInvincible = true;
                GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = invincibilityColor;
                StartCoroutine(cameraMotor.Shake(shakeDur, shakeMag));
            }
            else if (gameObject.name == "Boss")
            {
                StartCoroutine(GetComponent<ShakeObject>().Shake(shakeDur, shakeMag));
            }
            
        }
    }

    public void SetInvincibility(int _numFrames)
    {
        mCurrentInvincibilityFrame = _numFrames;
        mInvincible = true;
    }
}
