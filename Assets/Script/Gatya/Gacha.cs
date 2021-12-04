using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Gacha : MonoBehaviour
{
    [SerializeField] Button _button = default;
    [SerializeField] int _num = default;
    [SerializeField] GachaDate _gachaDate = default;
    // Start is called before the first frame update
    void Start()
    {
        _button.OnClickAsObservable().Subscribe(_ => Draw(_num)).AddTo(_button);
    }

    void Draw(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Lot(UnityEngine.Random.Range(0, 10000));
        }
    }

    void Lot(int rand)
    {
        float value = rand / 100;
        if (value < _gachaDate._superRareProbability)
        {
            Debug.Log("SR");
        }
        else if (value < _gachaDate._rareProbability + _gachaDate._superRareProbability)
        {
            Debug.Log("R");
        }
        else
        {
            Debug.Log("N");
        }
    }
}
