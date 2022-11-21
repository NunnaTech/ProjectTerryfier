using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using System;

public class StaminaBar :  MonoBehaviour
{
   
    // Start is called before the first frame update
    // void Start()
    // {
       
    // }

    // public void UseStamina(float amount)
    // {
    //     if(currentStamina - amount >0)
    //     {
    //         // Start losing stamina if it is posible
    //         StartCoroutine(LosingStaminaCoroutine(amount));
    //         // get Stamina again...
    //         StartCoroutine(RegenerateStamineCoroutine());

    //     }
    //     else
    //     {
    //         Debug.Log("No stamina");
    //     }
    // }
    // private IEnumerator LosingStaminaCoroutine(float amount)
    // {
    //     while (currentStamina > 0)
    //     {
    //         currentStamina -= amount;
    //         staminaSlider.value = currentStamina;
    //         yield return new WaitForSeconds(losingStaminaTime);
    //     }
    //     //GameObject.FindObjectOfType<PlayerFreeLookState>().isSprinting = false;
    // }
    // private IEnumerator RegenerateStamineCoroutine()
    // {
    //     while (currentStamina < maxStamina)
    //     {
    //         currentStamina += regenerateAmount;
    //         staminaSlider.value = currentStamina;
    //         yield return new WaitForSeconds(regenerateStaminaTime);
    //     }
    // }
}
