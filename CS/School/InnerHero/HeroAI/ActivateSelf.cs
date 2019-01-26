using System.Collections.Generic;
using UnityEngine;

public class ActivateSelf : MonoBehaviour
{
    public Color BtnColor;
    public Sprite icon;
    public Sprite PowerBarIcon;
    private GameObject playerObj;
    private player playerScript;

    public ActivateSelf()
    {
        base.\u002Ector();
    }

    private void Start()
    {
        this.playerObj = PlayerSingleton.Instance.getPlayer();
        this.playerScript = (player)this.playerObj.GetComponent<player>();
    }

    public void ActivateTree(GameObject obj)
    {
        this.playerScript.enableSelectedSB(((Component)this).get_gameObject(), ((isaActivesbType)((Component)this).get_gameObject().GetComponent<isaActivesbType>()).type);
        this.playerScript.ResetSBtargets();
        if (PlayerSingleton.Instance.ManualMode)
        {
            this.playerScript.target = obj;
            ((ActiveSkillbitBase)((Component)this).GetComponent<ActiveSkillbitBase>()).target = obj;
        }
        if (!this.TimeLineObjectCheck(obj))
        {
            List<GameObject> events = new List<GameObject>();
            events.Add(obj);
            Timeline.instance.TimeLineEvents.Insert(0, new roomData(((Component)obj.get_transform().get_root()).get_gameObject(), events));
        }
        else if (Object.op_Equality((Object)Timeline.instance.FirstRoom(), (Object)((Component)obj.get_transform().get_root()).get_gameObject()))
        {
            if (Timeline.instance.TimeLineEvents[0].eventsInRoom.Contains(obj))
            {
                int index = Timeline.instance.TimeLineEvents[0].eventsInRoom.IndexOf(obj);
                if (index == 0)
                    return;
                Timeline.instance.TimeLineEvents[0].eventsInRoom.RemoveAt(index);
                this.AddObjectAfterEnemys(obj);
            }
            else
                this.AddObjectAfterEnemys(obj);
        }
        else
        {
            int roomIndex = Timeline.instance.FindRoomIndex(((Component)obj.get_transform().get_root()).get_gameObject());
            List<GameObject> eventsInRoom = Timeline.instance.TimeLineEvents[roomIndex].eventsInRoom;
            if (!Timeline.instance.TimeLineEvents[roomIndex].eventsInRoom.Contains(obj))
            {
                eventsInRoom.Clear();
                eventsInRoom.Add(obj);
            }
            Timeline.instance.TimeLineEvents.RemoveAt(roomIndex);
            Timeline.instance.TimeLineEvents.Insert(0, new roomData(((Component)obj.get_transform().get_root()).get_gameObject(), eventsInRoom));
        }
    }

    private void AddObjectAfterEnemys(GameObject obj)
    {
        Timeline.instance.TimeLineEvents[0].eventsInRoom.Insert(0, obj);
    }

    private void EnemyFeedbackPriority()
    {
        if (PlayerSingleton.Instance.ManualMode)
            return;
        Debug.Log((object)"need to protect myself from this enemy first");
    }

    private bool TimeLineObjectCheck(GameObject obj)
    {
        if (Object.op_Equality((Object)obj, (Object)null))
            return false;
        GameObject gameObject = ((Component)obj.get_transform().get_root()).get_gameObject();
        for (int index = 0; index < Timeline.instance.TimeLineEvents.Count; ++index)
        {
            if (Object.op_Equality((Object)Timeline.instance.TimeLineEvents[index].room, (Object)gameObject))
                return true;
        }
        return false;
    }
}
