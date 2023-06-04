
using System.Collections.Generic;
using UnityEditor;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Ваши сохранения

        public int Money;
        public int LastSelectedCarIndex;
        public bool[] BuyedCar;

        public float MusicVolume = 1f;
        public float MainVolume = 1f;

        private const float DefaultMusicVolume = 1f;
        private const float DefaultMainVolume = 1f;
        private const int DefaultMoney = 10000;
        private const int DefaultLastSelectedCarIndex = 1;
        private bool[] DefaultBuyedCar = new bool[] { true };

        public void ResetVolumeOptions()
        {
            MusicVolume = DefaultMusicVolume;
            MainVolume = DefaultMainVolume;
        }

        public void ResetPlayerSaves()
        {
            Money = DefaultMoney;
            LastSelectedCarIndex = DefaultLastSelectedCarIndex;
            BuyedCar = DefaultBuyedCar;
        }

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            Money = DefaultMoney;
            BuyedCar = DefaultBuyedCar;

            // Длина массива в проекте должна быть задана один раз!
            // Если после публикации игры изменить длину массива, то после обновления игры у пользователей сохранения могут поломаться
            // Если всё же необходимо увеличить длину массива, сдвиньте данное поле массива в самую нижнюю строку кода
        }
    }
}
