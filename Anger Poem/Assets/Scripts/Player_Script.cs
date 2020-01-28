using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : Character_Script
{
    public float            rageSpeed,
                            rageDuration,
                            invulnerabilityDuration;
    public GameObject[]     wings;
    public ParticleSystem   flames;
    public int              maxHealth;
    public Text             healthCounter;

    internal bool           _raging = false;

    private int             _health;
    private float           _rageCounter,
                            _invulnerabilityCounter;

    internal override void Awake()
    {
        base.Awake();

        SetRage(false);
        _health = maxHealth;
        _rageCounter = rageDuration;

        UpdateHealth();
        //_currentFbDuration = fireballDuration;
    }

    private void FixedUpdate()
    {
        if (_raging)
        {
            GetComponent<Collider2D>().enabled = true;

            Move(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"));

            _rageCounter -= Time.deltaTime;

            if (_rageCounter <= 0f)
            {
                SetRage(false);
            }

            //_currentFbDuration -= Time.deltaTime;

            //if (_currentFbDuration <= 0f)
            //{
            //    _currentFbDuration = fireballDuration;

            //    ShootFireballs();
            //}
        }
        else
        {
            GetComponent<Collider2D>().enabled = _invulnerabilityCounter <= 0f;

            if (_invulnerabilityCounter > 0f)
            {
                _invulnerabilityCounter -= Time.deltaTime;
            }

            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        if (Input.GetButtonDown("Jump"))
        {
            SetRage(!_raging);
        }
    }

    public void SetRage(bool rage)
    {
        //Debug.Log("SET RAGE");
        foreach (GameObject item in wings)
        {
            item.SetActive(rage);
        }

        _currentSpeed = rage ? rageSpeed : walkSpeed;

        if (rage)
        {
            flames.Play();
        }
        else
        {
            flames.Stop();
            _health = maxHealth;
        }

        _raging = rage;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthCounter.text = _health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_raging &&
            collision.gameObject.tag == "NPC")
        {
            --_health;
            UpdateHealth();

            _invulnerabilityCounter = invulnerabilityDuration;

            if (_health <= 0)
            {
                SetRage(true);
            }
        }
    }

    //private void ShootFireballs()
    //{
    //    for (int i = 0; i <= numofFireballs; ++i)
    //    {
    //        GameObject newFireball = fireballPrefab;

    //        float   radians = ((360 / numofFireballs) * i) * Mathf.Deg2Rad,
    //                sin = Mathf.Sin(radians),
    //                cos = Mathf.Cos(radians);
    //        Vector2 rotVec = new Vector2((cos * Vector2.up.x) - (sin * Vector2.up.y), (sin * Vector2.up.x) + (cos * Vector2.up.y));
    //        newFireball.GetComponent<Flame_Script>()._direction = rotVec;

    //        Instantiate(newFireball, transform.position, Quaternion.identity);
    //    }
    //}
}
