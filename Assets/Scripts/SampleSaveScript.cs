using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Text;

public class SampleSaveScript : MonoBehaviour
{
    private StreamWriter sw;

    void Start()
    {
        sw = new StreamWriter(@"SaveData.csv", true, Encoding.GetEncoding("Shift_JIS"));
        string[] s1 = { "F", "J", "time" };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    public void SaveData(string txt1, string txt2, string txt3)
    {
        string[] s1 = { txt1, txt2, txt3 };
        string s2 = string.Join(",", s1);
        sw.WriteLine(s2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            sw.Close();
        }

    }
}

