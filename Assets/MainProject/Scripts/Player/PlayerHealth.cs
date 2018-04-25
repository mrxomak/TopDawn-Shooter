using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColor = new Color (1f,0f,0f,0.1f);

	//Animator anim;
	AudioSource playerAudio;
	PlayerMovement playerMovement;
	PlayerShooting playerShooting;

	bool isDead;
	bool isDamaged;

	void Awake()
	{
		//anim = GetComponent<Animator> ();
		playerMovement = GetComponent<PlayerMovement> ();
		playerAudio = GetComponent<AudioSource> ();
		playerShooting = GetComponentInChildren<PlayerShooting> ();
		currentHealth = startingHealth;

	}
	void Update()
	{
		if (isDamaged) {
			damageImage.color = flashColor;

		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		isDamaged = false;
	}
	public void TakeDamage(int amount)
	{
		isDamaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		playerAudio.Play ();

		if (currentHealth <= 0 && !isDead)
			Death ();
	}

	void Death()
	{
		isDead = true;

		//anim.SetTrigger ("Die");
        playerAudio.clip = deathClip;
		playerAudio.Play ();

		playerMovement.enabled = false;
		playerShooting.enabled = false;
	}
}
