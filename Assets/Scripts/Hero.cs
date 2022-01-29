using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FirstPlatformer
{
    public class Hero : MonoBehaviour
    {

        [SerializeField] private float _speed; //[SerializeField] это атрибут которые мы можем помечать разные члены класса, в данном случае он помечает поле сериализуемым то есть сохраненым ,
                                               //Unity будет отображать такое поле в инспекторе а во вторыз будет сохранять значение этого поля и данных компонента прикрепленнго к GameObject, 
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private LayerCheck _groundCheck;
        //[SerializeField] private Vector3 _groundCheckPositionDelta;
        //[SerializeField] private LayerMask _groundLayer; // Добавляем поле в котором будет физический слой земли тип LayerMask 
        //[SerializeField] private float _groundCheckRadisus; //радиус проверки земли



        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private int _coinsCount = 0;
        private Animator _animator;
        private SpriteRenderer _sprite;
        private bool _isGrounded;
        private bool _allowDoubleJump;


        private static readonly int isGroundKey = Animator.StringToHash("is-ground"); // int так как метод преобразует строку к int
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int verticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>(); //получаем физическое тело обьекта, или получаем компонент RigidBody
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void Update()
        {
            _isGrounded = IsGrounded();
        }

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * _speed; // считаем наши координаты
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);


            _animator.SetBool(isGroundKey, _isGrounded);
            _animator.SetBool(isRunning, _direction.x != 0);
            _animator.SetFloat(verticalVelocity, _rigidbody.velocity.y);
            //_animator.SetBool("is-ground", isGrounded); но данный метод не эффективен с точки зрения оптимизации так как каждый раз при фиксе апдейте вызываем метод StringToHash
            //_animator.SetBool("is-running", _direction.x != 0); // Задаем значение is-running которое мы создали в аниматоре для понимания бежим или нет
            //_animator.SetFloat("vertical-velocity", _rigidbody.velocity.y);

            UpdateSpriteDirection();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpingPressing = _direction.y > 0;

            if (_isGrounded) _allowDoubleJump = true;

            if (isJumpingPressing)
            {
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigidbody.velocity.y > 0)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;

            if(_isGrounded)
            {
                yVelocity += _jumpSpeed;
            } else if (_allowDoubleJump)
            {
                yVelocity = _jumpSpeed;
                _allowDoubleJump = false;
            }

            return yVelocity;
        }

        private void UpdateSpriteDirection()
        {
            if (_direction.x > 0)
            {
                _sprite.flipX = false;
            }
            else if (_direction.x < 0)
            {
                _sprite.flipX = true;
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

        private void OnTriggerEnter2D(Collider2D other) // Подсчет монет
        {
            switch (other.gameObject.layer)
            {
                case 8:
                    _coinsCount++;
                    Debug.Log($"Общее кол-во монет {_coinsCount}");
                    break;
                case 9:
                    _coinsCount = _coinsCount + 10;
                    Debug.Log($"Общее кол-во монет {_coinsCount}");
                    break;
            }
        }

        public void TakeDamage()
        {
            _animator.SetTrigger(Hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed); // при получении урона устанавливаем насколько подпрыгнет герой
        }

        //private void OnDrawGizmos() //метод отрисовывается во время отрисовки дебажных иконок и информации на нашей сцене 
        //{
        //    //Debug.DrawRay(transform.position, Vector3.down, IsGrounded() ? Color.green : Color.red); //отрисовываем дебажный луч, если колайдер найден луч зеленый, если нет красный
        //    Gizmos.color = IsGrounded() ? Color.green : Color.red;
        //    Gizmos.DrawSphere(transform.position, 0.3f); // Отрисовываем сферу, сначала позиция потом радиус, тут указываем мировые координаты
        //}


    }

}
