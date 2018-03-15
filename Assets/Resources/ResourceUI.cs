using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Resource))]
public class ResourceUI : MonoBehaviour {

    public Text label;
    public Text value;
    private Resource resource;    

    public void Awake()
    {
        resource = GetComponent<Resource>();

        resource.OnValueChange.AddListener(UpdateUI);
    }

    public void Start()
    { 
        label.text = resource.name;
        value.text = resource.Amount.ToString();
    }


    public void UpdateUI()
    {
        value.text = resource.Amount.ToString();
    }
}
