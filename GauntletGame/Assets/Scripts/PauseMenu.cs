using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool showing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMenu()
    {
        if (showing == false)
        {
            this.gameObject.SetActive(true);
            showing = true;
        }
        else
        {
            this.gameObject.SetActive(false);
            showing = false;
        }
    }

}
