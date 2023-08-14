using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

namespace VikingCrew.Tools.UI
{
    public class SpeechBubbleTmp : SpeechBubbleBase
    {
        [SerializeField] private TextMeshProUGUI _text;
        private TextAnimatorPlayer textAnimator;
        protected override void SetTextAlpha(float alpha)
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
        }

        protected override void SetText(string text)
        {
            textAnimator = _text.GetComponent<TextAnimatorPlayer>();
            _text.text = text;
            textAnimator.ShowText(text);
        }

        public bool CheckTextDisplayed()
        {
            return !textAnimator.isBaseInsideRoutine;
        }

        public void DisplayAllText()
        {
            textAnimator.SkipTypewriter();
        }
    }
}