using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Piece
{
    Steel,
    Cloth,
    Wood,
    Axe,
    Chisel,
    Torch,
    Blade,
    Bucket,
    Rope,
    Empty
}

public enum Type
{
    Normal,
    Create,
    Decompose
}

public class CraftingItem : MonoBehaviour, IPointerDownHandler
{
    public CraftingSystem system;
    public GameObject active;
    public GameObject[] pieces;
    public Piece piece;
    public Type type;

    [HideInInspector]
    public bool interact;

    void Start()
    {
        interact = !(type == Type.Normal);
    }

    void Update()
    {
        foreach (GameObject p in pieces)
        {
            p.SetActive(false);
        }
        if (piece != Piece.Empty)
        {
            pieces[(int)piece].SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (system.process)
        {
            return;
        }
        if (!interact)
        {
            system.Uncheck();
        }
        else if (type == Type.Decompose)
        {
            system.StartDecompose();
        }
        else if (type == Type.Create)
        {
            system.StartCrafting();
        }
        else if (piece != Piece.Empty)
        {
            active.SetActive(true);
            system.Interact(piece);
        }
    }
}
