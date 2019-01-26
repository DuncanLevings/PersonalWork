using System;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomShow : MonoBehaviour
{
  public GameObject GameMaster;
  private RandomMatchmaker masterScript;
  public Button createRoomBtn;
  public Text createRoomText;
  private string nameError;
  private string roomName;

  public CreateRoomShow()
  {
    base.\u002Ector();
  }

  private void Start()
  {
    this.masterScript = (RandomMatchmaker) this.GameMaster.GetComponent<RandomMatchmaker>();
    this.nameError = ((Text) ((Component) ((Component) this).GetComponentInChildren<isRoomNameError>()).get_gameObject().GetComponent<Text>()).get_text();
  }

  public void CreateGame()
  {
    bool flag = false;
    this.roomName = ((Text) ((Component) ((Component) this).GetComponentInChildren<isRoomNameText>()).get_gameObject().GetComponent<Text>()).get_text();
    foreach (RoomInfo room in PhotonNetwork.GetRoomList())
    {
      if (this.roomName == room.name)
        flag = true;
    }
    if (flag)
    {
      this.nameError = "Name Already Exists!";
    }
    else
    {
      this.nameError = string.Empty;
      GameObject.Find("CreateGameMenu").get_gameObject().SetActive(false);
      this.masterScript.rName = this.roomName;
      this.masterScript.cRoom = true;
      this.masterScript.OnJoinedLobby();
      Social.ReportProgress("CgkIp57Sv44CEAIQBg", 100.0, (Action<bool>) (success => {}));
    }
  }

  private void Update()
  {
    if (this.createRoomText.get_text() == string.Empty)
      ((Selectable) this.createRoomBtn).set_interactable(false);
    else
      ((Selectable) this.createRoomBtn).set_interactable(true);
  }
}
