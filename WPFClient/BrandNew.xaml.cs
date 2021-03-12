using System;
using System.Configuration;
using System.Windows;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using CarDealer.BusinessLayer;

namespace WPFClient
{
  /// <summary>
  /// Interaction logic for BrandNew.xaml
  /// </summary>
  public partial class BrandNew : Window
  {
    string conStr = ConfigurationManager.ConnectionStrings["con_ora"].ConnectionString;

    public BrandNew()
    {
      InitializeComponent();
    }

    private void BtnInsert_Click(object sender, RoutedEventArgs e)
    {
      var brand = new Brand()
      {
        Id = Convert.ToInt32(TxtId.Text),
        Name = TxtName.Text
      };
      if(brand.ValidateBrand(brand))
      {
        try
        {
          using(OracleConnection con = new OracleConnection(conStr))
          {
            using(OracleCommand cmd = new OracleCommand("pcd_brands_insert", con))
            {
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = brand.Id;
              cmd.Parameters.Add("p_name", OracleDbType.Varchar2, 100).Value = brand.Name;
              cmd.Parameters.Add("p_out", OracleDbType.Int32).Direction = ParameterDirection.Output;
              con.Open();
              cmd.ExecuteNonQuery();
              string output = cmd.Parameters["p_out"].Value.ToString();
              if(output == "1")
                MessageBox.Show("Inserted");
            }
          }
        }
        catch(Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
      }
      else
        MessageBox.Show("Not inserted. Ceck field values");
    }
  }
}
