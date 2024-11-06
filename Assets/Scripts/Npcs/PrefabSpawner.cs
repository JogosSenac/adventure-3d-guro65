using System.Collections;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Configurações do Spawner")]
    public GameObject prefabInimigo; // Prefab do inimigo a ser gerado
    public int quantidadeParaGerar = 10; // Quantidade de prefabs a ser gerada
    public float intervaloDeGeracao = 1.0f; // Tempo em segundos entre cada geração
    private int inimigosGerados = 0;

    void Start()
    {
        StartCoroutine(GerarInimigosComIntervalo());
    }

    private IEnumerator GerarInimigosComIntervalo()
    {
        while (inimigosGerados < quantidadeParaGerar)
        {
            // Gera o prefab em uma posição aleatória próxima ao spawner
            Vector3 posicaoAleatoria = transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            Instantiate(prefabInimigo, posicaoAleatoria, Quaternion.identity).tag = "Inimigo";

            inimigosGerados++;
            yield return new WaitForSeconds(intervaloDeGeracao);
        }
    }
}