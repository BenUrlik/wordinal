using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class interpreter : MonoBehaviour
{
    List<string> response = new List<string>();
    
    public List<string> Interpret(string userInput) {
        response.Clear();
        userInput = userInput.ToLower();
        string[] args = userInput.Split();
        int size = 0; 
        foreach(string str in args) { ++size; }
        Debug.Log(size);

        if(args[0] == "help" || args[0] == "commands") {
            response.Add("Help/Commands: Gives a list of commands");
            response.Add("Start Game: Starts the Worldinal Game");
            response.Add("Credits: Will list out the best, most handsome, startest, and most creative creators of Wordinal!");
            response.Add("Quit: Exits the game. Booooooo!");
            return response;
        }
        else if(args[0] == "credits") {
            response.Add("Gurkirat Saini");
            response.Add("Ben Urlik");
            return response;
        }
        else if(args[0] == "quit") {
            // UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
            return response;
        }
        else if(size > 1 && args[0] == "start" && args[1] == "game") {
            response.Add("This will start the game!");
            SceneManager.LoadScene("PlayScene");
            return response;
        }
        else {
            response.Add("Command not recognized. Type for help for a list of commands.");
            return response;
        }
    }
}
