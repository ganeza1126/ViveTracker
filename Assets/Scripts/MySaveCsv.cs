using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MySaveCsv : MonoBehaviour
{
    private string filePath = "/Resources/save.txt";
    private string path;
    private StreamWriter sw;

    private bool isRec = false;
    private GameObject activeObj;

    private static float countTime;
    private int datanum = 7;
    private int childCount;

    void Start()
    {
        //obj取得
        activeObj = this.gameObject;
        childCount = activeObj.transform.childCount;

    }
    private void Update()
    {
        countTime += Time.deltaTime;

        if (isRec)
        {
            addline();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                FinishRec();
            }
        }

    }

    void addline()
    {
        string[] str = new string[childCount+1];
        str[0] = countTime.ToString();
        for (int i = 0; i < childCount; i++)
        {
            GameObject childObj = activeObj.transform.GetChild(i).gameObject;
            str[i+1] = getInfo(childObj);
        }
        sw.WriteLine(string.Join(",", str));
    }

    string getInfo(GameObject Obj)
    {
        //オブジェクトの名前位置回転拡縮をリストで返す
        string[] info = new string[7];
        //プレハブのオリジナルのオブジェクトの名前を取得
        info[0] = Obj.transform.position.x.ToString();
        info[1] = Obj.transform.position.y.ToString();
        info[2] = Obj.transform.position.z.ToString();
        info[3] = Obj.transform.rotation.x.ToString();
        info[4] = Obj.transform.rotation.y.ToString();
        info[5] = Obj.transform.rotation.z.ToString();
        info[6] = Obj.transform.rotation.w.ToString();
        //        info[8] = Obj.transform.localScale.x.ToString();
        //        info[9] = Obj.transform.localScale.y.ToString();
        //        info[10] = Obj.transform.localScale.z.ToString();
        return string.Join(",", info);
    }

    void StartRec()
    {
        //録画開始
        isRec = true;
        countTime = 0;

        //保存先指定
        path = Application.dataPath + filePath;
        Debug.Log("Start Recorad Data To " + path);
        sw = new StreamWriter(path, true);
        //title設定
        string[] title = new string[datanum * childCount+1];
        title[0] = "time";
        for (int i = 0; i < childCount; i++)
        {
            GameObject childObj = activeObj.transform.GetChild(i).gameObject;
            GameObject original = PrefabUtility.GetCorrespondingObjectFromOriginalSource(childObj);
            if (original == null)
            { title[datanum*i+1] = childObj.name; }
            else
            { title[datanum*i+1] = original.name; }
            for(int k = 1; k < datanum; k++)
            {
                title[datanum * i + k+1] = " ";
            }
        }
        sw.WriteLine(string.Join(",",title));
        Debug.Log("title nakami" + title);
    }

    void FinishRec()
    {
        sw.Flush();
        sw.Close();
        isRec = false;
    }

}
