using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RarityList
{
    Normal,
    Rare,
    SuperRare,
}
public class CharactorBase : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] int _id;
    public int Id { get { return _id; } }
    public RarityList Rarity;
    GachaDate _gachaDate;
 
    private void Start()
    {
        _gachaDate = GachaDate.Instance;

        switch (Rarity)
        {
            case RarityList.Normal:
                _gachaDate._normalCharacterlist.Add((this, 0.1f));
                break;
            case RarityList.Rare:
                _gachaDate._rareCharacterlist.Add((this, 0.1f));
                break;
            case RarityList.SuperRare:
                _gachaDate._superRareCharacterlist.Add((this, 0.1f));
                break;

        }
    }
}
