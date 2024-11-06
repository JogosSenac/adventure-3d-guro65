using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Arma", menuName = "Novo Item/ Arma")]
public class Machado2 : Itens
{
    public int dano;
    public bool ehmagico;
    public int danoAdicional;

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

    public virtual int Dano()
    {
        return dano;
    }

    public virtual bool EhMagico()
    {
        return ehmagico;
    }

    public virtual int DanoAdicional()
    {
        return danoAdicional;
    }
}
