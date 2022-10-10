using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundChek;

    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private bool _isAvailableJump;
    private float _checkGroundRadius = 0.3f;
    
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        DisableRunAnimation();
        _isAvailableJump = Physics2D.OverlapCircle(_groundChek.position, _checkGroundRadius, _groundLayer);

        if (Input.GetAxis("Horizontal") < 0)
        {
            EnableRunAnimation();
            _spriteRenderer.flipX = true;
            transform.position += new Vector3(-1 * _speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            EnableRunAnimation();
            _spriteRenderer.flipX = false;
            transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isAvailableJump)
        {
            DisableRunAnimation();
            Jump();
        } 
    }

    private void EnableRunAnimation()
    {
        _animator.SetBool("isRun", true);
    }

    private void DisableRunAnimation()
    {
        _animator.SetBool("isRun", false);
    }
    private void EnableJumpAnimation()
    {
        _animator.SetTrigger("Jump");
    }

    private void Jump()
    {
        EnableJumpAnimation();
        _rigidBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
}
