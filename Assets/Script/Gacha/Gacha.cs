using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;
using Random = UnityEngine.Random;

public class Gacha : MonoBehaviour
{
    [SerializeField] Button _button = default;
    [SerializeField] int _num = default;
    GachaDate _gachaDate = default;
    [SerializeField] int _requestStone;
    [SerializeField] int _max = default;
    [SerializeField] int _count;
    User _user;
    string _url = "https://ixj1pygau7.execute-api.ap-northeast-1.amazonaws.com/dev/updateitem?UserId=";
    // Start is called before the first frame update
    void Start()
    {
        _user = User.Instance;
        _gachaDate = GachaDate.Instance;
        _requestStone = 5 * _num;
        //_button.OnClickAsObservable().Where(_ => _user.stone >= _requestStone).Subscribe(_ => { Draw(_num);  _user.stone -= _requestStone; }).AddTo(_button);
        _button.OnClickAsObservable().Where(_ => _user.stone >= _requestStone).Subscribe(async _ => {
            Debug.Log(await PostApi(_url + _user.userId));
        }).AddTo(_button);
    }

    void Draw(int num)
    {
        for (int i = 0; i < num; i++)
        {
            _count++;
            if (_count == _max)
            {
                MaxLot();
                _count = 0;
                continue;
            }
            if (i == 9)
            {
                RareLot(Random.Range(0, 10000));
            }
            else
            {
                Lot(Random.Range(0, 10000));
            }
        }
    }

    void Lot(int rand)
    {
        float value = rand / 100;
        if (value < _gachaDate.SuperRareProbability)
        {
            var lotChara =  CharacterLot(_gachaDate._superRareCharacterlist, Random.Range(0, _gachaDate.SuperRareProbability));
            //Debug.Log(lotChara.Id);
            CharacterGet(lotChara);
        }
        else if (value < _gachaDate.RareProbability + _gachaDate.SuperRareProbability)
        {
            var lotChara = CharacterLot(_gachaDate._rareCharacterlist, Random.Range(0, _gachaDate.RareProbability));
            //Debug.Log(lotChara.Id);
            CharacterGet(lotChara);
        }
        else
        {
            var lotChara = CharacterLot(_gachaDate._normalCharacterlist, Random.Range(0, _gachaDate.NormalProbability));
            //Debug.Log(lotChara.Id);
            CharacterGet(lotChara);
        }
    }

    void RareLot(int rand)
    {
        float value = rand / 100;
        if (value < _gachaDate.SuperRareProbability)
        {
            var lotChara = CharacterLot(_gachaDate._superRareCharacterlist, Random.Range(0, _gachaDate.SuperRareProbability));
            //Debug.Log(lotChara.Id);
            CharacterGet(lotChara);
        }
        else
        {
            var lotChara = CharacterLot(_gachaDate._rareCharacterlist, Random.Range(0, _gachaDate.RareProbability));
            //Debug.Log(lotChara.Id);
            CharacterGet(lotChara);
        }
    }

    void MaxLot()
    {
        var lotChara = CharacterLot(_gachaDate._superRareCharacterlist, Random.Range(0, _gachaDate.SuperRareProbability));
        //Debug.Log(lotChara.Id);
        CharacterGet(lotChara);
    }

    CharactorBase CharacterLot(List<(CharactorBase, float)> characterList, float rand)
    {
        float value = 0;
        for (int i = 0; i < characterList.Count;i++)
        {
            value += characterList[i].Item2;
            if (rand < value || i == characterList.Count - 1)
            {
                return characterList[i].Item1;
            }
        }
        return null;
    }

    void CharacterGet(CharactorBase getCharacter)
    {
        for(int i = 0; i < _user._characterList.Count;i++)
        {
            if (getCharacter.Id == _user._characterList[i].Item1.Id)
            {
                _user._characterList[i] = (_user._characterList[i].Item1, _user._characterList[i].Item2 + 1);
            }
        }
    }

    async UniTask<string> PostApi(string url)
    {
        UnityWebRequest response = UnityWebRequest.Post(url,"");
        response.SetRequestHeader("Content-Type", "application/json");
        await response.SendWebRequest();
        return response.downloadHandler.text;
    }
}
