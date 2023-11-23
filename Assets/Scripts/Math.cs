using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class Math : MonoBehaviour
{    
    public int[] chestWeights = { 100, 60, 50, 25, 15 };
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
    void Initialize()
    {
        woodChest = new ChestType("Wooden Chest", woodWeights);
        bronzeChest = new ChestType("Bronze Chest", bronzeWeights);
        silverChest = new ChestType("Silver Chest", silverWeights);
        goldChest = new ChestType("Gold Chest", goldWeights);
        platinumChest = new ChestType("Platinum Chest", platinumWeights);
    }

    void Start(){
        Initialize();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)){
            RollAndPrintResult();
        }
    }

    void RollAndPrintResult() {
        ChestType chestRolled = RollForChestType();
        string lootRolled = RollForLootRarity(chestRolled);
        Debug.Log("You got a " + lootRolled + " item from a " + chestRolled.name);
    }

    ChestType RollForChestType() {
        ChestType[] chestTypes = { woodChest, bronzeChest, silverChest, goldChest, platinumChest };
        return chestTypes[RollForIndex(chestWeights)];
    }

    string RollForLootRarity( ChestType selectedChest ) {
        string[] rarities = { "Common", "Uncommon", "Rare", "Epic", "Legendary" };
        int rarityIndex = RollForIndex(selectedChest.rarityWeights);
        return rarities[rarityIndex];
    }

    // this was the most (only) complex part so it probably warrants an explanation, even though it's definitely fine as a black box
    // rollRanges is the only real tricky part, but it essentially splits the weights into segments then checks if the rolled number is within that segment;
    // it then gives back which segment the roll was as a number between zero and the highest array index possible.
    // this returned number is to be used as an index of a list, so with a list of choices and an int array of weights you can roll any probability table.

    // the bottom part with the if statements and error message is also a little weird too,
    // with just the for loop it wouldn't be able to catch rolls in the highest roll range, so the if statement before it checks for that first then 
    // returns early in that case. 

    // returning zero at the end will never ever happen but i did need to add a default return path in case the if statements weren't called.
    // tl;dr you give it a list of weights and it gives back an index to put into a list or array of choices
    int RollForIndex( int[] weights ) {
        int totalWeight = 0;
        int finalArrayIndex = weights.Length - 1;
        int[] rollRanges = new int[weights.Length];
        
        for (int i = 0; i < weights.Length; i++) {
            rollRanges[i] = totalWeight;
            totalWeight += weights[i];
        }

        int roll = Random.Range(0, totalWeight);

        if (roll > rollRanges[finalArrayIndex]) {
            return finalArrayIndex;
        }

        for (int i = 0; i < rollRanges.Length; i++){
            if (roll >= rollRanges[i] && roll <= rollRanges[i + 1]) { 
                return i;
            }
        }
        Debug.Log("<color=red Error: </color> In theory this will never happen but if it does oops");
        return 0;
    }
}

public class ChestType
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int[] rarityWeights;

    public ChestType(string _name, int[] weights){
        this.name = _name;
        this.rarityWeights = weights;
    }
}

[CustomEditor(typeof(Math))]
public class Data : Editor {
    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement inspectorPanel = new VisualElement();

        for (int i = 0; i < chestWeights.Length; i++) {}
        // Add a simple label
        inspectorPanel.Add(new Label("This is a custom inspector"));

        // Return the finished inspector UI
        return inspectorPanel;
    }
}


