using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class NPCController : MonoBehaviour {
	public Transform[] points;
	private int destPoint = 0;

	private NavMeshAgent agent;
	Animator anim;
	Vector2 smoothDeltaPosition = Vector2.zero;
	Vector2 velocity = Vector2.zero;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator> ();

		// Disabling auto-braking allows for continuous movement
		// between points (ie, the agent doesn't slow down as it
		// approaches a destination point).
		agent.autoBraking = true;

		GotoNextPoint();
	}


	void GotoNextPoint() {
		anim.SetBool("ToWalk", true);
		// Returns if no points have been set up
		if (points.Length == 0)
			return;

		// Set the agent to go to the currently selected destination.
		agent.destination = points[destPoint].position;

		// Choose the next point in the array as the destination,
		// cycling to the start if necessary.
		destPoint = (destPoint + 1) % points.Length;

	}


	void Update () { 
		// Choose the next destination point when the agent gets
		// close to the current one.
		if (!agent.pathPending && agent.remainingDistance < 0.5f){
			anim.SetBool("ToWalk", false);
			GotoNextPoint();	
		}
			
	}
}

