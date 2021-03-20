using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

   private GunMotor mGunMotor;
   private Rigidbody2D mRigidbody2D;
   private Health mHealth;

   void Start()
   {
      mRigidbody2D = GetComponent<Rigidbody2D>();
      mHealth = GetComponent<Health>();
      mGunMotor = GetComponent<GunMotor>();
      transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      Cursor.visible = false;
   }

   void Update ()
   {
      HandleInput();
      if(mHealth.mHealthDepleted)
      {
         this.gameObject.SetActive(false);
         SceneManager.LoadScene(2);
      }
   }

   void HandleInput()
   {
      
      if (Input.GetButton("Fire1"))
      {
         mGunMotor.Fire();
         if(AudioManager.instance.getIsPlaying("PlayerFire") == false)
         {
            AudioManager.instance.playSound("PlayerFire");
         }
      }
      if(Input.GetButtonUp("Fire1"))
      {
         if (AudioManager.instance.getIsPlaying("PlayerFire") == true)
         {
            AudioManager.instance.pauseSound("PlayerFire");
         }
      }

      Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 3);
      mousePos.x = Mathf.Clamp(mousePos.x, -3.8f, 3.8f);
      mousePos.y = Mathf.Clamp(mousePos.y, -4.7f, 4.8f);
      transform.position = mousePos;


      //Vector2 mouseVel = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
      //mRigidbody2D.velocity = mouseVel;
   }


   void OnTriggerExit2D(Collider2D col)
   {
      if(col.tag == "PlayArea")
      {
      }
   }
   void OnTriggerStay2D(Collider2D col)
   {
      if (col.tag == "PlayArea")
      {
      }
   }


}
