using System.Collections;
using UnityEngine;

public class Npc : MonoBehaviour
{
    [Header("Configurações Principais")]
    [SerializeField] private GameObject player;
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float paraDeSeguirDistancia = 3f;
    [SerializeField] private float tempoEntreAcoes = 2.5f;
    [SerializeField] private bool estaSeguindo;
    [SerializeField] private bool estaAtacando;
    [SerializeField] private AudioClip morte;

    [Header("Drop de Poções")]
    [SerializeField] private GameObject pocaoPequenaPrefab;
    [SerializeField] private GameObject pocaoMediaPrefab;
    [SerializeField] private GameObject pocaoGrandePrefab;
    [SerializeField] private float chancePocaoPequena = 0.5f;  // 50% de chance de dropar
    [SerializeField] private float chancePocaoMedia = 0.3f;    // 30% de chance de dropar
    [SerializeField] private float chancePocaoGrande = 0.2f;   // 20% de chance de dropar

    [Header("Nome do NPC")]
    [SerializeField] private string npcNome;  // Nome do NPC

    public Machado arma;
    public int vida = 100;
    private Rigidbody rb;
    private Animator animator;
    private bool defendendo = false;
    private AudioSource audio;
    public GameObject painelMorte; // O painel de Game Over que aparece quando o Boss morre

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        estaSeguindo = false;
        estaAtacando = false;
        animator.SetBool("EstaParado", true);
        animator.SetBool("Defesa", false);
        audio = GetComponent<AudioSource>();

        if (arma == null)
        {
            arma = GetComponentInChildren<Machado>();
        }

        if (painelMorte != null)
        {
            painelMorte.SetActive(false); // Garante que o painel de morte esteja escondido no início
        }
    }

    private void Update()
    {
        if (estaSeguindo)
        {
            animator.SetBool("EstaParado", false);
            SeguirPlayer();
        }
    }

    public void ReceberDano(int dano)
    {
        int danoRecebido = defendendo ? dano / 2 : dano;
        vida -= danoRecebido;

        if (vida <= 0)
        {
            vida = 0;
            estaSeguindo = false;
            estaAtacando = false;
            rb.linearVelocity = Vector3.zero;

            if (npcNome == "Boss" && painelMorte != null)
            {
                // Pausa o jogo quando o Boss morrer
                Time.timeScale = 0f;
                painelMorte.SetActive(true);  // Exibe o painel quando o Boss morrer
            }

            DropPocao();
            Destroy(gameObject, 2f);
        }
        else
        {
            Debug.Log("NPC ainda tem vida: " + vida);
        }
    }

    private void DropPocao()
    {
        float randomValue = Random.value;

        if (randomValue <= chancePocaoGrande)
        {
            Instantiate(pocaoGrandePrefab, transform.position, Quaternion.identity);
            Debug.Log("Poção Grande dropada!");
        }
        else if (randomValue <= chancePocaoMedia + chancePocaoGrande)
        {
            Instantiate(pocaoMediaPrefab, transform.position, Quaternion.identity);
            Debug.Log("Poção Média dropada!");
        }
        else if (randomValue <= chancePocaoPequena + chancePocaoMedia + chancePocaoGrande)
        {
            Instantiate(pocaoPequenaPrefab, transform.position, Quaternion.identity);
            Debug.Log("Poção Pequena dropada!");
        }
    }

    private void SeguirPlayer()
    {
        Vector3 moveDirection = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = moveDirection * velocidade;

        if (Vector3.Distance(player.transform.position, transform.position) <= paraDeSeguirDistancia)
        {
            estaSeguindo = false;
            rb.linearVelocity = Vector3.zero;
            animator.SetBool("Andar", true);
            estaAtacando = false;
            return;
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            animator.SetBool("Andar", true);
            estaSeguindo = true;
        }
        else
        {
            estaSeguindo = false;
            animator.SetBool("Andar", false);
        }
    }

    public void EstaSeguindo()
    {
        animator.SetBool("Andar", true);
        estaSeguindo = true;
    }

    public void NaoEstaSeguindo()
    {
        estaSeguindo = false;
        animator.SetBool("Andar", false);
    }

    private IEnumerator ExecutarAtaque()
    {
        while (estaAtacando)
        {
            if (player == null) yield break;
            animator.SetTrigger("Ataque");
            yield return new WaitForSeconds(1f);

            if (arma != null)
            {
                player.GetComponent<Player>().ReceberDano(arma.dano);
            }

            yield return new WaitForSeconds(tempoEntreAcoes);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaSeguindo = false;
            estaAtacando = true;
            StartCoroutine(ExecutarAtaque());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaAtacando = false;
            StopCoroutine(ExecutarAtaque());
            estaSeguindo = true;
            animator.SetBool("Andar", true);
        }
    }

    private void OnDestroy()
    {
        player.GetComponent<Player>().SomarKill();
    }
}
