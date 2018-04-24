using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public string PlayerName;
	public Color Color;
	public TurnManager TurnManager;
	public Map Map;	
	
	private Unit _selectedUnit = null;
	public List<Unit> Units;

	private Unit _shipForTurn = null;

	public GameObject ShipConfirmPanel;
	public GameObject ShipActionPanel;
	public GameObject ShipBuyPanel;
	public ShipUI ShipUi;	
	
	public bool IsRotating = false;
	public bool IsFiring = false;

	public AudioManager AudioManager;
	
	void Awake ()
	{				
		Units  = new List<Unit>();
        PlayerName = gameObject.name;
		Color = Random.ColorHSV();
		ShipConfirmPanel.SetActive(false);
		ShipActionPanel.SetActive(false);
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

			if (Physics.Raycast(ray, out hit))
			{
				if (IsRotating)
				{
					RotateShip(hit);
				}
				else if (IsFiring)
				{
					Fire(hit);
				}
				else
				{
					Unit unit = hit.transform.GetComponent<Unit>();

					if (_shipForTurn != null) return;

					if (unit != null)
					{
						SelectUnit(unit);
					}
					else
					{
						if (_selectedUnit != null)
						{
							_selectedUnit.Deselect();
						}
					}
				}
			}		
		}
	}

	public void EnableFiring()
	{
		ShipActionPanel.SetActive(false);
		TurnManager.instructionLabel.text = "Select an enemy grid cell";
		IsFiring = true;
	}

	private void Fire(RaycastHit hit)
	{
		if (_shipForTurn.RemainingFirepower <= 0)
		{
			Debug.LogWarning("No more shots remaining");
			return;
		}
		
		Cell cell = hit.transform.GetComponent<Cell>();
		
		if(cell== null )
		{
			Debug.LogWarning("No cell selected");
			return;
		}

		if (cell.Map == Map)
		{
			Debug.LogWarning("Select an enemy grid cell");
			return;
		}

		Cell enemyCell = TurnManager.GetOtherPlayer().Map.GetCell(cell.XPos, cell.YPos);

		if (enemyCell.Unit == null)
		{
			Debug.Log("Shot was a miss");
		}
		else
		{
			AudioManager.PlaySound("TargetHit");
			Debug.Log("Shot was a hit");
			enemyCell.Unit.CurrentHitpoints--;
			CheckShipDestroyed(enemyCell.Unit);
		}

		_shipForTurn.RemainingFirepower--;

		if (_shipForTurn.RemainingFirepower == 0)
		{
			TurnManager.instructionLabel.text = "No more shots remaining";			
		}
		
		ShipUi.SetUnitDetails(_shipForTurn);
	}

	private void CheckShipDestroyed(Unit unit)
	{
		if (unit.CurrentHitpoints <= 0)
		{
			Debug.Log("Ship has been destroyed");

			foreach (var cell in unit.Locations)
			{
				cell.Unit = null;
			}
			
			TurnManager.GetOtherPlayer().Units.Remove(unit);
			Destroy(unit.gameObject);
			
			//AudioManager.PlaySound("TargetEliminated");
			
			CheckWin();
		}
	}

	private void CheckWin()
	{
		if(TurnManager.GetOtherPlayer().Units.Count <= 0)
		{
			TurnManager.instructionLabel.text = "YOU HAVE WON";
		}
	}

	private void RotateShip(RaycastHit hit)
	{
		Cell cell = hit.transform.GetComponent<Cell>();

		if (cell != null)
		{
			if (cell.IsRotateTarget)
			{
				AudioManager.PlaySound("Trans");
				
				_shipForTurn.transform.SetParent(hit.transform.gameObject.transform, false);
				_shipForTurn.Turn();

				if (_shipForTurn.Orientation == 0)
				{
					hit.transform.localEulerAngles = new Vector3(0,0,0);
				}
				else
				{
					hit.transform.localEulerAngles = new Vector3(0,-90,0);
				}

				_shipForTurn.Locations = Map.GetCellsInRow(cell, _shipForTurn.Length, _shipForTurn.Orientation);
							
				Map.ResetMRotationHighlight();

				IsRotating = false;

				_shipForTurn = null;
			}
		}
	}

	private void SelectUnit(Unit unit)
	{	
		AudioManager.PlaySound("StandingBy");
		
		if (unit.player != this) return;

		if (_selectedUnit != null)
		{
			_selectedUnit.Deselect();			
		}

		unit.Select();

		_selectedUnit = unit;
		ShipUi.SetUnitDetails(unit);
		ShipUi.Show(true);

		if (TurnManager._turnCount == 1 || TurnManager._turnCount == 2) return;

		if (_shipForTurn != null) return;
		
		ShipConfirmPanel.SetActive(true);			
	}

	public void SelectUnitForTurn()
	{
		if (_selectedUnit == null) return;
		
		AudioManager.PlaySound("Aye");

		TurnManager.instructionLabel.text = "Choose your action";
				
		_shipForTurn = _selectedUnit;

		ShipConfirmPanel.SetActive(false);
		
		ShipActionPanel.SetActive(true);
	}

	public void ShowRotationPoints()
	{
		ShipActionPanel.SetActive(false);
		
		TurnManager.instructionLabel.text = "Select one of the turn options";

		_shipForTurn.ShowRotationOptions();
	}
}
