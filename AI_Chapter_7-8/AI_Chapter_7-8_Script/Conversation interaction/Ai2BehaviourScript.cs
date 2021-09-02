using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ai2BehaviourScript : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public GameObject AiObject;
    Rigidbody2D rb;
    SpriteRenderer sr;
    public bool interaction;
    public bool movedelay;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ai")
        {
            if (interaction == true)
            {
                Vector3 dir = (AiObject.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(AiObject.transform.position, transform.position);
                CharacterAttribute targetAttribute = AiObject.GetComponent<CharacterAttribute>();
                CharacterAttribute myAttribute = gameObject.GetComponent<CharacterAttribute>();

                if (distance > targetAttribute.Radius + myAttribute.Radius)
                {
                    transform.position += dir * moveSpeed * Time.deltaTime;
                }
                if (distance <= targetAttribute.Radius + myAttribute.Radius)
                {
                    rb.velocity = new Vector3(0, 0);
                    sr.color = Color.green;
                    StartCoroutine("Interacting");
                    StartCoroutine("ColorDelay");
                }
            }

            if (interaction == false)
            {
                Vector3 dir2 = (transform.position - AiObject.transform.position).normalized;
                CharacterAttribute targetAttribute = AiObject.GetComponent<CharacterAttribute>();
                CharacterAttribute myAttribute = gameObject.GetComponent<CharacterAttribute>();
                transform.position += dir2 * moveSpeed * Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        interaction = true;
        movedelay = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        interaction = false;
        movedelay = false;
    }

    void Start()
    {
        interaction = false;
        movedelay = false;
        sr = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (movedelay == false)
        {
            StartCoroutine("RandomMove");
        }
    }

    IEnumerator ColorDelay()
    {
        yield return new WaitForSeconds(3f);
        if (sr.color == Color.green)
        {
            sr.color = Color.blue;
            interaction = true;
        }
    }
    IEnumerator Interacting()
    {
        yield return new WaitForSeconds(3f);
        Vector3 dir2 = (transform.position - AiObject.transform.position).normalized;
        transform.position += dir2 * moveSpeed * Time.deltaTime;
        interaction = false;
    }
    IEnumerator RandomMove()
    {
        movedelay = true;
        float dir1 = Random.Range(-1.0f, 1.0f);
        float dir2 = Random.Range(-1.0f, 1.0f);
        rb.velocity = new Vector3(dir1, dir2);
        yield return new WaitForSeconds(2f);
        if (interaction == false)
        {
            movedelay = false;
        }
    }
}
