using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FirstPlatformer
{
    public class Hero : MonoBehaviour
    {

        [SerializeField] private float _speed; //[SerializeField] ��� ������� ������� �� ����� �������� ������ ����� ������, � ������ ������ �� �������� ���� ������������� �� ���� ���������� ,
                                               //Unity ����� ���������� ����� ���� � ���������� � �� ������ ����� ��������� �������� ����� ���� � ������ ���������� ������������� � GameObject, 
        [SerializeField] private float _jumpSpeed;
        [SerializeField] private LayerCheck _groundCheck;
        //[SerializeField] private Vector3 _groundCheckPositionDelta;
        //[SerializeField] private LayerMask _groundLayer; // ��������� ���� � ������� ����� ���������� ���� ����� ��� LayerMask 
        //[SerializeField] private float _groundCheckRadisus; //������ �������� �����



        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private int _coinsCount = 0;
        private Animator _animator;
        private SpriteRenderer _sprite;


        private static readonly int isGroundKey = Animator.StringToHash("is-ground"); // int ��� ��� ����� ����������� ������ � int
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int verticalVelocity = Animator.StringToHash("vertical-velocity");

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>(); //�������� ���������� ���� �������, ��� �������� ��������� RigidBody
            _animator = GetComponent<Animator>();
            _sprite = GetComponent<SpriteRenderer>();
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }


        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

            var isJumping = _direction.y > 0;
            var isGrounded = IsGrounded();
            if (isJumping)
            {
                if (isGrounded && _rigidbody.velocity.y <= 0)
                {
                    _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse); // ������ ���� ������� �� ���������  � ��� � �������, � ������ ������ ������, ���� ��� ������ ������� � ������ ���� ������ ������, � ����� ������ ������� 
                }
            }
            else if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
            }

            _animator.SetBool(isGroundKey, isGrounded);
            _animator.SetBool(isRunning, _direction.x != 0);
            _animator.SetFloat(verticalVelocity, _rigidbody.velocity.y);
            //_animator.SetBool("is-ground", isGrounded); �� ������ ����� �� ���������� � ����� ������ ����������� ��� ��� ������ ��� ��� ����� ������� �������� ����� StringToHash
            //_animator.SetBool("is-running", _direction.x != 0); // ������ �������� is-running ������� �� ������� � ��������� ��� ��������� ����� ��� ���
            //_animator.SetFloat("vertical-velocity", _rigidbody.velocity.y);

            UpdateSpriteDirection();
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
            //var hit = Physics2D.CircleCast(transform.position, Vector2.down, 1, _groundLayer);    //����� Physics2D ����� ����� raycast ����������� ��� � ��� ������� � ���������� ���������� ��� ��������� �����������, ������� �������� ������� �� ������� ���� ���,
            //����� �������� ����������� �������� ����, ����� �������� ���������, ��� ��������� ������ �� �������� ��������� ����������� ����. ��� ���� �������� ��������� ��� ��������� �����������
            //��� �� ���� ����� CircleCast ��������� ������ ������� � ����������� 
            //return hit.collider != null; //���������� �������� �������� �� �� ��� �������� ���� ����������� � ���������� �����


        }

        private void OnTriggerEnter2D(Collider2D other) // ������� �����
        {
            switch (other.gameObject.layer)
            {
                case 8:
                    _coinsCount++;
                    Debug.Log($"����� ���-�� ����� {_coinsCount}");
                    break;
                case 9:
                    _coinsCount = _coinsCount + 10;
                    Debug.Log($"����� ���-�� ����� {_coinsCount}");
                    break;
            }
        }

        //private void OnDrawGizmos() //����� �������������� �� ����� ��������� �������� ������ � ���������� �� ����� ����� 
        //{
        //    //Debug.DrawRay(transform.position, Vector3.down, IsGrounded() ? Color.green : Color.red); //������������ �������� ���, ���� �������� ������ ��� �������, ���� ��� �������
        //    Gizmos.color = IsGrounded() ? Color.green : Color.red;
        //    Gizmos.DrawSphere(transform.position, 0.3f); // ������������ �����, ������� ������� ����� ������, ��� ��������� ������� ����������
        //}


    }

}
