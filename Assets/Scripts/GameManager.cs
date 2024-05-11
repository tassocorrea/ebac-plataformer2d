using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        Instance = this;

        else
            Destroy(gameObject);

    }

    [Header("Player")]
    public GameObject player;

    [Header("Enemies")]
    public List<GameObject> enemies;


}
