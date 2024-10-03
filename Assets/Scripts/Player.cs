using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool estaVivo = true;
    [SerializeField] private int forcaPulo;
    [SerializeField] private float velocidade;
    [SerializeField] private bool temChave;
    private Rigidbody rb;
    private bool estaPulando;
    private Vector3 angleRotation;
    private bool pegando;
    // Start is called before the first frame update
    void Start()
    {
        temChave = false;
        pegando = false;
        angleRotation = new Vector3(0, 90, 0);
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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

        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Pegando");
            pegando = true;
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

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Chave") && pegando)
        {
            temChave = true;
            pegando = false;
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Porta") && pegando && temChave)
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("Abrir");
            temChave = true;
        }

        if(other.gameObject.CompareTag("Bau") && pegando && temChave)
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("AbrirBau");
            temChave = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        pegando = false;
    }
    //desculpa
}
