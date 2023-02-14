using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class Player : MonoBehaviour
{
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

    void Update()
    {
        MoveBall();
    }

    void MoveBall()
    {
        if (Input.GetAxis("Horizontal") != 0f)
        {
            _rigidbody.AddForce(Input.GetAxis("Horizontal") * 0.5f, 0f, 0f);
        }
        if (Input.GetAxis("Vertical") != 0f)
        {
            _rigidbody.AddForce(0f, 0f, Input.GetAxis("Vertical") + 0.5f);
        }
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
