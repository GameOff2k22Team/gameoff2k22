using Febucci.UI;
using Scripts.StepTexts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TextManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogTextGameObject;
    [SerializeField] private TextAsset textAsset;

    private StepTexts _stepTexts;
    private  TextAnimatorPlayer _tutoTextAnimator;
    private  TextMeshProUGUI _tutoText;
    
    private string _currentTutoTargetIndicator;
    private bool isTextShowed = false;

    void Start()
    {
        _tutoTextAnimator = dialogTextGameObject.GetComponent<TextAnimatorPlayer>();
        _tutoText = dialogTextGameObject.GetComponent<TextMeshProUGUI>();
        _stepTexts = loadStepTexts(textAsset);

        _tutoTextAnimator.onTextShowed.AddListener(OnTextShowed);

        EventsManager.StartListening(EventsName.StartDialog.ToString(), StartAnimateText);
        //EventsManager.StartListening(StatesEvents.OnMenuIn.ToString(), DeleteText);
   }

    private void DeleteText(Args arg)
    {
        _tutoText.text = "";
    }

    private void StartAnimateText(Args arg)
    {
        StartCoroutine(StartStepTextCoroutine(_stepTexts.steps.Where(x => x.name == arg.message).FirstOrDefault(), _tutoTextAnimator));
    }

    private StepTexts loadStepTexts(TextAsset jsonFile)
    {
        return JsonUtility.FromJson<StepTexts>(jsonFile.text);
    }

    IEnumerator StartStepTextCoroutine(Step step, TextAnimatorPlayer ta)
    {
        var stepIndex = 0;
        while (stepIndex < step.texts.Length)
        {
            isTextShowed = false;
            ta.ShowText(step.texts[stepIndex]);

            while (!isTextShowed)
            {
                yield return 0;
            }
            stepIndex++;
        }
    }

    private void OnTextShowed()
    {
        isTextShowed = true;
    }
}
