using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSystem.Instance.Play("Party");
    }

    public void Credits()
    {
        AudioSystem.Instance.Stop("Party");
        AudioSystem.Instance.Play("Main Menu");
        GameManager.Instance.GoToCredits();
    }
}
