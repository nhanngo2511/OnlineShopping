using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OnlineShoppingDB
/// </summary>
public class OnlineShoppingDB
{
    private SqlConnection conn;

    private void Connection()
    {
        string constr = ConfigurationManager.ConnectionStrings["MyConnection"].ToString();

        conn = new SqlConnection(constr);

    }

    public bool CreateBrandDataAccess(string Name)
    {

        Connection();

        SqlCommand cmd = new SqlCommand("spCreateBrand", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@Name", Name);

        conn.Open();

        try
        {
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            conn.Close();
        }

    }

    public DataSet GetBrandsDataAccess()
    {
        Connection();

        SqlCommand cmd = new SqlCommand("spGetBrands", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        DataSet ds = new DataSet();
        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);

        try
        {
            cmd.ExecuteNonQuery();
            return ds;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            conn.Close();
        }


    }

}