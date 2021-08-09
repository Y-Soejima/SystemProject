using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameStateTest : MonoBehaviour
{
    enum GameState
    {
        StartGame,
        InGame,
        EndGame
    }
    GameState m_currentState;
    Subject<GameState> m_gameEvent = new Subject<GameState>();

    void Start()
    {
        m_currentState = GameState.StartGame;
        m_gameEvent.Where(m_currentState => m_currentState == GameState.StartGame).Distinct().Subscribe(_ => StartCoroutine(GameStart()));
        m_gameEvent.Where(m_currentState => m_currentState == GameState.InGame).Distinct().Subscribe( _ => StartCoroutine(InGame()));
        m_gameEvent.Where(_ => Input.GetKeyDown(KeyCode.A)).Where(m_currentState => m_currentState == GameState.InGame).Subscribe( _ => Debug.Log("A"));
        //m_gameEvent.Where(m_currentState => m_currentState == GameState.InGame).Distinct().Subscribe( _ => StartCoroutine(InGame()));
        m_gameEvent.Where(m_currentState => m_currentState == GameState.EndGame).Distinct().Subscribe(_ => StartCoroutine(EndGame()));
    }

    void Update()
    {
        m_gameEvent.OnNext(m_currentState);
    }

    IEnumerator GameStart()
    {
        Debug.Log("GameStart");
        yield return new WaitForSeconds(2);
        m_currentState = GameState.InGame;
    }

    IEnumerator InGame()
    {
        int count = 0;
        while(count < 10)
        {
            Debug.Log("InGame");
            count++;
            yield return new WaitForSeconds(1);
        }
        m_currentState = GameState.EndGame;
    }

    IEnumerator EndGame()
    {
        Debug.Log("GameEnd");
        yield return new WaitForEndOfFrame();
    }
}
