using UnityEngine;

public class DebuggerTest : MonoBehaviour
{
	private void Start ()
	{
	    
	}
	
	private void Update () 
	{
	    if (Input.GetKeyDown(KeyCode.F1))
	        Debugger.Log(LogType.Normal, "Normal"); 
        
        if (Input.GetKeyDown(KeyCode.F2))
            Debugger.Log(LogType.UI, "UI"); 
        
        if (Input.GetKeyDown(KeyCode.F3))
            Debugger.Log(LogType.StateMachine, "StateMachine");
        
        if (Input.GetKeyDown(KeyCode.F4))
            Debugger.Log(LogType.SkillSystem, "SkillSystem");
	}
}
