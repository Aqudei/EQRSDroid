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
    public class EmergenciesConfigReader
    {
        private List<Category> categories;
        private string configContent;
        private StreamReader reader;

        public EmergenciesConfigReader(Stream assetStream)
        {
            this.reader = new StreamReader(assetStream);
            configContent = reader.ReadToEnd();
            categories = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Category>>(configContent);
        }

        public IEnumerable<string> Read(string type)
        {
            var upper = type.ToUpper();
            var cat = categories.Where(w => w.Code == upper).FirstOrDefault();
            if (cat != null)
            {
                return cat.Emergencies;
            }
            else { return null; }
        }
    }
}