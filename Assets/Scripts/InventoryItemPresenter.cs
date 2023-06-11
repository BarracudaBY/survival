using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemPresenter : MonoBehaviour , IDragHandler, IEndDragHandler, IBeginDragHandler
{

    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private Image _iconField;

    private Transform _dragginParent;
    private Transform _originalParent;

    public void Init(Transform draggingParent)
    {
        _dragginParent = draggingParent;
        _originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent = _dragginParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int closestIndex = 0;

        for (int i = 0; i < _originalParent.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, _originalParent.GetChild(i).position) < Vector3.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
            {
                closestIndex = i;
            }

        }

        transform.parent = _originalParent;
        transform.SetSiblingIndex(closestIndex);
    }

    public void Render(IItems items)
    {
        _nameField.text = items.Name;
        _iconField.sprite = items.UIIcon;
    }
}
