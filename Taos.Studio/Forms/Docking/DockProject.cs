using ICSharpCode.TextEditor;
using Taos.Studio.Forms;
using Maikebing.Data.Taos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Taos.Studio.Properties;
using DarkUI.Controls;
using DarkUI.Docking;
using Taos.Studio.Classes;

namespace Taos.Studio
{
    public partial class DockProject : DarkToolWindow
    {

        public DockProject()
        {
            InitializeComponent();
        }

        public void LoadTree(Maikebing.Data.Taos.TaosConnection _db,string groupname= "Default Gruop")
        {
            if (treeProject.Nodes.Count == 0)
            {
                var default_group_node = new DarkTreeNode("Default Gruop");
                default_group_node.Tag = new NodeTag() { NodeType = NodeType.Group };
                default_group_node.ExpandedIcon = Taos.Studio.Properties.Resources.folder_open;
                default_group_node.Icon = Taos.Studio.Properties.Resources.folder_closed;
                treeProject.Nodes.Add(default_group_node);
            }
            if (!treeProject.Nodes.Any(n => (n.Tag as NodeTag)?.NodeType == NodeType.Group && n.Text == groupname))
            {
                var default_group_node = new DarkTreeNode(groupname);
                default_group_node.Tag = new NodeTag() { NodeType = NodeType.Group };
                default_group_node.ExpandedIcon = Taos.Studio.Properties.Resources.folder_open;
                default_group_node.Icon = Taos.Studio.Properties.Resources.folder_closed;
                treeProject.Nodes.Add(default_group_node);
            }
            var cnt = new TaosConnectionStringBuilder(_db.ConnectionString);
            var root = new DarkTreeNode(cnt.DataSource);
           
            root.ExpandedIcon = Taos.Studio.Properties.Resources.folder_open;
            root.Icon = Taos.Studio.Properties.Resources.folder_closed;
            root.Tag = new NodeTag() { NodeType = NodeType.Server, Taos = _db ,TaosInfo= cnt };
            var table = _db.CreateCommand("SHOW DATABASES").ExecuteReader().ToJson();
            table.ToList().ForEach(a =>
            {
                var name = Encoding.UTF8.GetString(a.Value<byte[]>("name")).TrimEnd('\0');
                var childNode = new DarkTreeNode(name);
                childNode.Icon = Taos.Studio.Properties.Resources.database;
                childNode.ExpandedIcon= Taos.Studio.Properties.Resources.database_connect;
                childNode.Tag = new NodeTag() { NodeType = NodeType.DataBase, Taos = _db,TaosInfo=cnt , DataBaseName=name,DBInfo=a };
                root.Nodes.Add(childNode);
            });
            var node = treeProject.Nodes.FirstOrDefault(n => (n.Tag as NodeTag)?.NodeType == NodeType.Group && n.Text == groupname);
            if (node != null)
            {
                node.Nodes.Add(root);
            }

        }
        public TaosConnection TaosConnection { get => (treeProject.SelectedNodes?.FirstOrDefault().Tag ?? treeProject.SelectedNodes?.FirstOrDefault()?.ParentNode?.Tag) as TaosConnection; }

        private void treeProject_SelectedNodesChanged(object sender, EventArgs e)
        {
            treeProject.SelectedNodes?.ToList().ForEach(dtx =>
            {
                var tag = dtx.Tag as NodeTag;
                if (tag!=null &&tag.NodeType== NodeType.DataBase)
                {
                    tag.Taos.ChangeDatabase(tag.DataBaseName);
                }
            });
        }

        private void menuDataBase_Opening(object sender, CancelEventArgs e)
        {
            var nt = treeProject.SelectedNodes?.FirstOrDefault()?.Tag as NodeTag;
            if (nt!=null)
            {
                switch (nt.NodeType)
                {
                    case NodeType.Group:
                        break;
                    case NodeType.DataBase:
                        break;
                    case NodeType.Table:
                        break;
                    case NodeType.InfoType:
                        break;
                    case NodeType.Field:
                        break;
                    case NodeType.Index:
                        break;
                    default:
                        break;
                }
            }    
        }
        public delegate void DShowDocument(string title,string sql,NodeTag nt);
        public event DShowDocument ShowDocument=null;
        private void sELECTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nt = treeProject.SelectedNodes?.FirstOrDefault()?.Tag as NodeTag;
            if (nt != null && nt.NodeType== NodeType.Table)
            {
                ShowDocument?.Invoke(sELECTToolStripMenuItem.Text, $"SELECT  * FROM {nt.TableName}",nt );
            }
        }

        private void treeProject_DoubleClick(object sender, EventArgs e)
        {
            var node = treeProject.SelectedNodes?.FirstOrDefault();
            var nt = node?.Tag as NodeTag;
            if (nt != null)
            {
                switch (nt.NodeType)
                {
                    case NodeType.Group:
                        break;
                    case NodeType.Server:
                        break;
                    case NodeType.DataBase:
                        {
                            nt.Taos.ChangeDatabase(nt.DataBaseName);
                            var jtable = nt.Taos.CreateCommand("SHOW TABLES ").ExecuteReader().ToJson();
                            jtable.ToList().ForEach(a =>
                            {
                                var name = Encoding.UTF8.GetString(a.Value<byte[]>("table_name")).TrimEnd('\0');
                                var ndx = node.Nodes.FirstOrDefault(n => n.Text == name);
                                if (ndx == null)
                                {
                                    ndx = new DarkTreeNode(name);
                                    node.Nodes.Add(ndx);
                                }

                                ndx.Icon = Properties.Resources.table;
                                ndx.ExpandedIcon = Resources.table_lightning;
                                var tnt = nt;
                                tnt.NodeType = NodeType.Table;
                                tnt.TableName = name;
                                tnt.TableInfo = a;
                                ndx.Tag = tnt;
                            });
                        }
                        break;
                    case NodeType.Table:
                        break;
                    case NodeType.InfoType:
                        break;
                    case NodeType.Field:
                        break;
                    case NodeType.Index:
                        break;
                    default:
                        break;
                }
               
            }
        }
    }
}
