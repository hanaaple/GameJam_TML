using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Option
{
    public readonly KeyCode keyCode;
    public readonly string name;

    public Option(KeyCode keyCode, string name)
    {
        this.keyCode = keyCode;
        this.name = name;
    }

}

[System.Serializable]
public class Dialogue
{
    public readonly string talkerName;
    public readonly Sprite image;
    [TextArea] public readonly string text;
    public readonly List<Option> optionList;

    public Dialogue(string talkerName, Sprite image, string text)
        : this(talkerName, image, text, new List<Option>()) { }

    public Dialogue(string talkerName, Sprite image, string text, List<Option> optionList)
    {
        this.talkerName = talkerName;
        this.image = image;
        this.text = text;
        this.optionList = optionList;
    }
}

public class DialogManger : MonoBehaviour
{
    private Text dialogContextText;
    public GameObject imageGO;
    private Image image;
    public GameObject dialogBox; // for active setting
    private Text talker;
    private Text dialogText;
    public GameObject endTalkCursor;


    public DialogContent dialogContent;

    [SerializeField] private int CHAR_PER_SECOND = 14;
    private IEnumerator dialogGenerator;
    private List<Option> availableFlow;
    private string lastStory;
    private Coroutine currentDialog = null;

    void Start()
    {
        imageGO.SetActive(true);
        dialogBox.SetActive(true);
        endTalkCursor.SetActive(true);

        // unity get gameobject and component  
        // image = GameObject.Find("DialogImage").GetComponent<Image>();
        // dialogBox = GameObject.Find("DialogBox");
        // EndTalkCursor = GameObject.Find("EndTalkCursor");
        talker = GameObject.Find("Talker").GetComponent<Text>();
        dialogText = GameObject.Find("DialogText").GetComponent<Text>();

        // inital setting for update method
        dialogGenerator = ShowNextDialog("main");
        availableFlow = new List<Option>();
        availableFlow.Add(new Option(KeyCode.Space, "main"));
        lastStory = "main";
    }
    void Update()
    {
        foreach (Option o in availableFlow)
        {
            if (Input.GetKeyDown(o.keyCode))
            {
                if (lastStory != o.name)
                {
                    dialogGenerator = ShowNextDialog(o.name);
                    lastStory = o.name;
                }

                // go to next dialogue
                bool hasDialog = dialogGenerator.MoveNext();


                if (hasDialog)
                {
                    List<Option> newOption = dialogGenerator.Current as List<Option>;
                    if (newOption.Count != 0)
                    {
                        availableFlow = newOption;
                    }
                }
                else // 대화 끝
                {
                    // 숨김
                    imageGO.SetActive(false);
                    dialogBox.SetActive(false);
                    endTalkCursor.SetActive(false);

                    Destroy(this.gameObject);

                    // SceneManager.LoadScene("SampleScene")
                }
            }
        }
    }

    IEnumerator<List<Option>> ShowNextDialog(string forkKey)
    {
        foreach (Dialogue dialog in dialogContent.dialogDic[forkKey])
        {
            if (currentDialog != null)
            {
                StopCoroutine(currentDialog);
            }
            talker.text = dialog.talkerName;
            image.sprite = dialog.image;
            currentDialog = StartCoroutine(appearText(dialog.text));
            yield return dialog.optionList;
        }
    }

    IEnumerator appearText(string dialog)
    {
        dialogText.text = "";
        endTalkCursor.SetActive(false);
        foreach (char c in dialog)
        {
            dialogText.text += c;
            yield return new WaitForSeconds(1.0f / CHAR_PER_SECOND);
        }
        endTalkCursor.SetActive(true);
    }
}
