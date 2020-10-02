using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;


namespace UMUI
{


    public class Data
    {

    }

    public class Data<T> : Data
    {

        public T value;

        public Data (T value)
        {
            this.value = value;
        }

    }

    public class GenericPanelData : UMUI.Data
    {

        public string title;
        public string message;
        public string negativeText;
        public UnityAction negativeEvent;
        public bool isPositiveButton;
        public UnityAction positiveEvent;
        public string positiveText;

        public GenericPanelData (string title, string message, string negativeText, UnityAction negativeEvent, bool isPositiveButton, UnityAction positiveEvent, string positiveText)
        {
            this.title = title;
            this.message = message;
            this.negativeText = negativeText;
            this.negativeEvent = negativeEvent;
            this.isPositiveButton = isPositiveButton;
            this.positiveEvent = positiveEvent;
            this.positiveText = positiveText;
        }

    }

    public interface IStorageService
    {

        IList<Data> data
        {
            get;
        }

        void Save (Data dataEntity);

        Data Load<T> ();

    }

    public class LocalStorageService : IStorageService
    {

        static LocalStorageService instance;
        public IList<Data> data
        {
            get;
        }

        public static LocalStorageService GetService ()
        {
            if (instance == null)
                instance = new LocalStorageService ();
            return instance;
        }

        private LocalStorageService ()
        {
            data = new List<Data> ();
        }


        public void Save (Data dataEntity)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].GetType ().Name == dataEntity.GetType ().Name)
                {
                    data[i] = dataEntity;
                    return;
                }
            }
          
            this.data.Add (dataEntity);
        }

        public Data Load<T> ()
        {
            return data.FirstOrDefault (data1 => data1.GetType () == typeof (T));
        }

    }


}