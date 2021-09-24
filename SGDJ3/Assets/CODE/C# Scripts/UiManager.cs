using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;
    public void Pause()
    {
        pausePanel.SetActive(true);
    }
    public void UnPause()
    {
        pausePanel.SetActive(false);
    }
}
