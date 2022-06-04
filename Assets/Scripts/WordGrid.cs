using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGrid : MonoBehaviour
{
    public Button letterButton;
    public Text letterText;

    public void setText(Button letterButton, string letter)
    {
        letterText = letterButton.GetComponentInChildren<Text>();
        letterText.text = letter;
    }

    public void incorrectLetter(Button letterButton) // sets the text color to grey if the letter is not in the word
    {
        var colorVar = letterButton.colors;
        colorVar.normalColor = Color.grey;
        colorVar.highlightedColor = Color.grey;
        colorVar.pressedColor = Color.grey;
        colorVar.selectedColor = Color.grey;
        colorVar.disabledColor = Color.grey;
        letterButton.colors = colorVar;
    }

    public void correctLetter(Button letterButton) // sets the text color to green if the letter is in in the word and in the right spot
    {
        var colorVar = letterButton.colors;
        colorVar.normalColor = Color.green;
        colorVar.highlightedColor = Color.green;
        colorVar.pressedColor = Color.green;
        colorVar.selectedColor = Color.green;
        colorVar.disabledColor = Color.green;
        letterButton.colors = colorVar;
    }

    public void maybeLetter(Button letterButton) // sets the text color to yellow if the letter is in the word but not in the right spot
    {
        var colorVar = letterButton.colors;
        colorVar.normalColor = Color.yellow;
        colorVar.highlightedColor = Color.yellow;
        colorVar.pressedColor = Color.yellow;
        colorVar.selectedColor = Color.yellow;
        colorVar.disabledColor = Color.yellow;
        letterButton.colors = colorVar;
    }

    public void resetLetter(Button letterButton) // resets the text color to white
    {
        var colorVar = letterButton.colors;
        colorVar.normalColor = Color.white;
        colorVar.highlightedColor = Color.white;
        colorVar.pressedColor = Color.white;
        colorVar.selectedColor = Color.white;
        colorVar.disabledColor = Color.white;
        letterButton.colors = colorVar;
    }
}
