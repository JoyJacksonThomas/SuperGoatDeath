using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBullet", menuName = "BulletType")]
public class BulletData : ScriptableObject{
   public enum FireType
   {
      STRAIGHT,
      SEEK_PERFECT
   };
   public enum EffectType
   {
      NO_EFFECT,
      POISON_EFFECT
   };
   public int damage;
   public float speed;
   public float startForce;
   public float life;
   public FireType fireType;
   public EffectType effectType;
   public Sprite sprite;
   public Color color;
   public float scale;
}
