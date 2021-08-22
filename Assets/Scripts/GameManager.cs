using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

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

    public GameObject gameover;

    [Header("Provoli Intros")]
    public List<GameObject> provolisIntros;
    public bool provoliTalking = false;

    [Header("Confetti")]
    public GameObject confeti;

    [Header("Items")]
    public List<GameObject> dpsItems;
    private List<GameObject> _updatedDpsItems;
    
    public List<GameObject> generalItems;
    private List<GameObject> _updatedGeneralItems;

    public List<GameObject> orbitalItems;
    private List<GameObject> _updatedOrbitalItems;

    public List<GameObject> effectItems;
    private List<GameObject> _updatedEffectItems;

    public List<GameObject> weaponItems;
    private List<GameObject> _updatedWeaponItems;

    private List<GameObject> _allitems = new List<GameObject>();
    private List<ItemEffect> _levelPickedItems = new List<ItemEffect>();

    private List<GameObject> _instantiatedObjects = new List<GameObject>();

    private int _sceneIndex = 2;
    private bool _needsToLoad = true;

    private Pause _pause;

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
        _pause = GetComponent<Pause>();
        Cursor.visible = false;

        SetUpItems();

        StopTheCount();
    }

    void Update()
    {
        if (_countTime)
            CountTime();

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

    public void RestartTheCount()
    {
        _hourCount = 0;
        _minuteCount = 0;
        _secondsCount = 0;
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

    public void PlayerDeath(bool gameOver = true)
    {
        _instantiatedObjects.ForEach(obj => {
            if (obj != null)
                Destroy(obj);
        });

        if (gameOver)
        {
            gameover.SetActive(true);
            Cursor.visible = true;
            StopTheCount();

            //Si elige reiniciar
            //RestarGame();
        }

        else
        {
            loading.SetActive(true);

            StartCoroutine(UnloadScene(_sceneIndex, true));

            player.RecoverLife(player.stats.maxHp);
            player.animator.SetTrigger("alive");
            player.animator.SetBool("muerte", false);
            player.UnDie();

            _levelPickedItems.ForEach(item => item.UnapplyEffect());
            SetUpItems();
        }
    }

    public void RestarGame()
    {
        Cursor.visible = false;

        SceneManager.LoadScene(1);
        RestartTheCount();
    }

    public void QuitMainMenu()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void AddInstantiatedObject(GameObject obj)
    {
        _instantiatedObjects.Add(obj);
    }

    /////////////////// Menus Game Methods //////////////////////////

    public void ResumeGame()
    {
        this._pause.Resume();
    }

    public void EndResumeAnim()
    {
        this._pause.EndPause();
    }

    /////////////////// Items Methods //////////////////////////

    private void SetUpItems()
    {
        _updatedDpsItems = dpsItems;
        _allitems.AddRange(dpsItems);

        _updatedGeneralItems = generalItems;
        _allitems.AddRange(generalItems);

        _updatedOrbitalItems = orbitalItems;
        _allitems.AddRange(orbitalItems);

        _updatedEffectItems = effectItems;
        _allitems.AddRange(effectItems);

        _updatedWeaponItems = weaponItems;
        _allitems.AddRange(weaponItems);
    }

    public GameObject GetItem(TypeOfItems type)
    {
        GameObject item;

        if (type.Equals(TypeOfItems.ALL))
            item = _allitems[Random.Range(0, _allitems.Count)];
        

        else if (type.Equals(TypeOfItems.DPS))
            item = _updatedDpsItems[Random.Range(0, _updatedDpsItems.Count)];

        else if (type.Equals(TypeOfItems.EFFECT))
            item = _updatedEffectItems[Random.Range(0, _updatedEffectItems.Count)];

        else if (type.Equals(TypeOfItems.GENERAL))
            item = _updatedGeneralItems[Random.Range(0, _updatedGeneralItems.Count)];

        else if (type.Equals(TypeOfItems.ORBITAL))
            item = _updatedOrbitalItems[Random.Range(0, _updatedOrbitalItems.Count)];

        else
            item = _updatedWeaponItems[Random.Range(0, _updatedWeaponItems.Count)];

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

    /////////////////// Other Methods //////////////////////////
    
    public PlayerInput PlayerInput()
    {
        return player.playerInput;
    }

}
