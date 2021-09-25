using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int levelObjective;

    private int friendCount;

    private int health;

    private int maxHealth = 3;

    private bool hasPotion;

    [SerializeField]
    private GameObject levelComplete_ui;
    [SerializeField]
    private GameObject levelOver_ui;

    public int LevelObjective { get => levelObjective; }
    public int Health { get => health; }
    public bool HasPotion { get => hasPotion; set => hasPotion = value; }

    private void Awake()
    {
        health = maxHealth;
        hasPotion = false;
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
        if (health < 1)
        {
            LevelOver();
        }

    }

    private void LevelCompleted()
    {
        Debug.Log("WIN");
        levelComplete_ui.SetActive(true);
    }

    private void LevelOver()
    {
        Debug.Log("LevelOver");
        levelOver_ui.SetActive(true);

    }
}
