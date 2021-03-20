using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGunType", menuName = "GunType")]
public class GunData : ScriptableObject
{
   public enum ProjectileType
   {
      BULLET,
      SCALING_CIRCLE
   };
   [Serializable]
   public class RealData
   {
      public ProjectileType projectileType;
      public BulletData bullet;
      public int fireRate;
      public Vector2 burstFireData;
      public int bulletSpawnDelay;
      public Vector2 bulletSpawnOffSet;
      public bool spawnAddative;
      public float bulletRotationOffSet;
   }
   public RealData[] data;

}
