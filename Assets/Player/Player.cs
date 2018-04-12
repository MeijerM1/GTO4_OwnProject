using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public string PlayerName;
	public Color Color;
	public TurnManager TurnManager;

	private Unit _selectedUnit = null;
	public ShipUI ShipUI;	
	public List<Unit> units;

	public GameObject ShipConfirmPanel;
	
	// Use this for initialization
	void Awake ()
	{		
		units  = new List<Unit>();
        PlayerName = gameObject.name;
		Color = Random.ColorHSV();
		ShipConfirmPanel.SetActive(false);
	}

	void Start()
	{
		transform.SetParent(TurnManager.transform);
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
		ShipUI.SetUnitDetails(unit);
		ShipUI.Show(true);

		if (TurnManager._turnCount == 1 || TurnManager._turnCount == 2) return;
		
		ShipConfirmPanel.SetActive(true);			
	}
}
