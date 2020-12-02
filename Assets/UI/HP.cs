using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private Text _hpText;
    private void Start()
    {
        _hpText = GetComponent<Text>();
    }
    
    private void Update()
    {
        _hpText.text = "HP: " + Wizard.health;
    }
}
