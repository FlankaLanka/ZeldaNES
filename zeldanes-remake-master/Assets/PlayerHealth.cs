using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Text HPbar;
    public float maxHP = 6;
    public float knockback_power = 10;

    private float currentHP;
    private float iframe;
    private bool canTakeDamage;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        iframe = 5; //seconds of invulnerability after taking damage
        canTakeDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collided_with = other.gameObject;

        if (collided_with.tag == "Enemy")
        {
            takeDamage(collided_with);
        }
    }

    private void takeDamage(GameObject collided_with)
    {
        if(canTakeDamage == true)
        {
            currentHP--;
            HPbar.text = currentHP.ToString();
            if (currentHP <= 0)
            {
                //dead
                SceneManager.LoadScene(0);
            }
        }

    }

}
