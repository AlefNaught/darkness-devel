using UnityEngine;
using System.Collections;

public class plyCursor : MonoBehaviour {

    public Texture2D cTexture; // Cursor texture
    public CursorMode cMode = CursorMode.Auto; //Allows any cursor of any hardware to work.
    public Vector2 cLocate = Vector2.zero; //location of texture starts at the top left.

    void Start()
    {
        Cursor.visible = true;
        cLocate = new Vector2(cTexture.width / 2, cTexture.height / 2);
        Cursor.SetCursor(cTexture, cLocate, cMode);

    }

    void Update()
    {
        Vector3 posMouse = Input.mousePosition;
        if (Input.GetButtonDown("Fire1"))
        {
            if (Camera.main == null)
            {
                Debug.LogError("Error, no Camera tagged as 'MainCamera'");
            }
            else
            {
                Ray rCast = Camera.main.ScreenPointToRay(posMouse);
                //RaycastHit2D rcIt = Physics2D.Raycast(Camera.main., rCast.direction);
                RaycastHit rcIt;
                if (Physics.Raycast(rCast, out rcIt))
                {
                    Debug.DrawLine(rCast.origin, rcIt.point, Color.green, 20);
                }
            }
        }
    }
}
