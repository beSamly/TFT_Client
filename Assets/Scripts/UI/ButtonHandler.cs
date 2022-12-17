using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Network;
using UnityEngine.UI;
using Assets;

public class ButtonHandler : MonoBehaviour
{
    private GameSystem gameSystem;

    [SerializeField]
    public Text loginIdInputField;

    private void Awake()
    {
        GameSystem[] gameSystemList = GameObject.FindObjectsOfType<GameSystem>();
        gameSystem = gameSystemList[0];
    }

    public void OnClickEnterButton()
    {
        gameSystem.networkManager.SendLoginRequest(loginIdInputField.text);
    }

    public void OnClickMatchRequest()
    {
        gameSystem.networkManager.SendMatchRequest();
    }
}
