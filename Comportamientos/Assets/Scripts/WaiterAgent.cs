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
            Moving,
            Acting,
            End
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
            switch (state)
            {
                case State.Free:
                    subState = SubState.Moving;
                    Idle();
                    break;
                case State.TakeOrder:
                    subState = SubState.Moving;
                    SubBehave();
                    break;
                case State.BringFood:
                    subState = SubState.Moving;
                    SubBehave();
                    break;
            }
        }

        protected void SubBehave()
        {
            switch (subState)
            {
                case SubState.Moving:
                    movement.moveTo(currentTask.Coordinates);
                    break;
                case SubState.Acting:
                    break;
                case SubState.End:
                    if (taskList.Count <= 0)
                    {
                        state = taskInterpreter[taskList[0].Id];
                        currentTask = taskList[0];
                        taskList.RemoveAt(0);
                        subState = SubState.Moving;
                        movement.moveTo(currentTask.Coordinates);
                    }
                    else
                    {
                        state = State.Free;
                        currentTask = null;
                    }
                    break;
            }
        }

        protected void Idle()
        {
            switch (subState)
            {
                case SubState.Moving:
                    movement.moveTo(currentTask.Coordinates);
                    break;
                case SubState.Acting:
                    break;
                case SubState.End:
                    break;
            }
        }

        public override void Notify(Task notification)
        {
            if(state == State.Free && currentTask != null)
            {
                currentTask = notification;
                state = taskInterpreter[currentTask.Id];
            }
            else
            {
                taskList.Add(notification);
                taskList.Sort(taskSort);
            }
        }
    }
}
