using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerSateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader {get; private set;}
    [field: SerializeField] public CharacterController CharacterController{get; private set;}
    [field: SerializeField] public Animator Animator { get; private set;}
    [field: SerializeField] public float FreeLookMovementSpeed { get; set;}
    [field: SerializeField] public ForceReceiver forceReceiver { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Slider staminaSlider;
    [field: SerializeField] public RunShake runShake;
    // health 
    [field: SerializeField] public float health = 100;
    public event EventHandler DiedPlayer;
    [field: SerializeField] public GameObject menuGameOver;

    
	// Audio
	public AudioSource walkSteps;
	public AudioSource fall;
	public AudioSource death;

    // Stamine data
    public bool isSprinting;
    [field: SerializeField] public float staminaUseAmount = 5;
    public float maxStamina = 100;
    public float currentStamina;

    // Stamine renegerators
    private float regenerateStaminaTime = 0.1f;
    private float regenerateAmount = 2;
    private float losingStaminaTime = 0.1f;

    // Running Params
    public float sprintingSpeedMultiplier = 6f;
    public float sprintSpeed = 2f;
    
    // Coroutine
    private Coroutine mycCourutineLosing;
    private Coroutine mycCourutineRegeneration;
    // Cinemachine
    public Transform MainCameraTransform { get; private set; }

    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
        MainCameraTransform = Camera.main.transform;
        // set current value in UI
        currentStamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(DieStateSwitch());
        }
    }
      private IEnumerator DieStateSwitch()
    {
        yield return new WaitForEndOfFrame();
        DiedPlayer?.Invoke(this, EventArgs.Empty);
        SwitchState(new PlayerDeathState(this));

    }
     public void UseStamina(float amount)
    {
        if(currentStamina - amount >0)
        {
            // Start losing stamina if it is posible
            if(mycCourutineLosing != null)
            {
                StopCoroutine(mycCourutineLosing);
            }
            mycCourutineLosing = StartCoroutine(LosingStaminaCoroutine(amount));
            // get Stamina again...
             if(mycCourutineRegeneration != null)
            {
                StopCoroutine(mycCourutineRegeneration);
            }
            mycCourutineRegeneration = StartCoroutine(RegenerateStamineCoroutine());
        }
        else
        {
            Debug.Log("No stamina");
        }
    }

    private IEnumerator LosingStaminaCoroutine(float amount)
    {
        while (currentStamina > 0)
        {
            currentStamina -= amount;
            staminaSlider.value = currentStamina;
            yield return new WaitForSeconds(losingStaminaTime);
        }
        mycCourutineLosing = null;
        isSprinting = false;
    }
    private IEnumerator RegenerateStamineCoroutine()
    {
        yield return new WaitForSeconds(3);
        while (currentStamina < maxStamina)
        {
            currentStamina += regenerateAmount;
            staminaSlider.value = currentStamina;
            yield return new WaitForSeconds(regenerateStaminaTime);
        }
        mycCourutineRegeneration = null;
    }


}
