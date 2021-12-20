using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

//Displays the all time best player from the playerList.
public class AllTimeBest : MonoBehaviour
{
    public enum DisplayType {
        NAME,
        SCORE
    }

    [SerializeField] private PlayerListSO playerList = null;
    [SerializeField] private DisplayType displayType = DisplayType.NAME;
    private Text displayedText;

    private void Awake() {
        displayedText = gameObject.GetComponent<Text>();
    }

    private void Update() {
        ulong score = 0;
        string playerName = "";

        if(playerList != null && playerList.playerList.Count > 0) {
            score = playerList.playerList.Select(x => x.hiScore).Max<ulong>();
            playerName = playerList.playerList.Find(x => x.hiScore == score).playerName;
        }

        switch(displayType) {
            case DisplayType.NAME:
            displayedText.text = playerName;
            break;
            case DisplayType.SCORE:
            displayedText.text = score.ToString();
            break;
        }
    }
}
