﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArenaShieldBotID : NetworkBehaviour
{
    // Start is called before the first frame update
    [SyncVar]
    [HideInInspector]public NetworkInstanceId ID;

}
