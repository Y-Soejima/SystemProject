using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeLine : MonoBehaviour
{
    [SerializeField] TimelineAsset[] m_timeLines;
    PlayableDirector m_director;
    int nextNum = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        m_director = this.GetComponent<PlayableDirector>();
        m_director.stopped += NextPlay;
    }
    void Start()
    {
        m_director.Play(m_timeLines[nextNum]);
    }

    void NextPlay(PlayableDirector abj)
    {
        nextNum++;
        if (nextNum != m_timeLines.Length)
        {
            m_director.Play(m_timeLines[nextNum]);
        }
    }
}
