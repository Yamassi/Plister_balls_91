using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public Transform BallPoint, MapPoint;
    public Action<float> OnBallFall;
    private GameObject _ballPrefab, _mapPrefab;
    private Ball _currentBall;
    private Map _currentMap;
    public async Task SetBall(int ballID, int weight)
    {
        float convertedWeight;
        switch (weight)
        {
            case 10:
                convertedWeight = 0.3f;
                break;
            case 50:
                convertedWeight = 0.6f;
                break;
            case 100:
                convertedWeight = 1f;
                break;
            default:
                convertedWeight = 1.5f;
                break;
        }


        _ballPrefab = await Tretimi.Assets.GetAsset<GameObject>($"GameBall{ballID}");
        _ballPrefab.GetComponent<Rigidbody2D>().gravityScale = convertedWeight;
    }
    public async Task SetMap(int mapID, int difficultyID)
    {
        _mapPrefab = await Tretimi.Assets.GetAsset<GameObject>($"GameMap{mapID}_{difficultyID}");
        GameObject map = Instantiate(_mapPrefab, MapPoint);
        _currentMap = map.GetComponent<Map>();

        for (int i = 0; i < _currentMap._xSlots.Count; i++)
        {
            Debug.Log($"Подписка на слот X {i}");
            _currentMap._xSlots[i].OnBallFallToXSlot += BallFallToXSlot;
        }
    }

    private void BallFallToXSlot(float coefficient)
    {
        OnBallFall?.Invoke(coefficient);
    }

    public void CreateBall()
    {
        GameObject ball = Instantiate(_ballPrefab, BallPoint);
        _currentBall = ball.GetComponent<Ball>();
    }
    public void LaunchBall()
    {
        _currentBall.Launch();
        CreateBall();
    }
    public void ClearGamePlay()
    {
        var balls = GetComponentsInChildren<Ball>();

        if (balls != null)
            for (int i = 0; i < balls.Length; i++)
            {
                Destroy(balls[i].gameObject);
            }

        if (_currentMap != null)
        {
            for (int i = 0; i < _currentMap._xSlots.Count; i++)
            {
                Debug.Log($"Отписка от слота X {i}");
                _currentMap._xSlots[i].OnBallFallToXSlot -= BallFallToXSlot;
            }
            Destroy(_currentMap.gameObject);
        }
    }
}