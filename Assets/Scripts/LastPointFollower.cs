using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LastPointFollower : MonoBehaviour
{
    public SpriteShapeController spriteShapeController;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = spriteShapeController.spline.GetPosition(spriteShapeController.spline.GetPointCount() - 1);
    }
}