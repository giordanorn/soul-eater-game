using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerBasicCloseAttack attack;

    private void Awake() {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerBasicCloseAttack>();
    }

    private void Update() {
        attack.checkAttack();
    }
}