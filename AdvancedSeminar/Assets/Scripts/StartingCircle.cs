using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartingCircle : MonoBehaviour
{
    public int sceneToLoad = 0;
    public bool mPlayerOver = false;

    public int TIME_TO_START = 4;
    private float mTimeToStart = 3;

    private TextMeshProUGUI cirleTimerText;
    private bool mAudioPlayed = false;

    private StartingCircle[] otherCircles;// = new StartingCircle[3];

    void Start()
    {
        cirleTimerText = GameObject.Find("CircleTimerText").GetComponent<TextMeshProUGUI>();
        otherCircles = FindObjectsOfType<StartingCircle>();
    }

    void Update()
    {
        if (mAudioPlayed)
        {
            if (AudioManager.instance.getIsPlaying("GameStart") == false)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else
        {
            if (mPlayerOver == true)
            {
                mTimeToStart -= Time.deltaTime;
                cirleTimerText.text = ((int)mTimeToStart).ToString();
            }
            else
            {
                mTimeToStart = TIME_TO_START;
                cirleTimerText.text = "";
            }
        }


        if (mTimeToStart < 1 && mAudioPlayed == false)
        {
            AudioManager.instance.playSound("GameStart");
            mAudioPlayed = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            mPlayerOver = true;
            for(int i = 0; i < otherCircles.Length; i++)
            {
                if(otherCircles[i] != this)
                {
                    otherCircles[i].enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            mPlayerOver = false;
            for (int i = 0; i < otherCircles.Length; i++)
            {
                if (otherCircles[i] != this)
                {
                    otherCircles[i].enabled = true;
                }
            }
        }
    }
}
