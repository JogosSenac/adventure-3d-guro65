using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bau : MonoBehaviour
{
    [SerializeField] GameObject particulas;
    [SerializeField] private bool ehMagico;
    [SerializeField] private int numeroDeChave;
    [SerializeField] private List<GameObject> itens = new List<GameObject>();
    [SerializeField] private int ouro;
    // Start is called before the first frame update
    void Start()
    {
        if(ehMagico)
        {
            particulas.SetActive(true);
            ouro = Random.Range(100, 500);
        }
        else
        {
            particulas.SetActive(false);
            ouro = Random.Range(0, 100);
        }
    }

    private void DesativarParticulas()
    {
        //particulas.SetActive(false);
        particulas.GetComponent<ParticleSystem>().Stop();
    }

    public int PegarOuro()
    {
        DesativarParticulas();
        StartCoroutine(ZerarBau());
        return ouro;
    }

    IEnumerator ZerarBau()
    {
        yield return new WaitForSeconds(2.5f);
        ouro = 0;
    }

    public int PegarNumeroFechadura()
    {
        return numeroDeChave;
    }

    public List<GameObject> AcessarConteudoBau()
    {
        return itens;
    }

    public void RemoverConteudoBau()
    {
        itens.Clear();
    }
}
