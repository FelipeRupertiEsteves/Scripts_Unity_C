using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensColetaveis : MonoBehaviour
{

    public bool itemDeEscudo;

    public bool itemDeLaserDuplo;

    public bool itemDevida;
    public int vidaParaDar;


    public float velocidadeDoItem;


    void Update()
    {
        MovimentarItem();
    }

        private void MovimentarItem()
    {
        transform.Translate(Vector3.left * velocidadeDoItem * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (itemDeEscudo == true)
            {
                collision.gameObject.GetComponent<VidaDoJogador>().AtivarEscudo();
            }

            if (itemDeLaserDuplo == true)
            {
                collision.gameObject.GetComponent<ControleDoJogador>().temLaserDuplo = false; // reset do power up
                collision.gameObject.GetComponent<ControleDoJogador>().tempoAtualDosLasersDuplos = collision.gameObject.GetComponent<ControleDoJogador>().tempoMaximoDosLasersDuplos; // reset do tempo de power up
                collision.gameObject.GetComponent<ControleDoJogador>().temLaserDuplo = true;
            }

            if (itemDevida == true)
            {
                collision.gameObject.GetComponent<VidaDoJogador>().GanharVida(vidaParaDar);
            }

            Destroy(this.gameObject);
        }


    }


}
