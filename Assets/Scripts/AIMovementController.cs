using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovementController : MonoBehaviour
{
    private Vector2 _direction = Vector2.down;

    [Header("Sprites Scripts")]
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;

    private AnimatedSpriteRenderer activeSpriteRenderer;

    [Header("Navmesh Agent")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _speed = 5f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()
    {
        if (_agent.velocity.normalized.magnitude > 0.01f)
        {
            _direction = _agent.velocity.normalized;

            if (Mathf.Abs(_direction.x) > Mathf.Abs(_direction.y))
            {
                SetDirection(_direction.x > 0 ? Vector2.right : Vector2.left, _direction.x > 0 ? spriteRendererRight : spriteRendererLeft);
            }
            else
            {
                SetDirection(_direction.y > 0 ? Vector2.up : Vector2.down, _direction.y > 0 ? spriteRendererUp : spriteRendererDown);
            }
        }
        else
        {
            SetDirection(Vector2.zero, activeSpriteRenderer);
        }
    }

    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        _direction = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = _direction == Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            DeathSequence();
        }
    }

    private void DeathSequence()
    {
        _agent.enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded()
    {
        gameObject.SetActive(false);
    }

}
