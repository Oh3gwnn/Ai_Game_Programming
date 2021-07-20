using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueCol : MonoBehaviour
{
    public GameObject Chest;
    public GameObject Targetai;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().position = new Vector2 (Targetai.transform.position.x, Targetai.transform.position.y);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "cube")
        {
            Targetai.GetComponent<AICharacter>().cubeIsNear = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "cube")
        {
            Targetai.GetComponent<AICharacter>().cubeIsNear = false;
        }
    }
}
