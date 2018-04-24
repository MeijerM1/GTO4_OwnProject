using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class TurnManager : MonoBehaviour
{
    public int PlayerAmount;
    public GameObject PlayerPrefab;

    public GameObject turnPanel;
    public Text turnText;
    
    [Range(1, 10)]
    public float TurnFadeTimer;

    public Text playerLabel;
    public Text instructionLabel;
    
    List<Player> players = new List<Player>();
    int activePlayer = 0;       

    public int _turnCount = 1;

    public AudioManager AudioManager;

    private void Start()
    {
        turnPanel.SetActive(false);
        
        for(int i = 0; i < PlayerAmount; i++)
        {
            GameObject player = Instantiate(PlayerPrefab);

            player.GetComponent<Player>().PlayerName = "Player " + (i + 1);
            player.name = "Player " + (i + 1);
            
            player.transform.SetParent(gameObject.transform);
            player.GetComponent<Player>().TurnManager = this;

            player.GetComponent<Player>().AudioManager = AudioManager;
            
            players.Add(player.GetComponent<Player>());
        }

        SetActivePlayer();
    }


    public void EndTurn()
    {
        _turnCount++;
        
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

                if (_turnCount > 2)
                {
                    players[i].GetComponent<Player>().ShipBuyPanel.SetActive(false);
                }
            }
            else
            {
                players[i].gameObject.SetActive(false);                
            }
        }  
        
        ShowTurnMessage();
        SetIntructionText();
    }

    public Player GetOtherPlayer()
    {
        if (activePlayer == 0)
        {
            return players[1];
        }
        else
        {
            return players[0];
        }        
    }

    private void SetIntructionText()
    {
        if (_turnCount == 1 || _turnCount == 2)
        {
            instructionLabel.text = "Place your ships";
        }
        else
        {
            instructionLabel.text = "Select the ship you want to use";            
        }
    }
    
    public void ShowTurnMessage()
    {
        if (_turnCount == 1 || _turnCount == 2)
        {
            StartCoroutine(ShowTurnStartMessage("Preparation phase"));            
        }
        else if(_turnCount % 2 == 0)
        {
            StartCoroutine(ShowTurnStartMessage("Turn " + _turnCount / 2));                        
        }
        else
        {
            StartCoroutine(ShowTurnStartMessage("Turn " + (_turnCount + 1) / 2));                                    
        }
    }

    IEnumerator ShowTurnStartMessage(String message)
    {
        turnText.text = message;
        turnPanel.SetActive(true);
        
        turnPanel.GetComponent<Image>().CrossFadeAlpha(1, 0, false);
        StartCoroutine(FadeTextToFullAlpha(0, turnText));
        
        yield return new WaitForSeconds(1);

        turnPanel.GetComponent<Image>().CrossFadeAlpha(0, TurnFadeTimer, false);
        StartCoroutine(FadeTextToZeroAlpha(TurnFadeTimer, turnText));
    }
    
    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
 
    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
