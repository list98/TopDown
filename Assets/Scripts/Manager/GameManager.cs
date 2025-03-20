using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player { get; private set; } 
    private ResourceController _PlayerResourceController;

    [SerializeField] private int currentWaveIndex = 0;

    private EnemyManager enemyManager;

    private UIManager uiManager;
    public static bool isFirstLoading = true;

    private void Awake()
    {
        instance = this;

        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        uiManager = FindObjectOfType<UIManager>();

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        _PlayerResourceController = player.GetComponent<ResourceController>();
        _PlayerResourceController.RemoveHealthChangeEvent(uiManager.ChangePlayerHP);
        _PlayerResourceController.AddHealthChangeEvent(uiManager.ChangePlayerHP);
    }

    private void Start()
    {
        if (!isFirstLoading)
        {
            StartGame();

        }
        else
        {
            isFirstLoading = false;
        }
    }
    public void StartGame()
    {
        uiManager.SetPlayGame();
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
        uiManager.ChangeWave(currentWaveIndex);
    }

    public void EndOfWave()
    {
        StartNextWave();
    }
    public void GameOver()
    {
        enemyManager.StopWave();
        uiManager.SetGameOver();
    }
    
}
