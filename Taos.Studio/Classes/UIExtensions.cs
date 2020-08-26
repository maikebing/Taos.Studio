using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using Maikebing.Data.Taos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Taos.Studio.Properties;

namespace Taos.Studio
{
    static class UIExtensions
    {
        public static void AddSeries(this Chart chartMain,string ymembers, ChartValueType  valueType )
        {
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            series1.Name = $"series_{ymembers}";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.XValueMember = "ts";
            series1.YValueMembers = ymembers;
            series1.YValueType = valueType;
            chartMain.Series.Add(series1);
        }
        public static void BindBsonData(this DataGridView grd,Chart  chart, TaskData data)
        {
            // hide grid if has more than 100 rows
            grd.Visible = data.Result.Count < 100;
            grd.Clear();
            DataTable dt = new DataTable();
            foreach (var doc in data.Result)
            {

                foreach (JProperty key in doc.Children())
                {
                    var col = dt.Columns[key.Name];
                    if (col == null)
                    {
                         dt.Columns.Add(key.Name).SetBsonType(key.Value);
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (JProperty key in doc.Children())
                {
                    dr.SetBsonValue(key.Name, key.Value);
                }
                dt.Rows.Add(dr);
            }
            grd.DataSource = dt;
            chart.Series.Clear();
            chart.DataSource = dt;
            if (dt.Columns.OfType<DataColumn>().Any(col => col.ColumnName == "ts"))
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "ts")
                    {
                        if (col.DataType == typeof(int))
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
                        else if (col.DataType == typeof(DateTime))
                        {
                            chart.AddSeries(col.ColumnName, ChartValueType.DateTime);
                        }
                    }
                }
                chart.DataBind();
            }
            if (data.LimitExceeded)
            {
                var limitRow = new DataGridViewRow();
                limitRow.CreateCells(grd);
                limitRow.DefaultCellStyle.ForeColor = Color.OrangeRed;
                var cell = limitRow.Cells[0];
                cell.Value = Resources.LimitExceeded;
                cell.ReadOnly = true;
                grd.Rows.Add(limitRow);
            }

            for (int i = 0; i <= grd.Columns.Count - 1; i++)
            {
                var colw = grd.Columns[i].Width;
                grd.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grd.Columns[i].Width = Math.Min(colw, 400);
            }

            if (grd.Rows.Count == 0)
            {
                grd.Columns.Add("no-data", Resources.NoResult);
            }

            grd.ReadOnly = grd.Columns["_id"] == null;
            grd.Visible = true;
        }
        public static void SetBsonType(this DataColumn row,  JToken value)
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
        public static void SetBsonValue(this DataRow row,string colname, JToken value)
        {
            if (value == null)
            {
                row.SetField(colname, "");
                return;
            }
            switch (value.Type)
            {
                case JTokenType.Boolean:
                    row.SetField(colname,   value.Value<bool>());
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
               
                case  JTokenType.Boolean:
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
                    cell.Value = Encoding.UTF8.GetString( value.Value<byte[]>() ).TrimEnd('\0') ;
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
            var dgvType = dgv.GetType();
            var pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(dgv, setting, null);
        }

        public static void Clear(this DataGridView grd)
        {
            grd.Columns.Clear();
            grd.DataSource = null;
        }

        public static void BindBsonData(this  TextEditorControl txt, TaskData data)
        {
            var index = 0;
            var sb = new StringBuilder();

       
                 
                 
                if (data.Result.Count > 0)
                {
                    foreach (var value in data.Result)
                    {
                        if (data.Result?.Count > 1)
                        {
                            sb.AppendLine($"/* {index++ + 1} */");
                        }

                    sb.Append( JsonConvert.SerializeObject(value));
                        sb.AppendLine();
                    }

                    if (data.LimitExceeded)
                    {
                        sb.AppendLine();
                        sb.AppendLine("/* Limit exceeded */");
                    }
                }
                else
                {
                    sb.AppendLine(Resources.NoResult);
                }

            txt.SetHighlighting("JSON");
            txt.Text = sb.ToString();
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
            var sb = new StringBuilder();

            if (!(ex is TaosException))
            {
                sb.AppendLine(ex.Message);
                sb.AppendLine();
                sb.AppendLine("===================================================");
                sb.AppendLine(ex.StackTrace);
            }
            else
            {
                var tex = ex as TaosException;
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

        public static void BindParameter(this TextEditorControl txt, TaskData data)
        {
            //txt.SuspendLayout();
            //txt.Clear();
            //txt.SetHighlighting("JSON");

            //var sb = new StringBuilder();

            //using (var writer = new StringWriter(sb))
            //{
            //    var w = new JsonWriter(writer)
            //    {
            //        Pretty = true,
            //        Indent = 2
            //    };

            //    w.Serialize(data.Parameters ?? BsonValue.Null);
            //}

            //txt.Text = sb.ToString();
            //txt.ResumeLayout();
        }
    }
}
