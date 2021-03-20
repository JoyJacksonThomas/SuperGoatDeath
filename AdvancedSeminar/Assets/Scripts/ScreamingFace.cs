using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamingFace : MonoBehaviour {

   public static ScreamingFace instance = null;

   int mNumFramesOnScreen = -1;

   SpriteRenderer mSpriteRenderer;

   private void Awake()
   {
      if (instance != null && instance != this)
      {
         Destroy(this.gameObject);
      }
      else
      {
         instance = this;
      }
      mSpriteRenderer = GetComponent<SpriteRenderer>();
   }

   void Update () {
		if(gameObject.activeSelf == true)
      {
         mNumFramesOnScreen--;
      }
      if (mNumFramesOnScreen < 0)
      {
         gameObject.SetActive(false);
      }
	}

   public void FlashOnScreen(int numFrames)
   {
      if(mSpriteRenderer.enabled == false)
      {
         //mSpriteRenderer.enabled = true;
      }
      gameObject.SetActive(true);
      mNumFramesOnScreen = numFrames;
      float posX = Random.Range(-2.7f, 2.7f);
      float posY = Random.Range(-2.7f, 3f);
      transform.position = new Vector3(posX, posY, transform.position.z);
   }
}
