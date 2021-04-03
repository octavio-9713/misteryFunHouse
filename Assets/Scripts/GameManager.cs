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

    public string sceneName;

    public Player player;

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
        }
    }

    void Start()
    {
        TextScore.text = Score.ToString();

        GameObject playerEntity = GameObject.FindGameObjectWithTag("Player");
        player = playerEntity.GetComponent<Player>();
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
}
