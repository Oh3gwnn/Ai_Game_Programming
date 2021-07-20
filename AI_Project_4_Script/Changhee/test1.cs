using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Box" || other.gameObject.tag == "startBox" ) {
            Destroy(other.gameObject);
        }
    }
}
