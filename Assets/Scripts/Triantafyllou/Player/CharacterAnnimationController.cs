using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnnimationController : MonoBehaviour
{
    Animator characterAnimator;
    Vector2 input;

    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        characterAnimator.SetFloat("InputX", input.x);
        characterAnimator.SetFloat("InputY", input.y);
    }
}
