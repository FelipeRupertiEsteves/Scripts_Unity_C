using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class chaveBoca : MonoBehaviour
{

    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public int Score;

    public GameObject collected;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            gameController.instance.totalScore += Score; //soma valor score da chave com o total score
            gameController.instance.UpdateScoreText();  

            Destroy(gameObject, 0.3f);

        }
    }

}
