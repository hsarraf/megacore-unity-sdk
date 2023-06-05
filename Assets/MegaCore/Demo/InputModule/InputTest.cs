using UnityEngine;

public class InputTest : MonoBehaviour
{
    public void OnRaycasted(RaycastHit hitInfo)
    {
        Debug.Log("raycast " + hitInfo.collider.name);
    }

    public void OnTouchStarted(Vector2 touchPosition)
    {
        Debug.Log("tapped " + touchPosition);
    }

    public void OnTouchEnded(Vector2 touchPosition, bool hasEnded)
    {
        Debug.Log("untapped " + touchPosition + " " + hasEnded);
    }

    public void OnTouchHolding(Vector2 touchPosition, float duration)
    {
        Debug.Log("holding " + touchPosition + " " + duration);
    }

    public void OnTouchMoving(Vector2 touchPosition, Vector2 dragSpeed)
    {
        Debug.Log("moving " + touchPosition + " " + dragSpeed);
    }

    public void OnTouchStationary(Vector2 touchPosition)
    {
        Debug.Log("stationary " + touchPosition);
    }

    public void OnHoldTimeTriggered(Vector2 touchPosition)
    {
        Debug.Log("holeTime tiggered " + touchPosition);
    }

    public void OnDragging(Vector2 touchPosition, Vector2 touchDirection, Vector2 touchvelocity, float duration)
    {
        Debug.Log("dragging " + touchPosition + " " + touchDirection + " " + touchvelocity + " " + duration);
    }

    public void OnDragLengthTriggered(Vector2 touchPosition, Vector2 touchDirection, Vector2 touchvelocity, float duration)
    {
        Debug.Log("drag length tiggered " + touchPosition + " " + touchDirection + " " + touchvelocity + " " +  duration);
    }

    // swipe callbaks
    //
    public void OnSwipeLeft()
    {
        Debug.Log("Swipe Left");
    }
    public void OnSwipeRight()
    {
        Debug.Log("Swipe Right");
    }
    public void OnSwipeUp()
    {
        Debug.Log("Swipe Up");
    }
    public void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
    }


}
