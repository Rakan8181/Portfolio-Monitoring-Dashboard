using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Trading.Library
{
        public class JReader : Newtonsoft.Json.JsonTextReader
        {
            public JReader(TextReader r) : base(r)
            {
            }
            public override bool Read()
            {
                bool b = base.Read();
                if (base.CurrentState == State.Property && ((string)base.Value).Contains(' '))
                {
                    base.SetToken(JsonToken.PropertyName, ((string)base.Value).Replace("1. ", ""));
                    base.SetToken(JsonToken.PropertyName, ((string)base.Value).Replace("2. ", ""));
                    base.SetToken(JsonToken.PropertyName, ((string)base.Value).Replace("3. ", ""));
                    base.SetToken(JsonToken.PropertyName, ((string)base.Value).Replace("4. ", ""));
                    base.SetToken(JsonToken.PropertyName, ((string)base.Value).Replace("5. ", ""));
                    base.SetToken(JsonToken.PropertyName, ((string)base.Value).Replace("6. ", ""));
                    base.SetToken(JsonToken.PropertyName, ((string)base.Value).Replace(" ", "_"));
                }
                return b;
            }
        }

}