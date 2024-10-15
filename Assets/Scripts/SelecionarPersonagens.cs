using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelecionarPersonagens : MonoBehaviour
{
    public string nome;
    public int dano;
    public int defesa;
    public bool selecionado;
    [SerializeField] private TextMeshProUGUI statusDefesa;
    [SerializeField] private TextMeshProUGUI statusDano;
    [SerializeField] private TextMeshProUGUI statusNome;

    private void Start()
    {
        statusDefesa = GameObject.FindWithTag("StatusDefesa").GetComponent<TextMeshProUGUI>();
        statusDano = GameObject.FindWithTag("StatusDano").GetComponent<TextMeshProUGUI>();
        statusNome = GameObject.FindWithTag("StatusNome").GetComponent<TextMeshProUGUI>();
    }


    private void OnMouseEnter() 
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        statusDefesa.text = "Defesa: " + defesa;
        statusDano.text = "Dano: " + dano;
        statusNome.text = "Nome: " + nome;
    }

    private void OnMouseExit() 
    {
        if(!selecionado)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            statusDefesa.text = "";
            statusDano.text = "";
            statusNome.text = "";
        }
        
    }

    private void OnMouseDown()
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        selecionado = true;
    }
}
