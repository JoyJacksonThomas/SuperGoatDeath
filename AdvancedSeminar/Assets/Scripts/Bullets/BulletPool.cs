using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

   public GameObject mBulletPrefab;
   private BulletMotor[] mBullets;
   public int mNumBullets;

	void Start () {
      mBullets = new BulletMotor[mNumBullets];
		for(int i = 0; i < mNumBullets; i++)
      {
         GameObject pew = Instantiate(mBulletPrefab, transform.position,
              Quaternion.Euler(0f, 0f, 0f)) as GameObject;
         mBullets[i] = pew.GetComponent<BulletMotor>();
      }
	}
	
	void Update () {
      deactiveBullet();
	}

   public void activateBullet(Vector3 pos, float rot, BulletData bulletdata)
   {
      for (int i = 0; i < mNumBullets; i++)
      {
         if (mBullets[i].mIsActive == false)
         {
            mBullets[i].mIsActive = true;
            mBullets[i].mBulletData = bulletdata;
            mBullets[i].transform.position = pos;
            mBullets[i].transform.rotation = Quaternion.Euler(0,0,rot);
            //mBullets[i].ApplyBulletData();
            break;
         }
      }
   }

   public void deactiveBullet()
   {

   }
}
