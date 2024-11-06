using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Itens item;
    private Color colorEmission;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
        colorEmission = GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }

    public void Pickup()
    {
        //InventoryManager.instatnce.AddItem(item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        //Pickup;
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.gray);
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.SetColor("_EmissionColor", colorEmission);
    }
}
