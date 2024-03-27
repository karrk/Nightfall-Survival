using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestSpace {
    public class Test1 : MonoBehaviour {
        // Start is called before the first frame update
        void Start() {
            GameManager.Instance.Event.CallEvent(eEventType.Test);
        }

        // Update is called once per frame
        void Update() {

        }
    }
}