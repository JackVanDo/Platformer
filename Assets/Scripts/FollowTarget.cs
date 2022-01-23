using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirstPlatformer
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _damping;


        private void LateUpdate()
        {
            var destination = new Vector3(_target.position.x, _target.position.y, transform.position.z); // тут берем координаты нашего обьекта который мы отслеживаем
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * _damping); // здесь уже указываем аналогичную позицию дл€ камеры, использую метод интерпол€ции
                                                                                                           // сглажива€ эффект перемещени€, сначала пишем позицию котора€ у камеры, далее позци€ цели, далее умножаем врем€ прошедшее с перд итерации умножеа€
                                                                                                           // на некий показатель плавности
        }
    }
}

