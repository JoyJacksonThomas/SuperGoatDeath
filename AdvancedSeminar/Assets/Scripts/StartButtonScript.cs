using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {
   
	void Start () {
      if (Cursor.visible == false)
      {
         Cursor.visible = true;
      }
	}
	
	void Update () {
		
	}

   private void OnMouseOver()
   {
      GetComponent<SpriteRenderer>().color = Color.red;
   }
   private void OnMouseExit()
   {
      GetComponent<SpriteRenderer>().color = Color.white;
   }

   private void OnMouseDown()
   {
      SceneManager.LoadScene(1);
   }
}
