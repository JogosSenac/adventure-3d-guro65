
using UnityEngine;

public class ChaveAntiga : MonoBehaviour
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
