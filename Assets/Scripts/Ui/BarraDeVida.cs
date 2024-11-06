using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraDeVida : MonoBehaviour
{
    [SerializeField] private Image barraVidaSprite; // Sprite da barra de vida
    [SerializeField] private GameObject painelMorte; // Painel a ser ativado quando a vida chegar a zero
    [SerializeField] private Button botaoReiniciar; // Botão para reiniciar o jogo ou alguma ação
    [SerializeField] private Text textoGameOver; // Texto que indica que o jogo acabou
    private Player player;

    void Start()
    {
        player = GetComponentInParent<Player>(); // Referência ao Player
        painelMorte.SetActive(false); // Garante que o painel de morte esteja inicialmente escondido
    }

    void Update()
    {
        if (player != null)
        {
            // Atualiza a barra de vida apenas se a vida atual mudar
            AtualizarBarraDeVida(player.VidaAtual, player.VidaMaxima);
            SeguirPlayer();
        }
    }

    // Atualiza a barra de vida com base na vida atual
    public void AtualizarBarraDeVida(int vidaAtual, int vidaMaxima)
    {
        if (barraVidaSprite != null)
        {
            if (vidaMaxima > 0)
            {
                barraVidaSprite.fillAmount = (float)vidaAtual / vidaMaxima;

                // Verifica se a barra de vida está vazia
                if (barraVidaSprite.fillAmount <= 0)
                {
                    MostrarPainelMorte(); // Chama o método para mostrar o painel de Game Over
                }
            }
            else
            {
                barraVidaSprite.fillAmount = 0; // Se a vida máxima for 0, a barra deve estar vazia
            }
        }
        else
        {
            Debug.LogWarning("Referência à barra de vida não está atribuída!", this);
        }
    }

    // Mostra o painel de morte
    private void MostrarPainelMorte()
    {
        painelMorte.SetActive(true); // Ativa o painel
        textoGameOver.text = "Game Over"; // Define o texto do Game Over
        botaoReiniciar.onClick.AddListener(ReiniciarJogo); // Adiciona um listener ao botão
    }

    
    public void ReiniciarJogo()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Jogo reiniciado!"); // Para fins de depuração
    }

    public void Voltar()
    {
        SceneManager.LoadScene("Menu");
    }

    // Faz a barra de vida seguir o Player e sempre se orientar para a câmera
    private void SeguirPlayer()
    {
        Camera camera = Camera.main;
        if (camera != null)
        {
            // Posição acima do Player
            transform.position = player.transform.position + Vector3.up * 2;
            transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        }
    }
}
