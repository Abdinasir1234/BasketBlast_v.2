using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool isCollidingWithBall;
    public GameObject FinishLevel;

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
            FinishLevel.SetActive(true);
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
}
