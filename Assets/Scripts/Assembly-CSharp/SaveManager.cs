using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public PlayerSave state;

    public static SaveManager Instance
    {
        get;
        set;
    }

    public SaveManager()
    {
    }

    private void Awake()
    {
        Object.DontDestroyOnLoad(base.gameObject);
        SaveManager.Instance = this;
        this.Load();
    }

    public T Deserialize<T>(string toDeserialize)
    {
        return (T)(new XmlSerializer(typeof(T))).Deserialize(new StringReader(toDeserialize));
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("save"))
        {
            this.NewSave();
            return;
        }
        this.state = this.Deserialize<PlayerSave>(PlayerPrefs.GetString("save"));
    }

    public void NewSave()
    {
        this.state = new PlayerSave();
        this.Save();
        MonoBehaviour.print("Creating new save file");
    }

    public void Save()
    {
        PlayerPrefs.SetString("save", this.Serialize<PlayerSave>(this.state));
    }

    public string Serialize<T>(T toSerialize)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        StringWriter stringWriter = new StringWriter();
        xmlSerializer.Serialize(stringWriter, toSerialize);
        return stringWriter.ToString();
    }
}