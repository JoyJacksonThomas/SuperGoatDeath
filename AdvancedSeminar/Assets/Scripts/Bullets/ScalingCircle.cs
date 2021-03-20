using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalingCircle : MonoBehaviour {

   public string mTargetID = "";
   public GameObject mTarget;

   public float scalingSpeed = 0;
   public float startRadius = 0;
   public float endRadius = 10;
   private Vector2 mFinalScale = Vector2.zero;

   public BulletData mBulletData;

   public GameObject ripplePrefab;
   private GameObject mRipple;
   public float rippleEndRadius;
   public float rippleScalingSpeed = 0;
   private bool mRippleDestroyed = false;
   private Vector2 mFinalRippleScale = Vector2.zero;

   void Start () {
      transform.localScale = Vector2.one * startRadius;
      mFinalScale = Vector2.one * endRadius;
      mTarget = GameObject.Find(mTargetID);

      mRipple = Instantiate(ripplePrefab, transform.position, transform.rotation) as GameObject;
      mRipple.transform.localScale = Vector2.zero;
      mFinalRippleScale = Vector2.one * rippleEndRadius;
   }
	
	void Update () {
      if (mRippleDestroyed == false)
      {
         Vector2 currentRippleScale = (Vector2)mRipple.transform.localScale + new Vector2(rippleScalingSpeed, rippleScalingSpeed);
         mRipple.transform.localScale = currentRippleScale;
         
         //mRipple.transform.localScale = Vector2.Lerp(mRipple.transform.localScale, mFinalRippleScale, rippleScalingSpeed);
         if (mRipple.transform.localScale.x <= mFinalRippleScale.x)
         {
            Destroy(mRipple);
            mRippleDestroyed = true;
            Debug.Log("RippleDestroyed");
         }
      }
      else
      {
         Vector2 currentScale = (Vector2)transform.localScale + new Vector2(scalingSpeed, scalingSpeed);
         transform.localScale = currentScale;
         //transform.localScale = Vector2.Lerp(transform.localScale, mFinalScale, scalingSpeed);
         if (transform.localScale.x >= mFinalScale.x)
         {
            Destroy(gameObject);
         }
      }
      
	}

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.name == mTargetID)
      {
         mTarget.GetComponent<Health>().damage(mBulletData.damage);
      }
   }
}
