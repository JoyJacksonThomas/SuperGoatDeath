using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

   public static AudioManager instance = null;

   [Serializable]
   public class RealData
   {
      public string audioID;
      public AudioSource audioSource;
   }
   public RealData[] audioChannels;


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
   }

   void Update() {

   }

   public void playSound(string audioID)
   {
      for (int i = 0; i < audioChannels.Length; i++)
      {
         if(audioChannels[i].audioID == audioID)
         {
            if(audioChannels[i].audioID == "ScalingCircle1" 
               || audioChannels[i].audioID == "ScalingCircle2" 
               || audioChannels[i].audioID == "ScalingCircle3" 
               || audioChannels[i].audioID == "ScalingCircle4")
            {
               GameObject.Find(audioChannels[i].audioID).GetComponent<RandomizedMidi>().RandomizePitch();
            }
            audioChannels[i].audioSource.Play();
         }
      }
   }

   public void pauseSound(string audioID)
   {
      for (int i = 0; i < audioChannels.Length; i++)
      {
         if (audioChannels[i].audioID == audioID)
         {
            audioChannels[i].audioSource.Pause();
         }
      }
   }

   public bool getIsPlaying(string audioID)
   {
      for (int i = 0; i < audioChannels.Length; i++)
      {
         if (audioChannels[i].audioID == audioID)
         {
            return audioChannels[i].audioSource.isPlaying;
         }
      }
      return false;
   }
   
}
