using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteHandler : MonoBehaviour
{
    public Sprite Red;
    public Sprite Blue;
    public Sprite Black;
    public Sprite White;
    public Sprite Yellow;
    public Sprite Green;

    void Start()
    {
        
        string character = PlayerPrefs.GetString("Character");

        if (character == "Red")
        {
            this.GetComponent<SpriteRenderer>().sprite = Red;  
        }
        if (character == "Blue")
        {
            this.GetComponent<SpriteRenderer>().sprite = Blue;
        }
        if (character == "Black")
        {
            this.GetComponent<SpriteRenderer>().sprite = Black;
        }
        if (character == "White")
        {
            this.GetComponent<SpriteRenderer>().sprite = White;
        }
        if (character == "Yellow")
        {
            this.GetComponent<SpriteRenderer>().sprite = Yellow;
        }
        if (character == "Green")
        {
            this.GetComponent<SpriteRenderer>().sprite = Green;
        }

        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        Sprite sprite = this.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < polygonCollider.pathCount; i++) { polygonCollider.SetPath(i, null); }
        polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();

        for (int i = 0; i < polygonCollider.pathCount; i++)
        {
            path.Clear();
            sprite.GetPhysicsShape(i, path);
            polygonCollider.SetPath(i, path.ToArray());
        }

    }

}
