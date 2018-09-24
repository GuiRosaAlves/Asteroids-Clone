using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class InputManager : MonoBehaviour {
    private Player playerReference;
    private Vector2 moveAxis;

    void Start()
    {
        playerReference = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        playerReference.Rotate(moveAxis.x);
        playerReference.Thrust(moveAxis);
    }

    void Update()
    {
        moveAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButton("Fire1"))
            playerReference.Fire();
        if(Input.GetButtonDown("Teleport"))
            playerReference.Teleport();
    }
}
