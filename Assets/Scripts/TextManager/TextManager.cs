//using Febucci.UI;
//using Scripts.StepTexts;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using TMPro;
//using UnityEngine;
//using UnityEngine.Events;
//using Zenject;

//public class TextManager
//{
//    private readonly StepTexts _stepTexts;
//    private readonly GameManager _gameManager;
//    private readonly PlayerManager _playerManager;
//    private readonly TextAnimatorPlayer _tutoTextAnimator;
//    private readonly TextMeshProUGUI _tutoText;
//    private readonly TextMeshProUGUI _yesNoPanelText;
//    private readonly TutoManager _tutoManager;
//    private readonly StatesManager _statesManager;
    
//    private string _currentTutoTargetIndicator;
//    private bool isTextShowed = false;

//    public TextManager(MenuLoader menuLoader, PrefabsLoaderManager prefabsLoaderManager, GameManager gameManager, PlayerManager playerManager, TutoManager tutoManager, StatesManager statesManager)
//    {
//        //pas utisé dans le projet de jam
//        //_statesManager = statesManager;
//        //_tutoManager = tutoManager;
//        //_playerManager = playerManager;
//        //_gameManager = gameManager;


//        //le text animator du game object qui contient le component textmeshPro et TextAnimatorPlayer
//        //dans ton cas tu devras placer le text en fonction de la position de la bulle
//        _tutoTextAnimator = menuLoader.TutoText.GetComponent<TextAnimatorPlayer>();
//        //le component  TextMeshProUGUI pour recuperer ou set le text : _tutoText.text = "ta grand mère le concombre"
//        _tutoText = menuLoader.TutoText.GetComponent<TextMeshProUGUI>();
//        // un autre text d'un autre game object toi tu en aura besoin que d'un seul
//        _yesNoPanelText = menuLoader.YesNoPopupText.GetComponent<TextMeshProUGUI>();

//        // ici c'est important, on vas aller load tout les dialogues qui proviennent d'un fichier json
//        // le parametre est un TextAsset que j'ai drag and drop dans un scriptableObject, je te laisse choisir comment toi
//        // tu le recuperera
//        _stepTexts = loadStepTexts(prefabsLoaderManager.StepTextsLoader.StepTextsJson);

//        //ici c'est un event que j'appel a chaque fois qu'un text du tuto a fini d'etre animé
//        _tutoTextAnimator.onTextShowed.AddListener(OnTextShowed);

//        //StartTutoText() est appelée quand le state change en State.Tuto
//        EventsManager.StartListening(StatesEvents.OnTutoIn.ToString(), StartTutoText);
//        //quand je repasse dans le menu je delete le text avec DeleteText()
//        EventsManager.StartListening(StatesEvents.OnMenuIn.ToString(), DeleteText);
//        //pas utilisé dans notre projet
//        EventsManager.StartListening(TutoManager.ON_TARGET_IS_LOOKED_EVENT_NAME.ToString(), ChangeLookedTargetAmountText);
//        //quand un des step de mon tuto est terminé je relance StartTutoText avec un autre paramètre string (qui est la key de la bulle de dialogue en gros)
//        EventsManager.StartListening(TutoManager.ON_FINISH_TARGET_STEP_EVENT_NAME.ToString(), StartTutoText);
//        //pareil ici
//        EventsManager.StartListening(TutoManager.ON_FINISH_MOVE_ON_AREA.ToString(), StartTutoText);
//        //ici c'est une popup avec deux bouttons yes, no on s'en branle
//        EventsManager.StartListening(MenuManager.ON_SHOW_YESNOPOPUP_EVENT_NAME.ToString(), ChangeYesNoPanelText);
//    }

//    private void DeleteText(Args arg)
//    {
//        //je degage le text (ici quand on change de state je degage le text pour pas qu'il gene)
//        _tutoText.text = "";
//    }

//    private void StartTutoText(Args arg)
//    {
//        // ici concentre toi 
//        //c'est cette methode que tu vas appelé avec en paramètre avec le bon nom du dialogue que tu veux faire apparaitre(oublie pas de la rename)
//        //je demarre une coroutine
//        _gameManager.StartCoroutine(
//            // la coroutine en question
//            // prends en premier paramètre un objet Step qui contient les text du dialogue, cet objet et recuperer avec sa key unique : "name"
//            //en deuxième paramètre il prends le text animator du text que tu veux animé (par animé, j'entends faire apparaitre le text progressivement )
//            StartStepTextCoroutine(_stepTexts.steps.Where(x => x.name == arg.message).FirstOrDefault(), _tutoTextAnimator));
//    }

//    private void ChangeYesNoPanelText(Args arg)
//    {
//        //on s'en branle
//        _yesNoPanelText.text = _stepTexts.steps.Where(x => x.name == arg.message).FirstOrDefault().texts[0];
//    }

//    private void ReplaceStaticsTagsFromTexts(Step step)
//    {
//        //on s'en branle
//        for (var i = 0; i < step.texts.Length; i++)
//        {
//            if (!string.IsNullOrEmpty(step.texts[i]))
//            {

//                step.texts[i] = step.texts[i].Replace("<UserName/>", _playerManager.PlayerName);
//                step.texts[i] = step.texts[i].Replace("<TutoTargets/>", "<br><green>" + _tutoManager.TotalLookedTargets.ToString() + "/4</green>");
//                _currentTutoTargetIndicator = _tutoManager.TotalLookedTargets.ToString() + "/4";
//            }
//        }
//    }

//    private StepTexts loadStepTexts(TextAsset jsonFile)
//    {
//        // ce return renvoie un objet StepTexts qui contient lui meme un Array de Step, et Step contient la key et les texts du dialogue que tu veux faire apparaitre
//        return JsonUtility.FromJson<StepTexts>(jsonFile.text);
//    }

//    IEnumerator StartStepTextCoroutine(Step step, TextAnimatorPlayer ta)
//    {
//        //on s'en branle
//        // ReplaceStaticsTagsFromTexts(step);

//        //ici je commence l'animation 
//        //oublie qu'un Step contient plusieurs texts , par exemple le Step avec comme key(step.name) "Salutation" contient le tableau de texts |"bojour monsieur connard", "ca va ?]
//        // de cette manière tu anim en une seul bulle de dialogue plusieurs phrase, un peu comme dans un zelda
//        var stepIndex = 0;
//        while (stepIndex < step.texts.Length)// t'en que l'index ne depasse pas la length du tableau de text, on évite un out of range exeption
//        {
//            // quand le text de Step[stepIndex] a terminé d'etre affiché
//            //rappel toi a la ligne 49 je fais  _tutoTextAnimator.onTextShowed.AddListener(OnTextShowed);
//            // donc l'animator appel la méthode OnTextShowed() une fois que le text de Step[stepIndex] à completement était appelé

//            isTextShowed = false;
//            //ici on commence a animé le text qui contient le textAnimator
//            ta.ShowText(step.texts[stepIndex]);

//            while (!isTextShowed)
//            {
//                yield return 0; // et ici on attends que isTextShowed passe a true
//            }
//            stepIndex++;// le text a fini d'etre affiché, j'incremente le step et on reviens ligne 119 pour animé le prochain text
//        }

//        //ca on s'en branle
//        if (_statesManager.CurrentState is States.Tuto)
//        {
//            _tutoManager.NextStep();
//        }
//    }

//    private void OnTextShowed()
//    {
//        isTextShowed = true;
//    }

//    private void ChangeLookedTargetAmountText(Args args)
//    {
//        // et ca on s'en branle
//        var newText = _tutoText.text.Replace(_currentTutoTargetIndicator, "<green>" + _tutoManager.TotalLookedTargets.ToString() + "/4</green>");
//        _currentTutoTargetIndicator = _tutoManager.TotalLookedTargets.ToString() + "/4";
//        _tutoText.text = newText;
//    }
//}
