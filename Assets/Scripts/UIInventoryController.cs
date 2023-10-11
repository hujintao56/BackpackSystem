using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    [SerializeField]
    private InventorySO inventoryData;

    private void Start()
    {
        PrepareUI();
        //inventoryData.Initialize();
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int obj)
    {
        throw new NotImplementedException();
    }

    private void HandleDragging(int obj)
    {
        throw new NotImplementedException();
    }

    private void HandleSwapItems(int arg1, int arg2)
    {
        throw new NotImplementedException();
    }

    private void HandleDescriptionRequest(int obj)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(obj);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(obj, item.ItemImage,
            item.Name, item.Description);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    inventoryUI.UpdateData(item.Key,
                        item.Value.item.ItemImage,
                        item.Value.quantity);
                }
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
