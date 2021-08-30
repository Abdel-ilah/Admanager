using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static string gameId = "gameid";
    public static string reward = "reward";
    public string txtHelpTheDev = "Do you want to see a video ad to help the developer?";
    public string [] funnyTexts = 
    {
        "You can press yes and go drink water!",
        "You can press yes and go say that you love your mom!",
        "You can press yes and go make pp."
    };
    public GameObject helpTheDev;
    static public GameObject messageBox;
    public Button btnYes, btnNo;
    public Text txt;

    void Start()
    {
        messageBox = helpTheDev;
        messageBox.SetActive(false);

        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);

        btnYes.gameObject.AddComponent<BtnController>().type = BtnController.BtnType.Yes;
        btnNo.gameObject.AddComponent<BtnController>().type = BtnController.BtnType.No;
        
        btnYes.gameObject.GetComponentInChildren<Text>().text = "YES :)";
        btnNo.gameObject.GetComponentInChildren<Text>().text = "no :(";

        txt.text = txtHelpTheDev;
        txt.text +="\n\n"+ funnyTexts[Random.Range(0,funnyTexts.Length)];
    }

    public void OnUnityAdsDidError(string message){ }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            CloseMessageBox();

        }
        else if (showResult == ShowResult.Skipped)  { } 
        else if (showResult == ShowResult.Failed)   { }
    }

    public void OnUnityAdsDidStart(string placementId){ }

    public void OnUnityAdsReady(string placementId){ }
    public static void ShowMessageBox()
    {
        messageBox.SetActive(!false);

    }
    public static void CloseMessageBox()
    {
        messageBox.SetActive(false);

    }

    public static void ShowAd()
    {
        if(Advertisement.IsReady(reward)) 
            Advertisement.Show(reward);
    }
}
public class BtnController: MonoBehaviour, IPointerDownHandler
{
    public BtnType type;
    public void OnPointerDown(PointerEventData eventData)
	{
        if(type == BtnType.Yes)
        {
            AdManager.ShowAd();
        }
        else
        {
            AdManager.CloseMessageBox();
        }
	}

	public enum BtnType { Yes, No}
}
