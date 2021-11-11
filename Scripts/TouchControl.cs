using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* Class TouchControl, which implements the logic of touch control.
 * With the touch control, you can control the object of the player spaceship, 
 * pointing your finger across the device screen its motion vector.
 * Also, by clicking on the lower right area of the device screen, you can shoot.
 */
public class TouchControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 origin;
    private Vector2 direction;  // The position to move towards.
    private Vector2 smoothDirection;  // The position to move from.

    public float smoothness;  // Distance to move current per call.
    public int pointerID;
    private bool touched;

    // Awake is called to initialize variables or states before the application start
    private void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }

    // OnPointerDown is called when user taps on the screen
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            origin = eventData.position;
            touched = true;
            pointerID = eventData.pointerId;  // get status and click number
        }
    }

    // OnDrag is called to handle finger movement across the screen
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)  // check if it is the same touch
        {
            Vector2 currentPosition = eventData.position;
            Vector2 directionResult = currentPosition - origin;
            direction = directionResult.normalized;
        }
    }

    // OnPointerUp is called when user's touch was released
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            direction = Vector2.zero;  // stop moving
        }
    }

    /* Method GetDirection returns a position of the moved object.
     * Here we use MoveTowards method, that moves a point current towards target.
     * By updating an object’s position each frame using the position calculated by this function, 
     * you can move it towards the target smoothly.
     */
    public Vector2 GetDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothness);
        return smoothDirection;
    }
}
