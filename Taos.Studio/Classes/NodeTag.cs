using Maikebing.Data.Taos;
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
       DataBase,
       Table,
       InfoType,
       Field,
       Index
    }
    public class NodeTag
    {
        public NodeType NodeType { get; set; }
        public TaosConnection Taos { get; set; }
    }
}
