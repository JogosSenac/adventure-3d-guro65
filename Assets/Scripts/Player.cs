using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool estaVivo = true;
    [SerializeField] private int forcaPulo;
    [SerializeField] private int ouro;
    [SerializeField] private int vida;
    [SerializeField] private float velocidade;
    [SerializeField] private bool temChave;
    [SerializeField] private bool pegando;
    [SerializeField] private bool podePegar;
    [SerializeField] private List<GameObject> inventario = new List<GameObject>();
    private Rigidbody rb;
    private bool estaPulando;
    private Vector3 angleRotation;
    // Start is called before the first frame update
    void Start()
    {
        temChave = false;
        pegando = false;
        podePegar = false;
        angleRotation = new Vector3(0, 90, 0);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.E) && podePegar)
        {
            animator.SetTrigger("Pegando");
            pegando = true;
        }
        if(Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Andar", true);
            animator.SetBool("AndarParaTras", false);
            Walk();
        }
        else if(Input.GetKey(KeyCode.S))
        {
            animator.SetBool("AndarParaTras", true);
            animator.SetBool("Andar", false);
            Walk();
        }
        else if(Input.GetKeyDown(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Andar", true);
        }
        else
        {
            animator.SetBool("Andar", false);
            animator.SetBool("AndarParaTras", false);
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            animator.SetBool("Andar", false);
            animator.SetBool("AndarParaTras", false);
        }

        if(Input.GetKeyDown(KeyCode.Space) && !estaPulando)
        {
            animator.SetTrigger("Pular");
            Jump();
        }

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Ataque");
        }

        if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Correndo", true);
            Walk(8);
        }
        else
        {
            animator.SetBool("Correndo", false);
        }

        if(!estaVivo)
        {
            animator.SetTrigger("EstaVivo");
            estaVivo = true;
        }

        TurnAround();
    }

    void FixedUpdate()
    {

    }

    private void Walk(float velo = 1)
    {
        if((velo ==1))
        {
            velo = velocidade;
        }
        float fowardInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * fowardInput;
        Vector3 moveFoward = rb.position + moveDirection * velo * Time.deltaTime;
        rb.MovePosition(moveFoward);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        estaPulando = true;
        animator.SetBool("EstaNoChao", false);
    }

    private void TurnAround()
    {
        float sideInput = Input.GetAxis("Horizontal");
        Quaternion deltaRotation = Quaternion.Euler(angleRotation * sideInput * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Chao"))
        {
            estaPulando = false;
            animator.SetBool("EstaNoChao", true);
        }
    }

    private void OnTriggerEnter()
    {
        podePegar = true;
    }

    private void OnTriggerStay(Collider other)
    {

        Debug.Log(other.gameObject.tag);
        
        
        if(other.gameObject.CompareTag("Chave") && pegando)
        {
            inventario.Add(Instantiate(other.gameObject.GetComponent<Chave>().CopiaDaChave()));
            int numero = other.gameObject.GetComponent<Chave>().PegarNumeroChave();
            Debug.LogFormat($"Chave número: {numero} inserida no invantario");
            Destroy(other.gameObject);
            pegando = false;
            podePegar = false;
        }

        if(other.gameObject.CompareTag("Porta") && pegando && temChave)
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Abrir");
            temChave = true;
        }

        if(other.gameObject.CompareTag("Bau") && pegando)
        {
            if(VerificaChave(other.gameObject.GetComponent<Bau>().PegarNumeroFechadura()))
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("AbrirBau");
                
            }
            else
            {
                Debug.Log("Voce não tem a chave.");
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        pegando = false;
        podePegar = false;
    }

    private bool VerificaChave(int chave)
    {
        foreach(GameObject item in inventario)
        {
            if(item.gameObject.CompareTag("Chave"))
            {
                if(item.gameObject.GetComponent<Chave>().PegarNumeroChave() == chave)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void PegarConteudoDoBau(GameObject bau)
    {
        Bau bauTesouro = bau.GetComponent<Bau>();

        ouro += bauTesouro.PegarOuro();

        if(bauTesouro.AcessarConteudoBau() != null)
        {
            foreach(GameObject item in bauTesouro.AcessarConteudoBau())
            {
                inventario.Add(item);
            }
            bauTesouro.RemoverConteudoBau();
        }
        
    }
    //desculpa
}
