using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class AnimalController : MonoBehaviour {

	private NavMeshAgent _agent;

	public GameObject Player;

	public float EnemyDistanceRun;

	public Animator anim;

	void Start()
	{
		_agent=GetComponent<NavMeshAgent>();

		anim = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		
		flee();
	}


	void flee ()
	{
		float distance = Vector3.Distance (transform.position, Player.transform.position);

	//	Debug.Log ("distance: " + distance);

		//run away
		if (distance <= EnemyDistanceRun) {
			//add if statement for animation and animator 
			Vector3 dirToPlayer = transform.position - Player.transform.position;

			Vector3 newPos = transform.position + dirToPlayer;
			anim.SetBool ("run", true);
			_agent.SetDestination (newPos);
	
		} 
		else 
		{
			anim.SetBool("run",false);
		}

	}
}
