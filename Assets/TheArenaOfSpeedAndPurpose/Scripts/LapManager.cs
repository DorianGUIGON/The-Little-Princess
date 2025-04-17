using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LapManager : MonoBehaviour
{
    public List<Checkpoint> checkpoints;
    public int totalLaps = 2;
    public UIManager ui;
    public GameObject finishMessageUI;
    public string mainMenuSceneName = "Menu";


    private List<PlayerRank> playerRanks = new List<PlayerRank>();
    private PlayerRank mainPlayerRank;
    public UnityEvent onPlayerFinished = new UnityEvent();

    void Start()
    {
        foreach(CarIdentity carIdentity in GameObject.FindObjectsOfType<CarIdentity>())
        {
            playerRanks.Add(new PlayerRank(carIdentity));
        }
        ListenCheckpoints(true);
        ui.UpdateLapText("Lap "+ playerRanks[0].lapNumber + " / " + totalLaps);
        mainPlayerRank = playerRanks.Find(player => player.identity.gameObject.tag == "Player");
    }

    void Update()
    {

        if (finishMessageUI.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
        }

    }


    private void ListenCheckpoints(bool subscribe)
    {
        foreach(Checkpoint checkpoint in checkpoints) {
            //TODO : refacctor onChekpointEnter event
            if (subscribe) checkpoint.onCheckpointEnter.AddListener(CheckpointActivated);
            else checkpoint.onCheckpointEnter.RemoveListener(CheckpointActivated);
        }
    }

    public void CheckpointActivated(CarIdentity car, Checkpoint checkpoint)
    {
        PlayerRank player = playerRanks.Find((rank) => rank.identity == car);

        if (checkpoints.Contains(checkpoint) && player!=null)
        {
            // if player has already finished don't do anything
            if (player.hasFinished) return;

            int checkpointNumber = checkpoints.IndexOf(checkpoint);
            // first time ever the car reach the first checkpoint
            bool startingFirstLap = checkpointNumber == 0 && player.lastCheckpoint == -1;
            // finish line checkpoint is triggered & last checkpoint was reached
            bool lapIsFinished = checkpointNumber == 0 && player.lastCheckpoint >= checkpoints.Count - 1;
            if (startingFirstLap || lapIsFinished) 
            { 
                player.lapNumber += 1;
                player.lastCheckpoint = 0;

                if (player.identity.driverName == "Player")
                {
                    PlayerRespawn respawn = car.GetComponent<PlayerRespawn>();
                    if (respawn != null)
                    {
                        respawn.UpdateLastCheckpoint(checkpoint);
                    }
                }

                // if this was the final lap
                if (player.lapNumber > totalLaps)
                {
                    player.hasFinished = true;
                    Debug.Log(player.identity.name + " finished");
                    // getting final rank, by finding number of finished players
                    player.rank = playerRanks.FindAll(player => player.hasFinished).Count;

                    // if first winner, display its name
                    if (player.rank == 1)
                    {
                        Debug.Log(player.identity.driverName + " won !");
                        ui.UpdateLapText(player.identity.driverName + " won");
                    }
                    else if (player == mainPlayerRank) // display player
                    {
                        ui.UpdateLapText("\nYou finished in " + mainPlayerRank.rank + " place");
                    }

                    if (player.identity.driverName == "Player")
                    {
                        finishMessageUI.SetActive(true);
                    }


                    if (player == mainPlayerRank)
                    {
                        onPlayerFinished.Invoke();
                    }
                }
                else
                {
                    Debug.Log(player.identity.driverName + ": lap " + player.lapNumber);

                    if (player.identity.driverName == "Player")
                    {
                        Debug.Log("Mise à jour UI : Lap " + player.lapNumber + " / " + totalLaps);
                        ui.UpdateLapText("Lap " + player.lapNumber + " / " + totalLaps);
                    }
                }

            }
            // next checkpoint reached
            else if (checkpointNumber == player.lastCheckpoint + 1)
            {
                player.lastCheckpoint += 1;

                if (player.identity.driverName == "Player")
                {
                    PlayerRespawn respawn = car.GetComponent<PlayerRespawn>();
                    if (respawn != null)
                    {
                        respawn.UpdateLastCheckpoint(checkpoint);
                    }

                    Debug.Log("player a passé un checkpoint");
                    Debug.Log(player.lastCheckpoint);
                }
            }
        }
    }
}
