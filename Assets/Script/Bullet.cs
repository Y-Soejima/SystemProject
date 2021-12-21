using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _posY;
    [SerializeField] float _time;


    private void Start()
    {
        StartCoroutine(Shot());
    }

    void Trajectory(GameObject target, float posY, float time)
    {
        var bullet = Instantiate(_bullet, this.gameObject.transform);
        bullet.AddComponent<Rigidbody>();
        Vector3 dir = target.transform.position - this.gameObject.transform.position;
        float distance = Vector3.Distance(target.transform.position, this.gameObject.transform.position);
        float rad = Mathf.Atan2(dir.z, dir.x);
        float Vy = ((9.8f * (time / 2)) / 2) + (posY / (time / 2));
        float cos = distance / time;
        float sin = 1 - (cos * cos);
        bullet.GetComponent<Rigidbody>().AddForce(Mathf.Cos(rad) * cos, Vy * sin, Mathf.Sin(rad) * cos, ForceMode.Impulse);
    }

    IEnumerator Shot()
    {
        while(true)
        {
            Trajectory(_target, _posY, _time);
            yield return new WaitForSeconds(1f);
        }
    }
}
