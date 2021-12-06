using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public static User Instance { get; private set; }
    public int stone;

    private void Awake()
    {
        Instance = this;
    }
}
