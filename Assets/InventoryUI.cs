using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using static Inventory;
using System;

public class InventoryUI : MonoBehaviour
{
    public XRNode controller;
    public Player player;

    private Vector2 inputAxis;
    private float radPerItem;
    private float startRad;
    private float radius;

    private Texture2D textureItemSelected;
    private Image imgItemSelected;

    void Start()
    {
        Transform contents = gameObject.transform;
        Vector2 itemSize = new Vector2(1, 1);

        radPerItem = (2*Mathf.PI) / player.inventory.items.Count;
        startRad = Mathf.PI / 2.0f;
        radius = 1.75f;

        textureItemSelected = (Texture2D)Resources.Load("selected");
        imgItemSelected = new GameObject("selected").AddComponent<Image>();
        imgItemSelected.sprite = Sprite.Create(textureItemSelected, new Rect(0, 0, textureItemSelected.width, textureItemSelected.height), new Vector2(0.5f, 0.5f));
        imgItemSelected.transform.SetParent(contents);
        imgItemSelected.transform.localScale = Vector3.one;
        imgItemSelected.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        imgItemSelected.rectTransform.sizeDelta = itemSize;

        for (int i=0;i< player.inventory.items.Count;i++)
        {
            ItemType it = player.inventory.items[i];
            float x = radius * Mathf.Cos(startRad + i * radPerItem);
            float y = radius * Mathf.Sin(startRad + i * radPerItem);

            if (i == player.itemSelected)
            {
                imgItemSelected.transform.localPosition = new Vector3(x, y, 0f);
            }

            Texture2D textureItem = (Texture2D)Resources.Load("test");

            Image img = new GameObject("item").AddComponent<Image>();
            img.sprite = Sprite.Create(textureItem, new Rect(0, 0, textureItem.width, textureItem.height), new Vector2(0.5f, 0.5f));
            img.transform.SetParent(contents);
            img.transform.SetAsFirstSibling();
            img.transform.localPosition = new Vector3(x, y, 0f);
            img.transform.localScale = Vector3.one;
            img.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            img.rectTransform.sizeDelta = itemSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controller);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        if(inputAxis.x == 0f && inputAxis.y == 0f) return;

        //Text text = GameObject.Find("Text").GetComponent<Text>();
        //text.text = menuPressed.ToString();

        inputAxis.x = -inputAxis.x;

        float rad = Mathf.Atan2(inputAxis.y, inputAxis.x);
        if(rad < 0f)
        {
            rad = Mathf.PI + Mathf.PI + rad;
        }

        float radPerItem = (2 * Mathf.PI) / player.inventory.items.Count;
        int index = (int)Math.Round(rad/radPerItem) - 1;
        if(index < -1)
        {
            index = player.inventory.items.Count - index;
        }

        player.itemSelected = index;

        float x = radius * Mathf.Cos(startRad + index * radPerItem);
        float y = radius * Mathf.Sin(startRad + index * radPerItem);
        imgItemSelected.transform.localPosition = new Vector3(x, y, 0f);
    }
}
