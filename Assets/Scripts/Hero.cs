using FirstPlatformer.Components;
using System;
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
        [SerializeField] private float _damageJumpSpeed;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private float _interactionRadius;
        [SerializeField] private LayerMask _interactionLayer;
    
        [SerializeField] private SpawnComponent _footStepParticles;
        [SerializeField] private SpawnComponent _jumpParticles;
        [SerializeField] private ParticleSystem _hitParticles;



        //[SerializeField] private Vector3 _groundCheckPositionDelta;
        //[SerializeField] private LayerMask _groundLayer; // ��������� ���� � ������� ����� ���������� ���� ����� ��� LayerMask 
        //[SerializeField] private float _groundCheckRadisus; //������ �������� �����



        private Vector2 _direction;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private bool _isGrounded;
        private bool _allowDoubleJump;
        private Collider2D[] _interactionResult = new Collider2D[1];
        private bool _isJumping;
        private CharStats _heroStats;

        private static readonly int isGroundKey = Animator.StringToHash("is-ground"); // int ��� ��� ����� ����������� ������ � int
        private static readonly int isRunning = Animator.StringToHash("is-running");
        private static readonly int verticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>(); //�������� ���������� ���� �������, ��� �������� ��������� RigidBody
            _animator = GetComponent<Animator>();
            _heroStats = GetComponent<CharStats>();
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
            var xVelocity = _direction.x * _speed; // ������� ���� ����������
            var yVelocity = CalculateYVelocity();
            _rigidbody.velocity = new Vector2(xVelocity, yVelocity);


            _animator.SetBool(isGroundKey, _isGrounded);
            _animator.SetBool(isRunning, _direction.x != 0);
            _animator.SetFloat(verticalVelocity, _rigidbody.velocity.y);
            //_animator.SetBool("is-ground", isGrounded); �� ������ ����� �� ���������� � ����� ������ ����������� ��� ��� ������ ��� ��� ����� ������� �������� ����� StringToHash
            //_animator.SetBool("is-running", _direction.x != 0); // ������ �������� is-running ������� �� ������� � ��������� ��� ��������� ����� ��� ���
            //_animator.SetFloat("vertical-velocity", _rigidbody.velocity.y);

            UpdateSpriteDirection();
        }

        private float CalculateYVelocity()
        {
            var yVelocity = _rigidbody.velocity.y;
            var isJumpingPressing = _direction.y > 0;

            if (_isGrounded)
            {
                _allowDoubleJump = true;
                _isJumping = false;
            }
            if (isJumpingPressing)
            {
                _isJumping = true;
                yVelocity = CalculateJumpVelocity(yVelocity);
            }
            else if (_rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelocity *= 0.5f;
            }

            return yVelocity;
        }

        private float CalculateJumpVelocity(float yVelocity)
        {
            var isFalling = _rigidbody.velocity.y <= 0.001f;
            if (!isFalling) return yVelocity;

            if (_isGrounded)
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
                transform.localScale = new Vector3(1, 1, 1);  // ������ ����������� �� ��� X ��� HERO � ���� �������� ���������
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
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


        public void TakeDamage()
        {
            _isJumping = false;
            _animator.SetTrigger(Hit);
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _damageJumpSpeed); // ��� ��������� ����� ������������� ��������� ���������� �����

            if(_heroStats._coinsCount > 0)
            {
                SpawnCoins();
            }
        }


        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_heroStats._coinsCount, 5); // ��������� ���� �� ������ ���-�� ����� ��� ��������, ���� ���� ������� 5 ���� ��� �� ������� ������� ����
            _heroStats._coinsCount -= numCoinsToDispose;

            var burst = _hitParticles.emission.GetBurst(0); // �������� ������ ��� � �������� ������ ���, ����� �������� ������ ����� � ������� 
            burst.count = numCoinsToDispose; //���-�� ������������ ����� ������ ���������� �� �������� ����
            _hitParticles.emission.SetBurst(0, burst); // ������������� ���������� ���-�� ����� � ������ �����
            _hitParticles.gameObject.SetActive(true); // ����� � ������� �������� ���� ��������������, ����� ���������� � � �����������
            _hitParticles.Play();
        }

        public void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(transform.position, _interactionRadius, _interactionResult, _interactionLayer); // ��������� �������� �������������� �������, �� �� �������� ������ ������

            for (int i = 0; i < size; i++)
            {
                var interactable = _interactionResult[i].GetComponent<InteractableComponent>();
                if(interactable != null)
                {
                    interactable.Interact();
                }
            }
        }


        public void SpawnFootDust() // ����� ������ ��� ��������� �������� � ������� ��������
        {
            _footStepParticles.Spawn(); 
        }


        public void JumpDust() // ����� ������ ��� ��������� �������� � ������
        {
            if (_isJumping)
            {
                _jumpParticles.Spawn();
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
