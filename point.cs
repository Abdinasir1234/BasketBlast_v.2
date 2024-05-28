using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point : MonoBehaviour
{
    public bool isCollidingWithBall;
    Audio audioManager;
    private void Awake()
    {

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        if (audioManager == null)
        {
            Debug.LogError("Audio manager not found! Make sure there is a GameObject with the tag 'Audio' and the Audio script attached.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            isCollidingWithBall = true;
            Score.instance.AddPoint();
            audioManager.PlaySFX(audioManager.levelUp);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
                isCollidingWithBall = false;
        }
    }

    private void Update()
    {
        if (!isCollidingWithBall)
        {
           Score.instance.CheckHighScore();
        }

        else
        {
            
        }

    }

   

}
