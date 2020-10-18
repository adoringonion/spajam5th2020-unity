using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class GotoTravel : MonoBehaviour
{

    public string url = "https://staynavi.direct/";
    public void gotoUrl()
    {
        Application.OpenURL(url);
    }
}
