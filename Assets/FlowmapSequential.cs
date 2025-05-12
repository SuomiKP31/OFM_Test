using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowmapSequential : MonoBehaviour
{
    [SerializeField] private List<Texture> VtuFlowmaps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private RenderTexture Rt;

    private int _curCounter;
    private Texture _curTexture;
    IEnumerator SwapFlowmapPer(float timeInterval)
    {
        while (_curCounter < VtuFlowmaps.Count)
        {
            _curTexture = VtuFlowmaps[_curCounter];
            _curCounter++;
            var meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer != null && meshRenderer.sharedMaterial.GetTexture("_FlowMap") != Rt)
            {
                meshRenderer.sharedMaterial.SetTexture("_FlowMap", Rt);
            }
            Graphics.Blit(_curTexture, Rt);
            
            Debug.Log($"Texture swapped to {_curTexture.name}");
            yield return new WaitForSeconds(timeInterval);
        }
        
        yield return null;
    }

    private void Start()
    {
        
        StartCoroutine(SwapFlowmapPer(0.02f));
    }
}
