using Maikebing.Data.Taos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Taos.Studio.Classes
{
    public enum NodeType
    {
      Group=1,
        Server,
       DataBase,
       Table,
       InfoType,
       Field,
       Index,
      
    }
    public class NodeTag
    {
        public NodeType NodeType { get; set; }
        public TaosConnection Taos { get; set; }
        public string DataBaseName { get; internal set; }
        public TaosConnectionStringBuilder TaosInfo { get; internal set; }
        public JToken DBInfo { get; internal set; }
        public string TableName { get; internal set; }
        public JToken TableInfo { get; internal set; }
    }
}
