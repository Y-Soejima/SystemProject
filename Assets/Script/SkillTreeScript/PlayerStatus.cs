using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int p_hp = 100;
    [SerializeField] private int p_mp = 100;
    [SerializeField] private int p_atk = 20;
    [SerializeField] private int p_matk = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HpChange(int value)
    {
        p_hp += value;
    }

    public void MpChange(int value)
    {
        p_mp += value;
    }

    public void AtkChange(int value)
    {
        p_atk += value;
    }

    public void MatkChange(int value)
    {
        p_matk += value;
    }
}
