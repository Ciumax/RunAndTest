using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICooldownScript : MonoBehaviour
{
    public Image imageCooldown;
    [SerializeField]
    private GameObject player;
    private float cooldown;
    bool isCooldown;

    private void Start()
    {
        cooldown = player.GetComponent<PlayerScript>().grenadeRate;
    }
    private void Update()
    {
        if (Input.GetButton("Fire2") && isCooldown == false)
        {
            isCooldown = true;
            

        }

        if(isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            if(imageCooldown.fillAmount >= 1 )
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
        
    }

   

}
