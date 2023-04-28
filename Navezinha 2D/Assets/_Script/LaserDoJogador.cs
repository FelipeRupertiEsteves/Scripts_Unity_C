using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoJogador : MonoBehaviour
{
    public GameObject impactoDoLaserDoJogador; // variavel para incluir o Prefab de Efeito do impacto do laser do jogador

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

            Instantiate(impactoDoLaserDoJogador, transform.position, transform.rotation);
            EfeitosSonoros.instance.somDeImpacto.Play();

            Destroy(this.gameObject);
        }
    }

}
