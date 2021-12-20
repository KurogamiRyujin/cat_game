using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//EarthUI gameobject behaviour to indicate the Earth's relative position with respect to its distance with the Boiler and Space.
public class EarthUIBehaviour : MonoBehaviour
{
    [Header("Three Zones Stats")]
    [SerializeField] private EarthStatsSO earthStats;
    [SerializeField] private SpaceStatsSO spaceStats;
    [SerializeField] private BoilerStatsSO boilerStats;

    [Header("Three Zones UI")]
    [SerializeField] private Image earthUI;
    [SerializeField] private Image spaceUI;
    [SerializeField] private Image boilerUI;

    [SerializeField] private RectTransform referencePanel;

    private void Update() {
        MoveEarthUI();
    }

    private void MoveEarthUI() {
        float referenceLength = referencePanel.rect.size.y;
        float earthUiY = earthUI.rectTransform.anchoredPosition.y;
        float spaceUiY = spaceUI.rectTransform.anchoredPosition.y;
        float boilerUiY = boilerUI.rectTransform.anchoredPosition.y;

        //EarthUI movement scaler
        float scaler = 0f;

        float deltaReferenceLength = referenceLength - (Mathf.Abs(spaceUiY) + Mathf.Abs(boilerUiY));

        float deltaBoilerSpace = Mathf.Abs(boilerStats.altitude - spaceStats.altitude);

        scaler = deltaReferenceLength / deltaBoilerSpace;

        float earthUIDisplacementY = earthStats.altitude * scaler;
        earthUI.rectTransform.anchoredPosition = new Vector2(earthUI.rectTransform.anchoredPosition.x, earthUIDisplacementY);
    }
}
