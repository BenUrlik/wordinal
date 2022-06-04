using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    public Hashtable wordleWords;
    public int size = 12947;
    public int attempts;
    public string guessWord;
    public bool gameFinished;
    public bool win;

    public Keyboard keyboard;
    public WordGrid wordGrid;
    public GameObject key;
    private Button keyButton;
    private Button letterButton;
    public GameObject line1;

    void Start() {
        attempts = 5;
        gameFinished = false; 
        win = false;
        wordleWords = new Hashtable();
        string filePath = Application.streamingAssetsPath + "/words" + ".txt";
        
        string[] readText = File.ReadAllLines(filePath);
        int i = 1;
        foreach (string s in readText)
        {
            wordleWords.Add(i, s);
            ++i;
        }

        int rnd_num = Random.Range(1, 12947);
        guessWord = wordleWords[rnd_num].ToString();
    }

    List<string> response = new List<string>();
        
    public List<string> processGame(string userInput) {
        response.Clear();
        userInput = userInput.ToLower();

        // if you are out of attempts only give the player the option to return to the terminal
        if (gameFinished && userInput == "return") { SceneManager.LoadScene("TerminalScene"); }
        if (win) { response.Add("Congratulations! You guessed the word correctly."); response.Add("Type 'return' to go back to the terminal!"); return response; } 
        
        // parse the word to make sure it is 5 letters
        if(userInput.Length > 5 || userInput.Length < 5) { response.Add("The inputted word must be 5 letters long!"); return response; }
        
        if(!wordleWords.ContainsValue(userInput)) { response.Add("Please enter a valid word!"); return response;}

        // Respond with remaining attempts
        if( userInput != guessWord ) { 

            // Parse the lines and then figure out if the letters are in the word
            
            // Highlighting of the keycodes
            for (int i = 0; i < 5; i++)
            {
                string enteredLetter = userInput.Substring(i, 1);
                enteredLetter = enteredLetter.ToUpper();
                // Debug.Log(enteredLetter);
                //Debug.Log("B" + (i+1) + "A" + attempts);
                keyButton = GameObject.Find(enteredLetter + "Key").GetComponent<Button>();
                letterButton = GameObject.Find("B" + (i+1) + "A" + (attempts)).GetComponent<Button>();
                keyboard.incorrectKey(keyButton);
                wordGrid.incorrectLetter(letterButton);
                for (int j = 0; j < 5; j++)
                {
                    if (enteredLetter == guessWord.Substring(j, 1).ToUpper())
                    {
                        keyboard.maybeKey(keyButton);
                        wordGrid.maybeLetter(letterButton);
                    }
                }
                if (enteredLetter == guessWord.Substring(i, 1).ToUpper())
                {
                    keyboard.correctKey(keyButton);
                    wordGrid.correctLetter(letterButton);
                }
                wordGrid.setText(letterButton, enteredLetter);
            }

            if (attempts == 0)
            {
                response.Add("You have lost Wordinal!"); response.Add("The word was: " + guessWord);
                response.Add("Type 'return' to go back to the terminal!");
                gameFinished = true;
                return response;
            }

            response.Add("You have " + attempts-- + " attempts remaining"); 
            return response; 
        }
        else if( userInput == guessWord ) { 
            for (int i = 0; i < 5; i++)
            {
                keyButton = GameObject.Find(userInput.Substring(i, 1).ToUpper() + "Key").GetComponent<Button>();
                letterButton = GameObject.Find("B" + (i + 1) + "A" + (attempts)).GetComponent<Button>();
                keyboard.correctKey(keyButton);
                wordGrid.correctLetter(letterButton);
                wordGrid.setText(letterButton, userInput.Substring(i, 1).ToUpper());
            }
            response.Add("Congratulations! You guessed the word correctly."); 
            response.Add("Type 'return' to go back to the terminal!");
            gameFinished = true; 
            win = true; 
            return response; 
        }
        else {
            response.Add("something went wrong");
            response.Add("Type 'return' to go back to the terminal!");
            attempts = 0;
            return response;
        }
    }
}
