using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthScript : MonoBehaviour
{
    [SerializeField]
    private Text healthText;
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        SetHp();
    }
    void SetHp()
    {
        healthText.text = "Health : " + player.GetComponent<PlayerScript>().HP;
    }
}
