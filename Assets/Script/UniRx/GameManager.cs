using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    [SerializeField] UniRxTestObserver unirxTest = default;
    bool muteki = false;
    // Start is called before the first frame update
    void Start()
    {
        unirxTest.Observable1
            .Where(_ => Input.GetKeyDown(KeyCode.Space))
            .Subscribe(_ => InGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InGame()
    {
        Debug.Log("Start");
    }

}
