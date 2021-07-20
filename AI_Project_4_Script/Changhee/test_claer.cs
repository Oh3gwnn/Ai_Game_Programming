using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class test_claer : MonoBehaviour
{
    private GameObject portal;
    private GameObject textui;
    void Start() {
        portal = GameObject.FindWithTag("portal");
        textui = GameObject.FindWithTag("cleartext");
        portal.SetActive(false);
        textui.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "cube") {
            Destroy(other.gameObject);
            portal.SetActive(true);
            textui.SetActive(true);
        }
    }
}
