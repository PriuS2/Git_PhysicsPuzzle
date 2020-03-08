using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Vector2 _velocity;
    private Vector2 _acceleration;
    private float _angularVelocity;
    private AudioSource _audioSource;
    private bool _collisionEnter;

    public float max = 0;
    
    public Rigidbody2D Rb2d { get; set; }
    public Transform Sprite { get; set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        Rb2d = GetComponent<Rigidbody2D>();
        Sprite = transform.GetChild(0);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var currentVel = Rb2d.velocity;
        _acceleration = currentVel - _velocity;

        _velocity = currentVel;
        _angularVelocity = Rb2d.angularVelocity;
        
        if (_velocity.magnitude > 0)
        {
            Sprite.rotation = Quaternion.identity;
        }

        //Debug
        var accelerationScale = _acceleration.magnitude;
        if (accelerationScale <= 0.05f) accelerationScale = 0;
        Debug.Log(accelerationScale);
        if (max < accelerationScale)
        {
            max = accelerationScale;
        }//debug End

        if (_collisionEnter)
        {
            _collisionEnter = false;
            PlayCollisionSound();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        _collisionEnter = true;
    }

    private void PlayCollisionSound()
    {
        var accelerationScale = _acceleration.magnitude;
        float volume = accelerationScale / 10;
        if (volume >= 1) volume = 1;
        else if (volume <= 0.1f) volume = 0.1f;
        _audioSource.volume = volume;
        _audioSource.Play();
    }
}
