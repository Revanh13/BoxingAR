using UnityEngine;

public class FistCollider : MonoBehaviour
{
	public GameObject player;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnCollisionEnter(Collision collision)
	{
		//кулак ударился с врагом
		if (collision.gameObject.tag == "evil")
		{
			GameObject evil = collision.gameObject;

			evil.GetComponent<Boxer>().GotHit(player.GetComponent<Boxer>().currentAttackMode);


		}
		if (collision.gameObject.tag == "shield")
		{
			//удар по щиту
		}
	}
}
