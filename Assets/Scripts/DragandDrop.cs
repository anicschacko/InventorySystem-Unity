using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour {

    private bool selected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(selected)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }
        if (Input.GetMouseButton(0))
            selected = false;


	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
            selected = true;
    }
}
