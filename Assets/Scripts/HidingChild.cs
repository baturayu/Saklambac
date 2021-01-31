using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HidingChild : MonoBehaviour
{
    public SpriteRenderer[] HeadRenderers;
    public SpriteRenderer[] FootRenderers;
    public Light2D Light;

    private bool _found;
    private bool _caught;
    public bool Found
    {
        get => _found;
        set
        {
            foreach (var spriteRenderer in HeadRenderers)
            {
                spriteRenderer.enabled = value;
            }
            _found = true;
        }
    }
    public bool Caught
    {
        get => _caught;
        set
        {
            foreach (var spriteRenderer in FootRenderers)
            {
                spriteRenderer.enabled = value;
            }
            Light.enabled = value;
            _caught = value;
        }
    }

    private void Start()
    {
        Light.enabled = false;
        foreach (var spriteRenderer in HeadRenderers)
        {
            spriteRenderer.enabled = false;
        }
        
        foreach (var spriteRenderer in FootRenderers)
        {
            spriteRenderer.enabled = false;
        }
    }
}