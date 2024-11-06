using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pocao", menuName = "Novo Item/ Pocao")]
public class Pocao1 : Itens
{
    public TipoPocao tipo;
    public enum TipoPocao{cura, magia, defesa, poder}
    public int tamanho;

    public override string Nome()
    {
        return nome;
    }

    public override string Descricao()
    {
        return descricao;
    }

    public override Sprite Sprite()
    {
        return sprite;
    }

    public override GameObject ItemPrefab()
    {
        return itemPrefab;
    }

    public string TipoDePocao()
    {
        return tipo.ToString();
    }

    public int Tamanho()
    {
        return tamanho;
    }
}
