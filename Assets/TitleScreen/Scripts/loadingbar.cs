using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingbar : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;
    private bool _canStart = false;
    public float speed = 0.0f;
    public GameObject vica; 
    public GameObject textToStart;
   

    // Use this for initialization
    void Start () {
        rectComponent = vica.GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }

    void Update()
    {
        if (! _canStart)
        {
            if (imageComp.fillAmount != 1f)
            {
                imageComp.fillAmount = imageComp.fillAmount + Time.deltaTime * speed;
            
            }
            else 
            { 
                _canStart = true;
                textToStart.SetActive(true);
                this.gameObject.SetActive(false);

            }

            if (_canStart)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    print("space key was pressed");
                }
            }
        }
    }
}
