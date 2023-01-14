using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{

    [SerializeField]
    GameObject list;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (list.gameObject.active)
        {
            list.SetActive(false);
        }
        else
        {
            list.SetActive(true);
        }
    }
}
