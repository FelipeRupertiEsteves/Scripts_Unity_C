using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public AudioSource musicaDoJogo;
    public AudioSource musicaDoGameOver;

    public Text textoDePontuacaoAtual;
    
    public GameObject painelDeGameOver;
    public Text textoDePontuacaoFinal;
    public Text textoDeHighscore;

    public int pontuacaoAtual;



    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        musicaDoJogo.Play();
        pontuacaoAtual = 0;
        textoDePontuacaoAtual.text = "PONTUAÇÃO: " + pontuacaoAtual;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AumentarPontuacao(int pontosParaGanhar)
    {
        pontuacaoAtual += pontosParaGanhar;
        textoDePontuacaoAtual.text = "PONTUAÇÃO: " + pontuacaoAtual;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        musicaDoJogo.Stop();
        musicaDoGameOver.Play();

        painelDeGameOver.SetActive(true);
        textoDePontuacaoFinal.text = "PONTUAÇÃO: " + pontuacaoAtual;

        if (pontuacaoAtual > PlayerPrefs.GetInt("HighScore"))           // Save do highscore
        {
            PlayerPrefs.SetInt("HighScore", pontuacaoAtual);
        }

        textoDeHighscore.text = "Pontuação Máxima: " + PlayerPrefs.GetInt("HighScore");
    }

}
