using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RichmondDay.Helpers {
    /// <summary>
    /// grabbed this online somewhere, it's a great class.
    /// </summary>
    public class SendGridSmtpApiHeader {
        private JObject data = new JObject();

        public void AddTo(string to) {
            if (data["to"] == null)
                data.Add(new JProperty("to", new JArray()));

            (data["to"] as JArray).Add(new JValue(to));
        }

        public void AddTo(string[] tos) {
            foreach (string to in tos)
                AddTo(to);
        }

        public void AddSubVal(string var, string val) {
            if (data["sub"] == null)
                data.Add(new JProperty("sub", new JObject()));

            if (data["sub"][var] == null)
                (data["sub"] as JObject).Add(new JProperty(var, new JArray()));

            (data["sub"][var] as JArray).Add(new JValue(val));
        }

        public void AddSubVal(string var, string[] vals) {
            foreach (string val in vals)
                AddSubVal(var, val);
        }

        //Not sure what this is for yet, use JObject for now.
        public void SetUniqueArgs(JObject val) {
            if (data["unique_args"] == null)
                data.Add(new JProperty("unique_args", val));
            else
                data["unique_args"] = val;
        }

        public void SetCategory(string cat) {
            if (data["category"] == null)
                data.Add(new JProperty("category", cat));
            else
                (data["category"] as JValue).Value = cat;
        }

        public void AddFilterSetting(string filter, string setting, object value) {
            if (data["filters"] == null)
                data.Add(new JProperty("filters", new JObject()));

            if (data["filters"][filter] == null)
                (data["filters"] as JObject).Add(new JProperty(filter, new JObject(new JProperty("settings", new JObject()))));

            if (data["filters"][filter]["settings"][setting] == null)
                (data["filters"][filter]["settings"] as JObject).Add(new JProperty(setting, value));
            else
                (data["filters"][filter]["settings"][setting] as JValue).Value = value;
        }

        public string ToJson() {
            using (StringWriter sw = new StringWriter()) {
                JsonTextWriter jw =
                new JsonTextWriter(sw) {
                    QuoteName = true,
                    Formatting = Formatting.None //use formatting so lines are not too long in the SMTP header
                };
                data.WriteTo(jw);
                return sw.ToString();
            }
        }

        public override string ToString() {
            string json = ToJson();
            return json;
        }
    }
}