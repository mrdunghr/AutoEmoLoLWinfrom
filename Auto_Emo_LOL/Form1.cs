using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Emo_LOL
{
    public partial class Form1 : Form
    {
        private Object reg;
        private string path;
        private JObject jsonObject;
        public Form1()
        {
            InitializeComponent();
            reg = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\Riot Game league_of_legends.live", "InstallLocation", null);
            path = reg + "/Config/PersistedSettings.json";
            label1.Text = path;

            string jsonContent = File.ReadAllText(path);
            jsonObject = JObject.Parse(jsonContent);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                AutoChampMastery(jsonObject, path, true);
            }
            else
            {
                AutoChampMastery(jsonObject, path, false);
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                AutoEmoteLaugh(jsonObject, path, true);
            }
            else
            {
                AutoEmoteLaugh(jsonObject, path, false);
            }
        }

        private static void AutoChampMastery(JObject jsonObject, string path, bool status)
        {
            // tìm đến đối tượng có tên "evtChampMasteryDisplay" - thông thạo tướng
            JToken targetSetting = jsonObject.SelectToken("$.files[?(@.name == 'Input.ini')].sections[?(@.name == 'GameEvents')].settings[?(@.name == 'evtChampMasteryDisplay')]");
            Console.WriteLine("OLD");
            Console.WriteLine(targetSetting);

            // kiểm tra xem có tìm thấy đối tượng không
            if (targetSetting != null)
            {
                // thay đổi giá trị của trường "value"
                string value = status ? "[q], [w], [e], [r], [a], [s], [d], [f]" : "[Ctrl][6]";
                targetSetting["value"] = value;
            }
            Console.WriteLine("NEW");
            Console.WriteLine(targetSetting);

            // Lưu lại các thay đổi vào file JSON
            File.WriteAllText(path, jsonObject.ToString());
        }
        private static void AutoEmoteLaugh(JObject jsonObject, string path, bool status)
        {
            // tìm đến đối tượng có tên "evtChampMasteryDisplay" - thông thạo tướng
            JToken targetSetting = jsonObject.SelectToken("$.files[?(@.name == 'Input.ini')].sections[?(@.name == 'GameEvents')].settings[?(@.name == 'evtEmoteLaugh')]");
            Console.WriteLine("OLD");
            Console.WriteLine(targetSetting);

            // kiểm tra xem có tìm thấy đối tượng không
            if (targetSetting != null)
            {
                // thay đổi giá trị của trường "value"
                string value = status ? "[s], [d], [f]" : "[Ctrl][4]";
                targetSetting["value"] = value;
            }
            Console.WriteLine("NEW");
            Console.WriteLine(targetSetting);

            // Lưu lại các thay đổi vào file JSON
            File.WriteAllText(path, jsonObject.ToString());
        }

        
    }
}
