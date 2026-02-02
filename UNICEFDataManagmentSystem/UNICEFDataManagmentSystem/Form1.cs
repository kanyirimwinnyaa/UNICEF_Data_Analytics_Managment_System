using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UNICEFDataManagmentSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'uNICEF_Data_AnalyticsDataSet2.Vaccine' table. You can move, or remove it, as needed.
            this.vaccineTableAdapter.Fill(this.uNICEF_Data_AnalyticsDataSet2.Vaccine);
            // TODO: This line of code loads data into the 'uNICEF_Data_AnalyticsDataSet.Region' table. You can move, or remove it, as needed.
            this.regionTableAdapter.Fill(this.UNICEF_Data_AnalyticsDataSet.Region);
            Region_Panel.Hide();
            Vaccine_Panel.Hide();
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-4U5DGULG\\SQLEXPRESS;Database=UNICEF_Data_Analytics;Integrated Security=True;TrustServerCertificate=True;");
        private void Region_Tab_Click(object sender, EventArgs e)
        {
            Vaccine_Panel.Hide();
            Region_Panel.Show();
        }

        private void Main_Tab_Click(object sender, EventArgs e)
        {
            Vaccine_Panel.Hide();
            Region_Panel.Hide();
        }
        private void Vaccine_Tab_Click(object sender, EventArgs e)
        {
            Region_Panel.Show();
            Vaccine_Panel.Show();
        }

        private void Add_Region_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("insert into Region values ('" + int.Parse(Region_ID_Input.Text) + "' , '" + Region_Name_Input.Text + "' , '" + iso3_Input.Text + "')", con);
                com.ExecuteNonQuery();
                MessageBox.Show("Sucsessfuly Inserted!");
                Region_ID_Input.Clear();
                Region_Name_Input.Clear();
                iso3_Input.Clear();
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error. Repeted Input.");
                Region_ID_Input.Clear();
                Region_Name_Input.Clear();
                iso3_Input.Clear();
            }
        }

        private void Update_Region_Click(object sender, EventArgs e)
        {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Region", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Region_DataGridView.DataSource = dt;
                con.Close();
        }
        private void Delete_Region_Click(object sender, EventArgs e)
        { 
                con.Open();
                SqlCommand com = new SqlCommand("Delete Region Where Region_ID = '" + int.Parse(Region_ID_Input.Text) + "'", con);
                com.ExecuteNonQuery();
                con.Close();
        }

        private void Add_Vaccine_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("insert into Vaccine values ('" + int.Parse(Vaccine_ID_Input.Text) + "' , '" + Vaccine_Name_Input.Text + "' , '" + Vaccine_Year_Input.Text + "')", con);
                com.ExecuteNonQuery();
                MessageBox.Show("Sucsessfuly Inserted!");
                Vaccine_ID_Input.Clear();
                Vaccine_Name_Input.Clear();
                Vaccine_Year_Input.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Error. Repeted Input.");
                Vaccine_ID_Input.Clear();
                Vaccine_Name_Input.Clear();
                Vaccine_Year_Input.Clear();
            }
        }

        private void Update_Vaccine_Click(object sender, EventArgs e)
        {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Vaccine", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Vaccine_DataGridView.DataSource = dt;
        }

        private void Delate_Vaccine_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand com = new SqlCommand("Delete Vaccine Where Vaccine_ID = '"+int.Parse(Vaccine_ID_Input.Text)+"'");
            com.ExecuteNonQuery();
            con.Close();
        }
    }
}
