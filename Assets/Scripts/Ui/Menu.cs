
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SelecaoDePersonagem");
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Sair() 
    {
        Application.Quit();
    }
}
