using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{

    [SerializeField] int dotNumber;
    [SerializeField] GameObject DotsParent;
    [SerializeField] GameObject DotsPrefab;
    [SerializeField] float dotSpacing;
    [SerializeField] [Range(0.01f,0.5f)] float dotMinScale;
    [SerializeField] [Range(0.5f,1f)] float dotMaxScale;

    Transform[] dotsList;
    Vector2 pos;
    float TimeStamp;

    private void Start()
    {
        prepareDots();
        Hide();
    }

    void Update()
    {
        
    }

    void prepareDots()
    {
        dotsList = new Transform[dotNumber];
        DotsPrefab.transform.localScale = Vector3.one*dotMaxScale;

        float scale = dotMaxScale;
        float scalefactor = scale/dotNumber;

        for (int i = 0; i < dotNumber; i++)
        {
            dotsList[i] = Instantiate(DotsPrefab,null).transform;
            dotsList[i].parent = DotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
            {
                scale -= scalefactor;
            }

        }
    }

    public void UpdateDots(Vector2 startPosition, Vector2 force)
    {
        
        float timeStep = 0.1f; 

        for (int i = 0; i < dotsList.Length; i++)
        {
            float t = i * timeStep;
            Vector2 dotPosition = CalculateDotPosition(startPosition, force, t);
            dotPosition.y = startPosition.y + dotPosition.y; 
            dotsList[i].transform.position = dotPosition;
        }
    }

   
    Vector2 CalculateDotPosition(Vector2 startPosition, Vector2 force, float time)
    {
        
        Vector2 velocity = force / 1;
        float gravity = Physics2D.gravity.y;

        float x = startPosition.x + velocity.x * time;
        float y = startPosition.y + velocity.y * time + (0.5f * gravity * Mathf.Pow(time, 2));

        return new Vector2(x, y);
    }

    public void Show()
    {
        DotsParent.SetActive(true);
    }
    public void Hide()
    {
        DotsParent.SetActive(false);
    }
}
