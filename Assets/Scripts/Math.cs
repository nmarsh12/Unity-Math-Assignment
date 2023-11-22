using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Math : MonoBehaviour
{
    List<ChestType> chestList = new List<ChestType>();
    
    int[] chestWeights = { 100, 60, 50, 25, 15 };
    int[] woodWeights = { 100, 50, 40, 25, 5 };
    int[] bronzeWeights = { 75, 60, 50, 30, 15 };
    int[] silverWeights = { 50, 50, 80, 50, 30 };
    int[] goldWeights = { 25, 25, 40, 100, 75 };
    int[] platinumWeights = { 0, 0, 25, 50, 50 };

    public ChestType woodChest;
    public ChestType bronzeChest;
    public ChestType silverChest;
    public ChestType goldChest;
    public ChestType platinumChest;

    
    // Start is called before the first frame update
    void Start()
    {
        woodChest = new ChestType("Wooden Chest", woodWeights);
        bronzeChest = new ChestType("Bronze Chest", bronzeWeights);
        silverChest = new ChestType("Silver Chest", silverWeights);
        goldChest = new ChestType("Gold Chest", goldWeights);
        platinumChest = new ChestType("Platinum Chest", platinumWeights);
        
        Debug.Log(RollForChestType().name);
    }

    ChestType RollForChestType(){
        int totalWeight = 0;
        int[] rollRanges = { 0, 0, 0, 0, 0 };

        ChestType[] chestTypes = { woodChest, bronzeChest, silverChest, goldChest, platinumChest };
        
        for (int i = 0; i < chestWeights.Length; i++) {
            rollRanges[i] = totalWeight;
            totalWeight += chestWeights[i];
        }

        int roll = Random.Range(0, totalWeight);
        Debug.Log(roll);

        if (roll > rollRanges[4]) {
            return chestTypes[4];
        }
        for (int i = 0; i < rollRanges.Length; i++){
            if (roll >= rollRanges[i] && roll < rollRanges[i + 1]){
                return chestTypes[i];
            }
        }
        Debug.Log("<color=red> Error: </color> Oops");
        return chestTypes[0];
    }

    ChestType RollForChestType(){
        int totalWeight = 0;
        int[] rollRanges = { 0, 0, 0, 0, 0 };

        ChestType[] chestTypes = { woodChest, bronzeChest, silverChest, goldChest, platinumChest };
        
        for (int i = 0; i < chestWeights.Length; i++) {
            rollRanges[i] = totalWeight;
            totalWeight += chestWeights[i];
        }

        int roll = Random.Range(0, totalWeight);
        Debug.Log(roll);

        if (roll > rollRanges[4]) {
            return chestTypes[4];
        }
        for (int i = 0; i < rollRanges.Length; i++){
            if (roll >= rollRanges[i] && roll < rollRanges[i + 1]){
                return chestTypes[i];
            }
        }
        Debug.Log("<color=red Error: </color> Oops");
        return chestTypes[0];
    }
}

public class ChestType
{
    string[] rarities = { "Common", "Uncommon", "Rare", "Epic", "Legendary" };
    public string name;
    public Dictionary<string, int> rarityWeights = new Dictionary<string, int>();

    public ChestType(string _name, int[] weights){
        this.name = _name;
        for (int i = 0; i < weights.Length; i++) {
            rarityWeights.Add(rarities[i], weights[i]);
        }
    }
}