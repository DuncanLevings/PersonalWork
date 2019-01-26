using UnityEngine;
using UnityEngine.UI;

public class RoomList : MonoBehaviour
{
  public GameObject prefabBtn;
  private GameObject[] roomList;

  public RoomList()
  {
    base.\u002Ector();
  }

  private void Start()
  {
  }

  private void OnEnable()
  {
    this.ListRooms();
  }

  private void ListRooms()
  {
    foreach (RoomInfo room in PhotonNetwork.GetRoomList())
    {
      GameObject gameObject = (GameObject) Object.Instantiate((Object) this.prefabBtn, ((Component) this).get_transform().get_position(), Quaternion.get_identity());
      gameObject.get_transform().set_parent(((Component) this).get_gameObject().get_transform());
      ((Transform) gameObject.GetComponent<RectTransform>()).set_localScale(new Vector3(3f, 2f, 1f));
      ((Object) gameObject).set_name(room.name);
      ((Text) gameObject.GetComponentInChildren<Text>()).set_text("Room name: " + room.name + "          #Players: " + (object) room.playerCount);
      ((roomNameHolder) gameObject.GetComponent<roomNameHolder>()).roomName = room.name;
      if (room.playerCount == 4)
      {
        ((Selectable) gameObject.GetComponent<Button>()).set_interactable(false);
        ((Text) gameObject.GetComponentInChildren<Text>()).set_text("Room name: " + room.name + "          #Players: FULL");
      }
      this.roomList = GameObject.FindGameObjectsWithTag("RoomListBtn");
    }
  }

  private void UpdateRooms()
  {
    foreach (RoomInfo room1 in PhotonNetwork.GetRoomList())
    {
      if (this.roomList != null)
      {
        foreach (GameObject room2 in this.roomList)
        {
          if (Object.op_Inequality((Object) room2, (Object) null))
          {
            if (room1.playerCount == 4)
            {
              if (((Object) room2).get_name() == room1.name)
                ((Selectable) room2.GetComponent<Button>()).set_interactable(false);
            }
            else
              ((Selectable) room2.GetComponent<Button>()).set_interactable(true);
          }
        }
      }
    }
  }

  public void RefreshList()
  {
    this.CleanUp();
    this.ListRooms();
  }

  private void CleanUp()
  {
    if (this.roomList == null)
      return;
    foreach (GameObject room in this.roomList)
    {
      if (Object.op_Inequality((Object) room, (Object) null))
        Object.Destroy((Object) room);
    }
  }

  private void OnDisable()
  {
    this.CleanUp();
  }

  private void Update()
  {
    this.UpdateRooms();
  }
}
