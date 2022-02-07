using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public class AccountCreate : MonoBehaviour
{
    [SerializeField] Button button;
    string url = "https://o1ye3iql8e.execute-api.ap-northeast-1.amazonaws.com/dev/PutItem?UserId=";
    User user;
    // Start is called before the first frame update
    void Start()
    {
        user = User.Instance;
        button.OnClickAsObservable().Subscribe(async _ => await Create(url + user.userId)).AddTo(this);
    }
    async UniTask Create(string url)
    {
        UnityWebRequest response = UnityWebRequest.Post(url, "");
        response.SetRequestHeader("Content-Type", "application/json");
        await response.SendWebRequest();
        //return response.downloadHandler.text;
    }
}
