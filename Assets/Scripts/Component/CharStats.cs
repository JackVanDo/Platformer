using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace FirstPlatformer.Components
{
    public class CharStats : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _coinsCount;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;


        public void ApplyDamage(int damageValue)
        {
            _health -= damageValue;
            _onDamage?.Invoke(); // �������� ��������� ��� ���� �������� �� ������� �� _onDamage = Null � ���� ��� ������� �����
            Debug.Log($"������� �������� {_health}");
            if (_health <= 0)
            {
                _onDie?.Invoke();
            }
        }

        public void ApplyTreatment(int treatmentValue)
        {
            _health += treatmentValue;
            Debug.Log($"������� �������� {_health}");
        }

        public void CollectCoins(int coinPrice)
        {
            _coinsCount += coinPrice;
            Debug.Log($"����� ���-�� ����� {_coinsCount}");
        }
    }
}


