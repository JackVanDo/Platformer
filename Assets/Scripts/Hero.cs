using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>(); //�������� ���������� ���� �������, ��� �������� ��������� RigidBody
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
                _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse); // ������ ���� ������� �� ���������  � ��� � �������, � ������ ������ ������ ���� ��� ������ ������� � ������ ���� ������ ������, � ����� ������ ������� 
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
        //var hit = Physics2D.CircleCast(transform.position, Vector2.down, 1, _groundLayer);    //����� Physics2D ����� ����� raycast ����������� ��� � ��� ������� � ���������� ���������� ��� ��������� �����������, ������� �������� ������� �� ������� ���� ���,
        //����� �������� ����������� �������� ����, ����� �������� ���������, ��� ��������� ������ �� �������� ��������� ����������� ����. ��� ���� �������� ��������� ��� ��������� �����������
        //��� �� ���� ����� CircleCast ��������� ������ ������� � ����������� 
        //return hit.collider != null; //���������� �������� �������� �� �� ��� �������� ���� ����������� � ���������� �����

        
    }

    //private void OnDrawGizmos() //����� �������������� �� ����� ��������� �������� ������ � ���������� �� ����� ����� 
    //{
    //    //Debug.DrawRay(transform.position, Vector3.down, IsGrounded() ? Color.green : Color.red); //������������ �������� ���, ���� �������� ������ ��� �������, ���� ��� �������
    //    Gizmos.color = IsGrounded() ? Color.green : Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.3f); // ������������ �����, ������� ������� ����� ������, ��� ��������� ������� ����������
    //}


}
