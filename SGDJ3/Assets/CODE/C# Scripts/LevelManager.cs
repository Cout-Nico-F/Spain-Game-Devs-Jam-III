using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int levelObjective;

    private int friendCount;

    public int LevelObjective { get => levelObjective; }

    public void FriendJoined()
    {
        friendCount++;
    }


    private bool CheckCompletionStatus()
    {
        if (friendCount >= LevelObjective)
        {
            return true;
        }else
            return false;
    }

}
