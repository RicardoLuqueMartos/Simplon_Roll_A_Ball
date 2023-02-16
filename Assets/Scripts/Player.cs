using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    Vector2 moveInput;
    Vector3 moveValue;


    private Rigidbody _rigidbody;
    private int ScoreValue = 0;

    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] LevelData currentLevel;

    [SerializeField] List<Target> TargetLists = new List<Target>();

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : " + ScoreValue;

        TargetLists = FindObjectsOfType<Target>().ToList();
    }

    private void OnEnable()
    {
        if (PlayerPrefs.GetInt("GameStarted") == 0)
            StartGame();
        else LoadGame();
    }

    void StartGame()
    {
        PlayerPrefs.SetInt("GameStarted", 1);
        ScoreValue = 0;
        PlayerPrefs.SetInt("Score", ScoreValue);
    }

    void LoadGame()
    {
        ScoreValue = PlayerPrefs.GetInt("Score");
        _scoreText.text = "Score : " + ScoreValue.ToString();
    }

    private void FixedUpdate()
    {
        ApplyMove();
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();       
    }

    void OnJump()
    {
        Debug.Log("OnJump");
        transform.GetComponent<Renderer>().material.color = Color.blue;
    }

    void ApplyMove()
    {
        moveValue = new Vector3(moveInput.x, 0, moveInput.y);
         
        _rigidbody.AddForce(moveValue  * moveSpeed);       
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            RemoveFromList(target);
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
            RemoveFromList(target);
            LaunchAction(target);
            Destroy(collision.gameObject);
            UpdateScore();
        }
    }

    void RemoveFromList(Target target)
    {
        TargetLists.Remove(target);
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

        VerifyGoal();

    }

    void VerifyGoal()
    {
        if (TargetLists.Count == 0)   
            LoadNextScene();
    }

    void LoadNextScene()
    {
        if (currentLevel != null) SceneManager.LoadScene(currentLevel.NextSceneName);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("GameStarted", 0);
        PlayerPrefs.SetInt("Score", 0);
    }
}
