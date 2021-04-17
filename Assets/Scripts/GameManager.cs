using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum TypeOfItems { DPS, GENERAL, ORBITAL, EFFECT, WEAPON, ALL }

    [Header("Timer")]
    public TextMeshProUGUI timer;
    public float minutesToFast = 2.5f;
    private float _secondsCount;
    private int _minuteCount;
    private int _hourCount;

    private bool _countTime = true;

    [Header("Player")]
    public Player player;

    [Header("Ui")]
    public GameUi gameUi;
    public Canvas canvas;
    public GameObject provolisIntroPlace;
    public GameObject loading;

    [Header("Provoli Intros")]
    public List<GameObject> provolisIntros;

    [Header("Confetti")]
    public GameObject confeti;

    [Header("Items")]
    public List<GameObject> dpsItems;
    public List<GameObject> generalItems;
    public List<GameObject> orbitalItems;
    public List<GameObject> effectItems;
    public List<GameObject> weaponItems;

    private List<GameObject> _allitems = new List<GameObject>();
    private List<ItemEffect> _levelPickedItems = new List<ItemEffect>();

    private int _sceneIndex = 2;
    private bool _needsToLoad = true;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
            _instance = this;
        
    }

    private void Start()
    {
        this._allitems.AddRange(dpsItems);
        this._allitems.AddRange(generalItems);
        this._allitems.AddRange(orbitalItems);
        this._allitems.AddRange(effectItems);
        this._allitems.AddRange(weaponItems);

        StopTheCount();
    }

    void Update()
    {
        if (_countTime)
            CountTime();

        if (Input.GetKey("r"))
        {
            PlayerDeath();
        }

        if (_needsToLoad)
        {
            StartCoroutine(LoadScene(_sceneIndex));
        }
    }

    /////////////////// Count time Methods //////////////////////////
    public void CountTime()
    {
        _secondsCount += Time.deltaTime;
        timer.text = ActualTime();
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

    public string ActualTime()
    {
        return _hourCount + " : " + _minuteCount + " : " + (int)_secondsCount;
    }

    public void StopTheCount()
    {
        this._countTime = false;
    }

    public void ResumeTheCount()
    {
        this._countTime = true;
    }

    public bool HasFastTime()
    {
        return _minuteCount <= minutesToFast * _sceneIndex;
    }

    /////////////////// Restart Game Methods //////////////////////////
    private void MovePlayer()
    {
        GameObject spawnpoint = GameObject.FindGameObjectWithTag("Player Spawnpoint");
        Vector3 camaraPos = new Vector3(spawnpoint.transform.position.x, spawnpoint.transform.position.y, Camera.main.transform.position.z);

        player.transform.position = spawnpoint.transform.position;
        Camera.main.transform.position = camaraPos;

        player.gameObject.SetActive(true);
        //_pickedItems.ForEach(item => item.ApplyEffect());

        if (provolisIntros.Count > 0)
            Instantiate(provolisIntros[0], provolisIntroPlace.transform);
        
    }

    /////////////////// Restart Game Methods //////////////////////////
    public void PlayerDeath()
    {
        loading.SetActive(true);

        GameObject[] rooms = GameObject.FindGameObjectsWithTag("SpawnPointSala");
        foreach (GameObject room in rooms)
        {
            RoomManager manager = room.GetComponent<RoomManager>();
            manager.RestoreRoom();
        }

        GameObject[] rewardRooms = GameObject.FindGameObjectsWithTag("Reward Room");
        foreach (GameObject room in rewardRooms)
        {
            RewardRoom manager = room.GetComponent<RewardRoom>();
            manager.RestoreRoom();
        }

        StartCoroutine(UnloadScene(_sceneIndex, true));
        StopTheCount();

        player.RecoverLife(player.stats.maxHp);
        player.animator.SetTrigger("alive");
        player.animator.SetBool("muerte", false);
        player.UnDie();
        _levelPickedItems.ForEach(item => item.UnapplyEffect());
    }

    public void RestarGame()
    {
        SceneManager.LoadScene(0);
        _hourCount = 0;
        _minuteCount = 0;
        _secondsCount = 0;
    }

    /////////////////// Items Methods //////////////////////////
    public GameObject GetItem(TypeOfItems type)
    {
        GameObject item;

        if (type.Equals(TypeOfItems.ALL))
            item = _allitems[Random.Range(0, _allitems.Count)];
        

        else if (type.Equals(TypeOfItems.DPS))
            item = dpsItems[Random.Range(0, dpsItems.Count)];

        else if (type.Equals(TypeOfItems.EFFECT))
            item = effectItems[Random.Range(0, effectItems.Count)];

        else if (type.Equals(TypeOfItems.GENERAL))
            item = generalItems[Random.Range(0, generalItems.Count)];

        else if (type.Equals(TypeOfItems.ORBITAL))
            item = orbitalItems[Random.Range(0, orbitalItems.Count)];

        else
            item = weaponItems[Random.Range(0, weaponItems.Count)];

        Pickable picked = item.GetComponent<Pickable>();
        _levelPickedItems.Add(picked.effect);

        return item;
    }

    /////////////////// Room Methods //////////////////////////
    public void NextRoom()
    {
        player.gameObject.SetActive(false);
        
        if (provolisIntros.Count > 0)
            provolisIntros.RemoveAt(0);
        
        _levelPickedItems.ForEach(item =>
        {
            dpsItems.Remove(item.gameObject);
            effectItems.Remove(item.gameObject);
            generalItems.Remove(item.gameObject);
            orbitalItems.Remove(item.gameObject);
            weaponItems.Remove(item.gameObject);
            _allitems.Remove(item.gameObject);
        });
        _levelPickedItems.Clear();

        StartCoroutine(UnloadScene(_sceneIndex));

        StopTheCount();
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        loading.SetActive(true);

        _needsToLoad = false;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        loading.SetActive(false);
        ResumeTheCount();
        MovePlayer();
    }

    IEnumerator UnloadScene(int sceneIndex, bool restart = false)
    {
        loading.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(sceneIndex);
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        _needsToLoad = true;

        if (!restart)
            _sceneIndex++;

        yield return new WaitForSeconds(1.5f);
    }
}
