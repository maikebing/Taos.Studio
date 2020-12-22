using ICSharpCode.TextEditor;
using Maikebing.Data.Taos;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Taos.Studio.Properties;

namespace Taos.Studio
{
    internal static class UIExtensions
    {
        internal static void FillTableToTree(this TreeNodeMouseClickEventArgs e, TaosConnection _db, ContextMenuStrip contextMenu, string rootkey, string title, string imgkey, string sql, string tablename)
        {
            TreeNode stable = null;
            if (!e.Node.Nodes.ContainsKey(rootkey))
            {
                stable = e.Node.Nodes.Add(rootkey, title, imgkey);
                stable.SelectedImageKey = "";
            }
            else
            {
                stable = e.Node.Nodes[rootkey];
            }
            JArray sjtable = _db.CreateCommand(sql).ExecuteReader().ToJson();
            List<string> stlst = new List<string>();

            sjtable.ToList().ForEach(a =>
            {
                string name = a.Value<string>(tablename).RemoveNull();
                stlst.Add(name);
                if (!stable.Nodes.ContainsKey(name))
                {
                    TreeNode node = stable.Nodes.Add(name, name, imgkey);
                    node.Tag = a;
                    node.ContextMenuStrip = contextMenu;
                }
            });
            foreach (TreeNode item in stable.Nodes)
            {
                if (stlst.Contains(item.Name))
                {
                    stlst.Remove(item.Name);
                }
            }
            stlst.ForEach(s =>
            {
                stable.Nodes.RemoveByKey(s);
            });
        }

        public static void AddSeries(this Chart chartMain, string ymembers, ChartValueType valueType)
        {
            Series series1 = new Series
            {
                Name = $"series_{ymembers}",
                ChartArea = "ChartArea1",
                ChartType = SeriesChartType.Line,
                Legend = "Legend1",
                XValueType = ChartValueType.DateTime,
                XValueMember = "ts",
                YValueMembers = ymembers,
                YValueType = valueType,
                Color = GetRandomColor()
            };
            chartMain.Series.Add(series1);
        }

        public static Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            //  对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);

            //  为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }

        internal static DataTable RemoveDataTableNullColumns(this DataTable dt)
        {
            try
            {
                foreach (DataColumn column in dt.Columns.Cast<DataColumn>().ToArray())
                {
                    if (column.ColumnName != "ts")
                    {
                        IEnumerable<DataRow> rows = dt.Rows.OfType<DataRow>();

                        if (rows.All(dr => dr.IsNull(column)))
                        {
                            dt.Columns.Remove(column);
                        }
                        else
                        {
                            if (column.DataType == typeof(int))
                            {
                                if (rows.Max(r => (int)r[column]) == rows.Min(r => (int)r[column]))
                                {
                                    dt.Columns.Remove(column);
                                }
                            }
                            if (column.DataType == typeof(long))
                            {
                                if (rows.Max(r => (long)r[column]) == rows.Min(r => (long)r[column]))
                                {
                                    dt.Columns.Remove(column);
                                }
                            }
                            else if (column.DataType == typeof(double))
                            {
                                if (rows.Max(r => (double)r[column]) == rows.Min(r => (double)r[column]))
                                {
                                    dt.Columns.Remove(column);
                                }
                            }
                            else if (column.DataType == typeof(float))
                            {
                                if (rows.Max(r => (float)r[column]) == rows.Min(r => (float)r[column]))
                                {
                                    dt.Columns.Remove(column);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

        
            }
            return dt;
        }

        public static void BindBsonData(this DataGridView grd, Chart chart, TaskData data, TextEditorControl text)
        {
            text.AppendLine($"{DateTime.Now}-准备数据..");
            grd.Clear();
            text.Clear();
            chart.Series.Clear();
           
            grd.DataSource = data.Result;
            DataTable dt = data.Result?.Copy().RemoveDataTableNullColumns();
            chart.DataSource = dt;
            text.AppendLine($"{DateTime.Now}-合成曲线图..");

            if (dt?.Columns.OfType<DataColumn>().Any(col => col.ColumnName == "ts") == true)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "ts")
                    {
                        if (col.DataType == typeof(long))
                        {
                            chart.AddSeries(col.ColumnName, ChartValueType.Int64);
                        }
                        else if (col.DataType == typeof(int))
                        {
                            chart.AddSeries(col.ColumnName, ChartValueType.Int32);
                        }
                        else if (col.DataType == typeof(float))
                        {
                            chart.AddSeries(col.ColumnName, ChartValueType.Single);
                        }
                        else if (col.DataType == typeof(double))
                        {
                            chart.AddSeries(col.ColumnName, ChartValueType.Double);
                        }
                    }
                }
                chart.DataBind();
            }
            if (data.LimitExceeded)
            {
                DataGridViewRow limitRow = new DataGridViewRow();
                limitRow.CreateCells(grd);
                limitRow.DefaultCellStyle.ForeColor = Color.OrangeRed;
                DataGridViewCell cell = limitRow.Cells[0];
                cell.Value = Resources.LimitExceeded;
                cell.ReadOnly = true;
                grd.Rows.Add(limitRow);
            }

            for (int i = 0; i <= grd.Columns.Count - 1; i++)
            {
                int colw = grd.Columns[i].Width;
                grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grd.Columns[i].Width = Math.Min(colw, 400);
            }

            if (grd.Rows.Count == 0)
            {
                grd.Columns.Add("no-data", Resources.NoResult);
            }

            grd.ReadOnly = grd.Columns["_id"] == null;
            grd.Visible = true;
            text.AppendLine($"{DateTime.Now}-完成..");
        }

        public static void SetBsonType(this DataColumn row, JToken value)
        {
            if (value == null)
            {
                row.DataType = typeof(object);
                return;
            }
            switch (value.Type)
            {
                case JTokenType.Boolean:
                    row.DataType = typeof(bool);
                    break;

                case JTokenType.Date:
                    row.DataType = typeof(DateTime);
                    break;

                case JTokenType.Bytes:
                    row.DataType = typeof(string);
                    break;

                case JTokenType.Integer:
                    row.DataType = typeof(int);
                    break;

                case JTokenType.Float:
                    row.DataType = typeof(float);
                    break;

                case JTokenType.String:
                    row.DataType = typeof(string);
                    break;

                case JTokenType.Guid:
                    row.DataType = typeof(Guid);
                    break;

                case JTokenType.TimeSpan:
                    row.DataType = typeof(TimeSpan);
                    break;

                default:
                    row.DataType = typeof(object);
                    break;
            }
        }

        public static void SetBsonValue(this DataRow row, string colname, JToken value)
        {
            if (value == null)
            {
                row.SetField(colname, "");
                return;
            }
            switch (value.Type)
            {
                case JTokenType.Boolean:
                    row.SetField(colname, value.Value<bool>());
                    break;

                case JTokenType.Date:
                    row.SetField(colname, value.Value<DateTime>());
                    break;

                case JTokenType.Bytes:
                    row.SetField(colname, Encoding.Default.GetString(value.Value<byte[]>()).TrimEnd('\0'));
                    break;

                case JTokenType.Integer:
                    row.SetField(colname, value.Value<int>());
                    break;

                case JTokenType.Float:
                    row.SetField(colname, value.Value<float>());
                    break;

                case JTokenType.String:
                    row.SetField(colname, value.Value<string>());
                    break;

                case JTokenType.Guid:
                    row.SetField(colname, value.Value<Guid>());
                    break;

                case JTokenType.TimeSpan:
                    row.SetField(colname, value.Value<TimeSpan>());
                    break;

                default:
                    row.SetField(colname, value.ToString());
                    break;
            }
        }

        public static void SetBsonValue(this DataGridViewCell cell, JToken value)
        {
            if (value == null)
            {
                cell.Value = "";
                cell.Tag = null;
                return;
            }

            switch (value.Type)
            {
                case JTokenType.Boolean:
                    cell.Value = value.Value<bool>().ToString().ToLower();
                    break;

                case JTokenType.Date:
                    cell.Value = value.Value<DateTime>().ToString();
                    break;

                case JTokenType.Null:
                    cell.Value = Resources.Null;
                    cell.Style.ForeColor = Color.Silver;
                    break;

                case JTokenType.Bytes:
                    cell.Value = Encoding.UTF8.GetString(value.Value<byte[]>()).TrimEnd('\0');
                    break;

                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.String:
                case JTokenType.Guid:
                case JTokenType.TimeSpan:
                    cell.Value = value.ToString();
                    break;

                default:
                    cell.Value = value.ToString();
                    break;
            }

            cell.ToolTipText = value.Type.ToString();
            cell.Tag = value;
        }

        public static void DoubleBuffered(this DataGridView dgv, bool setting)
        {
            Type dgvType = dgv.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        public static void Clear(this DataGridView grd)
        {
            grd.Columns.Clear();
            grd.DataSource = null;
        }

        public static void BindErrorMessage(this DataGridView grid, string sql, Exception ex)
        {
            grid.Clear();
            grid.Columns.Add("err", "Error");
            grid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Rows.Add(ex.Message);
        }

        public static void BindErrorMessage(this TextEditorControl txt, string sql, Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            if (!(ex is TaosException))
            {
                sb.AppendLine(ex.Message);
                sb.AppendLine();
                sb.AppendLine("===================================================");
                sb.AppendLine(ex.StackTrace);
            }
            else
            {
                TaosException tex = ex as TaosException;
                sb.AppendLine($"Error code: {tex.ErrorCode}");
                sb.AppendLine($"Error message:{tex.Message}");
                if (tex.Data != null)
                {
                    foreach (string item in tex.Data.Keys)
                    {
                        sb.AppendLine($"{item}:{tex.Data[item]}");
                    }
                }
                sb.AppendLine("===================================================");
                sb.AppendLine(tex.StackTrace);
            }

            txt.Highlighting = null;
            txt.Clear();
            txt.Text = sb.ToString();
        }
    }
}