using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public GameObject Target;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 dir = Target.transform.position - transform.position;
            dir.Normalize();

            float distance = Vector3.Distance(Target.transform.position, transform.position);
            CharacterAttribute targetAttribute = Target.GetComponent<CharacterAttribute>();
            CharacterAttribute myAttribute = gameObject.GetComponent<CharacterAttribute>();
            if (distance > targetAttribute.Radius + myAttribute.Radius)
            {
                transform.position += dir * moveSpeed * Time.deltaTime;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
    }

    void Start()
    {    
    }
    
    void Update()
    {
        
    }
}
