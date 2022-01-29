using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FirstPlatformer.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _action; //Это обьект для сериализации методов, в неё можем передать метод из другого компонета и вызвать его


        private void OnCollisionEnter2D(Collision2D other) //  встроенная функция показывает обьект с которым мы стригерились
        {
            if (other.gameObject.CompareTag(_tag)) //проверяем попал ли нужный нам тег в коллайдер который мы задали (для рестарта уровня, для сбора монет и тд)
            {
                    _action?.Invoke(other.gameObject);
            }
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}


