using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
	[Header ("space between menu items")]
	[SerializeField] Vector3 spacing;

	[Space]
	[Header ("Main button rotation")]
	[SerializeField] float rotationDuration;
	[SerializeField] Ease rotationEase;

	[Space]
	[Header ("Animation")]
	[SerializeField] float expandDuration;
	[SerializeField] float collapseDuration;
	[SerializeField] Ease expandEase;
	[SerializeField] Ease collapseEase;

	[Space]
	[Header ("Fading")]
	[SerializeField] float expandFadeDuration;
	[SerializeField] float collapseFadeDuration;

	Button mainButton;
	SettingsMenuItem[] menuItems;

	//is menu opened or not
	bool isExpanded = false;
	bool musicIsOn;

	[SerializeField] Vector3 mainButtonPosition;
	int itemsCount;

	[SerializeField] private GameObject Settings;
	[SerializeField] private GameObject TurnOffSound;
	private Image TurnOffSoundImg;

	void Start ()
	{
		TurnOffSoundImg = TurnOffSound.GetComponent<Image>();
		//add all the items to the menuItems array
		itemsCount = transform.childCount - 1;
		menuItems = new SettingsMenuItem[itemsCount];
		for (int i = 0; i < itemsCount; i++) {
			// +1 to ignore the main button
			menuItems [i] = transform.GetChild (i + 1).GetComponent <SettingsMenuItem> ();
		}

		mainButton = transform.GetChild (0).GetComponent <Button> ();
		mainButton.onClick.AddListener (ToggleMenu);
		//SetAsLastSibling () to make sure that the main button will be always at the top layer
		mainButton.transform.SetAsLastSibling ();

		mainButtonPosition = new Vector3(mainButton.transform.position.x, mainButton.transform.position.y, 0f);

		//set all menu items position to mainButtonPosition
		ResetPositions ();
	}

	void ResetPositions ()
	{
		for (int i = 0; i < itemsCount; i++) {
			menuItems [i].trans.position = new Vector3(mainButton.transform.position.x, mainButton.transform.position.y, mainButton.transform.position.y);
		}
	}

	void ToggleMenu ()
	{
		isExpanded = !isExpanded;

		if (isExpanded) {
			for (int i = 0; i < itemsCount; i++) {
				mainButtonPosition = new Vector3(mainButton.transform.position.x, mainButton.transform.position.y, 0f);
				menuItems [i].trans.DOMove (mainButtonPosition + Screen.width/10f*spacing * (i + 1), expandDuration).SetEase (expandEase);
				menuItems [i].img.DOFade (1f, expandFadeDuration).From (0f);
				TurnOffSoundImg.DOFade(1f, expandFadeDuration).From(0f);
			}
		} else {
			for (int i = 0; i < itemsCount; i++) {
				mainButtonPosition = new Vector3(mainButton.transform.position.x, mainButton.transform.position.y, 0f);
				menuItems [i].trans.DOMove (mainButtonPosition, collapseDuration).SetEase (collapseEase);
				menuItems [i].img.DOFade (0f, collapseFadeDuration);
				TurnOffSoundImg.DOFade(0f, 0.45f);
			}
		}


		mainButton.transform
			.DORotate (Vector3.forward * 180, rotationDuration)
            .From(Vector3.zero)
            .SetEase (rotationEase);
	}

	public void OnItemClick (int index)
	{
		switch (index) {
			case 0:
				//first button
				if (!musicIsOn)
				{
					musicIsOn = !musicIsOn;
				}
                else
                {
					musicIsOn = !musicIsOn;
				}
				break;
			case 1:
				//second button
				Settings.SetActive(true);
				break;
		}
	}

	public void StartCurotineCloseSettings()
    {
		StartCoroutine("CloseSettings");
    }

	public IEnumerator CloseSettings()
    {
		Settings.transform.DOScale(Vector3.zero, 0.9f);
		yield return new WaitForSeconds(1);
		Settings.SetActive(false);
	}


	void OnDestroy ()
	{
		//remove click listener to avoid memory leaks
		mainButton.onClick.RemoveListener (ToggleMenu);
	}
}
