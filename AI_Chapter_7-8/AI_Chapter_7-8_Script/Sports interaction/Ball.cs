using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 curPos;
    public Vector3 lastPos;
    public static Transform characterPos;
    public float speed;
    public int RedScore;
    public int BlueScore;
    public GameObject ball;
    public GameObject Red1;
    public GameObject Red6;
    public GameObject Red2;
    public GameObject Red3;
    public GameObject Red4;
    public GameObject Red5;
    public GameObject Blue1;
    public GameObject Blue2;
    public GameObject Blue3;
    public GameObject Blue4;
    public GameObject Blue5;
    public GameObject Blue6;

    //public bool ballMoving;

    void Start()
    {
        characterPos = this.transform;
        speed = 2f;
        RedScore = 0;
        BlueScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RedTeam.teamDistance = 10;
        BlueTeam.teamDistance = 10;
        curPos = transform.position;
        //if(Mathf.Round(curPos.x*10) == Mathf.Round(lastPos.x*10) && Mathf.Round(curPos.z * 10) == Mathf.Round(lastPos.z * 10))
        //{
        // ballMoving = false;
        //}
        //else
        //{
        //ballMoving = true;

        //}
        lastPos = curPos;
        Vector3 positionA = this.transform.position;
        Vector3 positionB = characterPos.transform.position;
        this.transform.position = Vector3.Lerp(positionA, positionB, Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlueGoal")
        {
            RedScore += 1;
            ball.transform.position = new Vector3(0, 0, 0);
            Red1.transform.position = new Vector3(-5, 0, 18);
            Red2.transform.position = new Vector3(5, 0, 18);
            Red3.transform.position = new Vector3(-5, 0, 9);
            Red4.transform.position = new Vector3(0, 0, 9);
            Red5.transform.position = new Vector3(5, 0, 9);
            Blue1.transform.position = new Vector3(-5, 0, -18);
            Blue2.transform.position = new Vector3(5, 0, -18);
            Blue3.transform.position = new Vector3(-5, 0, -9);
            Blue4.transform.position = new Vector3(0, 0, -9);
            Blue5.transform.position = new Vector3(5, 0, -9);
            Blue6.transform.position = new Vector3(4, 0, 0);
            Red6.transform.position = new Vector3(0, 0, 18);
        }

        if (other.gameObject.tag == "RedGoal")
        {
            BlueScore += 1;
            ball.transform.position = new Vector3(0, 0, 0);
            Red1.transform.position = new Vector3(-5, 0, 18);
            Red2.transform.position = new Vector3(5, 0, 18);
            Red3.transform.position = new Vector3(-5, 0, 9);
            Red4.transform.position = new Vector3(0, 0, 9);
            Red5.transform.position = new Vector3(5, 0, 9);
            Blue1.transform.position = new Vector3(-5, 0, -18);
            Blue2.transform.position = new Vector3(5, 0, -18);
            Blue3.transform.position = new Vector3(-5, 0, -9);
            Blue4.transform.position = new Vector3(0, 0, -9);
            Blue5.transform.position = new Vector3(5, 0, -9);
            Red6.transform.position = new Vector3(4, 0, 0);
            Blue6.transform.position = new Vector3(0, 0, -18);
        }
    }

}
