using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace winform_sample
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource1 = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            Dictionary<string, string> ExplainPairs = new Dictionary<string, string>
            {
                {"BLUECATS_HOST", "BLUECATS의 아이피 주소를 적는다. ex)127.0.0.1:7700 "}
            };

            var defaultEVs = new Dictionary<string, string>
            {
                { "BLUECATS_HOST", string.Empty},
                { "BLUECATS_DMS50", "false" },
                { "BLUECATS_DMS50_ROOTDIR", "false" }
            };
            
            var readEvs = Environment.GetEnvironmentVariables().ToDictionary();

            bindingSource1.DataSource = defaultEVs
                .Where(x => !readEvs.Keys.Contains(x.Key))
                .Union(readEvs)
                .Where(x => x.Key.Contains("BLUECATS"))
                .Select(x => new EnviromentValue
                {
                    Key = x.Key,
                    Value = x.Value,
                    Explain = ExplainPairs.Keys.Contains(x.Key) ? ExplainPairs[x.Key] : string.Empty
                })
                .OrderBy(x => x.Key)
                .ToList();

            dataGridView1.DataSource = bindingSource1;

            dataGridView1.AutoResizeColumns();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var data = bindingSource1.DataSource as List<EnviromentValue>;

            data.ForEach(x =>
            {
                Environment.SetEnvironmentVariable(x.Key, x.Value, EnvironmentVariableTarget.Machine);
            });
        }
    }
}
