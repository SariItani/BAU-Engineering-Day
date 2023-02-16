using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMajor : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;
    private string major;
    private int majorIndex;
    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<TMPro.TMP_Dropdown>();
        dropdown.value = PlayerPrefs.GetInt("Major Index", 0);
        dropdown.options[dropdown.value].text = PlayerPrefs.GetString("Major", "Computer");
    }
    public void ChoseMajor()
    {
        majorIndex = dropdown.value;
        major = dropdown.options[majorIndex].text;
        PlayerPrefs.SetString("Major", major);
        PlayerPrefs.SetInt("Major Index", dropdown.value);
        Debug.Log(major);
        Debug.Log(majorIndex);
    }
}
