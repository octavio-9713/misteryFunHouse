using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Timer")]
    public TextMeshProUGUI timer;
    private float _secondsCount;
    private int _minuteCount;
    private int _hourCount;

    [Header("Scene Name")]
    public string sceneName;

    [Header("Player")]
    public Player player;

    [Header("Ui")]
    public GameUi gameUi;
    public Canvas canvas;

    [Header("Confetti")]
    public GameObject confeti;

    [Header("Spawn Settings")]
    public int maxRooms = 5;
    public int actualRooms = 0;
    public bool finishSpawn = false;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

            GameObject playerEntity = GameObject.FindGameObjectWithTag("Player");
            player = playerEntity.GetComponent<Player>();
        }

    }

<<<<<<< HEAD
    void Start()
    {
        //TextScore.text = Score.ToString();
    }


=======
>>>>>>> 62d7930a7dd7ff11e7a3a2c1799af9fc4b8a7819
    void Update()
    {
        CountTime();

        if (Input.GetKey("r"))
        {
            RestarGame();
        }


    }

    public void CountTime()
    {
        _secondsCount += Time.deltaTime;
        timer.text = _hourCount + " : " + _minuteCount + " : " + (int)_secondsCount;
        if (_secondsCount >= 60)
        {
            _minuteCount++;
            _secondsCount = 0;
        }
        else if (_minuteCount >= 60)
        {
            _hourCount++;
            _minuteCount = 0;
        }

    }

    public void RestarGame()
    {
        SceneManager.LoadScene(sceneName);
        _hourCount = 0;
        _minuteCount = 0;
        _secondsCount = 0;
    }

    public void IncreaseRoom()
    {
        actualRooms++;

        if (actualRooms > maxRooms)
        {
            finishSpawn = true;
        }

    }
}
