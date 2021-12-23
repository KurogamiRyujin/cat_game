using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureColorController : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private float changeColorLerpSpeed = 3f;

    [Header("Colors")]
    [SerializeField] private Color negativeColorTarget;
    [SerializeField] private Color positiveColorTarget;


    [Header("Channels listening to")]
    [SerializeField] private FloatBroadcastingChannel colorPercentChannel;

    private Color targetColor;

    private void Awake() {
        targetColor = Color.white;
    }

    private void OnEnable() {
        //Register events
        colorPercentChannel.onEventRaised += ScaleColorByPercent;
    }

    private void OnDisable() {
        //Unregister events
        colorPercentChannel.onEventRaised -= ScaleColorByPercent;
    }

    private void Update() {
        rend.material.color = Color.Lerp(rend.material.color, targetColor, changeColorLerpSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Changes the texture's color based on a given percentage.
    /// 100% sets the target color to the positiveColorTarget.
    /// -100% sets the target color to the negativeColorTarget.
    /// </summary>
    /// <param name="percent"></param>
    private void ScaleColorByPercent(float percent) {
        if(percent == 0f) {
            targetColor = Color.white;
        }
        else {
            Color neutral = Color.white;
            Color diff = neutral - ((percent > 0f) ? positiveColorTarget : negativeColorTarget);

            percent = ClampPercent(percent);
            diff = diff * percent;
            targetColor = neutral - diff;
        }
    }

    private float ClampPercent(float percent) {
        percent = Mathf.Abs(percent);
        if(percent > 1f) {
            percent = 1f;
        }

        return percent;
    }
}
