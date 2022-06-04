using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public GameObject key;
    public Button keyButton;
    public Text keyText;

    void OnGUI()
    {
        Event e = Event.current;
        //Debug.Log(e.keyCode.ToString().Length);
        if (e.isKey && (e.keyCode.ToString().Length == 1))
        {
            //Debug.Log("Detected key code: " + e.keyCode);
            key = GameObject.Find(e.keyCode + "Key");
            keyButton = key.GetComponent<Button>();
            if (e.type == EventType.KeyDown)
                highlightKey(keyButton);
            if (e.type == EventType.KeyUp)
                unhighlightKey(keyButton);
        }
    }

    public void highlightKey(Button keyButton) // highlights the keycap when the key is pressed
    {
        var colorVar = keyButton.colors;
        colorVar.normalColor = new Color(0.2f, 0.2f, 0.2f);
        keyButton.colors = colorVar;
    }

    public void unhighlightKey(Button keyButton) // unhighlights the keycap when the key is pressed
    {
        var colorVar = keyButton.colors;
        colorVar.normalColor = new Color(0f, 0f, 0f);
        keyButton.colors = colorVar;
    }

    public void incorrectKey(Button keyButton) // sets the text color to grey if the letter is not in the word
    {
        keyText = keyButton.GetComponentInChildren<Text>();
        keyText.color = Color.grey;
    }

    public void correctKey(Button keyButton) // sets the text color to green if the letter is in in the word and in the right spot
    {
        keyText = keyButton.GetComponentInChildren<Text>();
        keyText.color = Color.green;
    }

    public void maybeKey(Button keyButton) // sets the text color to yellow if the letter is in the word but not in the right spot
    {
        keyText = keyButton.GetComponentInChildren<Text>();
        keyText.color = Color.yellow;
    }

    public void resetKey(Button keyButton) // resets the text color to white
    {
        keyText = keyButton.GetComponentInChildren<Text>();
        keyText.color = Color.white;
    }
}
