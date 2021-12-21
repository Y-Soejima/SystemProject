using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GachaDate : MonoBehaviour
{
    public static GachaDate Instance { get; private set; }
    //タプル(１つの変数に複数の値を格納できる)
    public List<(CharacterBase, float)> _normalCharacterlist = new List<(CharacterBase, float)>();
    public List<(CharacterBase, float)> _rareCharacterlist = new List<(CharacterBase, float)>();
    public List<(CharacterBase, float)> _superRareCharacterlist = new List<(CharacterBase, float)>();
    public float NormalProbability;
    public float RareProbability;
    public float SuperRareProbability;
    [SerializeField] List<CharacterBase> _superRarePiclist = new List<CharacterBase>();
    [SerializeField] List<float> _superRarePicWeight = new List<float>();
    [SerializeField] List<CharacterBase> _rarePiclist = new List<CharacterBase>();
    [SerializeField] List<float> _rarePicWeight = new List<float>();


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
        yield return new WaitForSeconds(0.1f);

        Set();
    }

    public void Set()
    {
        float superRareNotPicWeight = 0;
        float rareNotPicWeight = 0;
        for (int i = 0; i < _superRarePicWeight.Count; i++)
        {
            superRareNotPicWeight += _superRarePicWeight[i];
        }
        for (int i = 0; i < _rarePicWeight.Count; i++)
        {
            rareNotPicWeight += _rarePicWeight[i];
        }
        for (int i = 0; i < 3; i++)
        {
            List<(CharacterBase, float)> characterList = new List<(CharacterBase, float)>();
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
                            }
                            else if (m == _rarePiclist.Count - 1)
                            {
                                _rareCharacterlist[k] = (rareCharacter, (RareProbability * rareNotPicWeight) / (_rareCharacterlist.Count - _rarePiclist.Count));
                            }
                        }
                        break;
                    case 2:
                        var superRareCharacter = _superRareCharacterlist[k].Item1;
                        for (int m = 0; m < _superRarePiclist.Count; m++)
                        {
                            if (superRareCharacter.Id == _superRarePiclist[m].Id)
                            {
                                _superRareCharacterlist[k] = (superRareCharacter, SuperRareProbability * _superRarePicWeight[m]);
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
