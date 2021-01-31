using System.Collections.Generic;
using UnityEngine;

public class CharacterContoller : MonoBehaviour
{
    public Camera MainCamera;
    public Rigidbody2D RigidBody;
    public List<HidingChild> HidingChildren;
    private const float SPEED = 2f;
    private const float CATCH_RADIUS_SQUARED = 0.1f;
    
    private void Update()
    {
        var position = transform.position;
        var cameraPosition = position;
        cameraPosition.z = -10;
        MainCamera.transform.position = cameraPosition;
        
        var mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        transform.up = mousePosition - position;

        for (var i = 0; i < HidingChildren.Count; i++)
        {
            var child = HidingChildren[i];
            if (child.Found && !child.Caught && 
                Vector3.SqrMagnitude(transform.position - child.transform.position) <= CATCH_RADIUS_SQUARED)
            {
                HidingChildren.Remove(child);
                child.Caught = true;
            }
        }
    }

    private void FixedUpdate()
    {
        RigidBody.position += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * (Time.fixedDeltaTime * SPEED);
    }
}
