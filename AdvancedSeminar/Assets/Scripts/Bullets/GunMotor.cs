using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMotor : MonoBehaviour
{

   //public BulletPool mBulletPool;
   public GunData mGunData;
   public GameObject mBulletPrefab;
   public GameObject mScalingCirclePrefab;

   int[] mTimePast = new int[0];
   int[] mBurstTimer = new int[0];
   bool[] mSpawnDelayGratified = new bool[0];

   public string bulletSound = "";
   public string scalingCircleSound = "";

   AudioManager mAudioManager;

   private void Start()
   {
      initializeGunTimers();
      mAudioManager = AudioManager.instance;
   }

   void Update()
   {
      for (int i = 0; i < mTimePast.Length; i++)
      {
         mTimePast[i]++;
         mBurstTimer[i]++;
      }
   }
   private void FixedUpdate()
   {
      
   }

   public void initializeGunTimers()
   {
      if (mGunData != null)
      {
         mTimePast = new int[mGunData.data.Length];
         mBurstTimer = new int[mGunData.data.Length];
         mSpawnDelayGratified = new bool[mGunData.data.Length];
         for (int i = 0; i < mTimePast.Length; i++)
         {
            mTimePast[i] = 0;
            mBurstTimer[i] = 0;
            mSpawnDelayGratified[i] = false;
         }
      }
   }

   public void Fire()
   {
      bool bulletFired = false;
      bool scalingCircleFired = false;
      int numScalingCirclesFired = 0;
      for (int i = 0; i < mGunData.data.Length; i++)
      {
         if (mTimePast[i] >= mGunData.data[i].fireRate 
            && (mBurstTimer[i] <= mGunData.data[i].burstFireData.x || mBurstTimer[i] >= mGunData.data[i].burstFireData.y - 1)
            && ((mTimePast[i] > mGunData.data[i].bulletSpawnDelay && !mSpawnDelayGratified[i]) || mSpawnDelayGratified[i]))
         {
            Vector3 spawnPos = Vector3.zero;
            if (mGunData.data[i].spawnAddative)
            {
               spawnPos = transform.position + (Vector3)mGunData.data[i].bulletSpawnOffSet;
            }
            else
            {
               spawnPos = mGunData.data[i].bulletSpawnOffSet;
            }

            if (mGunData.data[i].projectileType == GunData.ProjectileType.BULLET)
            {
               bulletFired = true;
               GameObject pew = Instantiate(mBulletPrefab, spawnPos,
                     Quaternion.Euler(0f, 0f, mGunData.data[i].bulletRotationOffSet + transform.eulerAngles.z)) as GameObject;
               pew.GetComponent<BulletMotor>().mBulletData = mGunData.data[i].bullet;
               
            }
            if (mGunData.data[i].projectileType == GunData.ProjectileType.SCALING_CIRCLE)
            {
               numScalingCirclesFired++;
               scalingCircleFired = true;
               GameObject pew = Instantiate(mScalingCirclePrefab, spawnPos,
                     Quaternion.Euler(0f, 0f, mGunData.data[i].bulletRotationOffSet + transform.eulerAngles.z)) as GameObject;
               pew.GetComponent<ScalingCircle>().mBulletData = mGunData.data[i].bullet;
            }
            mTimePast[i] = 0;
            if (mBurstTimer[i] > mGunData.data[i].burstFireData.y)
            {
               mBurstTimer[i] = 0;
            }
            mSpawnDelayGratified[i] = true;
            //mBulletPool.activateBullet(spawnPos, mGunData.data[i].bulletRotationOffSet + transform.eulerAngles.z, mGunData.data[i].bullet);

            


         }
      }
      if(bulletFired)
      {
         mAudioManager.playSound(bulletSound);
      }
      if (scalingCircleFired)
      {
         for(int i = 0; i < numScalingCirclesFired; i++)
         {
            mAudioManager.playSound(scalingCircleSound + (i+1).ToString());
         }
        
      }
   }
}
