using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_AI : MonoBehaviour
{
    Rigidbody2D rb;
    Transform target;

    [Header("추격 속도")]
    [SerializeField] [Range(1f, 10f)] float moveSpeed = 3f;

    [Header("근접 거리")]
    [SerializeField] [Range(0f, 10f)] float contactDistance = 1f;

    bool follow = false;
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else
            rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        follow = true;
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        follow = false;
    }*/
}
