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
        _ballPrefab = await Tretimi.Assets.GetAsset<GameObject>($"GameBall{ballID}");
        _ballPrefab.GetComponent<Rigidbody2D>().gravityScale = weight;
    }
    public async Task SetMap(int mapID, int difficultyID)
    {
        if (_currentMap != null)
        {
            for (int i = 0; i < _currentMap._xSlots.Count; i++)
            {
                _currentMap._xSlots[i].OnBallFallToXSlot -= BallFallToXSlot;
            }
            Destroy(_currentMap.gameObject);
        }

        _mapPrefab = await Tretimi.Assets.GetAsset<GameObject>($"GameMap{mapID}_{difficultyID}");
        GameObject map = Instantiate(_mapPrefab, MapPoint);
        _currentMap = map.GetComponent<Map>();

        for (int i = 0; i < _currentMap._xSlots.Count; i++)
        {
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
}