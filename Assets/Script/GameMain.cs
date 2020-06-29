using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour {
    public Button btnLevel1;
    public Button btnLevel2;
    public Button btnLevel3;

    public Transform panelStart;
    public Transform panelCard;
    public Transform panelOver;


    // Start is called before the first frame update
    void Start(){
        btnLevel1.onClick.AddListener(() => {
            panelStart.gameObject.SetActive(false);
            panelCard.gameObject.SetActive(true);
            initCard(2, 3);
        });
        btnLevel2.onClick.AddListener(() => {
            panelStart.gameObject.SetActive(false);
            panelCard.gameObject.SetActive(true);
            initCard(2, 4);
        });
        btnLevel3.onClick.AddListener(() => {
            panelStart.gameObject.SetActive(false);
            panelCard.gameObject.SetActive(true);
            initCard(2, 5);
        });
        Button btnRestart = panelOver.Find("btn_restart").GetComponent<Button>();
        btnRestart.onClick.RemoveAllListeners();
        btnRestart.onClick.AddListener(goGameStartPage);

        Button btnReturn = panelCard.Find("btn_home").GetComponent<Button>();
        btnReturn.onClick.RemoveAllListeners();
        btnReturn.onClick.AddListener(goGameStartPage);
    }

    // Update is called once per frame
    void Update(){
    }

    void initCard(int row, int col){
        //1.加载卡牌图片
        Debug.LogError("row："+row+" col："+col);
        Sprite[] sps = Resources.LoadAll<Sprite>("");
        //2.计算需要加载卡牌的数量
        int totalCount = row * col / 2;

        Debug.LogError("计划加载："+totalCount);
        //3.计算随机加载卡牌的索引
        List<Sprite> spsList = new List<Sprite>();
        for (int i = 0; i < sps.Length; i++) {
            spsList.Add((sps[i]));
        }

        List<Sprite> needShowCardList = new List<Sprite>();
        while (totalCount > 0) {
            int randomIndex = Random.Range(0, spsList.Count);
            needShowCardList.Add(spsList[randomIndex]);
            needShowCardList.Add(spsList[randomIndex]);
            spsList.RemoveAt(randomIndex);
            totalCount--;
        }

        for (int i = 0; i < needShowCardList.Count; i++) {
            // Debug.LogError(needShowCardList[i].name);
        }

        //4.显示卡牌到UI上
        Transform contentRoot = panelCard.Find("Panel");
        // int maxCount = Mathf.Max(contentRoot.childCount, needShowCardList.Count);
        int maxCount = Mathf.Max(contentRoot.childCount, needShowCardList.Count);
        GameObject itemPrefab = contentRoot.GetChild(0).gameObject;
        Debug.LogError("maxCount："+maxCount);

        for (int i = 0; i < maxCount; i++) {
            GameObject itemObject = null;
            if (i < contentRoot.childCount) {
                itemObject = contentRoot.GetChild(i).gameObject;
            }
            else {
                itemObject = GameObject.Instantiate<GameObject>(itemPrefab);
                itemObject.transform.SetParent(contentRoot, false);
            }
            Debug.LogError("index："+i);
            itemObject.transform.Find("card_front").GetComponent<Image>().sprite = needShowCardList[i];
            CardFlipAnim cardFlipAnim = itemObject.GetComponent<CardFlipAnim>();
            cardFlipAnim.setDefaultStatus();
        }

        GridLayoutGroup glg = contentRoot.GetComponent<GridLayoutGroup>();

        float panelWidth = col * glg.cellSize.x + glg.padding.left + glg.padding.right + (col - 1) * glg.spacing.x;
        float panelHeight = row * glg.cellSize.y + glg.padding.top + glg.padding.bottom + (row - 1) * glg.spacing.y;
        contentRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(panelWidth, panelHeight);
    }

    public void checkIsGameOver(){
        Debug.LogError("checkIsGameOver");
        CardFlipAnim[] allCards = GameObject.FindObjectsOfType<CardFlipAnim>();

        if (allCards != null && allCards.Length > 0) {
            List<CardFlipAnim> cardInFront = new List<CardFlipAnim>();
            for (int i = 0; i < allCards.Length; i++) {
                CardFlipAnim cardTem = allCards[i];
                if (cardTem.isOnFront && !cardTem.isOver) {
                    cardInFront.Add(cardTem);
                }

                if (cardInFront.Count >= 2) {
                    string cardImageName1 = cardInFront[0].getCardImageName();
                    string cardImageName2 = cardInFront[1].getCardImageName();
                    if (cardImageName1 == cardImageName2) {
                        cardInFront[0].mathSuccess(); //标记为匹配结束
                        cardInFront[1].mathSuccess(); //标记为匹配结束
                    }
                    else {
                        cardInFront[0].matchFail(); //翻转到反面
                        cardInFront[1].matchFail(); //翻转到反面
                    }

                    bool isAllOver = true;
                    for (int j = 0; j < allCards.Length; j++) {
                        isAllOver &= allCards[j].isOver;

                    }

                    if (isAllOver) {
                        goGameOverPage();
                    }


                    break;
                }
            }
        }
    }

    private void goGameOverPage(){
        panelStart.gameObject.SetActive(false);
        panelCard.gameObject.SetActive(false);
        panelOver.gameObject.SetActive(true);
    }

    private void goGameStartPage(){
        panelStart.gameObject.SetActive(true);
        panelCard.gameObject.SetActive(false);
        panelOver.gameObject.SetActive(false);
    }
}