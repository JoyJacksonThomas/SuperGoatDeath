using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMovementPattern", menuName = "MovementPattern")]
public class MovementPattern : ScriptableObject{

   public enum AimingType
   {
      FACING_FORWARD,
      LOOK_AT_PLAYER,
      SPINNING,
      NUM_AIMING_TYPES
   }
   public AimingType aimingType = AimingType.FACING_FORWARD;

   [Serializable]
   public class RealData
   {
      public Vector2 target;
      public float speedToTarget;
      public bool shootOnTravel;
      public bool shootOnArrive;
      public float rotationSpeed;
   }
   public RealData[] data;
   public int mCurrentTargetIndex = 0;
}
