using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class Recibir : StateMachineBehaviour {

    public MaitreAgent maitre;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
	    if(maitre.taskNumber!=0)
        {
            if(maitre.currentTask.Id== "MesaLibre" && maitre.world.cola[0].ocupado && !maitre.world.cola[0].cliente.hasAsked)
            {
                maitre.world.cola[0].cliente.Notify(new Task("Sentarse", maitre.currentTask.Coordinates, maitre, Task.Receptor.Cliente));
                maitre.CalculateNavPos(maitre.currentTask.Coordinates.transform.position);
                maitre.currentTask.Coordinates.GetComponent<Mesa>().ChangeState(Mesa.Estado.Seleccionada);
                animator.SetTrigger("Acompañar");
            }
            else if(maitre.currentTask.Id == "ClienteEsperando" && maitre.world.genteDentro<maitre.world.mesas.Count)
            {
                int count = 0;
                while (count < maitre.world.mesas.Count && maitre.world.mesas[count].estadoActual != Mesa.Estado.Libre)
                    count++;
                maitre.currentTask.Emisor.Notify(new Task("Sentarse", maitre.world.mesas[count].gameObject, maitre, Task.Receptor.Cliente));
                maitre.CalculateNavPos(maitre.world.mesas[count].gameObject.transform.position);
                maitre.world.mesas[count].ChangeState(Mesa.Estado.Seleccionada);
                animator.SetTrigger("Acompañar");
            }
            maitre.Completed();
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
