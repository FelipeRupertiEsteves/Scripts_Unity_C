using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{

    public GameObject laserDoInimigo;
    public Transform localDoDisparo;
    public GameObject itemParaDropar;
    public GameObject efeitoDeExplosao;

    public float velocidadeDoInimigo;
    public int vidaMaximaDoinimigo;
    public int vidaAtualDoinimigo;
    public int pontosParaDar;
    public int chanceDeDrop;

    public float tempoMaximoEntreOsLasers;
    public float tempoAtualDosLasers;
     
    public bool inimigoAtirador;
    public bool inimigoAtivado;
    public int danoDaNave;

    // Start is called before the first frame update
    void Start()
    {
        inimigoAtivado = false;
        vidaAtualDoinimigo = vidaMaximaDoinimigo;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarInimigo();
        
        if (inimigoAtirador == true && inimigoAtivado == true)
        {
            AtirarLasers();
        }
        
    }

    public void AtivarInimigo()
    {
        inimigoAtivado = true;
    }

    private void MovimentarInimigo()
    {
        transform.Translate(Vector3.down * velocidadeDoInimigo * Time.deltaTime);
    }

    private void AtirarLasers()
    {
        tempoAtualDosLasers -= Time.deltaTime;

        if (tempoAtualDosLasers <= 0)
        {
            Instantiate(laserDoInimigo, localDoDisparo.position, Quaternion.Euler(0f,0f,90f));
            tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        }
    }

    public void MachucarInimigo(int danoParaReceber)
    {
        vidaAtualDoinimigo -= danoParaReceber;

        if(vidaAtualDoinimigo <= 0)
        {
            GameManager.instance.AumentarPontuacao(pontosParaDar);
            Instantiate(efeitoDeExplosao, transform.position, transform.rotation);
            EfeitosSonoros.instance.somDaExplosão.Play();

            int numeroAleatorio = Random.Range(0, 100);     

            if(numeroAleatorio <= chanceDeDrop)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }

            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<VidaDoJogador>().MachucarJogador(danoDaNave);
            Instantiate(efeitoDeExplosao, transform.position, transform.rotation);
            EfeitosSonoros.instance.somDaExplosão.Play();
            Destroy(this.gameObject);
        }
    }


}
