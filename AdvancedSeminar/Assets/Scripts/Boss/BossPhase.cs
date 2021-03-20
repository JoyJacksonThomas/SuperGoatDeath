using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPhase", menuName = "BossPhase")]
public class BossPhase : ScriptableObject {

   [Serializable]
   public class RealData
   {
      public MovementPattern movementPattern;
      public GunData gunData;
      public float healthPercent;
   }
   public RealData[] data;
}
