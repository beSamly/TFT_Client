using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using UnityEngine.UI;
using Assets;
using Assets.SceneManager;

public class LobbySceneButtonHandler : MonoBehaviour
{
    private LobbySceneManager manager;

    private void Awake()
    {
        LobbySceneManager[] managerList = GameObject.FindObjectsOfType<LobbySceneManager>();
        manager = managerList[0];
    }

    public void OnClickMatchRequest()
    {
        manager.OnClickMatchRequest();
    }

    public void OnClickMatchAccept()
    {
        manager.OnClickMatchAccept();
    }

    public void OnClickMatchCancel()
    {
        manager.OnClickMatchCancel();
    }
}
