using System;
using System.Data;
using System.Data.SqlClient;
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
            // TODO: This line of code loads data into the 'uNICEF_Data_AnalyticsDataSet4.Main' table. You can move, or remove it, as needed.
            //this.mainTableAdapter.Fill(this.uNICEF_Data_AnalyticsDataSet4.Main);
            // TODO: This line of code loads data into the 'uNICEF_Data_AnalyticsDataSet1.Region' table. You can move, or remove it, as needed.
            this.regionTableAdapter1.Fill(this.uNICEF_Data_AnalyticsDataSet1.Region);
            // TODO: This line of code loads data into the 'uNICEF_Data_AnalyticsDataSet3.Hospital' table. You can move, or remove it, as needed.
            this.hospitalTableAdapter.Fill(this.uNICEF_Data_AnalyticsDataSet3.Hospital);
            // TODO: This line of code loads data into the 'uNICEF_Data_AnalyticsDataSet2.Vaccine' table. You can move, or remove it, as needed.
            this.vaccineTableAdapter.Fill(this.uNICEF_Data_AnalyticsDataSet2.Vaccine);
            // TODO: This line of code loads data into the 'uNICEF_Data_AnalyticsDataSet.Region' table. You can move, or remove it, as needed.
            this.regionTableAdapter.Fill(this.UNICEF_Data_AnalyticsDataSet.Region);
            Region_Panel.Hide();
            Vaccine_Panel.Hide();
            Hospital_Panel.Hide();
            Main_DataGrid_View();
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-4U5DGULG\\SQLEXPRESS;Database=UNICEF_Data_Analytics;Integrated Security=True;TrustServerCertificate=True;");
        private void Hospital_Tab_Click(object sender, EventArgs e)
        {
            Vaccine_Panel.Show();
            Region_Panel.Show();
            Hospital_Panel.Show();
        }

        private void Region_Tab_Click(object sender, EventArgs e)
        {
            Hospital_Panel.Hide();
            Vaccine_Panel.Hide();
            Region_Panel.Show();
        }

        private void Main_Tab_Click(object sender, EventArgs e)
        {
            Hospital_Panel.Hide();
            Vaccine_Panel.Hide();
            Region_Panel.Hide();
            Main_DataGrid_View();
        }
        private void Vaccine_Tab_Click(object sender, EventArgs e)
        {
            Hospital_Panel.Hide();
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
        private void Main_DataGrid_View()
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Hospital.Hospital_Name, Region.Region_Name,Region.Region_iso3,Vaccine.Vaccine_Name,Vaccine.Vaccine_Year\r\nFROM (((Main\r\nINNER JOIN Region ON Main.Region_ID = Region.Region_ID)\r\nINNER JOIN Vaccine ON Main.Vaccine_ID = Vaccine.Vaccine_ID)\r\nINNER JOIN Hospital ON Main.Hospital_ID = Hospital.Hospital_ID);", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Main_DataGridView.DataSource = dt;
            con.Close();
        }
        private void Delete_Region_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("Delete Region Where Region_ID = (" + int.Parse(Region_ID_Input.Text) + ")", con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sucssesfully Deleted.");
                Region_ID_Input.Clear();
            }
            catch (Exception)
            {
                Region_ID_Input.Clear();
                MessageBox.Show("Please enter an ID to delete to.");
            }
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
            }
            catch (Exception)
            {
                MessageBox.Show("Error. Repeted Input.");
                Vaccine_ID_Input.Clear();
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
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("Delete Vaccine Where Vaccine_ID = (" + int.Parse(Vaccine_ID_Input.Text) + ")", con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sucssesfully Deleted.");
                Vaccine_ID_Input.Clear();
            }
            catch (Exception)
            {
                Vaccine_ID_Input.Clear();
                MessageBox.Show("Please enter an ID to delete to.");
            }
        }


        private void Add_Hospital_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("insert into Hospital Values ('" + int.Parse(Hospital_ID_Input.Text) + "' , '" + Hospital_Name_Input.Text + "')", con);
                com.ExecuteNonQuery();
                SqlCommand command = new SqlCommand("insert into Main values ('" + int.Parse(Hospital_ID_Input.Text) + "')");
                MessageBox.Show("Sucsessfuly Inserted!");
                Hospital_ID_Input.Clear();
                Hospital_Name_Input.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Error. Repeted Input.");
                Hospital_ID_Input.Clear();
            }
        }

        private void Update_Hospital_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Hospital", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Hospital_DataGridView.DataSource = dt;
        }

        private void Delete_Hospital_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand com = new SqlCommand("Delete Hospital Where Hospital_ID = (" + int.Parse(Hospital_ID_Input.Text) + ")", con);
                com.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sucssesfully Deleted.");
                Hospital_ID_Input.Clear();
            }
            catch (Exception)
            {
                Hospital_ID_Input.Clear();
                MessageBox.Show("Please enter an ID to delete to.");
            }
        }

        private void Search_Region_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Region where Region_ID = '" + int.Parse(Regon_Search.Text) + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Region_DataGridView.DataSource = null;
                Region_DataGridView.DataSource = dt;
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error. Please entar an ID to search to.");
            }
        }

        private void Search_Vaccine_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Vaccine where Vaccine_ID = '" + int.Parse(Search_Vaccine.Text) + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Vaccine_DataGridView.DataSource = null;
                Vaccine_DataGridView.DataSource = dt;
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error. Please entar an ID to search to.");
            }
        }
        private void Hospital_Search_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter("select * from Hospital where Hospital_ID = '" + int.Parse(Search_Hospital.Text) + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Hospital_DataGridView.DataSource = null;
                Hospital_DataGridView.DataSource = dt;
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Error. Please entar an ID to search to.");
            }
        }
    }
}
