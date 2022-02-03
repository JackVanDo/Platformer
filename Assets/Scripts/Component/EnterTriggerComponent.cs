using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace FirstPlatformer.Components
{

    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private UnityEvent _action; //��� ������ ��� ������������ �������, � �� ����� �������� ����� �� ������� ��������� � ������� ���
        [SerializeField] private EnterEvent _targetAction; //��� ������ ��� ������������ �������, � �� ����� �������� ����� �� ������� ��������� � ������� ���



        private void OnTriggerEnter2D(Collider2D other) // ���������� ������� ���������� ������ � ������� �� ������������
        {
            if (other.gameObject.CompareTag(_tag)) //��������� ����� �� ������ ��� ��� � ��������� ������� �� ������ (��� �������� ������, ��� ����� ����� � ��)
            {
                if(_action != null)
                {
                    _action.Invoke();
                    _targetAction?.Invoke(other.gameObject);
                }
            }
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}

