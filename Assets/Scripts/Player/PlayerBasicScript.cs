using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerBasicScript : MonoBehaviour
{
    public float health = 3;
    public Text healthText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            collision.gameObject.SetActive(false);
            healthText.SendMessage("SetHp");
        }
        if (health == 0)
            Death();
    }

    public void Death()
    {  
            SceneManager.LoadScene("SampleScene");
    }
}
