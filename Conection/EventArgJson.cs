using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conection
{
    public class EventArgJson: EventArgs
    {
        private readonly string _json;

        public EventArgJson(string json)
        {
            this._json = json;
        }

        public string Json
        {
            get { return _json; }
        } 

    }
}
