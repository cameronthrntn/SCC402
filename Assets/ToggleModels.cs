using UnityEngine;

public class ToggleModels : MonoBehaviour
{

	public GameObject monumentFull;
	public GameObject monumentPartial;
	public GameObject monumentDestroyed;
	public GameObject monumentWireframe;
	public GameObject monumentWireframePartial;
	
	public void toggleWireframe()
	{

		if (monumentFull.activeSelf || monumentWireframe.activeSelf)
		{
			monumentFull.SetActive(!monumentFull.activeSelf);
			monumentWireframe.SetActive(!monumentWireframe.activeSelf);
		}
		else if (monumentPartial.activeSelf || monumentWireframePartial.activeSelf)
		{
			monumentPartial.SetActive(!monumentPartial.activeSelf);
			monumentWireframePartial.SetActive(!monumentWireframePartial.activeSelf);
		}
		else if (monumentDestroyed.activeSelf) //  || monumentWireframeDestroyed.activeSelf
		{
			monumentDestroyed.SetActive(!monumentDestroyed.activeSelf);
//			monumentWireframeDestroyed.SetActive(!monumentWireframeDestroyed.activeSelf);
		}
	}

	public void setMonumentFull()
	{
		monumentFull.SetActive(true);
		monumentPartial.SetActive(false);
		monumentDestroyed.SetActive(false);
		monumentWireframe.SetActive(false);
		monumentWireframePartial.SetActive(false);
	}

	public void setMonumentPartial()
	{
		monumentFull.SetActive(false);
		monumentPartial.SetActive(true);
		monumentDestroyed.SetActive(false);
		monumentWireframe.SetActive(false);
		monumentWireframePartial.SetActive(false);
	}

	public void setMonumentDestroyed()
	{
		monumentFull.SetActive(false);
		monumentPartial.SetActive(false);
		monumentDestroyed.SetActive(true);
		monumentWireframe.SetActive(false);
		monumentWireframePartial.SetActive(false);
	}
}
