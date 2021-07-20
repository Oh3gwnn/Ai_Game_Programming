using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject box;
    public int maxHealth;
    public int curHealth;
    private Color defaultColor;
    public SpriteRenderer MySpriteRenderer;

    Rigidbody2D rigid;
    BoxCollider2D boxCollider;

    private void Start()
    {
        defaultColor = MySpriteRenderer.color;
        MySpriteRenderer = box.GetComponent<SpriteRenderer>();

    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void HitByAI()
    {
        curHealth -= 1;
        StartCoroutine(OnDamage());
    }

    IEnumerator OnDamage()
    {
        MySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0)
        {
            MySpriteRenderer.color = defaultColor;
        }
        else
        {
            MySpriteRenderer.color = Color.gray;
            Destroy(gameObject);
        }

    }

}
