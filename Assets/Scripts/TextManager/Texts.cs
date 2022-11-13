using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.StepTexts
{
    [System.Serializable]
    public class StepTexts
    {
        public Step[] steps;
    }

    [System.Serializable]
    public class Step
    {
        public string name;
        [SerializeField]
        public string[] texts;
    }

}
