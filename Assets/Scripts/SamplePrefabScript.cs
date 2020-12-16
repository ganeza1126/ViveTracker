using UnityEngine;
using System.IO;

public class SamplePrefabScript : MonoBehaviour
{
    private float time;
    private StreamWriter sw;

    GameObject SaveCsv;
    SampleSaveScript SampleSaveScript;

    void Start()
    {
        SaveCsv = GameObject.Find("SaveCsv");
        SampleSaveScript = SaveCsv.GetComponent<SampleSaveScript>();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F))
        {
            SampleSaveScript.SaveData("F", " ", time.ToString());
        }
    }
}


