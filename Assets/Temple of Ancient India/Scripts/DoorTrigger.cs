using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	public string openAnimName = "Doors01A_Open";
	public string closeAnimName = "Doors01A_Close";
	public float closeDoorTime = 3f; 
        public AudioClip Open;
        public AudioClip Close;
        


	Animator anim;

	bool closed = true;

	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider col)
	{
		if (closed)
		{
			closed = false;
                        GetComponent<AudioSource>().PlayOneShot(Open, 0.8F);
			anim.Play(openAnimName);
			StartCoroutine(CloseDoor());
		}
	}

	IEnumerator CloseDoor()
	{
		yield return new WaitForSeconds(closeDoorTime);
		if (!closed){
			closed = true;
                        GetComponent<AudioSource>().PlayOneShot(Close, 0.8F);
			anim.Play(closeAnimName);

		}
	}
}
