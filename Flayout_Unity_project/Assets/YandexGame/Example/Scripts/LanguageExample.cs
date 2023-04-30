using UnityEngine;
using UnityEngine.UI;

namespace YG.Example
{
	public class LanguageExample : MonoBehaviour
	{
		[SerializeField] string[] languages = new string[] {"ru", "tr", "en"};
		[SerializeField] string defaultLanguage = "en";

		Text textObj;

		private void Awake()
		{
			textObj = GetComponent<Text>();
			SwitchLanguage(YandexGame.savesData.language);
		}

		private void OnEnable() => YandexGame.SwitchLangEvent += SwitchLanguage;
		private void OnDisable() => YandexGame.SwitchLangEvent -= SwitchLanguage;

		public void SwitchLanguage(string lang)
		{
			foreach (var language in languages)
			{
				if (language == lang)
				{
					SetLanguage(lang);

					return;
				}
			}

			SetLanguage(defaultLanguage);
		}

		private void SetLanguage(string lang)
		{
            textObj.text = lang;
        }
	}
}