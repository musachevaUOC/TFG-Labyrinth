using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCounter : MonoBehaviour
{
    public GameObject heart;
    


    public void drawHearts(int quantity) {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i<quantity;i++) {
            RectTransform ch = Instantiate(heart, transform).GetComponent<RectTransform>();
            
            float w = ch.rect.width;
            ch.anchoredPosition = new Vector2((w * (2*i-quantity)/2), 0);
            
        }


    }
}
