using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject spawnerPrefab;
    GameObject player;
    GameObject spawner;
    private Vector3 playerPosition;
    private Vector3 spawnerPosition;

    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;

    private string sceneName;

    // IL FAUDRA CHECK LES CONDITIONS DE VICTOIRE ET DE DÉFAITE
    // ET APPELER CES VARIABLES PUBLIQUES DEPUIS LE CHARACTER.CONTROLLER
    public bool hasLose;
    public bool hasWin;

    // DITHER EFFECT

    public Material ditherMat;
    public Material thresholdMat;

    void Start()
    {
        hasLose = true;
        hasWin = false;

        winMenu.SetActive(false);
        loseMenu.SetActive(false);

        sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Cryptes":
                playerPosition = new Vector3(-53f, 0, 21);
                spawnerPosition = new Vector3(-14.5f, 0, 22.5f);
                break;

            case "Cimetiere":
                playerPosition = new Vector3(-73f, 8f, -16.5f);
                spawnerPosition = new Vector3(5f, 0, -15f);
                break;
        }

        InitAll();
    }

    void Update()
    {
        if (hasLose || hasWin)
        {
            RefreshAll();
            if (hasLose)
            {
                loseMenu.SetActive(true);
            }
            else if (hasWin)
            {
                winMenu.SetActive(true);
            }

            // LA IL FAUDRA METTRE EN CONDITION LE INPUT.GETAXIS QUI CORRESPOND AU CLIC GAUCHE
            // DE LA SOURIS ET LE BOUTTON DE TIR/A SUR LA MANETTE SI ON VEUT QUE LE JEU
            // PUISSE SE JOUER AU CLAVIER ET A LA SOURIS ET AUSSI A LA MANETTE
            if (Input.GetMouseButtonDown(0))
            {
                GuiSelect();
            }
        }
    }

    private void InitAll()
    {
        hasLose = false;
        hasWin = false;
        Destroy(player);
        player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        Camera.main.gameObject.AddComponent<Dither>();
        Camera.main.gameObject.GetComponent<Dither>().ditherMat = ditherMat;
        Camera.main.gameObject.GetComponent<Dither>().thresholdMat = thresholdMat;
        Camera.main.gameObject.GetComponent<Dither>().cam = Camera.main;
        Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
        spawner = Instantiate(spawnerPrefab, spawnerPosition, Quaternion.identity);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RefreshAll()
    {
        /* /.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\ *
        *                                                                           *
        *   PENSER A METTRE UN GAMEOBJECT INVISIBLE AVEC UN COLLIDER POUR FAIRE     *
        *   UN PLAFOND POUR LE NIVEAU DANS LA CRYPTE, ET FAIRE UN PLAFOND ET        *
        *   ET DES MURS QUI ENTOURENT LA ZONE DE JEU DANS LE NIVEAU DU CIMETIERE    *
        *                                                                           *
        *   COMME CA LES PROJECTILES DES ENNEMIS SERONT FORCÉMENT DESTROY A UN      *
        *   MOMENT ET ON EVITERA APRES PLUSIEURS RELOAD DU NIVEAU UNE POSSIBILITÉ   *
        *   OU ON SE RETROUVE AVEC UNE SURCHARGE DE GAME OBJECT ET DE MONOBEHAVIOUR *
        *                                                                           *
        * /.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\ */
        Destroy(spawner);
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<CharaterController>().enabled = false;
        player.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Gun>().enabled = false;
    }

    private void GuiSelect()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        /* /.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\ *
        *                                                                           *
        *   PENSER A BIEN PARAMETRER LES BUILD SETTINGS POUR LOAD CORRECTEMENT      *
        *   LES SCENES (Menu => File => BuildSettings)                              *
        *                                                                           *
        * /.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\/.\ */

        if (raycastResults.Count > 0)
        {
            foreach (RaycastResult go in raycastResults)
            {
                if (go.gameObject.tag == "UiButton")
                {
                    if (go.gameObject.name == "QuitButton")
                    {
                        SceneManager.LoadScene("MainMenu");
                    }
                    else if (go.gameObject.name == "RestartButton")
                    {
                        go.gameObject.transform.parent.gameObject.SetActive(false);
                        InitAll();
                    }
                    else if (go.gameObject.name == "NextButton")
                    {
                        if (sceneName == "Cimetiere")
                        {
                            SceneManager.LoadScene("Cryptes");
                        }
                        else if (sceneName == "Cryptes")
                        {
                            SceneManager.LoadScene("MainMenu");
                        }
                    }
                }
            }
        }
    }
}
