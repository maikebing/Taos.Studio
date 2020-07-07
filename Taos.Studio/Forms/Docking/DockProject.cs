﻿using ICSharpCode.TextEditor;
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

namespace Taos.Studio
{
    public partial class DockProject : DarkToolWindow
    {

        public DockProject()
        {
            InitializeComponent();
        }

        public void LoadTree(Maikebing.Data.Taos.TaosConnection _db)
        {
            var root = new DarkTreeNode(new TaosConnectionStringBuilder(_db.ConnectionString).DataSource);
            root.ExpandedIcon = Taos.Studio.Properties.Resources.folder_open;
            root.Icon = Taos.Studio.Properties.Resources.folder_closed;
            root.Tag = _db;
            var table = _db.CreateCommand("SHOW DATABASES").ExecuteReader().ToJson();
            table.ToList().ForEach(a =>
            {
                var name = Encoding.UTF8.GetString(a.Value<byte[]>("name")).TrimEnd('\0');
                var childNode = new DarkTreeNode(name);
                childNode.Icon = Taos.Studio.Properties.Resources.database;
                root.Nodes.Add(childNode);
            });
            treeProject.Nodes.Add(root);
        }
        public TaosConnection TaosConnection { get => (treeProject.SelectedNodes?.FirstOrDefault().Tag ?? treeProject.SelectedNodes?.FirstOrDefault()?.ParentNode?.Tag) as TaosConnection; }

        private void treeProject_SelectedNodesChanged(object sender, EventArgs e)
        {

        }
    }
}