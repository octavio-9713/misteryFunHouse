using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    public static int Score;
    public TextMeshProUGUI TextScore;

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

    void Start()
    {
        //TextScore.text = Score.ToString();
    }


    void Update()
    {
        if (Input.GetKey("r"))
        {
            RestarGame();
        }


    }

    public void SumarPuntos(int puntos)
    {
        Score = Score + puntos;
        TextScore.text = Score.ToString();

    }

    public void RestarGame()
    {
        SceneManager.LoadScene(sceneName);
        Score = 0;
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
