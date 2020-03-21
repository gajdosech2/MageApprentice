using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public GameObject gui;
    public CraftingItem[] items;

    public CraftingItem craft1;
    public CraftingItem craft2;
    public CraftingItem decomp;

    [HideInInspector]
    public bool process = false;

    public PlayerMovement movement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gui.SetActive(!gui.activeSelf);
            movement.enabled = !gui.activeSelf;
            Cursor.visible = gui.activeSelf;
            Cursor.lockState = gui.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public bool HasItem(Piece piece)
    {
        if (piece == Piece.Empty)
        {
            return true;
        }
        foreach (CraftingItem item in items)
        {
            if (item.piece == piece)
            {
                return true;
            }
        }
        return false;
    }

    public void StartCrafting()
    {
        Uncheck();
        foreach (CraftingItem item in items)
        {
            if ((int)item.piece < 3)
            {
                item.interact = true;
            }
        }
        craft1.active.SetActive(true);
        craft2.active.SetActive(true);
    }

    public void StartDecompose()
    {
        Uncheck();
        foreach (CraftingItem item in items)
        {
            if ((int)item.piece > 2 && item.piece != Piece.Empty)
            {
                item.interact = true;
            }
        }
        decomp.active.SetActive(true);
    }

    public void AddItem(Piece piece)
    {
        if (piece == Piece.Empty)
        {
            return;
        }
        foreach (CraftingItem item in items)
        {
            if (item.piece == Piece.Empty)
            {
                item.piece = piece;
                return;
            }
        }
    }

    public void RemoveItem(Piece piece)
    {
        foreach (CraftingItem item in items)
        {
            if (item.piece == piece)
            {
                item.piece = Piece.Empty;
                return;
            }
        }
    }

    public void Uncheck()
    {
        foreach (CraftingItem item in items)
        {
            item.interact = false;
            item.active.SetActive(false);
        }
        craft1.piece = Piece.Empty;
        craft2.piece = Piece.Empty;
        decomp.piece = Piece.Empty;
        craft1.active.SetActive(false);
        craft2.active.SetActive(false);
        decomp.active.SetActive(false);
    }

    public void Interact(Piece piece)
    {
        if (craft1.active.activeSelf && (int)piece < 3)
        {
            if (craft1.piece == Piece.Empty)
            {
                craft1.piece = piece;
            }
            else
            {
                craft2.piece = piece;
                StartCoroutine(Craft());
            }
        }
        else if (decomp.active.activeSelf && (int)piece > 2)
        {
            decomp.piece = piece;
            StartCoroutine(Decompose(piece));
        }
    }

    private IEnumerator Craft()
    {
        process = true;
        RemoveItem(craft1.piece);
        RemoveItem(craft2.piece);
        yield return new WaitForSeconds(0.5f);
        if (craft1.piece == Piece.Wood && craft2.piece == Piece.Wood)
        {
            AddItem(Piece.Bucket);
        }
        else if (craft1.piece == Piece.Steel && craft2.piece == Piece.Steel)
        {
            AddItem(Piece.Blade);
        }
        else if (craft1.piece == Piece.Cloth && craft2.piece == Piece.Cloth)
        {
            AddItem(Piece.Rope);
        }
        else if ((craft1.piece == Piece.Cloth && craft2.piece == Piece.Steel) || (craft1.piece == Piece.Steel && craft2.piece == Piece.Cloth))
        {
            AddItem(Piece.Axe);
        }
        else if ((craft1.piece == Piece.Wood && craft2.piece == Piece.Steel) || (craft1.piece == Piece.Steel && craft2.piece == Piece.Wood))
        {
            AddItem(Piece.Chisel);
        }
        else if ((craft1.piece == Piece.Cloth && craft2.piece == Piece.Wood) || (craft1.piece == Piece.Wood && craft2.piece == Piece.Cloth))
        {
            AddItem(Piece.Torch);
        }
        Uncheck();
        process = false;
    }

    private IEnumerator Decompose(Piece piece)
    {
        process = true;
        RemoveItem(piece);
        yield return new WaitForSeconds(0.75f);
        switch (piece)
        {
            case Piece.Axe:
                AddItem(Piece.Cloth);
                AddItem(Piece.Steel);
                break;
            case Piece.Chisel:
                AddItem(Piece.Wood);
                AddItem(Piece.Steel);
                break;
            case Piece.Torch:
                AddItem(Piece.Cloth);
                AddItem(Piece.Wood);
                break;
            case Piece.Blade:
                AddItem(Piece.Steel);
                AddItem(Piece.Steel);
                break;
            case Piece.Bucket:
                AddItem(Piece.Wood);
                AddItem(Piece.Wood);
                break;
            case Piece.Rope:
                AddItem(Piece.Cloth);
                AddItem(Piece.Cloth);
                break;
        }
        Uncheck();
        process = false;
    }
}
