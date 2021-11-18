using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Webshop.DTO;

namespace Webshop.DataSource
{
    public class Source_Card : DataSource_JSON<CardDTO>
    {
        private static string _PATH = AppDomain.CurrentDomain.BaseDirectory + "cards.json";

        public Source_Card() : base(_PATH){ }
    }
}
