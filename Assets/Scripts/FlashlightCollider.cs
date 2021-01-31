using UnityEngine;

public class FlashlightCollider : MonoBehaviour
{
    
    public Transform FlashlightTransform;
    private int _flashlightMask;
    private int _characterMask;

    private void Start()
    {
        _flashlightMask = LayerMask.GetMask("Flashlight");
        _characterMask = LayerMask.GetMask("Character");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var characterPosition = FlashlightTransform.position;
        var raycastHit = Physics2D.Raycast(characterPosition, other.transform.position - characterPosition, 
                                           float.PositiveInfinity, ~(_flashlightMask | _characterMask));
        var child = raycastHit.transform.GetComponent<HidingChild>();
        if (child != null)
        {
            child.Found = true;
        }
    }
}