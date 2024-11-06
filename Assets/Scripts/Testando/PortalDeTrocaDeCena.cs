using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaCenaComObjeto : MonoBehaviour
{
    // Nome da cena para a qual o jogo irá mudar
    public string nomeCenaDestino;

    // Transform que define a posição de spawn do jogador na nova cena
    public Transform pontoDeSpawn;

    // Método que é chamado quando o jogador toca em um objeto (por exemplo, através de colisão)
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto tocado é o jogador
        if (other.CompareTag("Player"))
        {
            // Salva o objeto tocado
            GameObject objetoToque = other.gameObject;

            // Remove o objeto da cena atual antes de carregar a nova cena
            DontDestroyOnLoad(objetoToque);

            // Muda para a nova cena
            SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode, objetoToque);
            SceneManager.LoadScene(nomeCenaDestino);
        }
    }

    // Este método é chamado quando a nova cena for carregada
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode, GameObject objetoToque)
    {
        // Mova o objeto para a nova cena
        if (objetoToque != null)
        {
            // Preserva o objeto entre as cenas
            DontDestroyOnLoad(objetoToque);

            // Teleporta o objeto para a posição desejada na nova cena
            if (pontoDeSpawn != null)
            {
                objetoToque.transform.position = pontoDeSpawn.position;  // Coloca o player no ponto de spawn
                objetoToque.transform.rotation = pontoDeSpawn.rotation;  // Ajusta a rotação para corresponder ao ponto de spawn
            }
            else
            {
                // Se não houver ponto de spawn, usa uma posição padrão (por exemplo, (0, 1, 0))
                objetoToque.transform.position = new Vector3(0f, 1f, 0f);
            }
        }

        // Remove o evento do carregamento da cena após a troca
        SceneManager.sceneLoaded -= (scene, mode) => OnSceneLoaded(scene, mode, objetoToque);
    }
}
