using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using EQRSDroid.Model;

namespace EQRSDroid.Utilities
{
    public class FirstAidConfigReader
    {
        private string fAidContent;
        private StreamReader reader;
        private List<Tip> tips;

        public FirstAidConfigReader(Stream fAidStream)
        {
            this.reader = new StreamReader(fAidStream);
            fAidContent = reader.ReadToEnd();
            Tips = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Tip>>(fAidContent);
        }

        public IEnumerable<Tip> GetTipsByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                return Tips;

            return Tips.Where(t => t.Title.ToLower().Contains(title.ToLower()));
        }

        public List<Tip> Tips
        {
            get
            {
                return tips;
            }

            set
            {
                tips = value;
            }
        }
    }
}