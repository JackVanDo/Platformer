using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _speed; //[SerializeField] это атрибут которые мы можем помечать разные члены класса, в данном случае он помечает поле сериализуемым то есть сохраненым ,
                                           //Unity будет отображать такое поле в инспекторе а во вторыз будет сохранять значение этого поля и данных компонента прикрепленнго к GameObject, 
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private LayerCheck _groundCheck;
    //[SerializeField] private Vector3 _groundCheckPositionDelta;
    //[SerializeField] private LayerMask _groundLayer; // Добавляем поле в котором будет физический слой земли тип LayerMask 
    //[SerializeField] private float _groundCheckRadisus; //радиус проверки земли



    private Vector2 _direction;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); //получаем физическое тело обьекта, или получаем компонент RigidBody
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }


    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        var isJumping = _direction.y > 0;
        if (isJumping)
        {
            if (IsGrounded())
            {
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse); // задает силу которую мы добавляем  и как её добавим, в данном случае наверх есть два режима импульс и просто сила просто толчок, в нашем случае импульс 
            }
        } else if (_rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return _groundCheck.IsTouchingLayer;
        //var hit = Physics2D.CircleCast(transform.position + _groundCheckPositionDelta, _groundCheckRadisus, Vector2.down, 0, _groundLayer);
        //var hit = Physics2D.CircleCast(transform.position, Vector2.down, 1, _groundLayer);    //Класс Physics2D имеет метод raycast откладывает луч в опр сторону и возвращает коордианты где произошло пересечение, сначала передает позиции от которой идет луч,
        //потом выбираем направление выстрела луча, далее выбираем дистанцию, ещё добавляем обьект до которого првоеряем пересечение луча. Как итог получаем структуру что описывает пересечение
        //так же есть метод CircleCast указываем радиус позицию и направление 
        //return hit.collider != null; //Возвращаем значение получили ли мы при отправке луча пересечение с колайдером земли

        
    }

    //private void OnDrawGizmos() //метод отрисовывается во время отрисовки дебажных иконок и информации на нашей сцене 
    //{
    //    //Debug.DrawRay(transform.position, Vector3.down, IsGrounded() ? Color.green : Color.red); //отрисовываем дебажный луч, если колайдер найден луч зеленый, если нет красный
    //    Gizmos.color = IsGrounded() ? Color.green : Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.3f); // Отрисовываем сферу, сначала позиция потом радиус, тут указываем мировые координаты
    //}


}
