using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//ATTRIBUTES
	public float speed;
	public float health;
	public static float maxHealth;
	//PROJECTILE
	public GameObject projectile;
	private static float projectileSpeed = 5.6f;
	public float fireRate;
	//AUDIO
	public AudioClip fireSound;
	public AudioClip hitSound;
	public AudioClip deathSound;
	//BORDERS
	private float padding = 1f;
	private float xmin;
	private float xmax;
	private float ymin = -3.4f;
	private float ymax = -1.4f;
	//SCRIPTS
	public LevelManager levelManager;
	private ScoreKeeper scoreKeeper;
	private HealthBar healthBar;
	//SHIELD POWERUP
	public GameObject[] shields;
	private GameObject shieldInstance;
	private static int shieldUp = 0;
	//DOUBLEFIRE POWERUP
	private static bool doubleFire = false;
	private static bool boolForInvoke = true;
	private static float doubleFireTime = 0;
	//HEALTHPACK
	private float healthPackHealthAdd = 50f;	


	void Awake(){
		doubleFire = false;
		boolForInvoke = true;
		healthBar = GameObject.Find ("HealthBar").GetComponent<HealthBar> ();
		healthBar.ResetHealth ();
		maxHealth = health;

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		
		xmin = leftMost.x + padding;
		xmax = rightMost.x - padding;
	}

	void Start(){
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
		scoreKeeper.Reset ();
	}

	void Update ()
	{
		if(Input.GetKeyDown("space")){
			if(doubleFire==false){
				InvokeRepeating("Fire", 0.00001f, fireRate);
			}
			if(doubleFire==true){
				InvokeRepeating("DoubleFire", 0.00001f, fireRate);
			}
		}
		if(Input.GetKeyUp("space")){
				CancelInvoke("Fire");
				CancelInvoke("DoubleFire");
		}
		if (Input.GetKey ("left")) {
			transform.position += Vector3.left * speed * Time.deltaTime;
		} 
		
		if (Input.GetKey ("right")) {
			transform.position += Vector3.right * speed * Time.deltaTime;			
		}
		
		if (Input.GetKey ("up")) {
			transform.position += Vector3.up * speed * Time.deltaTime;
		}
		
		if (Input.GetKey ("down")) {
			transform.position += Vector3.down * speed * Time.deltaTime;			
		}
		
		float newX = Mathf.Clamp (transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp (transform.position.y, ymin, ymax);
		transform.position = new Vector3 (newX, newY, transform.position.z);

		if (shieldUp>0) {
			shieldInstance.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		}

		if(doubleFire==true){
			CancelInvoke("Fire");
			if(boolForInvoke){
				InvokeRepeating("DoubleFire", 0.00001f, fireRate);
				boolForInvoke = false;
			}
			doubleFireTime = doubleFireTime + (1 * Time.deltaTime);
			if(doubleFireTime>10){
				CancelInvoke("DoubleFire");
				InvokeRepeating("Fire", 0.00001f, fireRate);
				doubleFire = false;
				boolForInvoke = true;
				doubleFireTime = 0;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		EnemyShot1 missile1 = col.gameObject.GetComponent(typeof(EnemyShot1)) as EnemyShot1;
		EnemyShot2 missile2 = col.gameObject.GetComponent(typeof(EnemyShot2)) as EnemyShot2;
		EnemyShot3 missile3 = col.gameObject.GetComponent(typeof(EnemyShot3)) as EnemyShot3;
		Shield shieldPowerUp = col.gameObject.GetComponent<Shield>() as Shield;
		HealthPack healthPack = col.gameObject.GetComponent<HealthPack>() as HealthPack;
		DoublePowerUp doubleMissile =col.gameObject.GetComponent(typeof(DoublePowerUp)) as DoublePowerUp;
		if (missile1) {
			if(shieldUp>0){
				shieldUp -= 1;
				missile1.Hit();
				Destroy (shieldInstance);
				if(shieldUp!=0){
					Shield(shieldUp);
				}
			}else{
				health = health - missile1.GetDamage();
				missile1.Hit();
				AudioSource.PlayClipAtPoint (hitSound, transform.position);
				if(health<=0){
					Destroy (gameObject);
					AudioSource.PlayClipAtPoint (deathSound, transform.position);
					levelManager.LoadLevel("Win Screen");
				}
			}
		}

		if (missile2) {
			if(shieldUp>0){
				shieldUp -= 1;
				missile2.Hit();
				Destroy (shieldInstance);
				if(shieldUp!=0){
					Shield(shieldUp);
				}
			}else{
				health = health - missile2.GetDamage();
				missile2.Hit();
				AudioSource.PlayClipAtPoint (hitSound, transform.position);
				if(health<=0){
					Destroy (gameObject);
					AudioSource.PlayClipAtPoint (deathSound, transform.position);
					levelManager.LoadLevel("Win Screen");
				}
			}
		}

		if (missile3) {
			if(shieldUp>0){
				shieldUp = 0;
				missile3.Hit();
				Destroy (shieldInstance);
				if(shieldUp!=0){
					Shield(shieldUp);
				}
			}else{
				health = health - missile3.GetDamage();
				missile3.Hit();
				AudioSource.PlayClipAtPoint (hitSound, transform.position);
				if(health<=0){
					Destroy (gameObject);
					AudioSource.PlayClipAtPoint (deathSound, transform.position);
					levelManager.LoadLevel("Win Screen");
				}
			}
		}
		if (shieldPowerUp) {
			if(shieldUp<3){
				shieldUp += 1;
			}
			shieldPowerUp.Hit();
			Destroy (shieldInstance);
			Shield (shieldUp);
		}

		if (doubleMissile) {
			doubleFireTime = 0;
			doubleFire = true;
			doubleMissile.Hit();
		}

		if (healthPack) {
			if(health<maxHealth+healthPackHealthAdd){
			   health += healthPackHealthAdd;
			}
			if(health > maxHealth){
				health = maxHealth;
			}
			healthPack.Hit();
		}
	}

	void Shield(int level){
		shieldInstance = Instantiate(shields[level-1], transform.position, Quaternion.identity) as GameObject;
	}

	void Fire(){
		GameObject beam = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0,projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	void DoubleFire(){
		GameObject beam1 = Instantiate(projectile, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		GameObject beam2 = Instantiate(projectile, new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
		beam1.rigidbody2D.velocity = new Vector3(0,projectileSpeed, 0);
		beam2.rigidbody2D.velocity = new Vector3(0,projectileSpeed, 0);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
	}

	public int LevelShield(){
		return shieldUp;
	}

	public float DoubleFireSeconds(){
		return doubleFireTime;
	}

	public bool DoubleFireActive(){
		return doubleFire;
	}

	public float GetHealth(){
		return health;
	}

}
