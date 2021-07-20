using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bush : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("bush")) //부딪힌 객체의 태그를 비교해서 적인지 판단합니다.
        {
            Destroy(other.gameObject);
            //적을 파괴합니다.
        }
    }
}
