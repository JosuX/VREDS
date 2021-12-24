using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour

{
    [SerializeField] GameObject menu, credits, controls;
    [SerializeField]
    private Text NameField, IDField;
    persistentscript ps;

    bool IsDigitsOnly(string id)
    {
        foreach (char c in id)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }
    public void tocredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
        controls.SetActive(false);
    }

    public void tocontrols()
    {
        menu.SetActive(false);
        credits.SetActive(false);
        controls.SetActive(true);
    }

public void tomenu()
    {
        menu.SetActive(true);
        credits.SetActive(false);
        controls.SetActive(false);
    }

    public void playbutton()
    {
        ps = FindObjectOfType<persistentscript>();
        ps.u_name = NameField.text;
        ps.u_ID = IDField.text;
        if (ps.u_name.Length >= 2  && ps.u_ID.Length == 8 && Regex.IsMatch(ps.u_ID, @"^[0-9]+$") && Regex.IsMatch(ps.u_name, @"^[a-zA-Z]+$")){
            SceneManager.LoadScene("MainScene");
           menu.SetActive(false);
            credits.SetActive(false);
            controls.SetActive(false);
        }

    }

    public void doExitGame()
    {
        Debug.Log("Game is exitting...");
        Application.Quit();

    }
}
