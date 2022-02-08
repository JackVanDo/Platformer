using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPlatformer.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target; //Обьект где будет создаваться спрайт
        [SerializeField] private GameObject _prefab; // Партикол который будем создавать


        [ContextMenu("Spawn")]
        public void Spawn()
        {
           var instantiate =  Instantiate(_prefab, _target.position, Quaternion.identity); // Для создания копии используем этот метод, метод перегруженый можем пользоваться разными параметрами
                                                                                           // 1 первый параметр то что мы будем клонировать
                                                                                           // 2 позиция 
                                                                                           // 3 кватернион это поворот префаба

            instantiate.transform.localScale = _target.lossyScale; // lossyScale - Скейлинг в мире, а локал скейл - скейлинг компонента от его родителя , сделал для того что бы партикл поворачивался вместе с родителем

        }
    }
}


