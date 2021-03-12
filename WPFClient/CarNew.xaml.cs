using CarDealer.BusinessLayer;
using System;
using System.Data;
using System.Windows;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;



namespace WPFClient
{
  /// <summary>
  /// Interaction logic for CarNew.xaml
  /// </summary>
  public partial class CarNew : Window
  {
    string conStr = ConfigurationManager.ConnectionStrings["con_ora"].ConnectionString;

    public CarNew()
    {
      InitializeComponent();

      try
      {
        using(OracleConnection con = new OracleConnection(conStr))
        {

          //PROBLEM

          using(OracleCommand cmd = new OracleCommand("select name, id from brands", con))
          {
            con.Open();

            using(OracleDataReader dr = cmd.ExecuteReader())
            {
              while(dr.Read())
              {
                CbxBrand.Items.Add(dr["name"].ToString());
              }
            }

            // PROBLEM
          }
        }
      }
      catch(Exception ex)
      {

        MessageBox.Show(ex.Message);
      }
    }

    private void BtnInsert_Click(object sender, RoutedEventArgs e)
    {
      var car = new Car()
      {
        Id = Convert.ToInt32(TxtId.Text),
        Name = TxtName.Text,
        BrandId = CbxBrand.SelectedIndex
      };
      if(car.ValidateCar(car))
      {
        try
        {
          using(OracleConnection con = new OracleConnection(conStr))
          {

            // PROBLEM

            using(OracleCommand cmd = new OracleCommand("pcd_cars_insert", con))
            {
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = car.Id;
              cmd.Parameters.Add("p_name", OracleDbType.Varchar2, 100).Value = car.Name;
              cmd.Parameters.Add("p_id_brands", OracleDbType.Int32).Value = car.BrandId;
              cmd.Parameters.Add("p_out", OracleDbType.Int32).Direction = ParameterDirection.Output;
              con.Open();
              cmd.ExecuteNonQuery();
              string output = cmd.Parameters["p_out"].Value.ToString();
              if(output == "1")
                MessageBox.Show("Inserted");
            }

            // PROBLEM

          }
        }
        catch(Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
      }
      else
        MessageBox.Show("Check field values");
    }

    private void BtnInsertNewBrand_Click(object sender, RoutedEventArgs e)
    {
      this.Hide();
      BrandNew brandNew = new BrandNew();
      brandNew.Show();
    }
  }
}
