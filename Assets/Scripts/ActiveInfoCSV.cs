using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ActiveInfoCSV : MonoBehaviour
{
    private string filePath = "/Resources/save.txt";

    void Start()
    {
        //Application.dataPathはプロジェクトデータのAssetフォルダまでのパス
        string path = Application.dataPath + filePath;
        Debug.Log("!!!!!!!!!!!!!!!!!!!!!!" + path);
        //inspectorの表示されているオブジェクト１つを取得
        //        GameObject activeObj = (GameObject)Selection.activeObject;
        //add
        GameObject activeObj = this.gameObject;

        int childCount = activeObj.transform.childCount;
        Debug.Log("child count is " + childCount);
        string[][] infoList = new string[childCount + 1][];
        infoList[0] = getInfo(activeObj);
        for (int i = 0; i < childCount; i++)
        {
            GameObject childObj = activeObj.transform.GetChild(i).gameObject;
            infoList[i+1] = getInfo(childObj);
            Debug.Log("aaaaaaaa"+infoList[i+1].Length);
        }
        saveCSV(path, infoList);
    }
    string[] getInfo(GameObject Obj)
    {
        //オブジェクトの名前位置回転拡縮をリストで返す
        string[] info = new string[11];
        //プレハブのオリジナルのオブジェクトの名前を取得
        GameObject original = PrefabUtility.GetCorrespondingObjectFromOriginalSource(Obj);
        if (original == null)
        { info[0] = Obj.name; }
        else
        { info[0] = original.name; }
        info[1] = Obj.transform.position.x.ToString();
        info[2] = Obj.transform.position.y.ToString();
        info[3] = Obj.transform.position.z.ToString();
        info[4] = Obj.transform.rotation.x.ToString();
        info[5] = Obj.transform.rotation.y.ToString();
        info[6] = Obj.transform.rotation.z.ToString();
        info[7] = Obj.transform.rotation.w.ToString();
        info[8] = Obj.transform.localScale.x.ToString();
        info[9] = Obj.transform.localScale.y.ToString();
        info[10] = Obj.transform.localScale.z.ToString();
        
        return info;
    }


    bool saveCSV(string path, string[][] strList)
    {

        try
        {
            using (StreamWriter writer = new StreamWriter(path, true))
            {

                foreach (string[] str2 in strList)
                {
                    writer.WriteLine( string.Join(",",str2) );
                }
                writer.Flush();
                writer.Close();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
        return true;
    }
}
