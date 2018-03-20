using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public string PlayerName;
	public Color Color;
	public Text label;

	public List<Unit> units;
	
	// Use this for initialization
	void Awake ()
	{		
		units  = new List<Unit>();
		transform.SetParent(GameObject.FindGameObjectWithTag("TurnManager").transform);
        PlayerName = gameObject.name;
		Color = Random.ColorHSV();
	}

	private void Start()
	{
		label.text = PlayerName;
	}
}
