using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviourScript : MonoBehaviour
{
    public GameObject One;
    Transform Onetr;
    void Start()
    {
        Onetr = One.transform;
    }

    void Update()
    {
        transform.position = new Vector3(Onetr.position.x, Onetr.position.y, transform.position.z);
    }
}
