using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int levelObjective;

    private int friendCount;

    private int health;

    private int maxHealth = 6;

    public int LevelObjective { get => levelObjective; }
    public int Health { get => health; }

    private void Awake()
    {
        health = maxHealth;
        levelObjective = 1;
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

    public void Damaged()
    {
        //TODO:desde aca llamar a la animacion de explosion de la olla
        health--;
        //y cambiar sprite de bruja durante unos segundos.
        //UiManager.RefreshUI();

    }

    private void LevelCompleted()
    {
        //do
        Debug.Log("WIN");
    }
}
