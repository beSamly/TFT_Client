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
            acceptMatchButton.SetActive(true);
            matchCancelButton.SetActive(false);
        }

        public void PendingMatchCanceled()
        {
            acceptMatchButton.GetComponent<Button>().interactable = true;
            acceptMatchButton.SetActive(false);
            matchCancelButton.SetActive(true);
        }

        public void MatchCancelRes()
        {
            matchCancelButton.GetComponent<Button>().interactable = true;
            matchCancelButton.SetActive(false);

            acceptMatchButton.SetActive(false);
            matchRequestButton.SetActive(true);
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
    }
}
