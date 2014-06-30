using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using LittlePlace.Api;
using LittlePlace.Api.Infrastructure;

namespace LittlePlace.WPClient.UI.Services
{
    public class SettingService : ISettingService
    {

        public CookieContainer AuthCookies
        {
            get { return GetCookies(); }
            set { SaveCookies(value); }
        }

        public T GetData<T>(string name)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(name))
                return (T)settings[name];
            else
                return default(T);
        }

        public T GetDataObject<T>(string name, bool create = false) where T : new()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(name))
                return (T)settings[name];
            else
                if (create)
                {
                    T setting = new T();
                    SetData(name, setting);
                    return setting;
                }
                else
                    return default(T);
        }

        public void SetData(string name, object value)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(name))
                settings[name] = value;
            else
                settings.Add(name, value);
            settings.Save();
        }

        private void SaveCookies(CookieContainer cookieContainer)
        {
            var cookies = cookieContainer.GetCookies(new Uri("/"));
            using (IsolatedStorageFile isf =IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs = isf.OpenFile("CookieExCookies",FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(isfs))
                    {
                        foreach (Cookie cookieValue in cookieContainer.GetCookies(new Uri(Constants.Host)))
                        {
                            sw.WriteLine(String.Format("{0} {1} {2}",cookieValue.Name,cookieValue.Value,cookieValue.Path));
                        }
                        sw.Close();
                    }
                }

            }
        }
        private CookieContainer GetCookies()
        {
            var cookieContainer = new CookieContainer();
      
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs =
                   isf.OpenFile("CookieExCookies", FileMode.OpenOrCreate))
                {
                    using (StreamReader sr = new StreamReader(isfs))
                    {
                        var cookie = sr.ReadLine();
                        if (!String.IsNullOrEmpty(cookie))
                        {
                            var resCookie = cookie.Split(new char[] {' '});
                            cookieContainer.Add(new Uri(Constants.Host), new Cookie(resCookie[0],resCookie[1],resCookie[2]));
                        }
                      
                    }
                }

            }
            return cookieContainer;
        }


        public void Clear()
        {
            IsolatedStorageSettings.ApplicationSettings.Clear();
        }

        public void Save()
        {
            IsolatedStorageSettings.ApplicationSettings.Save();
        }
    }
}
