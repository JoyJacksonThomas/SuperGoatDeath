using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector2 leftStick = Vector2.zero;
    public Vector2 rightStick = Vector2.zero;
    public bool dash = false;
    public bool counter = false;
    private PlayerMotor mPlayerMotor;

    void Start()
    {
        mPlayerMotor = GetComponent<PlayerMotor>();
    }

    void Update()
    {

        leftStick.x = Input.GetAxis("Horizontal");
        leftStick.y = Input.GetAxis("Vertical");
        rightStick.x = Input.GetAxis("RightHorizontal");
        rightStick.y = Input.GetAxis("RightVertical");
        dash = Input.GetButtonDown("Fire1");
        counter = Input.GetButtonDown("Fire2");
        mPlayerMotor.Aim(leftStick, rightStick);
        mPlayerMotor.Dash(dash, leftStick, rightStick);
        mPlayerMotor.Parry(counter);


    }

    void FixedUpdate()
    {
        mPlayerMotor.Move(leftStick);
    }
}
