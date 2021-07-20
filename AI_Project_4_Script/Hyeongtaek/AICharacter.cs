using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : MonoBehaviour
{
    public GameObject playerMesh;
    public GameObject altar;
    public Transform playerMark;
    public Transform cubeMark;
    public Transform cubeMesh;
    public Transform currentPlayerPosition;
    public Transform currentCubePosition;

    public float proximityValueX;
    public float proximityValueY;
    public float nearValue;

    public float cubeProximityX;
    public float cubeProximityY;
    public float nearCube;

    public float cubeMarkProximityX;
    public float cubeMarkProximityY;
    public bool cubeproX;
    public bool cubeproY;

    public bool playerOnMark;
    public bool cubeIsNear;

    public float speed;
    public bool Finding;

    public bool Finish;

    // Start is called before the first frame update
    void Start()
    {
        nearValue = 0.5f;
        speed = 2f;
    }
    
    // Update is called once per frame
    void Update()
    {

        // Calculates the current position of the player
        currentPlayerPosition.transform.position = playerMesh.transform.position;

        // Calculates the distance between the player and the player mark of the X axis
        proximityValueX = playerMark.transform.position.x - currentPlayerPosition.transform.position.x;

        // Calculates the distance between the player and the player mark of the Y axis
        proximityValueY = playerMark.transform.position.y - currentPlayerPosition.transform.position.y;

        // Calculates if the player is near of his mark position
        if ((proximityValueX + proximityValueY) < nearValue)
        {
            playerOnMark = true;
        }

        if (playerOnMark == true && cubeIsNear == false)
        {
            PositionChanging();
        }

        if (playerOnMark == true && cubeIsNear == true)
        {
            Finding = false;

            cubeMarkProximityX = cubeMark.position.x - currentCubePosition.transform.position.x;
            cubeMarkProximityY = cubeMark.position.y - currentCubePosition.transform.position.y;
            if (cubeMarkProximityX < 0)
            {
                cubeproX = true;
            }
            else
            {
                cubeproX = false;
            }
            if (cubeMarkProximityY < 0)
            {
                cubeproY = true;
            } else
            {
                cubeproY = false;
            }


            if (Mathf.Abs(cubeMarkProximityX) < Mathf.Abs(cubeMarkProximityY))
            {
                MoveX2();
                if (transform.position.y == cubeMesh.transform.position.y + 1.0f || transform.position.y == cubeMesh.transform.position.y - 1.0f)
                {
                    MoveX1();
                    if (transform.position.x == currentCubePosition.transform.position.x)
                    {
                        PushY();
                    }
                }
            }

            if (Mathf.Abs(cubeMarkProximityX) > Mathf.Abs(cubeMarkProximityY))
            {
                MoveY1();
                if (transform.position.x == cubeMesh.transform.position.x + 1.0f || transform.position.x == cubeMesh.transform.position.x - 1.0f)
                {
                    MoveY2();
                    if (transform.position.y == cubeMesh.transform.position.y)
                    {
                        PushX();
                    }
                }
            }
        }
    }

    void PositionChanging()
    {
        Finding = true;
        Vector2 positionA = transform.position;
        Vector2 positionB = cubeMesh.transform.position;
        transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
    }
    void MoveY1()
    {
        if (cubeproX == true)
        {
            if (cubeproY == true)
            {
                if (cubeMesh.transform.position.x - cubeMark.transform.position.x < 0)
                {
                    Vector2 positionA = transform.position;
                    Vector2 positionB = new Vector2(cubeMesh.transform.position.x - 1.0f, transform.position.y);
                    transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }

                if (cubeMesh.transform.position.x - cubeMark.transform.position.x > 0)
                {
                    Vector2 positionA = transform.position;
                    Vector2 positionB = new Vector2(cubeMesh.transform.position.x + 1.0f, transform.position.y);
                    transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }
            }
            else
            {
                if (cubeMesh.transform.position.y - cubeMark.transform.position.y < 0)
                {
                    Vector2 positionA = transform.position;
                    Vector2 positionB = new Vector2(cubeMesh.transform.position.x + 1.0f, transform.position.y);
                    transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }

                if (cubeMesh.transform.position.y - cubeMark.transform.position.y > 0)
                {
                    Vector2 positionA = transform.position;
                    Vector2 positionB = new Vector2(cubeMesh.transform.position.x - 1.0f, transform.position.y);
                    transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }
            }
        }
        else
        {
            if (cubeproY == true)
            {
                if (cubeMesh.transform.position.x - cubeMark.transform.position.x > 0)
                {
                    Vector2 positionA = transform.position;
                    Vector2 positionB = new Vector2(cubeMesh.transform.position.x + 1.0f, transform.position.y);
                    transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }

                if (cubeMesh.transform.position.x - cubeMark.transform.position.x < 0)
                {
                    Vector2 positionA = transform.position;
                    Vector2 positionB = new Vector2(cubeMesh.transform.position.x - 1.0f, transform.position.y);
                    transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }
            }
            else
            {
                if (cubeMesh.transform.position.y - cubeMark.transform.position.y > 0)
                {
                    Vector2 positionA = transform.position;
                    Vector2 positionB = new Vector2(cubeMesh.transform.position.x + 1.0f, transform.position.y);
                    transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }

                if (cubeMesh.transform.position.y - cubeMark.transform.position.y < 0)
                {
                    Vector2 positionA = transform.position;
                    Vector2 positionB = new Vector2(cubeMesh.transform.position.x - 1.0f, transform.position.y);
                    transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }
            }
        }
    }

    void MoveY2()
    { 
        Vector2 positionA = transform.position;
        Vector2 positionB = new Vector2(transform.position.x, cubeMesh.position.y);
        transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
    }
    void MoveX1()
    {
        Vector2 positionA = transform.position;
        Vector2 positionB = new Vector2(cubeMesh.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
    }

    void MoveX2()
    {
        if (cubeproX == true)
        {
            if (cubeproY == false)
            {
                if (cubeMesh.transform.position.y - cubeMark.transform.position.y > 0)
                {
                    Vector2 positionA = this.transform.position;
                    Vector2 positionB = new Vector2(this.transform.position.x, cubeMesh.position.y + 1.0f);
                    this.transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }

                if (cubeMesh.transform.position.y - cubeMark.transform.position.y < 0)
                {
                    Vector2 positionA = this.transform.position;
                    Vector2 positionB = new Vector2(this.transform.position.x, cubeMesh.position.y - 1.0f);
                    this.transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }
            }
            else
            {
                if (cubeMesh.transform.position.x - cubeMark.transform.position.x < 0)
                {
                    Vector2 positionA = this.transform.position;
                    Vector2 positionB = new Vector2(this.transform.position.x, cubeMesh.position.y - 1.0f);
                    this.transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }

                if (cubeMesh.transform.position.x - cubeMark.transform.position.x > 0)
                {
                    Vector2 positionA = this.transform.position;
                    Vector2 positionB = new Vector2(this.transform.position.x, cubeMesh.position.y + 1.0f);
                    this.transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }
            }
        }
        else
        {
            if (cubeproY == true)
            {
                if (cubeMesh.transform.position.y - cubeMark.transform.position.y > 0)
                {
                    Vector2 positionA = this.transform.position;
                    Vector2 positionB = new Vector2(this.transform.position.x, cubeMesh.position.y + 1.0f);
                    this.transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }

                if (cubeMesh.transform.position.y - cubeMark.transform.position.y < 0)
                {
                    Vector2 positionA = this.transform.position;
                    Vector2 positionB = new Vector2(this.transform.position.x, cubeMesh.position.y - 1.0f);
                    this.transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }
            }
            else
            {
                if (cubeMesh.transform.position.x - cubeMark.transform.position.x < 0)
                {
                    Vector2 positionA = this.transform.position;
                    Vector2 positionB = new Vector2(this.transform.position.x, cubeMesh.position.y - 1.0f);
                    this.transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }

                if (cubeMesh.transform.position.x - cubeMark.transform.position.x > 0)
                {
                    Vector2 positionA = this.transform.position;
                    Vector2 positionB = new Vector2(this.transform.position.x, cubeMesh.position.y + 1.0f);
                    this.transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
                }
            }
        }
    }

    void PushX()
    {
        Vector2 positionA = transform.position;
        Vector2 positionB = new Vector2(cubeMark.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
    }
    void PushY()
    {
        Vector2 positionA = transform.position;
        Vector2 positionB = new Vector2(transform.position.x, cubeMark.position.y);
        transform.position = Vector2.MoveTowards(positionA, positionB, Time.deltaTime * speed);
    }
}
