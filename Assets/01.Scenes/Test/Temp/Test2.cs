using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestSpace {
    public class Test2 : MonoBehaviour {
        private void Awake() {
            GameManager.Instance.Event.RegisterEvent(eEventType.Test, test);
        }

        // Start is called before the first frame update
        void Start() {

        }

        private void test() {
            Debug.Log("sss");
        }
    }
}