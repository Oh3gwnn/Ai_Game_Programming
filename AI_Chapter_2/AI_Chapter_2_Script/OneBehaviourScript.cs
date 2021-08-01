using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public GameObject One;
    private Transform tr;
    
    void Start()
    {
        One.transform.position = new Vector3(0, 0, 0);
        tr = GetComponent<Transform>();
    }
    
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h > 0) transform.localScale = new Vector3(-5, 5, 1);
        else if (h < 0) transform.localScale = new Vector3(5, 5, 1);
    
        tr.Translate(new Vector3(h,v,0) * moveSpeed * Time.deltaTime);
    }
}
