using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer; // Добавляем поле в котором будет физический слой земли 
    private Collider2D _collider;

    public bool IsTouchingLayer;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>(); //базовый класс для всех коллайдеров
    }

    private void OnTriggerStay2D(Collider2D other) //срабатывают когда в пересечение попадает какой либо коллайдер, проверяет когда назодится в каком либо слое
    {
        IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer); //проверяет соприкосается ли его колайдер с каким либо слоем что указан в скобках
    }

    private void OnTriggerExit2D(Collider2D other) // проверяет когда выходим из какого либо слоя
    {
        IsTouchingLayer = _collider.IsTouchingLayers(_groundLayer);
    }

}
