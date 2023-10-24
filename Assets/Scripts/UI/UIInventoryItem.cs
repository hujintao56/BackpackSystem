using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour
    {
        [SerializeField]
        private Image background;
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text quantityTxt;
        [SerializeField]
        private TMP_Text newTxt;

        public Color selectedColor = new Color(255f / 255f, 144f / 255f, 0f / 255f, 255f / 255f);
        public Color unselectedColor = new Color(255f / 255f, 186f / 255f, 97f / 255f, 184f / 255f);

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag,
            OnRightMouseBtnClick;

        private bool empty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;
        }

        public void Deselect()
        {
            background.color = unselectedColor;
        }

        public void SetData(Sprite sprite, int quantity, bool isNew)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            quantityTxt.text = quantity + "";
            empty = false;
            if (isNew)
                newTxt.gameObject.SetActive(true);
            else
                newTxt.gameObject.SetActive(false);
        }

        public void Select()
        {
            background.color = selectedColor;
            newTxt.gameObject.SetActive(false);
        }

        public void OnBeginDrag()
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrop()
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag()
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnPointerClick(BaseEventData data)
        {
            PointerEventData pointerData = (PointerEventData)data;
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }
    }
}