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
        Canvas canvas = transform.GetChild(0).gameObject.GetComponent<Canvas>() ;
        for (int i=0;i<inventory.items.Count;i++)
        {
            ItemType it = inventory.items[i];

            Image img = new GameObject("item").AddComponent<Image>();
            Texture2D textureItem = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Resources/test.png");
            Sprite spriteItem = Sprite.Create(textureItem, new Rect(0, 0, textureItem.width, textureItem.height), new Vector2(0.5f, 0.5f));
            img.sprite = spriteItem;

            img.transform.SetParent(canvas.transform);

            img.transform.localPosition = Vector3.zero;
            SpriteRenderer spriteRenderer = img.GetComponent<SpriteRenderer>();
            spriteRenderer.size = new Vector2(7, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
