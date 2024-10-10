using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    [SerializeField] private int numeroDaChave;

    public GameObject CopiaDaChave()
    {
        gameObject.SetActive(false);
        return gameObject;
    }
    
    public int PegarNumeroChave()
    {
        return numeroDaChave;
    }
}
