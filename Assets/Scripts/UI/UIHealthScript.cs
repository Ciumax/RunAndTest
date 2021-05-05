using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthScript : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        SetHp();
    }
    void SetHp()
    {
        _healthText.text = "Health: " + _player.GetComponent<PlayerBasicScript>().health;
    }
}
