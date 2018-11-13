using UnityEngine;

public class ToggleModels : MonoBehaviour
{

	public GameObject monumentFull;
	public GameObject monumentPartial;
	public GameObject monumentDestroyed;
	public GameObject monumentWireframe;
	
	public void toggleWireframe()
	{
		bool isWireframeActive = monumentWireframe.activeSelf;
		monumentFull.SetActive(isWireframeActive);
		monumentPartial.SetActive(false);
		monumentDestroyed.SetActive(false);
		monumentWireframe.SetActive(!isWireframeActive);
	}

	public void setMonumentFull()
	{
		monumentFull.SetActive(true);
		monumentPartial.SetActive(false);
		monumentDestroyed.SetActive(false);
		monumentWireframe.SetActive(false);
	}

	public void setMonumentPartial()
	{
		monumentFull.SetActive(false);
		monumentPartial.SetActive(true);
		monumentDestroyed.SetActive(false);
		monumentWireframe.SetActive(false);
	}

	public void setMonumentDestroyed()
	{
		monumentFull.SetActive(false);
		monumentPartial.SetActive(false);
		monumentDestroyed.SetActive(true);
		monumentWireframe.SetActive(false);
	}
}
