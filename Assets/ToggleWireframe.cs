using UnityEngine;

public class ToggleWireframe : MonoBehaviour
{

	public GameObject monumentFull;
	public GameObject monumentWireframe;
	
	public void setModelType()
	{
		bool isMonumentFullActive = monumentFull.activeSelf;
		
		monumentFull.SetActive(!isMonumentFullActive);
		monumentWireframe.SetActive(isMonumentFullActive);
	}
}
