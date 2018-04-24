using System;
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
	
	public void SetUnitDetails(Unit unit)
	{
		Type.text = unit.name.Substring(0, unit.name.IndexOf("(")) ;
		Hp.text = unit.CurrentHitpoints + "/" + unit.MaxHitpoints;
		Firepower.text = unit.RemainingFirepower.ToString() +"/" + unit.Firepower.ToString();
		Movement.text = unit.RemainingMovementRange + "/" + unit.MaxMovementRange;

		gameObject.SetActive(true);
	}

	public void Show(bool flag)
	{
		gameObject.SetActive(flag);
	}
	
}
