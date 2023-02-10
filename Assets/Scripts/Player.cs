using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private int ScoreValue = 0;
    // TODO reset score
    [SerializeField] private TMP_Text _scoreText;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : " + ScoreValue;
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) 
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, Input.GetAxis("Vertical") + 0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            LaunchAction(target);
            Destroy(other.gameObject);
            UpdateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            LaunchAction(target);
            Destroy(collision.gameObject);
            UpdateScore();
        }
    }

    void LaunchAction(Target target)
    {
        if (target._action != null) {
            target._action.LaunchAction(target);
    
        }
    }

    private void UpdateScore()
    {
        ScoreValue ++;
        _scoreText.text = "Score : " + ScoreValue.ToString();
        PlayerPrefs.SetInt("Score", ScoreValue);
    }
}
