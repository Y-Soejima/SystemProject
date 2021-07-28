using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class FloorCreation : MonoBehaviour
{
    int m_createCount;
    [SerializeField] float m_x;
    [SerializeField] float m_z;
    [SerializeField] Transform m_firstCreatePosition;
    [SerializeField]int m_createFloorNum = 5;
    [SerializeField] GameObject[] m_floorPrefab;
    [SerializeField] GameObject m_lastFloorPrefab;
    GameObject[] m_floors;
    Subject<Unit> m_createEvent = new Subject<Unit>();
    void Start()
    {
        m_floors = new GameObject[m_createFloorNum];
        m_createEvent.Subscribe( _ => FloorCreate());
        m_createEvent.Subscribe( _ => FloorDelete());
        StartCoroutine(FloorCor());
    }

    void FloorCreate()
    {
        if (m_createCount < m_createFloorNum - 1)
        {
            int floorNum = UnityEngine.Random.Range(0, m_floorPrefab.Length);
            var floor = Instantiate(m_floorPrefab[floorNum], new Vector3(m_firstCreatePosition.position.x + m_createCount * m_x, 0, m_firstCreatePosition.position.z + m_createCount * m_z), m_firstCreatePosition.rotation);
            m_floors[m_createCount] = floor;
            m_createCount++;
        }
        else
        {
            var floor = Instantiate(m_lastFloorPrefab, new Vector3(m_firstCreatePosition.position.x + m_createCount * m_x, 0, m_firstCreatePosition.position.z + m_createCount * m_z), m_firstCreatePosition.rotation);
            m_floors[m_createCount] = floor;
            m_createCount++;
        }
    }

    void FloorDelete()
    {
        if (m_createCount >= 4)
        {
            Destroy(m_floors[m_createCount - 4].gameObject);
            m_floors[m_createCount - 4] = null;
        }
    }

    IEnumerator FloorCor()
    {
        while(m_createCount < m_createFloorNum)
        {
            m_createEvent.OnNext(Unit.Default);
            yield return new WaitForSeconds(2);
        }
    }
}
