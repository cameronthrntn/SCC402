using UnityEngine;

public class ToggleModels : MonoBehaviour
{

	private GameObject monumentFull;
	private GameObject monumentPartial;
	private GameObject monumentDestroyed;
	private GameObject monumentWireframe;
	private GameObject monumentWireframePartial;

	public void Start()
	{
		GameObject castleModels = GameObject.Find("CastleModels");
		GameObject castleWireframes = GameObject.Find("CastleWireframes");

		monumentFull = FindObject(castleModels, "castleFull");
		monumentPartial = FindObject(castleModels, "castlePartial");
		monumentDestroyed = FindObject(castleModels, "castleDestroyed");

		monumentWireframe = FindObject(castleWireframes, "wireframeFull");
		monumentWireframePartial = FindObject(castleWireframes, "wireframePartial");
	}

	public static GameObject FindObject(GameObject parent, string name)
	{
		Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
		foreach(Transform t in trs){
			if(t.name == name){
				return t.gameObject;
			}
		}
		return null;
	}

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
