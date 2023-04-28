using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para manipular as cenas de jogo (fases)

public class PainelDeGameOver : MonoBehaviour
{

    public void ReiniciarJogo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //load na cena: (pega cena ativa atual.(pelo nome))
    }

    public void SairDoJogo()
    {
        Application.Quit(); // Quita do aplicativo
        Debug.Log("Saiu do jogo!"); // para debugar pela unity
    }


}
