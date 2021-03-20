using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour {
   public TrailRenderer trailRenderer;
   private GunMotor mGunMotor;
   private Rigidbody2D mRigidbody2D;
   public float speed = 0;
   public float dashLength = 2;
   private float facingDirection = 0;
   public bool softLocked = false;
   public float softLockSpeed = 10f;
   public float softLockRadius = 10f;
   public Transform bossTransform;
   private Health mHealth;

   public bool parrying;
   public Color parryingColor;
   public int numParryingFrames = 0;
   private int currentParryingFrames = 0;

   public Vector4 leftRightDownUpBounds = Vector4.zero;
   
   private LineRenderer mDashLine;
   public bool dashLineVisible = false;
   public int numFramesDashVisible = 60;
   private int currentDashFrame = 0;
   public float dashThickness = 0;
   private float currentDashThickness = 0;

   void Start () {
      mRigidbody2D = GetComponent<Rigidbody2D>();
      mGunMotor = GetComponent<GunMotor>();
      mHealth = GetComponent<Health>();
      mDashLine = GameObject.Find("Line").GetComponent<LineRenderer>();
      dashThickness = mDashLine.startWidth;
   }
	
	void Update () {
      if (mHealth.mHealthDepleted)
      {
         this.gameObject.SetActive(false);
         SceneManager.LoadScene(1);
      }

      if(parrying)
      {
         currentParryingFrames++;
         if(currentParryingFrames > numParryingFrames)
         {
            currentParryingFrames = 0;
            parrying = false;
            GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = Color.white;
         }
      }

      if(dashLineVisible)
      {
         currentDashFrame++;
         currentDashThickness = (float)(numFramesDashVisible - currentDashFrame) / (float)(numFramesDashVisible);
         currentDashThickness *= dashThickness;
         //mDashLine.startWidth = currentDashThickness;
         //mDashLine.endWidth = currentDashThickness;
         mDashLine.SetWidth(currentDashThickness, currentDashThickness);
         if (currentDashFrame > numFramesDashVisible)
         {
            currentDashFrame = 0;
            dashLineVisible = false;
            for (int i = 0; i < 5; i++)
            {
               mDashLine.SetPosition(i, Vector3.zero);
            }
         }
      }
   }

   public void Move(Vector2 _direction)
   {
      if(!parrying)
      {
         mRigidbody2D.velocity = _direction * speed;
         Vector2 clampedPosition = Vector2.zero;
         clampedPosition.x = Mathf.Clamp(transform.position.x, leftRightDownUpBounds.x, leftRightDownUpBounds.y);
         clampedPosition.y = Mathf.Clamp(transform.position.y, leftRightDownUpBounds.z, leftRightDownUpBounds.w);
         transform.position = clampedPosition;
      }
   }

   public void Aim(Vector2 _leftStick, Vector2 _rightStick)
   {
      if (!parrying)
      {
         if (_rightStick != Vector2.zero)
         {
            SoftLock(_rightStick);
            transform.rotation = Quaternion.Euler(0, 0, facingDirection);
            mGunMotor.Fire();
         }
         else if (_leftStick != Vector2.zero)
         {
            _leftStick.x = -_leftStick.x;
            facingDirection = Vector3.Angle(_leftStick, Vector2.up);
            if (_leftStick.x < 0)
            {
               facingDirection = -facingDirection;
            }
            transform.rotation = Quaternion.Euler(0, 0, facingDirection);
         }
         
      }
         
   }

   public void Dash(bool _dash, Vector2 _leftStick, Vector2 _rightStick)
   {
      if(!parrying)
      {
         if (_dash)
         {
            Vector2 dashDir = Vector2.zero;
            if (_leftStick == Vector2.zero)
            {
               dashDir = ((Vector2)transform.up * dashLength);
            }
            else
            {
               dashDir = (_leftStick.normalized * dashLength);
            }
            for(int i = 0; i < 5; i++)
            {
               mDashLine.SetPosition(i, transform.position + (Vector3)(((float)i/4)*dashDir));
            }
            transform.position = ((Vector2)transform.position + dashDir);
            dashLineVisible = true;
            currentDashFrame = 0;
            currentDashThickness = dashThickness;
         }
      }
   }

   private void SoftLock(Vector2 _rightStick)
   {
      Vector2 bossDirectionVector = bossTransform.position - transform.position;
      float bossAngleDiff = Vector3.Angle(bossDirectionVector, _rightStick);
      if (bossDirectionVector.x < 0)
      {
         bossAngleDiff = -bossAngleDiff;
      }
      if(Mathf.Abs(bossAngleDiff) < softLockRadius)
      {
         //
         float bossDir = Vector3.Angle(bossDirectionVector, Vector2.up);
         if (bossDirectionVector.x > 0)
         {
            bossDir = -bossDir;
         }
         facingDirection = Mathf.Lerp(facingDirection, bossDir, softLockSpeed);
         softLocked = true;
      }
      else
      {
         _rightStick.x = -_rightStick.x;
         facingDirection = Vector3.Angle(_rightStick, Vector2.up);
         if (_rightStick.x < 0)
         {
            facingDirection = -facingDirection;
         }
         softLocked = false;
      }
   }

   public void Parry(bool _parry)
   {
      if(_parry)
      {
         mRigidbody2D.velocity = Vector2.zero;
         parrying = true;
         GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().color = parryingColor;
      }
   }
}
