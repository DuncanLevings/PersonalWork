using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;

public class RoomInfo
{
  private Hashtable customPropertiesField = new Hashtable();
  protected bool openField = true;
  protected bool visibleField = true;
  protected bool autoCleanUpField = PhotonNetwork.autoCleanUpPlayerObjects;
  protected byte maxPlayersField;
  protected string nameField;
  protected internal int masterClientIdField;

  protected internal RoomInfo(string roomName, Hashtable properties)
  {
    this.InternalCacheProperties(properties);
    this.nameField = roomName;
  }

  public bool removedFromList { get; internal set; }

  protected internal bool serverSideMasterClient { get; private set; }

  public Hashtable customProperties
  {
    get
    {
      return this.customPropertiesField;
    }
  }

  public string name
  {
    get
    {
      return this.nameField;
    }
  }

  public int playerCount { get; private set; }

  public bool isLocalClientInside { get; set; }

  public byte maxPlayers
  {
    get
    {
      return this.maxPlayersField;
    }
  }

  public bool open
  {
    get
    {
      return this.openField;
    }
  }

  public bool visible
  {
    get
    {
      return this.visibleField;
    }
  }

  public override bool Equals(object p)
  {
    Room room = p as Room;
    if (room != null)
      return this.nameField.Equals(room.nameField);
    return false;
  }

  public override int GetHashCode()
  {
    return this.nameField.GetHashCode();
  }

  public override string ToString()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.playerCount);
  }

  public string ToStringFull()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.playerCount, (object) ((IDictionary) this.customPropertiesField).ToStringFull());
  }

  protected internal void InternalCacheProperties(Hashtable propertiesToCache)
  {
    if (propertiesToCache == null || ((Dictionary<object, object>) propertiesToCache).Count == 0 || ((object) this.customPropertiesField).Equals((object) propertiesToCache))
      return;
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 251))
    {
      this.removedFromList = (bool) propertiesToCache.get_Item((object) (byte) 251);
      if (this.removedFromList)
        return;
    }
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) byte.MaxValue))
      this.maxPlayersField = (byte) propertiesToCache.get_Item((object) byte.MaxValue);
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 253))
      this.openField = (bool) propertiesToCache.get_Item((object) (byte) 253);
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 254))
      this.visibleField = (bool) propertiesToCache.get_Item((object) (byte) 254);
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 252))
      this.playerCount = (int) (byte) propertiesToCache.get_Item((object) (byte) 252);
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 249))
      this.autoCleanUpField = (bool) propertiesToCache.get_Item((object) (byte) 249);
    if (((Dictionary<object, object>) propertiesToCache).ContainsKey((object) (byte) 248))
    {
      this.serverSideMasterClient = true;
      bool flag = this.masterClientIdField != 0;
      this.masterClientIdField = (int) propertiesToCache.get_Item((object) (byte) 248);
      if (flag)
        PhotonNetwork.networkingPeer.UpdateMasterClient();
    }
    ((IDictionary) this.customPropertiesField).MergeStringKeys((IDictionary) propertiesToCache);
  }
}
