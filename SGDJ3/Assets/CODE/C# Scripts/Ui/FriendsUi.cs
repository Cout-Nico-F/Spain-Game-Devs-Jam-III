using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendsUi : MonoBehaviour
{
    private int friends;

    [SerializeField]
    private Text text1;
    [SerializeField]
    private Text text2;
    [SerializeField]
    private LevelManager levelManager;

    private void Awake()
    {
        friends = 0;
    }

    private void Start()
    {
        text1.text = friends.ToString();
        text2.text = levelManager.LevelObjective.ToString();
    }

    public void AddFriend()
    {
        friends++;
        text1.text = friends.ToString();
    }
}
