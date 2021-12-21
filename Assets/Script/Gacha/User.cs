using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class User : MonoBehaviour
{
    public static User Instance { get; private set; }
    public int stone;
    GachaDate _gachaDate;
    public List<(CharacterBase, int)> _characterList = new List<(CharacterBase, int)>();
    [SerializeField] List<CharacterBase> _characterBases =  new List<CharacterBase> ();
    [SerializeField] List<int> _charaCount = new List<int>();

    private void Awake()
    {
        Instance = this;
        _gachaDate = GachaDate.Instance;
    }

    async void Start()
    {
        await StartCoroutine(_gachaDate.LotSet());

        ListAdd();
    }

    private void Update()
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            _characterBases[i] = _characterList[i].Item1;
            _charaCount[i] = _characterList[i].Item2;
        }
    }
    public void ListAdd()
    {
        for (int i = 0; i < _gachaDate._normalCharacterlist.Count; i++)
        {
            _characterList.Add((_gachaDate._normalCharacterlist[i].Item1, 0));
        }
        for (int i = 0; i < _gachaDate._rareCharacterlist.Count; i++)
        {
            _characterList.Add((_gachaDate._rareCharacterlist[i].Item1, 0));
        }
        for (int i = 0; i < _gachaDate._superRareCharacterlist.Count; i++)
        {
            _characterList.Add((_gachaDate._superRareCharacterlist[i].Item1, 0));
        }
        for (int i = 0; i < _characterList.Count; i++)
        {
            _characterBases.Add(_characterList[i].Item1);
            _charaCount.Add(_characterList[i].Item2);
        }
    }
}
