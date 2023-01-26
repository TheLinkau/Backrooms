using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public enum ItemType { Light, Key1, Key2}
    public List<ItemType> items = new List<ItemType>();
}
