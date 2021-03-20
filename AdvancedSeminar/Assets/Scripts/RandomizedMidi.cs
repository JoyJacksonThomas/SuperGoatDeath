using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedMidi : MonoBehaviour {

   private AudioSource mAudio;
   public int[] mNoteValues = { 0, 2, 4, 5, 7, 9, 11, 12 };
   private int currentNote = 0;

   private void Start()
   {
      //{ 0, 2, 4, 5, 7, 9, 11, 12 };
      mAudio = GetComponent<AudioSource>();
   }

   void Update() {
      
   }

   public void RandomizePitch()
   {
      int randNote = Random.Range(0, mNoteValues.Length);
      if (currentNote == randNote)
      {
         randNote++;
         randNote %= mNoteValues.Length;
      }
      currentNote = randNote;
      float pitch = Mathf.Pow(2, mNoteValues[currentNote] / 12.0f);
      
      mAudio.pitch = pitch;
   }

}
