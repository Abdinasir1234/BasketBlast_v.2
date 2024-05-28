
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instinstance;


   
    Camera cam;
    private Vector2 startingPosition;
    public Ball[] ball;
    private Ball defaultBall; 
    public Trajectory trajectory;
    [SerializeField] float pushForce = 10f; 

    bool isDragging;

    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    float distance;

    Audio audioManager;
    private void Awake()
    {
       
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
        if (audioManager == null)
        {
            Debug.LogError("Audio manager not found! Make sure there is a GameObject with the tag 'Audio' and the Audio script attached.");
        }
    }

    private void Start()
    {
        cam = Camera.main;

        if (ball.Length > 0)
        {
            startingPosition = ball[0].transform.position; 
        }
    }

    void Update()
    {
        if (defaultBall != null && defaultBall.transform.position.y < -11)
        {
            ResetBallPosition();
        }

        Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            defaultBall = null;
            foreach (var b in ball) 
            {
               
                if (b.col == Physics2D.OverlapPoint(pos))
                {
                    Time.timeScale = 0.1f;
                    isDragging = true;
                    defaultBall = b;
                    OnDragStart();
                    break; 
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (defaultBall != null)
            {
                Time.timeScale = 1f;
                isDragging = false;
                OnDragEnd();
            }
        }
        if (isDragging)
        {
            OnDrag();
        }
    }


    public void ResetBallPosition()
    {
        if (defaultBall != null)
        {
            defaultBall.transform.position = startingPosition;
            defaultBall.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            defaultBall.GetComponent<Rigidbody2D>().angularVelocity = 0;
            defaultBall.GetComponent<Rigidbody2D>().isKinematic = true;
        }

    }

    private void OnDragStart()
    {
   
        if (defaultBall != null)
        {
            defaultBall.GetComponent<Rigidbody2D>().isKinematic = true;
            trajectory.Show();
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnDrag()
    {
        endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (endPoint - startPoint).normalized; 
        force = direction * distance * pushForce; 

        trajectory.UpdateDots(defaultBall.transform.position, force);
    }

    public  void OnDragEnd()
    {
        trajectory.Hide();
        defaultBall.GetComponent<Rigidbody2D>().isKinematic = false;
        defaultBall.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        audioManager.PlaySFX(audioManager.ballThrow);
    }
}

