using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMark : MonoBehaviour
{
    public bool IsTarget { get; set; } = false;

    private MeshRenderer _meshRenderer;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        IsTarget = false;
    }

    private void LateUpdate()
    {
        _meshRenderer.enabled = IsTarget;
        IsTarget = false;
    }
}
