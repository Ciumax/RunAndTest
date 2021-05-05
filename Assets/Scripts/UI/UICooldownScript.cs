using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICooldownScript : MonoBehaviour
{
    public Image imageCooldown;
    public bool IsCooldown;
    [SerializeField]
    private GameObject _cooldownValue;
    private float _cooldown;
   

    private void Start()
    {
        _cooldown = _cooldownValue.GetComponent<GrenadeScript>().grenadeRate;
    }
    private void Update()
    {
        if (Input.GetButton("Fire2") && IsCooldown == false)
        {
            IsCooldown = true;
        }
        if(IsCooldown)
        {
            imageCooldown.fillAmount += 1 / _cooldown * Time.deltaTime;
            if(imageCooldown.fillAmount >= 1 )
            {
                imageCooldown.fillAmount = 0;
                IsCooldown = false;
            }
        }
    }
}
