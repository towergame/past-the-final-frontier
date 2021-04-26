using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ButtonBase : MonoBehaviour
{
	public UnityEvent<ButtonContext> click;
	public virtual void Click(ButtonContext context)
	{
		// do shit
		Debug.Log("A button were pressed.");
		context.metaManager.playClick();
		click.Invoke(context);
	}

	public Color basicColor;
	public Color hoverColor;
	public bool dim = false;
	private TextMeshPro textmesho;

	void Start()
	{
		if (transform.TryGetComponent<TextMeshPro>(out textmesho)) return;
		textmesho = transform.GetChild(0).GetComponent<TextMeshPro>();
	}

	void OnMouseEnter()
	{
		if (!dim) return;
		textmesho.color = hoverColor;
	}

	void OnMouseExit()
	{
		if (!dim) return;
		textmesho.color = basicColor;
	}
}
