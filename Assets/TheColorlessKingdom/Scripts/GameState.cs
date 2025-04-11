using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState
{
    private static HashSet<string> gameState = new HashSet<string>();

    public static void AddState(string state)
    {
        gameState.Add(state);
    }

    public static bool checkState(string state)
    {
        if (gameState.Contains(state))
        {
            Debug.Log("L'état " + state + " est présent.");
            return true;
        }
        else
        {
            Debug.Log("L'état " + state + " n'est pas présent.");
            return false;
        }
    }
}
