using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeball : MonoBehaviour {
    [SerializeField] private Material redMatA;
    [SerializeField] private Material redMatB;
    [SerializeField] private Material blueMatA;
    [SerializeField] private Material blueMatB;

    private MeshRenderer meshRenderer;

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    public void SetRed() {
        Debug.Log("setRed");
        Material[] mats = meshRenderer.materials;
        mats[0] = redMatA;
        mats[1] = redMatB;
        meshRenderer.materials = mats;
    }
    
    public void SetBlue() {
        Material[] mats = meshRenderer.materials;
        mats[0] = blueMatA;
        mats[1] = blueMatB;
        meshRenderer.materials = mats;
    }
}
