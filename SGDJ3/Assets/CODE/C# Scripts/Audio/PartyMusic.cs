using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //AudioSystem.Instance.Play("Party Music");
    }

    public void Credits()
    {
        GameManager.Instance.GoToCredits();
    }
}
