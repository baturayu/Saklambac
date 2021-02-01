using System;
using System.Collections.Generic;
using UnityEngine;

public class CallController : MonoBehaviour
{
    public ParticleSystem CallParticle;
    private List<HidingChild> _hidingChildren;

    private void Start()
    {
        _hidingChildren = GetComponent<CharacterContoller>().HidingChildren;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CallClosestChild(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CallClosestChild(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CallClosestChild(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CallClosestChild(3);
        }
    }

    private void CallClosestChild(int childIndex)
    {
        DisplayHint(transform.position, _hidingChildren[childIndex].transform.position);
    }

    private bool IsInScreen(Camera cam, Vector3 pos)
    {
        var pixelPoint = cam.WorldToScreenPoint(pos);
        if (pixelPoint.x < 0 || pixelPoint.x > cam.pixelWidth ||
            pixelPoint.y < 0 || pixelPoint.y > cam.pixelHeight)
        {
            return false;
        }

        return true;
    }

    private void DisplayHint(Vector3 playerPosition, Vector3 childPosition)
    {
        var cam = Camera.main;
        if (IsInScreen(cam, childPosition)) return;

        var diffVector = (childPosition - playerPosition).normalized;
        var slope = diffVector.y / diffVector.x;
        var aspectRatio = 1f / cam.aspect;
        Vector2 arrowPosInPixels;
        if (Math.Abs(slope) > aspectRatio)
        {
            var xOffset = (cam.pixelHeight / 2f) / slope;
            if (diffVector.y > 0f)
            {
                arrowPosInPixels = new Vector3((cam.pixelWidth / 2f) + xOffset, cam.pixelHeight);
            }
            else
            {
                arrowPosInPixels = new Vector3((cam.pixelWidth / 2f) - xOffset, 0);
            }
        }
        else
        {
            var yOffset = slope * cam.pixelWidth / 2;
            if (diffVector.x > 0f)
            {
                arrowPosInPixels = new Vector3(cam.pixelWidth, (cam.pixelHeight / 2f) + yOffset);
            }
            else
            {
                arrowPosInPixels = new Vector3(0, (cam.pixelHeight / 2f) - yOffset);
            }
        }

        var particlePos = cam.ScreenToWorldPoint(arrowPosInPixels);
        particlePos.z = 0;
        var particleTransform = CallParticle.transform;
        particleTransform.position = particlePos;
        particleTransform.up = -diffVector;
        CallParticle.Play();
    }
}