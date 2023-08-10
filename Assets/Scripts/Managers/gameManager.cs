using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    #region parameters
    /// <summary>
    /// Desired duration for match.
    /// </summary>
    [SerializeField]
    private float _matchDuration = 60.0f;
    #endregion
    #region references
    /// <summary>
    /// Unique GameManager instance (Singleton Pattern).
    /// </summary>
    static private gameManager _instance;
    /// <summary>
    /// Public accesor for GameManager instance.
    /// </summary>
    static public gameManager Instance
    {
        //TODO
        get
        {
            return _instance;
        }
    }
    /// <summary>
    /// Reference to UI Manager.
    /// </summary>
    private UI_Manager _myUIManager;
    /// <summary>
    /// Reference to player.
    /// </summary>
    private GameObject _player;
    /// <summary>
    /// Reference to IA player.
    /// </summary>
    private GameObject _player2;
    /// <summary>
    /// Reference to Main Camera
    /// </summary>
    private GameObject _camera;
    #endregion
    #region properties
    /// <summary>
    /// List containing all live enemies.
    /// </summary>
    private List<EnemyController> _listOfEnemies;
    /// <summary>
    /// Remaining time to finish match.
    /// </summary>
    private float _timeLeft;
    /// <summary>
    /// Integer version of remaining time to finish match, dispayed on UI.
    /// </summary>
    private int _displayTimeLeft;
    #endregion
    #region methods
    /// <summary>
    /// Initializes GameManager instance and list of enemies.
    /// </summary>
    private void Awake()
    {
        _instance = this;
        _listOfEnemies = new List<EnemyController>();
    }
    /// <summary>
    /// Public method for enemies to register on Game Manager.
    /// </summary>
    /// <param name="enemyToAdd"></param>
    public void RegisterEnemy(EnemyController enemyToAdd)
    {
        _listOfEnemies.Add(enemyToAdd);
    }
    /// <summary>
    /// Public method to manage enemies death.
    /// </summary>
    /// <param name="deadEnemy"></param>
    public void OnEnemyDies(EnemyController deadEnemy)
    {
        _listOfEnemies.Remove(deadEnemy);
        deadEnemy.gameObject.SetActive(false);
        _myUIManager.UpdateEnemiesLeft(_listOfEnemies.Count);
    }
    /// <summary>
    /// Public method to manage player being hurted.
    /// </summary>
    /// <param name="lifePoints"></param>
    public void OnPlayerDamage(int lifePoints)
    {
        _myUIManager.UpdatePlayerLife(lifePoints);
        if (lifePoints <= 0)
        {
            OnPlayerDefeat();
        }
    }
    /// <summary>
    /// Called on player's victory.
    /// Sets UI Manager accordingly and deactivates player.
    /// </summary>
    private void OnPlayerVictory()
    {
        _myUIManager.SetPlayerVictory(true);
        _myUIManager.SetContinueButton(true);
        if (_player != null) Destroy(_player);
        else Destroy(_player2);
        this.enabled = false;
    }
    /// <summary>
    /// Called on player's defeat.
    /// Set UI Manager accordingly, deactivates enemies and player.
    /// </summary>
    public void OnPlayerDefeat()
    {
        _myUIManager.SetGameOver(true);
        _myUIManager.SetContinueButton(true);
        if (_player != null) Destroy(_player);
        else Destroy(_player2);
        this.enabled = false;
        _timeLeft = 0;
    }
    /// <summary>
    /// Initializes match.
    /// Activates player and enemies and performs initialization stuff.
    /// </summary>
    public void StartMatch()
    {
        //El único cambio realizado es que se destruye el player correspondiente y se le pasa a la cámara el player que tiene que seguir.
        _myUIManager.SetMainMenu(false);
        _player.SetActive(true);
        Destroy(_player2);
        _camera.GetComponent<CameraMovementController>().setPlayer(_player);
        this.enabled = true;
        _timeLeft = _matchDuration;
        for (int x=0; x < _listOfEnemies.Count; x++)
        {
            _listOfEnemies[x].StartEnemy();
        }
        _myUIManager.UpdateEnemiesLeft(_listOfEnemies.Count);
        _myUIManager.UpdatePlayerLife(3);
    }
    public void WatchMatch()
    {
        //El único cambio realizado es que se destruye el player correspondiente y se le pasa a la cámara el player que tiene que seguir.
        _myUIManager.SetMainMenu(false);
        _player2.SetActive(true);
        _camera.GetComponent<CameraMovementController>().setPlayer(_player2);
        Destroy(_player);
        this.enabled = true;
        _timeLeft = _matchDuration;
        for (int x = 0; x < _listOfEnemies.Count; x++)
        {
            _listOfEnemies[x].StartEnemy();
        }
        _myUIManager.UpdateEnemiesLeft(_listOfEnemies.Count);
        _myUIManager.UpdatePlayerLife(3);
    }
    /// <summary>
    /// Reloads scene after match.
    /// </summary>
    public void Continue()
    {
        SceneManager.LoadScene("Spherofobia");
    }
    /// <summary>
    /// Allows to quit game.
    /// </summary>
    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;            //Para salir del play del editor
        Application.Quit();             //Para salir del play de ejecutable
    }
    #endregion
    /// <summary>
    /// Finds UI Manager and Player.
    /// Deactivates player and GameManager.
    /// </summary>
    void Start()
    {   //Lo añadido es buscar al segundo jugador y a la cámara.
        _player = GameObject.FindWithTag("Player");
        _player2 = GameObject.FindWithTag("IAPLAYER");
        _player.SetActive(false);
        _player2.SetActive(false);
        _camera= GameObject.FindWithTag("MainCamera");
        _myUIManager = GameObject.FindWithTag("Canvas").GetComponent<UI_Manager>();
        this.enabled = false;        
        _timeLeft = _matchDuration;
        _listOfEnemies[0].gameObject.SetActive(true);
    }
    /// <summary>
    /// Checks victory and defeat conditions, calling required methods.
    /// Updates time on UI Manager.
    /// </summary>
    void Update()                   //Comprobar condiciones de victoria o derrota
    {
        if (_timeLeft <= 0.0f/*||vidas==0*/)
        {
            OnPlayerDefeat();
        }
        else if (_listOfEnemies.Count == 0)
        {
            OnPlayerVictory();
        }
        _displayTimeLeft = (int)_timeLeft;
        _myUIManager.UpdateTime(_displayTimeLeft);
        _timeLeft -= Time.deltaTime;
    }
}
