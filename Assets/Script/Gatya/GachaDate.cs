using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaDate : MonoBehaviour
{
    //タプル(１つの変数に複数の値を格納できる)
    public List<(CharacterBase, float)> _normalCharacterlist = new List<(CharacterBase, float)>();
    public List<(CharacterBase, float)> _rareCharacterlist = new List<(CharacterBase, float)>();
    public List<(CharacterBase, float)> _superRareCharacterlist = new List<(CharacterBase, float)>();
    public float NormalProbability;
    public float _rareProbability;
    public float _superRareProbability;

}
