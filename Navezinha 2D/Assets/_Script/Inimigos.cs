using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{

    public GameObject laserDoInimigo;
    public Transform localDoDisparo;

    public GameObject itemParaDropar;

    public float velocidadeDoInimigo;
    
    public int vidaMaximaDoinimigo;
    public int vidaAtualDoinimigo;

    public float tempoMaximoEntreOsLasers;
    public float tempoAtualDosLasers;

    public int pontosParaDar;

    public bool inimigoAtirador;

    public int chanceDeDrop;


    

    // Start is called before the first frame update
    void Start()
    {
        vidaAtualDoinimigo = vidaMaximaDoinimigo;
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarInimigo();
        
        if (inimigoAtirador == true)
        {
            AtirarLasers();
        }
        
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

            int numeroAleatorio = Random.Range(0, 100);     

            if(numeroAleatorio <= chanceDeDrop)
            {
                Instantiate(itemParaDropar, transform.position, Quaternion.Euler(0f, 0f, 0f));
            }

            Destroy(this.gameObject);
        }
    }

   

}
