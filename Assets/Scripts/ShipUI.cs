using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{

	public Text Type;
	public Text Hp;
	public Text Firepower;
	public Text Movement;

	private void Start()
	{
		gameObject.SetActive(false);
	}
	
	void SetUnitDetails(Unit unit)
	{
		Debug.Log("Setting ship UI");
		
		Type.text = unit.name;
		Hp.text = unit.currentHitpoints + "/" + unit.maxHitpoints;
		Firepower.text = unit.firepower.ToString();
		Movement.text = unit.remainingMovementRange + "/" + unit.maxMovementRange;

		gameObject.SetActive(true);
	}

	public void HideShipDetails()
	{
		gameObject.SetActive(false);
	}
	
}
