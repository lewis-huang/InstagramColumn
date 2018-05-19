using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Text.RegularExpressions;

namespace FetchInstagram
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();
			connString.Server = "192.168.1.112";
			connString.UserID = "huangyun";
			connString.Password = "huangyun";
			connString.Database = "pythondb";

			MySqlConnection connection = new MySqlConnection(connString.ConnectionString);
			MySqlCommand cmd = connection.CreateCommand();
			connection.Open();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "insertNewColumn";
			cmd.Parameters.Add(new MySqlParameter("pColumnName", richTextBox1.Text));
			cmd.Parameters.Add(new MySqlParameter("pColumnUrl", textBox1.Text));

			cmd.ExecuteNonQuery();
			connection.Close();


		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{
			string columnurl = richTextBox1.Text;
			Regex filterReg = new Regex(@".com[/]([\s\S])*", RegexOptions.IgnoreCase);
			Regex subfixReg = new Regex(@"[/]([\s\S])*[/]", RegexOptions.IgnoreCase);

			if (filterReg.IsMatch(columnurl))
			{
				if (subfixReg.IsMatch(filterReg.Match(columnurl).Value))
				{
					textBox1.Text = subfixReg.Match(filterReg.Match(columnurl).Value).Value.Replace('/', '.');
				}
			}
		}
	}
}
