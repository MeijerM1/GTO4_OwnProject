using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public Map map;

	public float panSpeed = 20f;
	public float panBorder = 10f;

	private Vector2 panLimitX;
	private Vector2 panLimitZ;

	public Vector2 scrollLimit;

	public float scrollSpeed = 20f;
	
	private Vector3 velocity = Vector3.zero;

	private void Start()
	{
		map =  GameObject.FindGameObjectWithTag("Map").GetComponent<Map>();
		CalculateOffset();
	}

	// Update is called once per frame
	public void Update ()
	{
		Vector3 pos = transform.position;

		if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorder)
		{
			pos.z += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("s") || Input.mousePosition.y < panBorder)
		{
			pos.z -= panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorder)
		{
			pos.x += panSpeed * Time.deltaTime;
		}
		if (Input.GetKey("a") || Input.mousePosition.x < panBorder)
		{
			pos.x -= panSpeed * Time.deltaTime;
		}

		float scroll = Input.GetAxis("Mouse ScrollWheel");
		pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

		pos.x = Mathf.Clamp(pos.x, panLimitX.x, panLimitX.y);
		pos.y = Mathf.Clamp(pos.y, scrollLimit.x, scrollLimit.y);
		pos.z = Mathf.Clamp(pos.z, panLimitZ.x, panLimitZ.y);
		
		//transform.position = pos;
		transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, 0.05f);
	}

	private void CalculateOffset()
	{
		float width = map.SizeX * map.Prototype.GetComponent<Renderer>().bounds.size.x;
		float height = map.SizeY * map.Prototype.GetComponent<Renderer>().bounds.size.z;
		
		panLimitX = new Vector2(0, width);
		panLimitZ = new Vector2(-15, height - 15);
	}
}
