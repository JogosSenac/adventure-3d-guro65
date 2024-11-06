
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SelecionarPersonagens : MonoBehaviour
{
    public string nome;
    public int dano;
    public int velocidade;
    public int vida;
    public bool selecionado;
    public GameObject prefabPersonagem;
    [SerializeField] private TextMeshProUGUI statusVelocidade;
    [SerializeField] private TextMeshProUGUI statusDano;
    [SerializeField] private TextMeshProUGUI statusNome;
    [SerializeField] private TextMeshProUGUI statusVida;
    public string cenaDestino;
    public Transform pontoDeSpawn;
    private static GameObject personagemSelecionado;
    private static Transform pontoDeSpawnSelecionado;

    private void Start()
    {
        statusVelocidade = GameObject.FindWithTag("statusVelocidade").GetComponent<TextMeshProUGUI>();
        statusDano = GameObject.FindWithTag("StatusDano").GetComponent<TextMeshProUGUI>();
        statusNome = GameObject.FindWithTag("StatusNome").GetComponent<TextMeshProUGUI>();
        statusVida = GameObject.FindWithTag("StatusVida").GetComponent<TextMeshProUGUI>();
    }

    private void OnMouseEnter()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        statusVelocidade.text = "Velocidade: " + velocidade;
        statusDano.text = "Dano: " + dano;
        statusNome.text = "Nome: " + nome;
        statusVida.text = "Vida: " + vida;
    }

    private void OnMouseExit()
    {
        if (!selecionado)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            statusVelocidade.text = "";
            statusDano.text = "";
            statusNome.text = "";
            statusVida.text = "";
        }
    }

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        selecionado = true;
        personagemSelecionado = prefabPersonagem;
        pontoDeSpawnSelecionado = pontoDeSpawn;
        DontDestroyOnLoad(personagemSelecionado);
    }

    public void TrocarCena()
    {
        if (selecionado)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(cenaDestino);
        }
        else
        {
            Debug.Log("Nenhum personagem selecionado!");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (personagemSelecionado != null)
        {
            if (pontoDeSpawnSelecionado != null)
            {
                Instantiate(personagemSelecionado, pontoDeSpawnSelecionado.position, pontoDeSpawnSelecionado.rotation);
            }
            else
            {
                Instantiate(personagemSelecionado, Vector3.zero, Quaternion.identity);
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
