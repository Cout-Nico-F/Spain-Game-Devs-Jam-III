using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private int levelObjective;

    private int friendCount;

    private int health;
    private int maxHealth = 6;

    public int LevelObjective { get => levelObjective; }


    private void Awake()
    {
        health = maxHealth;
    }

    //Este metodo lo llamamos al detectar colision pocion-npc y comprobar que son del mismo color.
    public void FriendJoined()
    {
        friendCount++;

        if (friendCount >= LevelObjective)
        {
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        //do
    }
}
