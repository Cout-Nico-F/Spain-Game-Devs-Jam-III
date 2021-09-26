using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyNpc : MonoBehaviour
{
    [SerializeField]
    private string npcId;


    private void Start()
    {
        if (HasInvitation())
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private bool HasInvitation()
    {
        foreach (var item in GameManager.Instance.PartyInvitations)
        {
            if (item.Equals(npcId))
            {
                return true;
            }
        }
        return false;
    }
}
