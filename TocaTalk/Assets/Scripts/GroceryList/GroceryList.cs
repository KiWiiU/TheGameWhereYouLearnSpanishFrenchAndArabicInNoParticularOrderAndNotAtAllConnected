using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryList : MonoBehaviour
{
    Canvas canvas;
    public bool isOpen;
    public event Action OnClosed;
    public void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        isOpen = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Holder.groceryList)
        {
            if (isOpen)
            {
                CloseGroceryList();
            }
            else
            {
                OpenGroceryList();
            }
        }   
    }
    public void OpenGroceryList()
    {
        isOpen = true;
        Holder.canPlayerMove = false;
        canvas.enabled = true;
    }

    public void CloseGroceryList()
    {
        isOpen = false;
        Holder.canPlayerMove = true;
        canvas.enabled = false;
        OnClosed?.Invoke();
    }
}
