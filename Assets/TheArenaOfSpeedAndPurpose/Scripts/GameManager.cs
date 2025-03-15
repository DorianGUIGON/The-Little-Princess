using System.Collections;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public AIControls[] aiControls;
    public LapManager lapTracker;
    public TricolorLights tricolorLights;

    public AudioSource audioSource;
    public AudioClip lowBeep;
    public AudioClip highBeep;

    public FollowPlayer followPlayerCamera;
    public Animator cameraIntroAnimator;

    void Awake()
    {
        StartIntro();
    }

    public void StartIntro()
    {
        followPlayerCamera.enabled = false;
        cameraIntroAnimator.enabled = true;
        FreezePlayers(true);
    }

    public void StartCountdown()
    {
        followPlayerCamera.enabled = true;
        cameraIntroAnimator.enabled = false;
        StartCoroutine("Countdown");
    }


    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("3");
        tricolorLights.SetProgress(1);
        audioSource.PlayOneShot(lowBeep);
        yield return new WaitForSeconds(1);
        Debug.Log("2");
        tricolorLights.SetProgress(2);
        audioSource.PlayOneShot(lowBeep);
        yield return new WaitForSeconds(1);
        Debug.Log("1");
        tricolorLights.SetProgress(3);
        audioSource.PlayOneShot(lowBeep);
        yield return new WaitForSeconds(1);
        Debug.Log("GO");
        tricolorLights.SetProgress(4);
        audioSource.PlayOneShot(highBeep);
        StartRacing();
        yield return new WaitForSeconds(2f);
        tricolorLights.SetAllLightsOff();
    }

    public void StartRacing()
    {
        followPlayerCamera.enabled = true;
        FreezePlayers(false);
    }

    void FreezePlayers(bool freeze)
    {
        if (freeze) {
            playerControls.enabled = false;
            foreach(AIControls ai in aiControls)
            {
                ai.enabled = false;
            }
        } else {
            playerControls.enabled = true;
            foreach(AIControls ai in aiControls)
            {
                ai.enabled = true;
            }
        }
    }
}