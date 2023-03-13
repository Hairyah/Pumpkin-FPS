using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelMenu;
    [SerializeField] GameObject newRun;
    [SerializeField] GameObject selectLevel;
    [SerializeField] GameObject quit;

    void Start()
    {
        levelMenu.SetActive(false);

        mainMenu.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        mainMenu.transform.GetChild(0).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
    }

    void Update()
    {
        // LA IL FAUDRA METTRE EN CONDITION LE INPUT.GETAXIS QUI CORRESPOND AU CLIC GAUCHE
        // DE LA SOURIS ET LE BOUTTON DE TIR/A SUR LA MANETTE SI ON VEUT QUE LE JEU
        // PUISSE SE JOUER AU CLAVIER ET A LA SOURIS ET AUSSI A LA MANETTE
        if (Input.GetMouseButtonDown(0))
        {
            GuiSelect();
        }
    }

    private void GuiSelect()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        Debug.Log(pointer);

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (RaycastResult go in raycastResults)
            {
                if (go.gameObject.tag == "UiButton")
                {
                    if (go.gameObject.name == "NewRun")
                    {
                        SceneManager.LoadScene("Cimetiere");
                    }
                    else if (go.gameObject.name == "SelectLevel")
                    {
                        newRun.SetActive(false);
                        selectLevel.SetActive(false);
                        quit.SetActive(false);
                        levelMenu.SetActive(true);
                    }
                    else if (go.gameObject.name == "Quit")
                    {
                        Application.Quit();
                    }
                    else if (go.gameObject.name == "Cimetiere")
                    {
                        SceneManager.LoadScene("Cimetiere");
                    }
                    else if (go.gameObject.name == "Cryptes")
                    {
                        SceneManager.LoadScene("Cryptes");
                    }
                    else if (go.gameObject.name == "Return")
                    {
                        levelMenu.SetActive(false);
                        newRun.SetActive(true);
                        selectLevel.SetActive(true);
                        quit.SetActive(true);
                    }
                }
            }
        }
    }
}
