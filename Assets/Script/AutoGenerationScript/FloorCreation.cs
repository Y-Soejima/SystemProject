using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCreation : MonoBehaviour
{
    [SerializeField] int min_x;
    [SerializeField] int min_y;
    [SerializeField] int max_x;
    [SerializeField] int max_y;
    [SerializeField] int floorNum;
    [SerializeField] GameObject floorPrefab;
    void Start()
    {
        floorCreate();
    }

    void floorCreate()
    {

    }
}
