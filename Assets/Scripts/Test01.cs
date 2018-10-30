using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test01 : MonoBehaviour
{


    [SerializeField] private float playerSpeed = 5f;

    public Transform gameElements;

    private Transform inventory1, inventory2;
    public GameObject pshuriken, psword;
    public GameObject shuriken, sword;

    private float verticalExtent;
    private float horizontalExtent;
    private float playerRadius;
    private float shurikenButtonRadius;
    private float swordButtonRadius;
    private float distance = 10f;

    private bool lookingRight, selected;

    private Vector3 movePlayer;
    private Vector3 jumpPlayer;

    // Use this for initialization
    void Start()
    {
        verticalExtent = Camera.main.orthographicSize;
        horizontalExtent = verticalExtent * Camera.main.aspect;

        playerRadius = this.transform.localScale.x / 2;
        lookingRight = true;

        shurikenButtonRadius = shuriken.transform.localScale.x / 2;
        //swordButtonRadius = sword.transform.localScale.x / 2;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        WeaponSelection();
        //WeaponDrag()
        if (selected == true)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shuriken.transform.position = new Vector2(cursorPosition.x, cursorPosition.y);
        }
        if (Input.GetMouseButton(0))
            selected = false;
    }

    void PlayerMovement()
    {
        /*Horizontal Player Movement*/
        movePlayer = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        this.transform.position += movePlayer * playerSpeed * Time.deltaTime;

        /*Player Jump Movement*/
        jumpPlayer = new Vector3(0f, Input.GetAxis("Jump"), 0f);
        this.transform.position += jumpPlayer * playerSpeed * Time.deltaTime;

        /*Clamp Player Movements*/
        var pos = this.transform.position;
        pos.x = Mathf.Clamp(pos.x, -horizontalExtent + playerRadius, horizontalExtent - playerRadius);
        this.transform.position = pos;

        /*Flippig Character based on keypress*/
        if (Input.GetKeyDown(KeyCode.A) && lookingRight || Input.GetKeyDown(KeyCode.D) && !lookingRight)
        {
            lookingRight = !lookingRight;
            Flip();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Shuriken")
        {
            inventory1 = gameElements.transform.Find("Canvas/Inventory/InventoryPanel/InventorySlot/ItemButton/Shuriken");
            col.gameObject.SetActive(false);
            inventory1.gameObject.SetActive(true);
        }
        if (col.gameObject.name == "Sword")
        {
            inventory2 = gameElements.transform.Find("Canvas/Inventory/InventoryPanel/InventorySlot (1)/ItemButton/Sword");
            col.gameObject.SetActive(false);
            inventory2.gameObject.SetActive(true);
        }
    }

    void Flip()
    {
        Vector3 scale = this.transform.localScale;
        scale.x = -scale.x;
        this.transform.localScale = scale;
    }

    void WeaponSelection()
    {
        if (Input.GetKey(KeyCode.Alpha1) && inventory1)
        {
            psword.SetActive(false);
            pshuriken.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Alpha2) && inventory2)
        {
            pshuriken.SetActive(false);
            psword.SetActive(true);
        }
    }

    /*public bool Between(float val, float low, float high)
    {
        if (val > low || val < high)
            return true;
        else
      */

    public void OnMouseDrag()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shuriken.transform.position = new Vector3(cursorPos.x, cursorPos.y, 0f);
        }
    }
}