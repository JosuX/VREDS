using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class violation_tracker : MonoBehaviour
{
    private List<string> overspeeding = new List<string>();
    private List<string> schoolzone = new List<string>();
    private List<string> invalid_turn = new List<string>();
    private List<string> minor = new List<string>();
    private List<string> stoplight = new List<string>();
    private List<string> illegal_zone = new List<string>();
    private List<string> counterflow = new List<string>();
    public bool Overwrite = true;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField] Sprite yellow, red;
    [SerializeField] GameObject image;
    private persistentscript ps;

    public void Start()
    {
        ListUp();
        ps = FindObjectOfType<persistentscript>();
        
    }

    public void overspeed(string con)
    {
        overspeeding.Add(con);
        ListUp();
        StartCoroutine(Fade(red));
    }

    public void illegal_z(string con)
    {
        illegal_zone.Add(con);
        ListUp();
        StartCoroutine(Fade(red));
    }

    public void counterf(string con)
    {
        counterflow.Add(con);
        ListUp();
        StartCoroutine(Fade(red));
    }

    public void school(string con)
    {
        schoolzone.Add(con);
        ListUp();
        StartCoroutine(Fade(red));
    }

    public void invalid_t(string con)
    {
        invalid_turn.Add(con);
        ListUp();
        StartCoroutine(Fade(red));
    }

    public void min(string con)
    {
        minor.Add(con);
        ListUp();
        StartCoroutine(Fade(yellow));
    }

    public void stop_l(string con)
    {
        stoplight.Add(con);
        ListUp();
        StartCoroutine(Fade(red));
    }

    private void ListUp()
    {
        text.text = "\n\n Stoplight = " + stoplight.Count + "\n Invalid Turns = " + invalid_turn.Count
            + "\n SchoolZone = " + schoolzone.Count + "\n Overspeeding = " + overspeeding.Count + "\n Minor = " + minor.Count + "\n Illegal Zone = " + illegal_zone.Count +
            "\n Counterflow = " + counterflow.Count;
    }
    IEnumerator Fade(Sprite sprite)
    {
        float ft = 0;
        Color c = image.GetComponent<Image>().color;
        image.GetComponent<Image>().sprite = sprite;
        while (ft < 1)
        {
            c.a = ft;
            image.GetComponent<Image>().color = c;
            ft += Time.deltaTime;
            yield return null;
        }

        while (ft > 0)
        {
            c.a = ft;
            image.GetComponent<Image>().color = c;
            ft -= Time.deltaTime;
            yield return null;
        }

    }

    public void export_csv()
    {
        string projectDirectory;
        ps = FindObjectOfType<persistentscript>();
        if (ps == null)
        {
            
            projectDirectory = Application.dataPath + "/RecentViolationCompilation.csv";
        }
        else
        {
            projectDirectory = Application.dataPath + "/" + ps.u_name +"_" + ps.u_ID + ".csv";
        }

        var ranges = new List<int>();
        ranges.Add(overspeeding.Count);
        ranges.Add(schoolzone.Count);
        ranges.Add(invalid_turn.Count);
        ranges.Add(minor.Count);
        ranges.Add(stoplight.Count);
        ranges.Add(counterflow.Count);
        ranges.Add(illegal_zone.Count);

        if (!Overwrite)
        {
            if(File.Exists(projectDirectory))
            {
                print("Cancelled Export Because File Exists");
                return;
            }
            print("File does not exist, Generating Compilation");
            string delimiter = ",";
            string write_header = "Overspeeding" + delimiter + "School Zone" + delimiter + "Invalid Turns" + delimiter + "Minor Violations" + delimiter + "Stoplight" + delimiter + "Illegal Zone" + delimiter + "Counterflow" + Environment.NewLine;
            File.WriteAllText(projectDirectory, write_header);

            for (int i = 0; i <= ranges.Max(); i += 1)
            {
                overspeeding.Add("");
                schoolzone.Add("");
                invalid_turn.Add("");
                minor.Add("");
                stoplight.Add("");
                counterflow.Add("");
                illegal_zone.Add("");
                string append_row = overspeeding[i].Replace(',', '|') + delimiter + schoolzone[i].Replace(',', '|') + delimiter + invalid_turn[i].Replace(',', '|') + delimiter + minor[i].Replace(',', '|') + delimiter + stoplight[i].Replace(',', '|') + delimiter + illegal_zone[i].Replace(',', '|') + delimiter + counterflow[i].Replace(',', '|') + Environment.NewLine;
                File.AppendAllText(projectDirectory, append_row);
            }
        }
        else if (Overwrite)
        {
            print("If file exists, it will be overwritten");
            File.Delete(projectDirectory);
            string delimiter = ",";
            string write_header = "Overspeeding" + delimiter + "School Zone" + delimiter + "Invalid Turns" + delimiter + "Minor Violations" + delimiter + "Stoplight" + delimiter + "Illegal Zone" + delimiter + "Counterflow" + Environment.NewLine;
            File.WriteAllText(projectDirectory, write_header);

            for (int i = 0; i <= ranges.Max(); i += 1)
            {
                overspeeding.Add("");
                schoolzone.Add("");
                invalid_turn.Add("");
                minor.Add("");
                stoplight.Add("");
                counterflow.Add("");
                illegal_zone.Add("");
                string append_row = overspeeding[i].Replace(',', '|') + delimiter + schoolzone[i].Replace(',', '|') + delimiter + invalid_turn[i].Replace(',', '|') + delimiter + minor[i].Replace(',', '|') + delimiter + stoplight[i].Replace(',', '|') + delimiter + illegal_zone[i].Replace(',', '|') + delimiter + counterflow[i].Replace(',', '|') + Environment.NewLine;
                File.AppendAllText(projectDirectory, append_row);
            }
        }
        SceneManager.LoadScene("Game Menu");

    }

}
