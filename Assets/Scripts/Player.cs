using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.InputSystem;
using System.Xml.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10;
    Vector2 moveInput;
    Vector3 moveValue;


    private Rigidbody _rigidbody;
    private int ScoreValue = 0;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _speedText;

    [SerializeField] LevelData currentLevel;

    [SerializeField] List<Target> TargetLists = new List<Target>();
    public List<ObjectData> ObjectsList = new List<ObjectData>();
    public List<SpawnerObject> SpawnerObjectsList = new List<SpawnerObject>();

    bool buttonLocked = false;
    [SerializeField] GameObject button;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _scoreText.text = "Score : " + ScoreValue;

        TargetLists = FindObjectsOfType<Target>().ToList();
    }

    private void OnEnable()
    {
        TargetLists = FindObjectsOfType<Target>().ToList();
        SpawnerObjectsList = FindObjectsOfType<SpawnerObject>().ToList();

        _speedText.text = "Speed : " + moveSpeed.ToString();

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
        if (buttonLocked == false)
        {
            if (moveSpeed == 10) moveSpeed = 20;
            else if (moveSpeed == 20) moveSpeed = 30;
            else if (moveSpeed == 30) moveSpeed = 10;
            _speedText.text = "Speed : "+ moveSpeed.ToString();
            LockButton();
            Invoke("UnlockButton", 1);
        }
    }

    void LockButton()
    {
        buttonLocked = true;
        button.SetActive(false);
    }
    void UnlockButton()
    {
        buttonLocked = false;
        button.SetActive(true);

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
            LaunchAction(target);
            RemoveFromList(target, other.gameObject);
            UpdateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            LaunchAction(target);
            RemoveFromList(target, collision.gameObject);
            UpdateScore();
        }
    }

    void RemoveFromList(Target target, GameObject obj)
    {
        TargetLists.Remove(target);
        Destroy(obj);
    }

    void LaunchAction(Target target)
    {
        if (target._action != null) {
            target._action.LaunchAction(target, this);
    
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
