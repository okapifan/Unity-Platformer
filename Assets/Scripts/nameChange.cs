using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameChange : MonoBehaviour
{
    public InputField mainInputField;
    private GameObject player;
    private GameObject playerLabel;
    public DataManager dataManager;

    public void Start()
    {
        playerLabel = GameObject.Find("player_label");
        player = playerLabel.transform.parent.gameObject;
        mainInputField.text = playerLabel.GetComponent<TextMesh>().text;
        //Adds a listener to the main input field and invokes a method when the value changes.
        mainInputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck()
    {
        playerLabel.GetComponent<TextMesh>().text = mainInputField.text;
        dataManager.data.name = mainInputField.text;
        player.GetComponent<PlayerMovement>().player_name = mainInputField.text;
        dataManager.save();
    }
}
