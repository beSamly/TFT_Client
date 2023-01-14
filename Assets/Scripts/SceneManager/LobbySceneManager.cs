using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SceneManager
{
    public class LobbySceneManager : MonoBehaviour
    {
        private ButtonHandler buttonHandler;

        [SerializeField]
        private GameObject acceptMatchButton;

        [SerializeField]
        private GameObject matchRequestButton;

        [SerializeField]
        private GameObject matchCancelButton;

        private GameSystem gameSystem;

        private void Awake()
        {
            acceptMatchButton.SetActive(false);
            matchCancelButton.SetActive(false);
        }

        private void LoadGameSystem()
        {
            GameSystem[] gameSystemList = GameObject.FindObjectsOfType<GameSystem>();
            gameSystem = gameSystemList[0];
        }
        /* Network Request */
        public void OnClickMatchRequest()
        {
            if (!gameSystem)
            {
                LoadGameSystem();
            }
            matchRequestButton.GetComponent<Button>().interactable = false;
            gameSystem.networkManager.SendMatchRequest();
        }

        public void OnClickMatchAccept()
        {
            acceptMatchButton.GetComponent<Button>().interactable = false;
            gameSystem.networkManager.SendMatchAcceptRequest();
        }

        public void OnClickMatchCancel()
        {
            matchCancelButton.GetComponent<Button>().interactable = false;
            gameSystem.networkManager.SendMatchCancelRequest();
        }

        /* Network Response */
        public void PendingMatchCreated()
        {
            Debug.Log("Pending Match Created");
            matchRequestButton.SetActive(false);
            acceptMatchButton.SetActive(true);
            matchCancelButton.SetActive(false);
        }

        public void PendingMatchCanceled()
        {
            Debug.Log("Pending Match Canceled");

            SetAllButtonInteractable();

            matchRequestButton.SetActive(false);
            acceptMatchButton.SetActive(false);
            matchCancelButton.SetActive(true);
        }

        public void MatchCancelRes()
        {
            Debug.Log("Match Cancel Res");

            SetAllButtonInteractable();

            matchRequestButton.SetActive(true);
            matchCancelButton.SetActive(false);
            acceptMatchButton.SetActive(false);
        }

        public void MatchRequestResponse(bool result)
        {
            if (result)
            {
                matchCancelButton.SetActive(true);
                matchRequestButton.SetActive(false);
            } else
            {

            }
        }

        private void SetAllButtonInteractable()
        {
            acceptMatchButton.GetComponent<Button>().interactable = true;
            matchCancelButton.GetComponent<Button>().interactable = true;
            matchRequestButton.GetComponent<Button>().interactable = true;
        }
    }
}
