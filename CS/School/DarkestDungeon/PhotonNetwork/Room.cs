using ExitGames.Client.Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : RoomInfo
{
  internal Room(string roomName, RoomOptions options)
    : base(roomName, (Hashtable) null)
  {
    if (options == null)
      options = new RoomOptions();
    this.visibleField = options.isVisible;
    this.openField = options.isOpen;
    this.maxPlayersField = options.maxPlayers;
    this.autoCleanUpField = false;
    this.InternalCacheProperties(options.customRoomProperties);
    this.propertiesListedInLobby = options.customRoomPropertiesForLobby;
  }

  public new int playerCount
  {
    get
    {
      if (PhotonNetwork.playerList != null)
        return PhotonNetwork.playerList.Length;
      return 0;
    }
  }

  public new string name
  {
    get
    {
      return this.nameField;
    }
    internal set
    {
      this.nameField = value;
    }
  }

  public int maxPlayers
  {
    get
    {
      return (int) this.maxPlayersField;
    }
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set maxPlayers when not in that room.");
      if (value > (int) byte.MaxValue)
      {
        Debug.LogWarning((object) ("Can't set Room.maxPlayers to: " + (object) value + ". Using max value: 255."));
        value = (int) byte.MaxValue;
      }
      if (value != (int) this.maxPlayersField && !PhotonNetwork.offlineMode)
      {
        NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) byte.MaxValue, (object) (byte) value);
        Hashtable gameProperties = hashtable;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties, (Hashtable) null, false);
      }
      this.maxPlayersField = (byte) value;
    }
  }

  public new bool open
  {
    get
    {
      return this.openField;
    }
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set open when not in that room.");
      if (value != this.openField && !PhotonNetwork.offlineMode)
      {
        NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) (byte) 253, (object) value);
        Hashtable gameProperties = hashtable;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties, (Hashtable) null, false);
      }
      this.openField = value;
    }
  }

  public new bool visible
  {
    get
    {
      return this.visibleField;
    }
    set
    {
      if (!this.Equals((object) PhotonNetwork.room))
        Debug.LogWarning((object) "Can't set visible when not in that room.");
      if (value != this.visibleField && !PhotonNetwork.offlineMode)
      {
        NetworkingPeer networkingPeer = PhotonNetwork.networkingPeer;
        Hashtable hashtable = new Hashtable();
        ((Dictionary<object, object>) hashtable).Add((object) (byte) 254, (object) value);
        Hashtable gameProperties = hashtable;
        networkingPeer.OpSetPropertiesOfRoom(gameProperties, (Hashtable) null, false);
      }
      this.visibleField = value;
    }
  }

  public string[] propertiesListedInLobby { get; private set; }

  public bool autoCleanUp
  {
    get
    {
      return this.autoCleanUpField;
    }
  }

  protected internal int masterClientId
  {
    get
    {
      return this.masterClientIdField;
    }
    set
    {
      this.masterClientIdField = value;
    }
  }

  public void SetCustomProperties(
    Hashtable propertiesToSet,
    Hashtable expectedValues = null,
    bool webForward = false)
  {
    if (propertiesToSet == null)
      return;
    Hashtable stringKeys1 = ((IDictionary) propertiesToSet).StripToStringKeys();
    Hashtable stringKeys2 = ((IDictionary) expectedValues).StripToStringKeys();
    bool flag = stringKeys2 == null || ((Dictionary<object, object>) stringKeys2).Count == 0;
    if (!PhotonNetwork.offlineMode)
      PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(stringKeys1, stringKeys2, webForward);
    if (!PhotonNetwork.offlineMode && !flag)
      return;
    this.InternalCacheProperties(stringKeys1);
    NetworkingPeer.SendMonoMessage(PhotonNetworkingMessage.OnPhotonCustomRoomPropertiesChanged, (object) stringKeys1);
  }

  public void SetPropertiesListedInLobby(string[] propsListedInLobby)
  {
    Hashtable gameProperties = new Hashtable();
    gameProperties.set_Item((object) (byte) 250, (object) propsListedInLobby);
    PhotonNetwork.networkingPeer.OpSetPropertiesOfRoom(gameProperties, (Hashtable) null, false);
    this.propertiesListedInLobby = propsListedInLobby;
  }

  public override string ToString()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.playerCount);
  }

  public new string ToStringFull()
  {
    return string.Format("Room: '{0}' {1},{2} {4}/{3} players.\ncustomProps: {5}", (object) this.nameField, !this.visibleField ? (object) "hidden" : (object) "visible", !this.openField ? (object) "closed" : (object) "open", (object) this.maxPlayersField, (object) this.playerCount, (object) ((IDictionary) this.customProperties).ToStringFull());
  }
}
