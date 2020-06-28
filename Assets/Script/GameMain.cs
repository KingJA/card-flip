using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameMain : MonoBehaviour
{

   public Button btnLevel1;
   public Button btnLevel2;
   public Button btnLevel3;

   public Transform panelStart;
   public Transform panelCard;
   public Transform panelOver;



    // Start is called before the first frame update
    void Start()
    {
        btnLevel1.onClick.AddListener(()=>{
            panelStart.gameObject.SetActive(false);
            panelCard.gameObject.SetActive(true);
            initCard(2,3);
        });
        btnLevel2.onClick.AddListener(()=>{
            panelStart.gameObject.SetActive(false);
            panelCard.gameObject.SetActive(true);
             initCard(2,4);
        });
        btnLevel3.onClick.AddListener(()=>{
            panelStart.gameObject.SetActive(false);
            panelCard.gameObject.SetActive(true);
             initCard(2,5);
        });
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initCard(int row,int col){
      Sprite[] sps=  Resources.LoadAll<Sprite>("");

      for (int i = 0; i < sps.Length; i++)
      {
          Debug.LogError(sps[i].name);
      }
    }
}
