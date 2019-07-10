using UnityEngine;
using System.Collections;

public class Damage : DamageBase
{
    public bool Explosive;
    public float ExplosionRadius = 20;
    public float ExplosionForce = 1000;
	public bool HitedActive = true;
	public float TimeActive = 0;
	private float timetemp = 0;
	[HideInInspector]
	public bool isCollided;
	[HideInInspector]
	public GameObject collidedObject;
	private bool gotTrueKillShot;


    private void Start()
    {
        if (!Owner || !Owner.GetComponent<Collider>()) return;
        Physics.IgnoreCollision(GetComponent<Collider>(), Owner.GetComponent<Collider>());
		
		timetemp = Time.time;
    }

    private void Update()
    {
		if(!HitedActive){
			if(Time.time >= (timetemp + TimeActive)){
				Active();
			}
		}
    }

    public void Active()
    {
        if (Effect)
        {
        //    GameObject obj = (GameObject) Instantiate(Effect, transform.position, transform.rotation);
//            Destroy(obj, 3);	
        }

        if (Explosive)
            ExplosionDamage();


        Destroy(gameObject);
    }

    private void ExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Collider hit = hitColliders[i];
            if (!hit)
                continue;

            if (hit.gameObject.GetComponent<DamageManager>())
            {
                if (hit.gameObject.GetComponent<DamageManager>())
                {
                    hit.gameObject.GetComponent<DamageManager>().ApplyDamage(Damage);
                }
            }
            if (hit.GetComponent<Rigidbody>())
                hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0f);
        }

    }

    private void NormalDamage(Collision collision)
    {
        if (collision.gameObject.GetComponent<DamageManager>())
        {
            collision.gameObject.GetComponent<DamageManager>().ApplyDamage(Damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
		

	//	Debug.Log ("Collided with"+collision.gameObject.name);
//		Debug.Log("Collied at Distance"+Vector3.Distance(transform.position,GameObject.FindGameObjectWithTag("Player").transform.position));
		if(HitedActive){
        	if (collision.gameObject.tag != "Particle" && collision.gameObject.tag != this.gameObject.tag)
        	{
				Debug.Log ("Enetered in Collsison");
            	if (!Explosive)
                	NormalDamage(collision);
				if (collision.gameObject.tag == "Enemy") {
					isCollided = true;
					Debug.Log ("Enetered in Enemy  Collsison");
					collidedObject = collision.gameObject;
					//GetComponent<killShot> ().SwitchCamera ();
					//if (!collision.gameObject.GetComponent<HealthManager> ().playerDead) {
					//	collision.gameObject.GetComponent<HealthManager> ().ApplyDamage(100f,this.gameObject.tag);
					//}
				} else {
					//if(GetComponent<killShot> ().isKillShot){
						NormalizeTime ();
					//}
				}
				Active();
        	}
		}
    }
	public void NormalizeTime(){
		Time.timeScale = 1f;
		//killShot.KillShotGoing = false;
		//transform.Find ("ActionCamera").gameObject.SetActive (false);
		//GameObject.Find ("GameManager").GetComponent<GameSetup> ().ShowUI ();
		//GameObject.Find ("PlayerCamera").transform.GetChild(0).gameObject.SetActive (true);
	}
}
