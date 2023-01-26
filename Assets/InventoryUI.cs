using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using static Inventory;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    void Start()
    {
        Transform contents = gameObject.transform.GetChild(0).GetChild(0).GetChild(0);
        //HorizontalLayoutGroup hlg = contents.GetComponent<HorizontalLayoutGroup>();

        Vector2 itemSize = new Vector2(50,50);

        for (int i=0;i<inventory.items.Count;i++)
        {
            ItemType it = inventory.items[i];

            Image img = new GameObject("item").AddComponent<Image>();
            Texture2D textureItem = (Texture2D)Resources.Load("test");

            Sprite spriteItem = Sprite.Create(textureItem, new Rect(0, 0, textureItem.width, textureItem.height), new Vector2(0.5f, 0.5f));
            img.sprite = spriteItem;

            img.transform.SetParent(contents);

            img.transform.localPosition = new Vector3(img.transform.localPosition.x, img.transform.localPosition.y, 0f);
            img.transform.localScale = Vector3.one;
            img.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            img.rectTransform.sizeDelta = itemSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
