using UnityEngine;

public class Pocao : MonoBehaviour
{
    [SerializeField] private int vidaParaAumentar = 20;  // Quantidade que a vida máxima e vida atual irão aumentar

    private bool jogadorNoRange = false;  // Verifica se o player está no alcance da poção

    private void Update()
    {
        if (jogadorNoRange && Input.GetKeyDown(KeyCode.E))
        {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if (player != null)
            {
                player.AumentarVida(vidaParaAumentar);  // Chama a função para aumentar a vida e a vida máxima
                Destroy(gameObject);  // Destrói a poção após ser usada
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorNoRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorNoRange = false;
        }
    }
}
