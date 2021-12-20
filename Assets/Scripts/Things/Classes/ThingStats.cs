using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ThingStats
{
    public float weight = 1f;
    public int heatValue = 2;

    private List<StatusEffect> statusEffects = new List<StatusEffect>();

    public void AddStatus(StatusEffect statusEffect) {
        if(!CheckStatusFor(statusEffect.GetType())) {
            statusEffects.Add(statusEffect);
        }
    }

    public void RemoveStatus(StatusEffect statusEffect) {
        if(statusEffects.Contains(statusEffect)) {
            statusEffects.Remove(statusEffect);
        }
    }

    public void CleanseAllStatus() {
        statusEffects.Clear();
    }

    public void ApplyStatuses() {
        List<StatusEffect> statusesToRemove = new List<StatusEffect>();
        for(int i = 0; i < statusEffects.Count; i++) {
            statusEffects[i].ApplyStatus(this);
            bool isStillActive = statusEffects[i].DecayDuration();
            

            if(!isStillActive) {
                statusesToRemove.Add(statusEffects[i]);
            }
        }
        for(int i = 0; i < statusesToRemove.Count; i++) {
            RemoveStatus(statusesToRemove[i]);
        }
    }

    public bool CheckStatusFor(Type statusType) {
        return statusEffects.Contains(statusEffects.Find(x => x.GetType().Name == statusType.Name));
    }
}
