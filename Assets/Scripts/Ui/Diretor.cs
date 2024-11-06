using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Diretor : MonoBehaviour
{
    //player
    [SerializeField] private Player player;
    //audio source
    [SerializeField] private AudioSource audioPlayer;
    // texto pontos
    [SerializeField] private TextMeshProUGUI pontos;
    [SerializeField] private TextMeshProUGUI pontosRestantes;
    [SerializeField] private TextMeshProUGUI avisoMissao;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioPlayer = GetComponent<AudioSource>();
        pontos = GameObject.Find("Pontos").GetComponent<TextMeshProUGUI>();
        pontosRestantes = GameObject.Find("PontosRestantes").GetComponent<TextMeshProUGUI>();
        avisoMissao = GameObject.Find("Aviso").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        pontos.text = player.ContagemDeKill().ToString();

        avisoMissao.text = "Derrote os esqueletos";
    }
}
