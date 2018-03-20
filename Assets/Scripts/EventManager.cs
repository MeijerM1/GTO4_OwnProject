using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class UnitSelectEvent : UnityEvent<Unit>
{
	
}

public class EventManager : MonoBehaviour
{

	private Dictionary<string, UnityEvent> _events;
	
	public static UnitSelectEvent SelectEvent = new UnitSelectEvent();

	private static EventManager _eventManager;

	public static EventManager instance
	{
		get
		{
			if (!_eventManager)
			{
				_eventManager = FindObjectOfType<EventManager>();

				if (!_eventManager)
				{
					Debug.LogError("There is no active event manager in scene.");
				}
				else
				{
					_eventManager.Init();
				}								
			}

			return _eventManager;
		}
	}

	private void Init()
	{		
		
		
		if (_events == null)
		{
			_events = new Dictionary<string, UnityEvent>();
		}
	}

	public static void StartListening(string eventName, UnityAction listener)
	{
		UnityEvent newEvent = null;

		if (instance._events.TryGetValue(eventName, out newEvent))
		{
			newEvent.AddListener(listener);
		}
		else
		{
			newEvent = new UnityEvent ();
			newEvent.AddListener (listener);
			instance._events.Add (eventName, newEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		if (_eventManager == null) return;

		UnityEvent thisEvent = null;

		if (instance._events.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	public static void TriggerEvent(string eventName)
	{
		UnityEvent thisEvent = null;

		if (instance._events.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke();
		}
	}
}
