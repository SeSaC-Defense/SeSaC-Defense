using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMChanger : MonoBehaviour
{
    private int ix = 0;
    public void ChangeTrack()
    {
        ix = ix == 0 ? 1 : 0;
        BGMManager.Instance.ChangeTrackTo(ix);
    }
}
