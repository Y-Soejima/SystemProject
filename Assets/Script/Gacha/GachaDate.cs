using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GachaDate : MonoBehaviour
{
    public static GachaDate Instance { get; private set; }
    //タプル(１つの変数に複数の値を格納できる)
    public List<(CharactorBase, float)> _normalCharacterlist = new List<(CharactorBase, float)>();
    public List<(CharactorBase, float)> _rareCharacterlist = new List<(CharactorBase, float)>();
    public List<(CharactorBase, float)> _superRareCharacterlist = new List<(CharactorBase, float)>();
    public float[] list;
    public float NormalProbability;
    public float RareProbability;
    public float SuperRareProbability;
    [SerializeField] List<CharactorBase> _superRarePiclist = new List<CharactorBase>();
    [SerializeField] List<float> _superRarePicWeight = new List<float>();
    [SerializeField] List<CharactorBase> _rarePiclist = new List<CharactorBase>();
    [SerializeField] List<float> _rarePicWeight = new List<float>();
    public float superRareNotPicWeight = 1;
    public float rareNotPicWeight = 1;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //StartCoroutine(LotSet());
    }

    public IEnumerator LotSet()
    {
        yield return new WaitForSeconds(0.5f);

        Set();
    }

    public void Set()
    {
        list = new float[_rareCharacterlist.Count];
       
        for (int i = 0; i < _superRarePicWeight.Count; i++)
        {
            superRareNotPicWeight -= _superRarePicWeight[i];
        }
        for (int i = 0; i < _rarePicWeight.Count; i++)
        {
            rareNotPicWeight -= _rarePicWeight[i];
        }
        for (int i = 0; i < 3; i++)
        {
            List<(CharactorBase, float)> characterList = new List<(CharactorBase, float)>();
            switch (i)
            {
                case 0:
                    characterList = _normalCharacterlist;
                    break;
                case 1:
                    characterList = _rareCharacterlist;
                    break;
                case 2:
                    characterList = _superRareCharacterlist;
                    break;
            }
            for (int k = 0; k < characterList.Count; k++)
            {
                switch (i)
                {
                    case 0:
                        var normalCharacter = _normalCharacterlist[k].Item1;
                        _normalCharacterlist[k] = (normalCharacter, NormalProbability / _normalCharacterlist.Count);
                        break;
                    case 1:
                        var rareCharacter = _rareCharacterlist[k].Item1;
                        for (int m = 0; m < _rarePiclist.Count; m++)
                        {
                            if (rareCharacter.Id == _rarePiclist[m].Id)
                            {
                                _rareCharacterlist[k] = (rareCharacter, RareProbability * _rarePicWeight[m]);
                                break;
                            }
                            else if (m == _rarePiclist.Count - 1)
                            {
                                _rareCharacterlist[k] = (rareCharacter, (RareProbability * rareNotPicWeight) / (_rareCharacterlist.Count - _rarePiclist.Count));
                            }
                        }
                        list[k] = _rareCharacterlist[k].Item2;
                        break;
                    case 2:
                        var superRareCharacter = _superRareCharacterlist[k].Item1;
                        for (int m = 0; m < _superRarePiclist.Count; m++)
                        {
                            if (superRareCharacter.Id == _superRarePiclist[m].Id)
                            {
                                _superRareCharacterlist[k] = (superRareCharacter, SuperRareProbability * _superRarePicWeight[m]);
                                break;
                            }
                            else if (m == _superRarePiclist.Count - 1)
                            {
                                _superRareCharacterlist[k] = (superRareCharacter, (SuperRareProbability * superRareNotPicWeight) / (_superRareCharacterlist.Count - _superRarePiclist.Count));
                            }
                            
                        }
                        
                        break;
                }
            }
        }
    }
}
