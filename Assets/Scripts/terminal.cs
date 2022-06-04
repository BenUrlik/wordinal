using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class terminal : MonoBehaviour
{
    public GameObject directoryLine;
    public GameObject responseLine;

    public InputField terminalInput;
    public GameObject userInputLine;
    public ScrollRect sr;
    public GameObject msgList;

    interpreter interp;
    game rules;

    private void Start() {
        interp = GetComponent<interpreter>();
        rules = GetComponent<game>();
    }

    private void OnGUI() {
        if(terminalInput.isFocused && terminalInput.text != "" && Input.GetKeyDown(KeyCode.Return)) {
            // Store user input
            string userInput = terminalInput.text;

            //Clear the input field
            ClearInputField();

            //Instantiate a gameobject with a directory prefix
            AddDirectoryLine(userInput);

            //Add the interpreter lines
            int lines = 0;
            if(SceneManager.GetActiveScene().name == "PlayScene") { lines = AddInterpreterLines(rules.processGame(userInput)); }
            else if(SceneManager.GetActiveScene().name == "TerminalScene") { lines = AddInterpreterLines(interp.Interpret(userInput)); }

            //Scroll to the bottom of the scrollRect
            ScrollToBottom(lines);

            //Move the user input line to the end
            userInputLine.transform.SetAsLastSibling();

            //Refocus the input field
            terminalInput.ActivateInputField();
            terminalInput.Select();
        }
    }

    void ClearInputField() {
        terminalInput.text = "";
    }

    void AddDirectoryLine(string userInput) {
        //Resizing the command line container, so the scrollRect doesnt break
        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x, msgListSize.y + 44.0f);

        //Instantiate the directory line.
        GameObject msg = Instantiate(directoryLine, msgList.transform);

        //Set its child index.
         msg.transform.SetSiblingIndex(msgList.transform.childCount - 1);

        //Set the text of this new gameObject.
        msg.GetComponentsInChildren<Text>()[1].text = userInput;
    }

    int AddInterpreterLines(List<string> interpretation) {
        for(int i = 0; i < interpretation.Count; ++i) {
            // Instantiate the response line.
            GameObject res = Instantiate(responseLine, msgList.transform);

            // Set it to the end of all the messages
            res.transform.SetAsLastSibling();

            // Get the size of the message list, and resize
            Vector2 listSize = msgList.GetComponent<RectTransform>().sizeDelta;
            msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(listSize.x, listSize.y + 44.0f);

            // Set the text of this response line to be whatever the interpreter string is
            res.GetComponentsInChildren<Text>()[0].text = interpretation[i];
        }

        return interpretation.Count;
    }

    void ScrollToBottom(int lines) {
        if(lines > 4) {
            sr.velocity = new Vector2(0,450);
        }
        else {
            sr.verticalNormalizedPosition = 0;
        }
    }
}
