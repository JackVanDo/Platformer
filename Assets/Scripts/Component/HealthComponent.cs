using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace FirstPlatformer.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;


        public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            _onDamage?.Invoke(); // �������� ��������� ��� ���� �������� �� ������� �� _onDamage = Null � ���� ��� ������� �����
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }
    }
}


