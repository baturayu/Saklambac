using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HidingChild : MonoBehaviour
{
    public SpriteRenderer Renderer;
    public Light2D Light;

    private bool _found;
    private bool _caught;
    public bool Found
    {
        get => _found;
        set
        {
            Renderer.enabled = value;
            _found = true;
        }
    }
    public bool Caught
    {
        get => _caught;
        set
        {
            Light.enabled = value;
            _caught = value;
        }
    }
}