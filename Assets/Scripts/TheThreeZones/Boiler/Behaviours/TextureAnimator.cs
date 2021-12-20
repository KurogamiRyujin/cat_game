using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimator : MonoBehaviour
{
    [SerializeField] private Renderer rend;
    [SerializeField] private float animationSpeed = 0.5f;

    private List<Material> materials;

    private void Awake() {
        materials = new List<Material>(rend.materials);
    }

    private void Update() {
        Animate();
    }

    private void Animate() {
        Material material = materials[0];
        Vector2 offset = material.mainTextureOffset + new Vector2(animationSpeed, animationSpeed);
        material.mainTextureOffset = Vector2.Lerp(material.mainTextureOffset, offset, Time.deltaTime);
    }
}
