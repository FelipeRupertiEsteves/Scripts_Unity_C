using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoJogador : MonoBehaviour
{


    public float velocidadeDoLaser;

    public int danoParaDar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movimentarLaser();
    }

    private void movimentarLaser()
    {
        transform.Translate(Vector3.up * velocidadeDoLaser * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            collision.gameObject.GetComponent<Inimigos>().MachucarInimigo(danoParaDar);
            Destroy(this.gameObject);
        }
    }

}
