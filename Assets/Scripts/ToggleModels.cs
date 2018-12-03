using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleModels : MonoBehaviour
{

	private GameObject monumentFull;
	private GameObject monumentPartial;
	private GameObject monumentDestroyed;
	private GameObject monumentWireframe;
	private GameObject monumentWireframePartial;
	private GameObject monumentWireframeDestroyed;

	private float transparency = 0.7f;

    public Scrollbar mainSlider;

    private void OnEnable()
	{
		ReceiveResult.OnVoiceEvent += EventAction;
	}

	private void OnDisable()
	{
		ReceiveResult.OnVoiceEvent -= EventAction;
	}

	private void EventAction(string action)
	{
        switch (action)
        {
            case ReceiveResult.EVENT_FULL_CASTLE:
	            setMonumentFull();
                break;
            case ReceiveResult.EVENT_PARTIAL_CASTLE:
	            setMonumentPartial();
                break;
            case ReceiveResult.EVENT_DESTROYED_CASTLE:
	            setMonumentDestroyed();
                break;
            case ReceiveResult.EVENT_FULL_WIREFRAME:
	            setMonumentFull();
				toggleWireframe();
                break;
            case ReceiveResult.EVENT_PARTIAL_WIREFRAME:
	            setMonumentPartial();
	            toggleWireframe();
                break;
            case ReceiveResult.EVENT_DESTROYED_WIREFRAME:
	            setMonumentDestroyed();
	            toggleWireframe();
                break;
        }
	}


	public void Start()
	{
		GameObject castleModels = GameObject.Find("CastleModels");
		GameObject castleWireframes = GameObject.Find("CastleWireframes");

		monumentFull = FindObject(castleModels, "castleFull");
		monumentPartial = FindObject(castleModels, "castlePartial");
		monumentDestroyed = FindObject(castleModels, "castleDestroyed");

		monumentWireframe = FindObject(castleWireframes, "wireframeFull");
		monumentWireframePartial = FindObject(castleWireframes, "wireframePartial");
		monumentWireframeDestroyed = FindObject(castleWireframes, "wireframeDestroyed");
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
		GameObject modelToSwap = null;
		GameObject wireframeToSwap = null;

		if (monumentFull.activeSelf || monumentWireframe.activeSelf)
		{
			modelToSwap = monumentFull;
			wireframeToSwap = monumentWireframe;
		}
		else if (monumentPartial.activeSelf || monumentWireframePartial.activeSelf)
		{
			modelToSwap = monumentPartial;
			wireframeToSwap = monumentWireframePartial;
		}
		else if (monumentDestroyed.activeSelf || monumentWireframeDestroyed.activeSelf)
		{
			modelToSwap = monumentDestroyed;
			wireframeToSwap = monumentWireframeDestroyed;
		}

		if (modelToSwap == null || wireframeToSwap == null)
		{
			Debug.LogError("Model or Wireframe are null");
			return;
		}

		setModelActive(modelToSwap, wireframeToSwap.activeSelf);
		setWireframeActive(wireframeToSwap, !wireframeToSwap.activeSelf);

	}

	private void setModelActive(GameObject model, bool active)
	{
		// To disable and enable the model
//		model.SetActive(active);

		// To set model to be semi-transparent
		// 0 means opaque
		setModelTransparency(model, active == true ? 0 : transparency);
	}

	private void setWireframeActive(GameObject wireframe, bool active)
	{
		wireframe.SetActive(active);
	}

	/**
	 * Assuming @param model is the castle model container
	 * Gets the actual object with the Mesh Renderer component (which is called 'default' in the children of each castle model)
	 */
	private void setModelTransparency(GameObject model, float transparency)
	{
		GameObject modelMeshObject = FindObject(model, "default");

		foreach (Material material in modelMeshObject.GetComponent<Renderer>().materials)
		{
			if (!material.HasProperty("_Transparency"))
			{
				continue;
			}
			material.SetFloat("_Transparency", transparency);
		}
	}

	public void setMonumentFull()
	{
		monumentFull.SetActive(true);
		monumentPartial.SetActive(false);
		monumentDestroyed.SetActive(false);
		monumentWireframe.SetActive(false);
		monumentWireframePartial.SetActive(false);
		monumentWireframeDestroyed.SetActive(false);

		setModelTransparency(monumentFull, 0);
	}

	public void setMonumentPartial()
	{
		monumentFull.SetActive(false);
		monumentPartial.SetActive(true);
		monumentDestroyed.SetActive(false);
		monumentWireframe.SetActive(false);
		monumentWireframePartial.SetActive(false);
		monumentWireframeDestroyed.SetActive(false);

		setModelTransparency(monumentPartial, 0);
	}

	public void setMonumentDestroyed()
	{
		monumentFull.SetActive(false);
		monumentPartial.SetActive(false);
		monumentDestroyed.SetActive(true);
		monumentWireframe.SetActive(false);
		monumentWireframePartial.SetActive(false);
		monumentWireframeDestroyed.SetActive(false);

		setModelTransparency(monumentDestroyed, 0);
	}

    public void timeLineChange() {
        if (mainSlider != null) { //If for some reason this function is called without the slider being defined.
            float value = mainSlider.value;

            if (value < 0.25) {
                //Debug.Log("Destroyed");
	            setMonumentFull();
            } else if (value >= 0.75) {
                //Debug.Log("Full");
	            setMonumentDestroyed();
            } else {
                //Debug.Log("Partial");
                setMonumentPartial();
            }
        }
    }

}
