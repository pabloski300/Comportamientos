using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.AbstractClasses;

namespace Assets.Scripts
{
    public class WaiterAgent : StandardAgent
    {
        State state;
        SubState subState;

        List<Task> taskList;
        Task currentTask;

        TaskOrder taskSort = new TaskOrder();

        AgentMovement movement;

        Dictionary<string, State> taskInterpreter = new Dictionary<string, State>
        {
            { "TakeOrder",State.TakeOrder},
            { "BringGood",State.BringFood}
        };

        enum State
        {
            Free,
            TakeOrder,
            BringFood
        }

        enum SubState
        {
            Idle,
            Moving,
            Acting
        }
        // Use this for initialization
        void Start()
        {
            taskList = new List<Task>();
            movement = GetComponent<AgentMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            Behave();
        }

        protected override void Behave()
        {
            switch (subState)
            {
                case SubState.Idle:
                    if(taskList.Count>0)
                    {
                        state = taskInterpreter[taskList[0].Id];
                        currentTask = taskList[0];
                        taskList.RemoveAt(0);
                        subState = SubState.Moving;
                        movement.moveTo(currentTask.Coordinates);
                    }
                    break;
                case SubState.Moving:
                    break;
                case SubState.Acting:
                    break;
            }
        }

        public override void Notify(Task notification)
        {
            taskList.Add(notification);
            taskList.Sort(taskSort);
        }
    }
}
