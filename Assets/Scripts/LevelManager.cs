using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    [Header("/- Level Settings - /")] public List<BrickData> brickData = new List<BrickData>();
    public List<int> brickChange = new List<int>();
    public Tilemap tilemapLevelData;
    public Transform brickHolder;
    public Brick brickPrefab;
    public GameObject gridObject;
    [Header("/- Current Level Data -/")] public List<ItemData> currentItemDatas = new List<ItemData>();
    public List<Brick> currentBricks = new List<Brick>();
    [Header("/- Another Settings -/")] public int currentBallDamage;
    public int maxPlayerHealth;
    private int _currentPlayerHealth;
    public GameObject[] limits;
    public int currentPoint;


    private void Awake()
    {
        instance = this;
        foreach (GameObject limit in limits)
        {
            limit.SetActive(false);
        }

        
        _currentPlayerHealth = maxPlayerHealth;
        ReadLevelData();
    }

    private void Start()
    {
        currentBallDamage = Ball.instance.ballDamage;
    }

    #region Prepare Level

    public void ReadLevelData() // ToDo Read Level Data
    {
        currentItemDatas.Clear();
        int indexOnMap = 0;
        for (int y = tilemapLevelData.cellBounds.yMin; y < tilemapLevelData.cellBounds.yMax; y++)
        {
            for (int x = tilemapLevelData.cellBounds.xMin; x < tilemapLevelData.cellBounds.xMax; x++)
            {
                if (tilemapLevelData.HasTile(new Vector3Int(x, y, 0)))
                {
                    indexOnMap++;
                    ItemData itemData = new ItemData(indexOnMap, new Vector2Int(x, y));
                    currentItemDatas.Add(itemData);
                }
            }
        }

        gridObject.SetActive(false);
        CreateBrickData();
    }

    private void CreateBrickData() // ToDo Create Brick Data
    {
        for (int i = 0; i < currentItemDatas.Count; i++)
        {
            int changeA = UnityEngine.Random.Range(0, 100);
            if (changeA >= 0 && changeA <= brickChange[0])
            {
                currentItemDatas[i].brickData = brickData[0];
            }
            else if (changeA > brickChange[0] && changeA <= brickChange[1] + brickChange[0])
            {
                currentItemDatas[i].brickData = brickData[1];
            }
            else if (changeA > brickChange[1] + brickChange[0] &&
                     changeA <= brickChange[2] + brickChange[1] + brickChange[0])
            {
                currentItemDatas[i].brickData = brickData[2];
            }
            else
            {
                currentItemDatas[i].brickData = brickData[3];
            }
        }

        BuildLevel();
    }

    private void BuildLevel() // ToDo Build Level
    {
        currentBricks.Clear();
        for (int i = 0; i < currentItemDatas.Count; i++)
        {
            Brick brick = Instantiate(brickPrefab, brickHolder);
            brick.InitBrick(currentItemDatas[i].brickData, currentItemDatas[i].posTile);
            brick.name = "" + i;
            currentBricks.Add(brick);
        }
    }

    #endregion

    #region LevelControllers

    private bool ControlGameWin()
    {
        if (currentBricks.Count == 0)
            return true;
        return false;
    }

    public void ScoreAddition(int score)
    {
        Debug.Log("Score Addition  : " + score );
        UiManager.instance.UpdateAdditionalScoreText(score);
        currentPoint += score;
        UiManager.instance.UpdateAdditionalScoreText(score);
        UiManager.instance.UpdateScoreText(currentPoint);
    }

    public void BrickDestroyed()
    {
        if (ControlGameWin())
        {
            PlayerPrefs.SetInt("GameLevel", PlayerPrefs.GetInt("GameLevel") + 1);
            GamePlayManager.instance.NewLevel();
        }
    }

    public void BallDestroyed()
    {
        if (Ball.instance.hasStarted)
        {
            Ball.instance.hasStarted = false;
            _currentPlayerHealth--;
            if (_currentPlayerHealth <= 0)
            {
                GamePlayManager.instance.NewLevel();
            }
            else
            {
                UiManager.instance.UpdateHealthBars(_currentPlayerHealth);
            }
        }
        
    }

    #endregion
}