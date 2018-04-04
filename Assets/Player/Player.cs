using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public string PlayerName;
	public Color Color;
	public Text label;

	private Unit _selectedUnit = null;

	public List<Unit> units;
	
	// Use this for initialization
	void Awake ()
	{		
		units  = new List<Unit>();
		transform.SetParent(GameObject.FindGameObjectWithTag("TurnManager").transform);
        PlayerName = gameObject.name;
		Color = Random.ColorHSV();
	}

	public void Start()
	{
		label.text = PlayerName;
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (!Physics.Raycast(ray, out hit)) return;

			Unit unit = hit.transform.GetComponent<Unit>();

			if (unit != null)
			{
				SelectUnit(unit);
			}
		}
	}

	private void SelectUnit(Unit unit)
	{	
		if (unit.player != this) return;

		if (_selectedUnit != null)
		{
			_selectedUnit.Deselect();			
		}

		unit.Select();

		_selectedUnit = unit;
	}
}
