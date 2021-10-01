using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsUi : MonoBehaviour
{
    private int friends;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private LevelManager levelManager;

    private void Awake()
    {
        friends = 0;
    }

    private void Start()
    {
        scoreText.text = friends + "/" + levelManager.LevelObjective;
    }

    public void AddFriend()
    {
        friends++;
        scoreText.text = friends + "/" + levelManager.LevelObjective;
    }
}
