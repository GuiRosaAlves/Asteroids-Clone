using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EdgeWarpingScript : MonoBehaviour {

    public static Vector2 screenBottomLeft;
    public static Vector2 screenTopRight;

    private Vector3 newPos;
    private Vector2 offset, bottomLeftWithOffset, topRightWithOffset;

    void Awake()
    {
        if(screenBottomLeft == Vector2.zero && screenTopRight == Vector2.zero)
        {
            screenBottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
            screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0));
        }
        Vector3 objSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
        offset = new Vector2(objSize.x, objSize.y) / 2;
        bottomLeftWithOffset = screenBottomLeft - offset;
        topRightWithOffset = screenTopRight + offset;
}

    void LateUpdate()
    {
        newPos = transform.position;
        
        if (transform.position.x > topRightWithOffset.x)
            newPos.x = bottomLeftWithOffset.x;
        if (transform.position.x < bottomLeftWithOffset.x)
            newPos.x = topRightWithOffset.x;
        if (transform.position.y > topRightWithOffset.y)
            newPos.y = bottomLeftWithOffset.y;
        if (transform.position.y < bottomLeftWithOffset.y)
            newPos.y = topRightWithOffset.y;

        transform.position = newPos;
    }
}
