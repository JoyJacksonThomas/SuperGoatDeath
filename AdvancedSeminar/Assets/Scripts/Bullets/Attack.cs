using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

   public string mTargetID = "";
   public GameObject mTarget;

   // Use this for initialization
   void Start () {
      mTarget = GameObject.Find(mTargetID);
   }
	
	// Update is called once per frame
	void Update () {
		
	}

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.gameObject.name == mTargetID)
      {
         //mTarget.GetComponent<Health>().damage(mBulletData.damage);
         Destroy(gameObject);
      }
   }
}
