using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader {get; private set;}
    [field: SerializeField] public CharacterController CharacterController{get; private set;}
    [field: SerializeField] public Animator Animator { get; private set;}
    [field: SerializeField] public float FreeLookMovementSpeed { get; set;}
    [field: SerializeField] public ForceReceiver forceReceiver { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Slider staminaSlider;
    
    

    public Transform MainCameraTransform { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
        MainCameraTransform = Camera.main.transform;
    }

}
