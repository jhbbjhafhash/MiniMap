using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomExtentions;

using UnityEngine.SceneManagement;


public class GameBehavior : MonoBehaviour, IManager
{
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
    public bool showWinScreen = false;
    public bool showLossScreen = false;

    public bool JumpBoostEnabled = false;
    public bool SpeedBoostEnabled = false;

    public string labelText = "Collect all 4 items and win your freedom!";
    public int maxItems = 4;
    private int _itemsCollected = 0;

    public Stack<string> lootStack = new Stack<string>();
    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;
    
    public int Items 
    {
        get { return _itemsCollected; }

        set {
            _itemsCollected = value;
            Debug.LogFormat ("Items:{0}", _itemsCollected);

        if(_itemsCollected >= maxItems)
        {
            labelText = "You've found all the items!";

            showWinScreen = true;

            Time.timeScale = 0f;
        }
        else
        {
            labelText = "Item found, only" + (maxItems - _itemsCollected) + " more to go!";
        }
        }
    }

    private int _playerHP = 10;

    public int HP 
    {
        get { return _playerHP; }
        set {
            _playerHP = value;
            Debug.LogFormat ("Lives: {0}", _playerHP);
            if(_playerHP <= 0)
            {
                labelText = "You want another life with that?";
                showLossScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                labelText = "Ouch... That's gotta hurt.";
            }
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;
    }

    void Start()
    {
        Initialize();
        InventoryList<string> inventoryList = new InventoryList<string>();

        inventoryList.SetItem("Potion");
        Debug.Log(inventoryList.item);
    }

    public void Initialize()
    {
        _state = "Manager initialized..";
        _state.FancyDebug();
        debug(_state);
        LogWithDelegate(debug);

        lootStack.Push("Sword of Doom");
        lootStack.Push("HP+");
        lootStack.Push("Golden Key");
        lootStack.Push("Winged Boot");
        lootStack.Push("Mythril Bracers");

        GameObject player = GameObject.Find("Player");
        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();
        playerBehavior.playerJump += HandlePlayerJump;
    }

    public void HandlePlayerJump()
    {
        debug("Player has jumped...");
    }

    public static void Print(string newText)
    {
        Debug.Log(newText);
    }

    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task...");
    }

     public void PrintLootReport()
        {
            var currentItem = lootStack.Pop();
            var nextItem = lootStack.Peek();
            Debug.LogFormat("You got a {0}! You've got a good chance of finding a {1} next!", currentItem, nextItem);
            Debug.LogFormat("There are {0} random loot items waiting for you!", lootStack.Count);
        }

    void OnGUI()
    {
        GUI.Box(new Rect(20, 20, 150, 25) , "Player Health:" + _playerHP);

        GUI.Box(new Rect(20, 50, 150, 25) , "Items Collected:" + _itemsCollected);

        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 300, 50) , labelText);

        if(showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "YOU WON!"));
            {
                Utilities.RestartLevel(0);
            }
        }

        if(SpeedBoostEnabled == true)
        {
             GUI.Box(new Rect(20, 90, 150, 25) , "Speed Boost Enabled");
        }

         if(JumpBoostEnabled == true)
        {
             GUI.Box(new Rect(20, 120, 150, 25) , "Jump Boost Enabled");
        }

        if(showLossScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 100), "You lose..."))
            {
                try
                {
                    Utilities.RestartLevel(-1);
                    debug("Level restarted successfully...");
                }

                catch (System.ArgumentException e)
                {
                    Utilities.RestartLevel(0);
                    debug("Reverting tp scene 0:" + e.ToString());
                }

                finally
                {
                    debug("Restart handles...");
                }
            }
        }
    }
}
