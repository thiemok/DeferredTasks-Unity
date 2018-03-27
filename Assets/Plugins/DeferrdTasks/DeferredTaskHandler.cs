using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeferredTasks {

    public class DeferredTaskHandler : MonoBehaviour {

        // Static singleton property
        public static DeferredTaskHandler Instance { get; private set; }

        private List<Action> tasks = new List<Action>();

        void Awake() {
          Instance = this;
        }

        // Use this for initialization
        void Start() {
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

        // Update is called once per frame
        void Update() {
            List<Action> currentTasks = this.tasks;
            this.tasks = new List<Action>();
            foreach (Action task in currentTasks) {
                try {
                    task.Invoke();
                } catch (Exception e) {
                    Debug.LogException(e);
                }
            }
        }

        public void AddTask(Action task) {
            this.tasks.Add(task);
        }
    }
}
