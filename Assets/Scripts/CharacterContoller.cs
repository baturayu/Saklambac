using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContoller : MonoBehaviour
{
    public Camera MainCamera;
    public Rigidbody2D RigidBody;
    public Animator HeadAnimator;
    public Animator FeetAnimator;
    public List<HidingChild> HidingChildren;
    public Transform UpperBody;
    public Transform FeetTransform;
    private const float SPEED = 2f;
    private const float CATCH_RADIUS_SQUARED = 0.1f;
    private static readonly int _headIdleAnimation = Animator.StringToHash("Base Layer.HeadIdleAnimation");
    private static readonly int _headMoveAnimation = Animator.StringToHash("Base Layer.HeadMoveAnimation");
    private static readonly int _feetIdleAnimation = Animator.StringToHash("Base Layer.FeetIdleAnimation");
    private static readonly int _feetMoveAnimation = Animator.StringToHash("Base Layer.FeetMoveAnimation");
    private bool _moving;
    
    private void Update()
    {
        var position = transform.position;
        var cameraPosition = position;
        cameraPosition.z = -10;
        MainCamera.transform.position = cameraPosition;
        
        var mousePosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        UpperBody.up = -(mousePosition - position);

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
        var direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        RigidBody.position += direction * (Time.fixedDeltaTime * SPEED);
        if (direction.sqrMagnitude > 0f)
        {
            if (Math.Abs(Vector3.Angle(direction, -UpperBody.up)) < 120f)
            {
                FeetTransform.up = -direction;
            }
            else
            {
                FeetTransform.up = direction;
            }
            
            if (!_moving)
            {
                _moving = true;
                StartMoveAnimation();
            }
        }
        else
        {
            if (_moving)
            {
                _moving = false;
                StopMoveAnimation();
            }
        }
    }

    private void StartMoveAnimation()
    {
        HeadAnimator.Play(_headMoveAnimation);
        FeetAnimator.Play(_feetMoveAnimation);
    }

    private void StopMoveAnimation()
    {
        HeadAnimator.Play(_headIdleAnimation);
        FeetAnimator.Play(_feetIdleAnimation);
    }
}
