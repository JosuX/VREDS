using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Master : MonoBehaviour
{
    bool end = false;
    [SerializeField] GameObject endwindow;
    violation_tracker vt;
    string win = "Congratulations For Completing The Course!\nPress R to Run Again\nPress Enter to Export Violations Data";
    string lose = "You Crashed!!\nPress R to Retry\nPress Enter to Export Violations Data";
    Light lighting;

    private void Start()
    {
        Cursor.visible = false;
        endwindow.SetActive(false);
        vt = FindObjectOfType<violation_tracker>();
        endwindow.GetComponentInChildren<TextMeshProUGUI>().text = win;
        lighting = FindObjectOfType<Light>();
        lighting.enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            lighting.enabled = !lighting.enabled;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Game Menu");
        }

        if (!end)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            reload();
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            vt.export_csv();
        }
    }

    public void EndRun(bool completed)
    {
        print("End of Simulation");
        end = true;
        FindObjectOfType<car_scripts>().enabled = false;

        if(completed)
        {
            endwindow.GetComponentInChildren<TextMeshProUGUI>().text = win;
        }
        else
        {
            endwindow.GetComponentInChildren<TextMeshProUGUI>().text = lose;
        }

        endwindow.SetActive(true);
    }

    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
