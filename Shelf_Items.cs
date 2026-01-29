using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;

public class Shelf_Items : MonoBehaviour
{
    // Prefab for the items to be spawned
    [Header("Item Prefabs")]
    [SerializeField] public GameObject Apple;
    [SerializeField] public GameObject Banana;
    [SerializeField] public GameObject Carrot;
    [SerializeField] public GameObject Beans;
    [SerializeField] public GameObject Cucumber;
    [SerializeField] public GameObject Ramen;
    //[SerializeField] public GameObject Milk;
    //[SerializeField] public GameObject Soda;
    [SerializeField] public GameObject Ball;
    [SerializeField] public GameObject Chips;
    [SerializeField] public GameObject Pear;
    [SerializeField] public GameObject Chcocolate;

    public string ItemOnShelf; // Item currently on the shelf
    public bool IsItemOnShelf = false; // Flag to check if an item is on the shelf


    public Transform spawnPoint; // Point where the item will be spawned
    [Header("Customer Navigation")]
    public Transform customerTargetPoint; // Assign in Inspector: empty GameObject in front of shelf for AI to walk to

    [Header("Shelf Slots")]
    public List<Transform> itemSlots; // Assign each "Item x on gas station shelf x" here in Inspector

    private int itemIndex = -1; //

    public enum ShelfItemType { Apple, Banana, Carrot, Beans, Cucumber, Ramen, Ball, Chips, Pear, Chcocolate }
    public ShelfItemType shelfItemType; // Set this in Inspector

    private void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            SpawnItem();
        }
    }
    private void Update()
    {
        
    }

    private void SpawnItem()
    {
        if (itemSlots != null && itemSlots.Count > 0 && !IsItemOnShelf && NetworkManager.Singleton.IsServer)
        {
            int slotIndex = UnityEngine.Random.Range(0, itemSlots.Count); // Safe!
            int itemTypeIndex = UnityEngine.Random.Range(0, 9); // 0-9 for your 10 items
    
            GameObject itemPrefab = null;
            Quaternion itemRotation = itemSlots[slotIndex].rotation;

            switch (itemTypeIndex)
            {
                case 0: itemPrefab = Apple;      itemRotation = Quaternion.Euler(-90f, 0f, 0f); break;
                case 1: itemPrefab = Banana;     itemRotation = Quaternion.Euler(0f, -90f, 0f); break;
                case 2: itemPrefab = Carrot;     itemRotation = Quaternion.Euler(0f, -90f, 0f); break;
                case 3: itemPrefab = Beans;      itemRotation = Quaternion.Euler(-142.587f, 52.684f, -7.686f); break;
                case 4: itemPrefab = Cucumber;   itemRotation = Quaternion.Euler(0f, 90f, 0f); break;
                case 5: itemPrefab = Ramen;      itemRotation = Quaternion.Euler(-90f, 0f, 0f); break;
                case 6: itemPrefab = Ball;       itemRotation = Quaternion.Euler(0f, 0f, 0f); break;
                case 7: itemPrefab = Chips;      itemRotation = Quaternion.Euler(-90f, -90f, 0f); break;
                case 8: itemPrefab = Pear;       itemRotation = Quaternion.Euler(-90f, 0f, 0f); break;
                case 9: itemPrefab = Chcocolate; itemRotation = Quaternion.Euler(0f, 0f, 90f); break;
            }

            if (itemPrefab != null)
            {
                GameObject itemInstance = Instantiate(itemPrefab, itemSlots[slotIndex].position, itemRotation);
                var netObj = itemInstance.GetComponent<NetworkObject>();
                netObj.Spawn();
                itemInstance.transform.SetParent(itemSlots[slotIndex]);
                itemInstance.transform.rotation = itemRotation;
                AssignItemOnShelf(itemTypeIndex); // Use itemTypeIndex, not slotIndex
            }
        }
        else
        {
            Debug.LogError("Shelf_Items: itemSlots not set or empty.");
        }
    }

    private void AssignItemOnShelf(int index)
    {
        switch (index)
        {
            case 0: ItemOnShelf = "Apple"; break;
            case 1: ItemOnShelf = "Banana"; break;
            case 2: ItemOnShelf = "Carrot"; break;
            case 3: ItemOnShelf = "Beans"; break;
            case 4: ItemOnShelf = "Cucumber"; break;
            case 5: ItemOnShelf = "Ramen"; break;
            case 6: ItemOnShelf = "Ball"; break;
            case 7: ItemOnShelf = "Chips"; break;
            case 8: ItemOnShelf = "Pear"; break;
            case 9: ItemOnShelf = "Chcocolate"; break;
            default: ItemOnShelf = ""; break;
        }
        IsItemOnShelf = true; // Set the flag to true when an item is assigned
    }
}
