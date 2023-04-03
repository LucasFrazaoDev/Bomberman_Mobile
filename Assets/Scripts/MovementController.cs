using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public FloatingJoystick moveJoystick;

    private Rigidbody2D _rigibody;
    private Vector2 _direction = Vector2.down;
    public float speed = 5f;
    
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;

    private AnimatedSpriteRenderer activeSpriteRenderer;

    private void Awake()
    {
        _rigibody = GetComponent<Rigidbody2D>();
        activeSpriteRenderer = spriteRendererDown;
    }

    private void Update()
    {
        _direction = moveJoystick.Direction;

        int xDirection = Mathf.RoundToInt(_direction.x);
        int yDirection = Mathf.RoundToInt(_direction.y);

        if (xDirection == 0 && yDirection == 1)
            SetDirection(Vector2.up, spriteRendererUp);
        else if (xDirection == 0 && yDirection == -1)
            SetDirection(Vector2.down, spriteRendererDown);
        else if (xDirection == -1 && yDirection == 0)
            SetDirection(Vector2.left, spriteRendererLeft);
        else if (xDirection == 1 && yDirection == 0)
            SetDirection(Vector2.right, spriteRendererRight);
        else
            SetDirection(Vector2.zero, activeSpriteRenderer);
    }

    private void FixedUpdate()
    {
        Vector2 position = _rigibody.position;
        Vector2 translation = _direction * speed * Time.fixedDeltaTime;

        _rigibody.MovePosition(position + translation);
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
        enabled = false;
        GetComponent<BombController>().enabled = false;

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
        FindObjectOfType<GameManager>().CheckWinState();
    }
}
