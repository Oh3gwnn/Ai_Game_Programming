using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2_AI : MonoBehaviour
{
    private int startCount = 0;
    private Transform cube;
    private Vector3 cubePosition;
    private Transform cubeMark;
    private Vector3 cubeMarkPosition;
    private Vector3 calPosition;
    private List<float> boxArray = new List<float>();
    private float c_near = 0;
    private float c_nearvalue = 0;
    private List<GameObject> boxobj;
    private bool state;
    private GameObject test;
    private Vector3 testposi;


    void Start() {
        cube = GameObject.FindWithTag("cube").transform;
        cubePosition = cube.position;
        cubeMark = GameObject.FindWithTag("cubeMark").transform;
        cubeMarkPosition = cubeMark.position;
        test = GameObject.FindWithTag("cubeMark");
        testposi = test.GetComponent<BoxCollider2D>().bounds.center;
        // boxobj= new List<GameObject>(GameObject.FindGameObjectsWithTag("Box"));
        // GetComponent<PolygonCollider2D>()
        // GetComponent<PolygonCollider2D>().bounds.center;
    }

    void Update() {
        boxobj= new List<GameObject>(GameObject.FindGameObjectsWithTag("Box"));
        cubePosition = cube.position;
        if (startCount < 3) {
            transform.position = Vector3.MoveTowards(transform.position, cubePosition, Time.deltaTime);
        }
    }

    void path() {
        boxArray.Clear();
        state = false;
        foreach (GameObject box in boxobj) {
            if (box.transform.position.y - cubePosition.y > -1) state = true;
            if (box.transform.position.y > cubePosition.y && box.transform.position.y - cubePosition.y < 1){
                boxArray.Add(box.transform.position.x);
            }
        }
        if (state) {
            if (boxArray.Count == 0) {
                cube.Translate(Vector2.up * 0.5f);
                transform.Translate(Vector2.down * 0.2f);
            } else {
                c_near = Mathf.Abs(cubePosition.x - boxArray[0]);
                c_nearvalue = boxArray[0];
                for (int i=1; i<boxArray.Count; i++) {
                    if (Mathf.Abs(cubePosition.x - boxArray[i]) < c_near) {
                    c_near = Mathf.Abs(cubePosition.x - boxArray[i]);
                    c_nearvalue = boxArray[i];  
                    }
                }
                if (c_near > 1.2f) {
                    cube.Translate(Vector2.up * 0.5f);
                } else {
                    if (cubePosition.x - c_nearvalue > 0.1) {
                        cube.Translate(Vector2.right);
                        transform.Translate(Vector2.left * 0.2f);
                        transform.Translate(Vector2.down * 0.2f);
                    }
                    if (cubePosition.x - c_nearvalue < 0.1) {
                        cube.Translate(Vector2.left);
                        transform.Translate(Vector2.down * 0.2f);
                        transform.Translate(Vector2.right * 0.2f);

                    }
                }
            }
        } else {
                // cube.transform.position = Vector3.MoveTowards(cube.transform.position, cubeMarkPosition, 0.2f);
                cube.transform.position = Vector3.MoveTowards(cube.transform.position, testposi, 0.2f);
                calPosition = cubeMarkPosition - cube.transform.position;
                calPosition.Normalize();
                transform.Translate(-calPosition * 0.3f);

                // transform.position = -Vector3.MoveTowards(transform.position, cubeMarkPosition, 0.2f);
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "cube") {
            // Invoke("path", 0.5f);
            path();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "startBox") {
            startCount++;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "startBox") {
            startCount--;
        }
    }
}