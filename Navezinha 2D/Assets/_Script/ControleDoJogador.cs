using System.Collections;
using System.Collections.Generic;
using UnityEngine;




// --- ---- ------ MOVIMENTAÇÃO E DISPARO DE NAVEZINHA 2D




public class ControleDoJogador : MonoBehaviour
{
    public Rigidbody2D rb2D;

    public GameObject laserDoJogador;

    public Transform localDoDisparoUnico;
    public Transform localDoDisparoDaEsquerda;
    public Transform localDoDisparoDaDireita;

    public float tempoMaximoDosLasersDuplos;
    public float tempoAtualDosLasersDuplos;

    public float velocidadeDaNave = 4f;

    public bool temLaserDuplo;
    public bool jogadorEstaVivo;

    private Vector2 teclasApertadas;

    

    // Start is called before the first frame update
    void Start()
    {
        temLaserDuplo = false;

        tempoAtualDosLasersDuplos = tempoMaximoDosLasersDuplos;

        jogadorEstaVivo = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarJogador();
        
        if(jogadorEstaVivo == true)
        {
            AtirarLaser();
        }
        
        if (temLaserDuplo == true)
        {
            tempoAtualDosLasersDuplos -= Time.deltaTime;

            if (tempoAtualDosLasersDuplos <= 0)
            {
                DesativarLaserDuplo();
            }
        }
    }

    private void MovimentarJogador()
    {
        teclasApertadas = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb2D.velocity = teclasApertadas.normalized * velocidadeDaNave; // normalized faz com que não some a velocidade nos eixos ao andar em diagonais
    }

    private void AtirarLaser()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!temLaserDuplo)
            {
                Instantiate(laserDoJogador, localDoDisparoUnico.position, localDoDisparoUnico.rotation);
            }
            else
            {
                Instantiate(laserDoJogador, localDoDisparoDaDireita.position, localDoDisparoDaDireita.rotation);
                Instantiate(laserDoJogador, localDoDisparoDaEsquerda.position, localDoDisparoDaEsquerda.rotation);
            }
            EfeitosSonoros.instance.somDoLaserDoJogador.Play();
        }
    }

    private void DesativarLaserDuplo()
    {
        temLaserDuplo = false;
        tempoAtualDosLasersDuplos = tempoMaximoDosLasersDuplos;
    }

}
