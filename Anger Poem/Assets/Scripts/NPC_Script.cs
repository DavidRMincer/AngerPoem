using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Script : Character_Script
{
    public enum AIStage
    {
        WAITING, WALKING, DEAD
    };

    public Sprite       deadSprite;
    public GameObject   player;
    public float        waitTime,
                        walkTime;
    
    private AIStage     _currentState;
    private float       _currentCountdown;
    private Vector2     _direction;
    private bool        _scared;

    internal override void Awake()
    {
        base.Awake();

        _currentState = AIStage.WAITING;
        _scared = false;
    }

    private void FixedUpdate()
    {
        switch (_currentState)
        {
            case AIStage.WAITING:
                _currentCountdown -= Time.deltaTime;
                _rb.velocity = Vector2.zero;

                if (_currentCountdown <= 0f)
                {
                    _currentCountdown = Random.Range(waitTime / 4, waitTime);
                    _currentState = AIStage.WALKING;

                    _direction = (player.transform.position - transform.position).normalized;
                    _direction = _scared ? -_direction : _direction;
                }
                break;
            case AIStage.WALKING:
                _currentCountdown -= Time.deltaTime;
                Move(_direction.x, _direction.y);

                if (_currentCountdown <= 0f)
                {
                    _currentCountdown = Random.Range(waitTime / 4, waitTime);
                    _currentState = AIStage.WAITING;
                }
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        _scared = player.GetComponent<Player_Script>()._raging ? true : _scared;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _currentCountdown = Random.Range(waitTime / 4, waitTime);
            _currentState = AIStage.WAITING;

            if (collision.gameObject.GetComponent<Player_Script>()._raging)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().sprite = deadSprite;
                _rb.velocity = Vector2.zero;

                _currentState = AIStage.DEAD;
            }
        }
    }
}
