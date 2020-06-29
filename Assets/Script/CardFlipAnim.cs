using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardFlipAnim : MonoBehaviour, IPointerClickHandler {
    Transform cardFront;
    Transform cardBack;
    float flipDuaration = 0.2f;
    public bool isOnFront = false;
    public bool isOver = false;

    // Start is called before the first frame update
    void Start(){

    }

     void Awake(){
         cardFront = transform.Find("card_front");
         cardBack = transform.Find("card_back");
    }

    // Update is called once per frame
    void Update(){
    }

    public void OnPointerClick(PointerEventData eventData){
        // Debug.LogError("点击");
        if (isOnFront) {
            StartCoroutine(FlipCardToBack());
        }
        else {
            StartCoroutine(FlipCardToFront());
        }
    }

    IEnumerator FlipCardToFront(){
        //1.翻转反面到90度
        cardFront.gameObject.SetActive((false));
        cardBack.gameObject.SetActive((true));
        cardFront.rotation = Quaternion.identity;
        while (cardBack.rotation.eulerAngles.y < 90) {
            cardBack.rotation *= Quaternion.Euler(0, Time.deltaTime * 90f * (1f / flipDuaration), 0);
            if (cardBack.rotation.eulerAngles.y > 90) {
                cardBack.rotation = Quaternion.Euler(0, 90f, 0);
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        //2.翻转正面到0度
        cardFront.gameObject.SetActive((true));
        cardBack.gameObject.SetActive((false));
        cardFront.rotation = Quaternion.Euler(0, 90, 0);
        while (cardFront.rotation.eulerAngles.y > 0) {
            cardFront.rotation *= Quaternion.Euler(0, -Time.deltaTime * 90f * (1f / flipDuaration), 0);
            if (cardFront.rotation.eulerAngles.y > 90) {
                cardFront.rotation = Quaternion.Euler(0, 0, 0);
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        isOnFront = true;
        Camera.main.gameObject.GetComponent<GameMain>().checkIsGameOver();
    }

    IEnumerator FlipCardToBack(){
        //1.翻转反面到90度
        cardBack.gameObject.SetActive((false));
        cardFront.gameObject.SetActive((true));
        cardBack.rotation = Quaternion.identity;
        while (cardFront.rotation.eulerAngles.y < 90) {
            cardFront.rotation *= Quaternion.Euler(0, Time.deltaTime * 90f * (1f / flipDuaration), 0);
            if (cardFront.rotation.eulerAngles.y > 90) {
                cardFront.rotation = Quaternion.Euler(0, 90f, 0);
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        //2.翻转正面到0度
        cardBack.gameObject.SetActive((true));
        cardFront.gameObject.SetActive((false));
        cardBack.rotation = Quaternion.Euler(0, 90, 0);
        while (cardBack.rotation.eulerAngles.y > 0) {
            cardBack.rotation *= Quaternion.Euler(0, -Time.deltaTime * 90f * (1f / flipDuaration), 0);
            if (cardBack.rotation.eulerAngles.y > 90) {
                cardBack.rotation = Quaternion.Euler(0, 0, 0);
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        isOnFront = false;
    }

    internal string getCardImageName(){
        return cardFront.GetComponent<Image>().sprite.name;
    }

    public void mathSuccess(){
        isOver = true;
        cardFront.gameObject.SetActive(false);
        cardBack.gameObject.SetActive(false);
    }

    public void matchFail(){
        StartCoroutine(FlipCardToBack());
    }

    public void setDefaultStatus(){
       cardFront.gameObject.SetActive(false);
       cardBack.gameObject.SetActive(true);
       this.isOver = false;
       this.isOnFront = false;
       cardFront.rotation=Quaternion.identity;
       cardBack.rotation=Quaternion.identity;
    }
}