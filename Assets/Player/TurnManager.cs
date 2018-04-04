using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public int PlayerAmount;
    public GameObject PlayerPrefab;

    public Text playerLabel;
    
    List<Player> players = new List<Player>();
    int activePlayer = 0;

    private void Start()
    {
        for(int i = 0; i < PlayerAmount; i++)
        {
            GameObject player = Instantiate(PlayerPrefab);

            player.GetComponent<Player>().PlayerName = "Player " + (i + 1);
            player.name = "Player " + (i + 1);
            
            player.transform.SetParent(gameObject.transform);
            
            players.Add(player.GetComponent<Player>());
        }

        SetActivePlayer();        
    }


    public void EndTurn()
    {
        if (activePlayer == players.Count - 1)
        {
            activePlayer = 0;
        }
        else
        {
            activePlayer++;            
        }
        SetActivePlayer();
    }

    private void SetActivePlayer()
    {        
        for (int i = 0; i < players.Count; i++)
        {
            if (i == activePlayer)
            {
                players[i].gameObject.SetActive(true);
                playerLabel.text = players[i].PlayerName;
            }
            else
            {
                players[i].gameObject.SetActive(false);                
            }
        }  
    }
}
