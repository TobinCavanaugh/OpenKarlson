using System;
using UnityEngine;
using UnityEngine.UI;

public class GE_UIResponder : MonoBehaviour
{
    public string m_PackageTitle = "-";

    public string m_TargetURL = "www.unity3d.com";

    public GE_UIResponder()
    {
    }

    public void OnButton_AssetName()
    {
        Application.OpenURL(this.m_TargetURL);
    }

    private void Start()
    {
        GameObject mPackageTitle = GameObject.Find("Text Package Title");
        if (mPackageTitle != null)
        {
            mPackageTitle.GetComponent<Text>().text = this.m_PackageTitle;
        }
    }

    private void Update()
    {
    }
}