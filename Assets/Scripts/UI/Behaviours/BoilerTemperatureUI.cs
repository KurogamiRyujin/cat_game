using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI component to display Boiler Temperature on a UI gameobject.
public class BoilerTemperatureUI : MonoBehaviour
{
    [Header("Boiler Condition")]
    [SerializeField] private BoilerConditionSO boilercondition;
    private Text temperatureText;

    private void Awake() {
        temperatureText = gameObject.GetComponent<Text>();
    }

    private void Update() {
        temperatureText.text = boilercondition.temperature.ToString() + " C";
    }
}
