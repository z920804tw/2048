using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public enum GameState
{
    None,
    GeneraGrid,
    Wait,
    Move,
    End,
}
public class GameManager : MonoBehaviour
{
    public GameState state;
    bool isChange;
    // Start is called before the first frame update


    void Start()
    {
        state = GameState.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Wait)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {

            }
            else if (Input.GetKeyDown(KeyCode.S))
            {

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {

            }
            else if (Input.GetKeyDown(KeyCode.D))
            {

            }
        }
    }

    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }


}
