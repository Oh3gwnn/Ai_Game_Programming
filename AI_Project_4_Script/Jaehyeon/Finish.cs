using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadScene을 사용하는데 필요

public class Finish : MonoBehaviour
{
    //목표 지점에 닿으면 씬 전환
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("골");
            SceneManager.LoadScene("Ai-2 jh");

        }

    }
}
