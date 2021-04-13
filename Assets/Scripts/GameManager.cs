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
    public int rewardsRooms = 2;
    
    private int _actualRooms = 0;
    private int _actualRewardRooms = 0;
    
    public bool finishSpawn = false;
    private bool _needsSpecial = true;

    [Header("Items")]
    public GameObject[] items;
    private List<GameObject> _allItems;

    [Header("End Rooms")]
    private List<GameObject> _endRooms = new List<GameObject>();

    [Header("Rooms")]
    public RoomSpecialTemplates specialRooms;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            //Destroy(this.gameObject);
        }
        else
        {
            _instance = this;

            GameObject playerEntity = GameObject.FindGameObjectWithTag("Player");
            player = playerEntity.GetComponent<Player>();

            _allItems = new List<GameObject>(items);
        }

    }

    void Update()
    {
        CountTime();

        if (Input.GetKey("r"))
        {
            RestarGame();
        }


    }

    /////////////////// Count time Methods //////////////////////////
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

    /////////////////// Restart Game Methods //////////////////////////
    public void RestarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _hourCount = 0;
        _minuteCount = 0;
        _secondsCount = 0;
    }

    /////////////////// Room Methods //////////////////////////
    public void IncreaseRoom()
    {
        _actualRooms++;

        if (_actualRooms > maxRooms)
        {
            finishSpawn = true;
        }

    }

    public void AddEndRoom(int openSide, GameObject room)
    {
        if (_needsSpecial)
        {
            GameObject[] salas;

            if (_actualRewardRooms < rewardsRooms)
            {
                salas = openSide == 1 ? specialRooms.topRewardsRoom : openSide == 2 ?
                    specialRooms.rightRewardsRooms : openSide == 3 ? specialRooms.bottomRewardsRooms : specialRooms.leftRewardsRooms;
                _actualRewardRooms++;
            }

            else
            {
                salas = openSide == 1 ? specialRooms.bossTopRooms : openSide == 2 ?
                    specialRooms.bossRightRooms : openSide == 3 ? specialRooms.bossBottomRooms : specialRooms.bossLeftRooms;

                _needsSpecial = false;
            }

            int _rand = Random.Range(0, salas.Length - 1);

            GameObject sala = salas[_rand];

            Instantiate(sala, room.transform.position, room.transform.rotation);
            Destroy(room);
        }
    }

    public GameObject GetItem()
    {
        GameObject item = _allItems[Random.Range(0, _allItems.Count)];
        _allItems.Remove(item);
        return item;
    }
}
