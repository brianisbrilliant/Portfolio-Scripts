using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BFButtonTest : MonoBehaviour {

	bool isRed = false;
	[Tooltip("This is the name of the parameter that you want this button to affect.")]
	public string parameterName;
	[Tooltip("This is the object that has the animator you want to affect.")]
	public Animator animator;

	[Tooltip("If you are using triggers, this should be off. if you are using an int, this should be on.")]
	public bool usingStates = false;

	private int currentState = 0;
	public int highestState = 2;

	public void OnButtonDown(Hand givenHand) {
		ColorSelf(Color.cyan);
		givenHand.TriggerHapticPulse(1000);
		if(usingStates) {
			currentState++;
			if(currentState > highestState) {
				currentState = 0;
			}
			animator.SetInteger(parameterName, currentState);
		} else {
			animator.SetTrigger(parameterName);
		}
	}

	public void OnButtonUp() {
		ColorSelf(Color.white);
	}

	public void OnButtonPressed() {
		if(isRed) ColorSelf(Color.blue);
		else ColorSelf(Color.red);
		isRed = !isRed;
	}

	private void ColorSelf(Color newColor)
	{
		Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
		for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
		{
			renderers[rendererIndex].material.color = newColor;
		}
	}
	

	/*
		// public void OnButtonDown(Hand fromHand)
		// {
		// 	ColorSelf(Color.cyan);
		// 	fromHand.TriggerHapticPulse(1000);
		// }

		// public void OnButtonUp(Hand fromHand)
		// {
		// 	ColorSelf(Color.white);
		// }

		// private void ColorSelf(Color newColor)
		// {
		// 	Renderer[] renderers = this.GetComponentsInChildren<Renderer>();
		// 	for (int rendererIndex = 0; rendererIndex < renderers.Length; rendererIndex++)
		// 	{
		// 		renderers[rendererIndex].material.color = newColor;
		// 	}
		// }
	*/

}
