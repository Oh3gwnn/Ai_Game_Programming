using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTeam : MonoBehaviour
{
    public float speed;
    public GameObject ball;
    public bool hasBall;
    public float ballDistance;
    public static float teamDistance;
    public bool nearTheBall;
    public List<Transform> teamCharacters;
    public int randomChoice;
    public float teamdist;
    public int randomAction;
    public GameObject Blue;
    public List<Transform> goal;
    public int randomgoal;
    public int randomd;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        teamDistance = 5f;
        hasBall = false;
        randomd = Random.Range(0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        teamdist = teamDistance;
        randomAction = Random.Range(1, 100);
        randomChoice = Random.Range(0, 5);
        randomgoal = Random.Range(0, 6);
        
        if (hasBall == false && nearTheBall == true)
        {
            Vector3 positionA = this.transform.position;
            Vector3 positionB = ball.transform.position;
            this.transform.position = Vector3.MoveTowards(positionA, positionB, Time.deltaTime * speed);
        }
        if (Mathf.Abs(this.transform.position.z - ball.transform.position.z) < 1.28)
        {
            hasBall = true;
            ball.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z - 1.25f);
        }

        if (Mathf.Abs(this.transform.position.z - ball.transform.position.z) >= 1.32)
            hasBall = false;

        if (hasBall == true)
        {
            Vector3 positionR = this.transform.position;
            Vector3 positionBG = goal[randomd].transform.position;
            this.transform.position = Vector3.MoveTowards(positionR, positionBG, Time.deltaTime * speed);
            if (Mathf.Abs(this.transform.position.z - Blue.transform.position.z) < 20f)
            {
                GameObject.Find("Ball").GetComponent<Ball>().speed = 3f;
                Rshoot();
                hasBall = false;
            }

            if (Physics.SphereCast(transform.position, 5f, Vector3.forward, out RaycastHit hit, 5f, LayerMask.GetMask("Blue"))!=true)
            {
                GameObject.Find("Ball").GetComponent<Ball>().speed = 2f;
                passBall();
                hasBall = false;
            }

        }

        ballDistance = Vector3.Distance(transform.position, ball.transform.position);
        if (teamDistance > ballDistance)
        {
            teamDistance = ballDistance;
        }
        if (teamDistance == ballDistance)
        {
            nearTheBall = true;
        }
        if (teamDistance < ballDistance)
        {
            nearTheBall = false;
        }

    }

    void passBall()
    {
        
        
        Ball.characterPos = teamCharacters[randomChoice];
    }

    void Rshoot()
    {
        
        Ball.characterPos = goal[randomgoal];
    }
}
