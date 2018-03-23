using ONLINE_SHOPPING.ENTITIES;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ONLINE_SHOPPING.DAL //DATA ACCESS LAYER
{
    public class OnlineShoppingDB
    {

        public SqlConnection conn;

        public void Connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["MyConnection"].ToString();

            conn = new SqlConnection(constr);

        }
        public void CreateProduct(string Name, float Price, int Quantity, float DiscountPrice, float WholesalePrice, string ShortDescription, string LongDescription, string WholesaleDescription, string BrandDetail, Boolean InStock, Boolean IsPublic, Boolean IsHidden, int? OrderIndex, int? BrandID, int CategoryID, int? AgeTypeID, int? CountryTypeID, int ModeID, List<string> ImageNames, out int ID)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spCreateProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", Name);
            cmd.Parameters.Add("@Price", Price);
            cmd.Parameters.Add("@Quantity", Quantity);
            cmd.Parameters.Add("@DiscountPrice", DiscountPrice);
            cmd.Parameters.Add("@WholesalePrice", WholesalePrice);
            cmd.Parameters.Add("@ShortDescription", ShortDescription);
            cmd.Parameters.Add("@LongDescription", LongDescription);
            cmd.Parameters.Add("@WholesaleDescription", WholesaleDescription);
            cmd.Parameters.Add("@BrandDetail", BrandDetail);
            cmd.Parameters.Add("@InStock", InStock);
            cmd.Parameters.Add("@IsPublic", IsPublic);
            cmd.Parameters.Add("@IsHidden", IsHidden);

            if (!OrderIndex.HasValue)
            {
                cmd.Parameters.Add("@OrderIndex", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@OrderIndex", OrderIndex.Value);
            }


            if (!BrandID.HasValue)
            {
                cmd.Parameters.Add("@BrandID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@BrandID", BrandID.Value);
            }

            cmd.Parameters.Add("@CategoryID", CategoryID);

            if (!AgeTypeID.HasValue)
            {
                cmd.Parameters.Add("@AgeTypeID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@AgeTypeID", AgeTypeID.Value);
            }

            if (!CountryTypeID.HasValue)
            {
                cmd.Parameters.Add("@CountryTypeID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@CountryTypeID", CountryTypeID.Value);
            }

            cmd.Parameters.Add("@ModeID", ModeID);

            if (ImageNames == null)
            {
                cmd.Parameters.Add("@DefaultImage", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@DefaultImage", ImageNames[0]);
            }


            SqlParameter IDoutput = cmd.Parameters.Add("@ID", SqlDbType.Int);
            IDoutput.Direction = ParameterDirection.Output;

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();

                ID = Convert.ToInt32(IDoutput.Value);
                if (ImageNames != null)
                {
                    CreateImages(ID, ImageNames);
                }

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

        public void CreateImages(int ProductID, List<string> ImageNames)
        {

            Connection();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;

            string query = "";

            for (int i = 0; i < ImageNames.Count; i++)
            {
                query += "INSERT INTO Images VALUES(" + ProductID + ", N\'" + ImageNames[i] + "\', GETDATE()) ";
            }

            conn.Open();

            try
            {
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
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

        public void UpdateProduct(int ID, string Name, float Price, int Quantity, float DiscountPrice, float WholesalePrice, string ShortDescription, string LongDescription, string WholesaleDescription, string BrandDetail, Boolean InStock, Boolean IsPublic, Boolean IsHidden, int? OrderIndex, int? BrandID, int CategoryID, int? AgeTypeID, int? CountryTypeID, int ModeID, List<string> ImageNames)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spUpdateProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", ID);
            cmd.Parameters.Add("@Name", Name);
            cmd.Parameters.Add("@Price", Price);
            cmd.Parameters.Add("@Quantity", Quantity);
            cmd.Parameters.Add("@DiscountPrice", DiscountPrice);
            cmd.Parameters.Add("@WholesalePrice", WholesalePrice);
            cmd.Parameters.Add("@ShortDescription", ShortDescription);
            cmd.Parameters.Add("@LongDescription", LongDescription);
            cmd.Parameters.Add("@WholesaleDescription", WholesaleDescription);


            if (String.IsNullOrEmpty(BrandDetail))
            {
                cmd.Parameters.Add("@BrandDetail", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@BrandDetail", BrandDetail);
            }


            cmd.Parameters.Add("@InStock", InStock);
            cmd.Parameters.Add("@IsPublic", IsPublic);
            cmd.Parameters.Add("@IsHidden", IsHidden);
            if (!OrderIndex.HasValue)
            {
                cmd.Parameters.Add("@OrderIndex", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@OrderIndex", OrderIndex.Value);
            }


            if (!BrandID.HasValue)
            {
                cmd.Parameters.Add("@BrandID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@BrandID", BrandID.Value);
            }

            cmd.Parameters.Add("@CategoryID", CategoryID);

            if (!AgeTypeID.HasValue)
            {
                cmd.Parameters.Add("@AgeTypeID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@AgeTypeID", AgeTypeID.Value);
            }

            if (!CountryTypeID.HasValue)
            {
                cmd.Parameters.Add("@CountryTypeID", DBNull.Value);
            }
            else
            {
                cmd.Parameters.Add("@CountryTypeID", CountryTypeID.Value);
            }

            cmd.Parameters.Add("@ModeID", ModeID);

            conn.Open();

            try
            {

                if (ImageNames != null)
                {
                    CreateImages(ID, ImageNames);
                }

                cmd.ExecuteNonQuery();
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

        public void UpdateBrand(int ID, string Name)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spUpdateBrand", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", ID);
            cmd.Parameters.Add("@Name", Name);
          
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
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

        public void DeleteBrand(int ID)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spDeleteBrand", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", ID);          

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
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

        public void DeleteImage(int ID)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spDeleteImage", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", ID);
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();

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

        public Product GetProduct(int ID)
        {

            Connection();

            Product prod = new Product();

            SqlCommand cmd = new SqlCommand("spGetProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID", ID));

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable ProductTable = ds.Tables[0];
            DataTable ImagesTable = ds.Tables[1];
            //DataTable ProductRelatesTable = ds.Tables[2];

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                DataRow dr = ProductTable.Rows[0];
                prod.ID = ID;
                prod.Name = dr["Name"].ToString();
                prod.Price = float.Parse(dr["Price"].ToString());
                prod.Quantity = Int32.Parse(dr["Quantity"].ToString());
                prod.DiscountPrice = Int32.Parse(dr["DiscountPrice"].ToString());
                prod.WholesalePrice = Int32.Parse(dr["WholesalePrice"].ToString());
                prod.ShortDescription = dr["ShortDescription"].ToString();
                prod.LongDescription = dr["LongDescription"].ToString();
                prod.WholesaleDescription = dr["WholesaleDescription"].ToString();
                prod.BrandDetail = dr["BrandDetail"].ToString();
                prod.InStock = Boolean.Parse(dr["InStock"].ToString());
                prod.IsPublic = Boolean.Parse(dr["IsPublic"].ToString());
                prod.IsHidden = Boolean.Parse(dr["IsHidden"].ToString());
                prod.OrderIndex = Int32.Parse(dr["OrderIndex"].ToString());
                prod.DefaultImage = dr["DefaultImage"].ToString();

                Brand brand = new Brand();
                brand.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "BrandID")) ? Int32.Parse(CheckAndGetDataValue(dr, "BrandID")) : (int?)null;
                brand.Name = dr["BrandName"].ToString();
                prod.Brand = brand;

                Category cate = new Category();
                cate.ID = Int32.Parse(dr["Level1CategoryID"].ToString());
                cate.Name = dr["Level1CategoryName"].ToString();
                prod.Category = cate;

                Level2Category lv2cate = new Level2Category();
                lv2cate.ID = Int32.Parse(dr["CategoryID"].ToString());
                lv2cate.Name = dr["Level2CategoryName"].ToString();
                prod.Level2Category = lv2cate;

                AgeType agetype = new AgeType();
                agetype.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "AgeTypeID")) ? Int32.Parse(CheckAndGetDataValue(dr, "AgeTypeID")) : (int?)null;
                agetype.Name = dr["AgeTypeName"].ToString();
                prod.AgeType = agetype;

                CountryType countrytype = new CountryType();
                countrytype.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "CountryTypeID")) ? Int32.Parse(CheckAndGetDataValue(dr, "CountryTypeID")) : (int?)null;
                countrytype.Name = dr["CountryTypeName"].ToString();
                prod.CountryType = countrytype;

                Mode mode = new Mode();
                mode.ID = Int32.Parse(dr["ModeID"].ToString());
                mode.Name = dr["ModeName"].ToString();
                prod.Mode = mode;

                List<Image> imgs = new List<Image>();
                for (int i = 0; i < ImagesTable.Rows.Count; i++)
                {
                    //int ProductID = Int32.Parse(ImagesTable.Rows[i]["ProductID"].ToString());

                    Image img = new Image();

                    img.ID = Int32.Parse(ImagesTable.Rows[i]["ID"].ToString());
                    img.ProductID = Int32.Parse(ImagesTable.Rows[i]["ProductID"].ToString());
                    img.Name = ImagesTable.Rows[i]["Name"].ToString();

                    imgs.Add(img);
                }

                prod.Images = imgs;

                return prod;
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

        public string CheckAndGetDataValue(DataRow dr, string rowname)
        {
            string result;

            result = (!dr.IsNull(rowname)) ? dr[rowname].ToString() : String.Empty;

            return result;
        }

        public void GetAllSelectList(out List<Brand> Brands, out List<Category> Categories, out List<AgeType> AgeTypes, out List<CountryType> CoutryTypes, out List<Mode> Modes)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spGetAllSelectListNessessary", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            List<Brand> brs = new List<Brand>();

            DataSet ds = new DataSet();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);


            DataTable BraTable = ds.Tables[0];
            DataTable CateTable = ds.Tables[1];
            DataTable AgeTypTable = ds.Tables[2];
            DataTable CouTryTable = ds.Tables[3];
            DataTable ModeTable = ds.Tables[4];

            try
            {
                cmd.ExecuteNonQuery();

                List<Brand> bras = new List<Brand>();
                for (int i = 0; i < BraTable.Rows.Count; i++)
                {
                    Brand bra = new Brand();

                    bra.ID = Int32.Parse(BraTable.Rows[i]["ID"].ToString());
                    bra.Name = BraTable.Rows[i]["Name"].ToString();

                    bras.Add(bra);
                }
                Brands = bras;

                //

                List<Category> cates = new List<Category>();
                for (int i = 0; i < CateTable.Rows.Count; i++)
                {
                    Category cate = new Category();

                    cate.ID = Int32.Parse(CateTable.Rows[i]["ID"].ToString());
                    cate.Name = CateTable.Rows[i]["Name"].ToString();

                    cates.Add(cate);
                }
                Categories = cates;

                //

                List<AgeType> agetyps = new List<AgeType>();
                for (int i = 0; i < AgeTypTable.Rows.Count; i++)
                {
                    AgeType agetyp = new AgeType();

                    agetyp.ID = Int32.Parse(AgeTypTable.Rows[i]["ID"].ToString());
                    agetyp.Name = AgeTypTable.Rows[i]["Name"].ToString();

                    agetyps.Add(agetyp);
                }
                AgeTypes = agetyps;

                //

                List<CountryType> coutyps = new List<CountryType>();
                for (int i = 0; i < CouTryTable.Rows.Count; i++)
                {
                    CountryType coutyp = new CountryType();

                    coutyp.ID = Int32.Parse(CouTryTable.Rows[i]["ID"].ToString());
                    coutyp.Name = CouTryTable.Rows[i]["Name"].ToString();

                    coutyps.Add(coutyp);
                }
                CoutryTypes = coutyps;

                //

                List<Mode> mods = new List<Mode>();
                for (int i = 0; i < ModeTable.Rows.Count; i++)
                {
                    Mode mod = new Mode();

                    mod.ID = Int32.Parse(ModeTable.Rows[i]["ID"].ToString());
                    mod.Name = ModeTable.Rows[i]["Name"].ToString();

                    mods.Add(mod);
                }
                Modes = mods;

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


        public List<Product> GetProducts(int? ID, int? BrandID, int? ModeID, int? CategoryID, bool? InStock, bool? IsPublic, int PageSize, out int TotalRecords, int? Page = 1)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spGetProducts4", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            ID = (!ID.HasValue) ? 0 : ID.Value;
            BrandID = (!BrandID.HasValue) ? 0 : BrandID.Value;
            CategoryID = (!CategoryID.HasValue) ? 0 : CategoryID.Value;
            ModeID = (!ModeID.HasValue) ? 0 : ModeID.Value;

            cmd.Parameters.Add(new SqlParameter("@ID", ID));
            cmd.Parameters.Add(new SqlParameter("@BrandID", BrandID));
            cmd.Parameters.Add(new SqlParameter("@CategoryID", CategoryID));
            cmd.Parameters.Add(new SqlParameter("@ModeID", ModeID));

            if (!InStock.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@InStock", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@InStock", InStock.Value));
            }

            if (!IsPublic.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@IsPublic", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@IsPublic", IsPublic.Value));
            }


            cmd.Parameters.Add(new SqlParameter("@Page", Page));
            cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));

            SqlParameter TotalRecordsOutput = cmd.Parameters.Add("@TotalRecords", SqlDbType.Int);
            TotalRecordsOutput.Direction = ParameterDirection.Output;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable ProductTable = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();
                TotalRecords = Convert.ToInt32(TotalRecordsOutput.Value);

                List<Product> prods = new List<Product>();

                for (int i = 0; i < ProductTable.Rows.Count; i++)
                {
                    Product prod = new Product();

                    DataRow dr = ProductTable.Rows[i];

                    prod.ID = Int32.Parse(dr["ID"].ToString());
                    prod.Name = dr["Name"].ToString();
                    prod.Price = float.Parse(dr["Price"].ToString());
                    prod.Quantity = Int32.Parse(dr["Quantity"].ToString());
                    prod.DiscountPrice = Int32.Parse(dr["DiscountPrice"].ToString());
                    prod.WholesalePrice = Int32.Parse(dr["WholesalePrice"].ToString());
                    prod.ShortDescription = dr["ShortDescription"].ToString();
                    prod.LongDescription = dr["LongDescription"].ToString();
                    prod.WholesaleDescription = dr["WholesaleDescription"].ToString();
                    prod.BrandDetail = dr["BrandDetail"].ToString();
                    prod.InStock = Boolean.Parse(dr["InStock"].ToString());
                    prod.IsPublic = Boolean.Parse(dr["IsPublic"].ToString());
                    prod.IsHidden = Boolean.Parse(dr["IsHidden"].ToString());
                    prod.OrderIndex = Int32.Parse(dr["OrderIndex"].ToString());
                    prod.DefaultImage = dr["DefaultImage"].ToString();

                    Brand brand = new Brand();
                    brand.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "BrandID")) ? Int32.Parse(CheckAndGetDataValue(dr, "BrandID")) : (int?)null;
                    brand.Name = dr["BrandName"].ToString();
                    prod.Brand = brand;

                    Category cate = new Category();
                    cate.ID = Int32.Parse(dr["Level1CategoryID"].ToString());
                    cate.Name = dr["Level1CategoryName"].ToString();
                    prod.Category = cate;

                    Level2Category lv2cate = new Level2Category();
                    lv2cate.ID = Int32.Parse(dr["Level2CategoryID"].ToString());
                    //  lv2cate.Name = dr["Level2CategoryName"].ToString();
                    prod.Level2Category = lv2cate;

                    AgeType agetype = new AgeType();
                    agetype.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "AgeTypeID")) ? Int32.Parse(CheckAndGetDataValue(dr, "AgeTypeID")) : (int?)null;
                    agetype.Name = dr["AgeTypeName"].ToString();
                    prod.AgeType = agetype;

                    CountryType countrytype = new CountryType();
                    countrytype.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "CountryTypeID")) ? Int32.Parse(CheckAndGetDataValue(dr, "CountryTypeID")) : (int?)null;
                    countrytype.Name = dr["CountryTypeName"].ToString();
                    prod.CountryType = countrytype;

                    Mode mode = new Mode();
                    mode.ID = Int32.Parse(dr["ModeID"].ToString());
                    mode.Name = dr["ModeName"].ToString();
                    prod.Mode = mode;

                    List<Image> imgs = new List<Image>();

                    //Image img = new Image();
                    //img.Name = dr["ImageName"].ToString();
                    //img.ProductID = prod.ID;

                    //imgs.Add(img);
                    //prod.Images = imgs;

                    prods.Add(prod);
                }

                return prods;

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


        public List<Image> GetImages(int ID)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spGetImagesByProductID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID", ID));

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable ImageTable = ds.Tables[0];

            try
            {
                conn.Open();

                List<Image> imgs = new List<Image>();
                cmd.ExecuteNonQuery();

                for (int i = 0; i < ImageTable.Rows.Count; i++)
                {
                    Image img = new Image();
                    DataRow dr = ImageTable.Rows[i];
                    img.ID = ID;
                    img.Name = dr["Name"].ToString();
                    img.ProductID = Int32.Parse(dr["ProductID"].ToString());
                    imgs.Add(img);
                }

                return imgs;
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

        public List<Product> GetWholesalePrice(int[] IDs)
        {

            Connection();

            List<Product> prods = new List<Product>();

            for (int i = 0; i < IDs.Length; i++)
            {

                SqlCommand cmd = new SqlCommand("spGetWholesalePrice", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@ID", IDs[i]));
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();

                da.Fill(ds);

                DataTable ProductTable = ds.Tables[0];

                conn.Open();

                try
                {
                    cmd.ExecuteNonQuery();

                    Product prod = new Product();

                    DataRow dr = ProductTable.Rows[0];
                    prod.ID = IDs[i];
                    prod.Name = dr["Name"].ToString();
                    prod.Price = float.Parse(dr["Price"].ToString());
                    prod.WholesalePrice = Int32.Parse(dr["WholesalePrice"].ToString());
                    prod.DefaultImage = dr["DefaultImage"].ToString();

                    prods.Add(prod);
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

            return prods;

        }




        public List<Category> GetMainCategories()
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spGetMainCategories", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<Category> cates = new List<Category>();

            DataSet ds = new DataSet();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            try
            {
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Category cate = new Category();

                    cate.ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                    cate.Name = dt.Rows[i]["Name"].ToString();

                    cates.Add(cate);
                }
                return cates;
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

        public List<Category> GetLevel2CategoriesByMainCategoryID(int? ID = 1)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spGetLevel2CategoriesByMainCategoryID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@MainCategoryID", ID);

            List<Category> cates = new List<Category>();

            DataSet ds = new DataSet();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            try
            {
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Category cate = new Category();

                    cate.ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                    cate.Name = dt.Rows[i]["Name"].ToString();

                    cates.Add(cate);
                }
                return cates;
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

        public List<Category> GetCategories()
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spGetCategories", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<Category> cates = new List<Category>();

            DataSet ds = new DataSet();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            try
            {
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Category cate = new Category();

                    if (dt.Rows[i].IsNull("Level2CategoryID"))
                    {
                        cate.ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                        cate.Name = dt.Rows[i]["Name"].ToString();

                        cates.Add(cate);
                    }


                }

                for (int i = 0; i < cates.Count; i++)
                {
                    List<Category> lv2cates = new List<Category>();

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (!(dt.Rows[j].IsNull("Level2CategoryID")))
                        {
                            if (Int32.Parse(dt.Rows[j]["Level2CategoryID"].ToString()) == cates[i].ID)
                            {
                                Category lv2cate = new Category();

                                lv2cate.ID = Int32.Parse(dt.Rows[j]["ID"].ToString());
                                lv2cate.Name = dt.Rows[j]["Name"].ToString();

                                lv2cates.Add(lv2cate);
                            }
                        }
                        else
                        {
                            continue;
                        }


                    }
                    cates[i].Level2Category = lv2cates;
                }
                return cates;
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




        public List<Brand> GetBrands()
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spGetBrands", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<Brand> brs = new List<Brand>();

            DataSet ds = new DataSet();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            try
            {
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Brand br = new Brand();

                    br.ID = Int32.Parse(dt.Rows[i]["ID"].ToString());
                    br.Name = dt.Rows[i]["Name"].ToString();

                    brs.Add(br);
                }
                return brs;
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


        public List<Product> SetValueForProductModel(DataTable dt)
        {
            List<Product> ListProductName = new List<Product>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];


                Product TmpProd = new Product();

                //List<Image> TmpImgs = new List<Image>();
                //Image TmpImg = new Image();


                TmpProd.ID = Int32.Parse(dr["ID"].ToString());
                TmpProd.Name = dr["Name"].ToString();
                TmpProd.Price = float.Parse(dr["Price"].ToString());
                TmpProd.Quantity = Int32.Parse(dr["Quantity"].ToString());
                TmpProd.DiscountPrice = Int32.Parse(dr["DiscountPrice"].ToString());
                TmpProd.WholesalePrice = Int32.Parse(dr["WholesalePrice"].ToString());
                TmpProd.InStock = Boolean.Parse(dr["InStock"].ToString());
                TmpProd.DefaultImage = dr["DefaultImage"].ToString();

                Mode mode = new Mode();
                mode.ID = Int32.Parse(dr["ModeID"].ToString());
                TmpProd.Mode = mode;

                Category cate = new Category();
                cate.ID = Int32.Parse(dr["CategoryID"].ToString());
                TmpProd.Category = cate;

                //TmpImg.Name = dr["ImageName"].ToString();
                //TmpImgs.Add(TmpImg);
                //TmpProd.Images = TmpImgs;


                ListProductName.Add(TmpProd);
            }

            return ListProductName;
        }



        //public Product GetDetailsProduct(int ID)
        //{
        //    Connection();

        //    Product DetaProd = new Product();

        //    SqlCommand cmd = new SqlCommand("spGetProduct", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@ID", ID));

        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    DataSet ds = new DataSet();
        //    da.Fill(ds);

        //    DataTable DetaProdTable = ds.Tables[0];
        //    DataTable ImgsProdTable = ds.Tables[1];

        //    try
        //    {
        //        conn.Open();

        //        cmd.ExecuteNonQuery();

        //        //set value to Detail Product
        //        DataRow dr = DetaProdTable.Rows[0];

        //        DetaProd.ID = Int32.Parse(dr["ID"].ToString()); ;
        //        DetaProd.Name = dr["Name"].ToString();
        //        DetaProd.Price = float.Parse(dr["Price"].ToString());
        //        DetaProd.Quantity = Int32.Parse(dr["Quantity"].ToString());
        //        DetaProd.DiscountPrice = Int32.Parse(dr["DiscountPrice"].ToString());
        //        DetaProd.WholesalePrice = Int32.Parse(dr["WholesalePrice"].ToString());
        //        DetaProd.ShortDescription = dr["ShortDescription"].ToString();
        //        DetaProd.LongDescription = dr["LongDescription"].ToString();
        //        DetaProd.WholesaleDescription = dr["WholesaleDescription"].ToString();
        //        DetaProd.BrandDetail = dr["BrandDetail"].ToString();
        //        DetaProd.InStock = Boolean.Parse(dr["InStock"].ToString());
        //        DetaProd.IsPublic = Boolean.Parse(dr["IsPublic"].ToString());
        //        DetaProd.IsHidden = Boolean.Parse(dr["IsHidden"].ToString());
        //        DetaProd.OrderIndex = Int32.Parse(dr["OrderIndex"].ToString());
        //        DetaProd.DefaultImage = dr["DefaultImage"].ToString();

        //        Brand brand = new Brand();
        //        brand.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "BrandID")) ? Int32.Parse(CheckAndGetDataValue(dr, "BrandID")) : (int?)null;
        //        brand.Name = CheckAndGetDataValue(dr, "BrandName");
        //        DetaProd.Brand = brand;

        //        List<Category> lv2Cates = new List<Category>();
        //        Category lv2Cate = new Category();
        //        lv2Cate.ID = Int32.Parse(dr["Level2CategoryID"].ToString());
        //        lv2Cate.Name = dr["Level2CategoryName"].ToString();
        //        lv2Cates.Add(lv2Cate);

        //        Category Cate = new Category();
        //        Cate.ID = Int32.Parse(dr["Level1CategoryID"].ToString());
        //        Cate.Name = dr["Level1CategoryName"].ToString();
        //        Cate.Level2Category = lv2Cates;
        //        DetaProd.Category = Cate;

        //        AgeType AgeType = new AgeType();
        //        AgeType.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "AgeTypeID")) ? Int32.Parse(CheckAndGetDataValue(dr, "AgeTypeID")) : (int?)null;
        //        AgeType.Name = CheckAndGetDataValue(dr, "AgeTypeName");
        //        DetaProd.AgeType = AgeType;

        //        CountryType CountryType = new CountryType();
        //        CountryType.ID = !String.IsNullOrEmpty(CheckAndGetDataValue(dr, "CountryTypeID")) ? Int32.Parse(CheckAndGetDataValue(dr, "CountryTypeID")) : (int?)null;
        //        CountryType.Name = CheckAndGetDataValue(dr, "CountryTypeName");
        //        DetaProd.AgeType = AgeType;

        //        Mode Mode = new Mode();
        //        Mode.ID = Int32.Parse(dr["ModeID"].ToString());
        //        Mode.Name = dr["ModeName"].ToString();

        //        // set value to images product

        //        List<Image> ProImgs = new List<Image>();

        //        for (int i = 0; i < ImgsProdTable.Rows.Count; i++)
        //        {
        //            dr = ImgsProdTable.Rows[i];

        //            Image Img = new Image();

        //            Img.Name = dr["Name"].ToString();

        //            ProImgs.Add(Img);

        //        }

        //        DetaProd.Images = ProImgs;

        //        return DetaProd;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}


        public List<Product> GetRelateProducts(int BrandID, int CurrentProductID)
        {
            Connection();

            List<Product> RelaProds = new List<Product>();

            SqlCommand cmd = new SqlCommand("spGetRelateProducts", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@BrandID", BrandID));
            cmd.Parameters.Add(new SqlParameter("@CurrentProductID", CurrentProductID));

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable RelaProductTable = ds.Tables[0];


            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                //set value to Detail Product


                for (int i = 0; i < RelaProductTable.Rows.Count; i++)
                {
                    Product RelaProd = new Product();

                    DataRow dr = RelaProductTable.Rows[i];


                    RelaProd.ID = Int32.Parse(dr["ID"].ToString()); ;
                    RelaProd.Name = dr["Name"].ToString();
                    RelaProd.Price = float.Parse(dr["Price"].ToString());

                    RelaProd.DiscountPrice = Int32.Parse(dr["DiscountPrice"].ToString());
                    RelaProd.WholesalePrice = Int32.Parse(dr["WholesalePrice"].ToString());

                    RelaProd.IsPublic = Boolean.Parse(dr["IsPublic"].ToString());
                    RelaProd.IsHidden = Boolean.Parse(dr["IsHidden"].ToString());

                    Mode mode = new Mode();
                    mode.ID = Int32.Parse(dr["ModeID"].ToString());
                    RelaProd.Mode = mode;

                    Brand brand = new Brand();
                    brand.Name = dr["BrandName"].ToString();
                    RelaProd.Brand = brand;

                    RelaProd.DefaultImage = dr["DefaultImage"].ToString();

                    RelaProds.Add(RelaProd);

                }


                return RelaProds;

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






        //public Footer GetFooter()
        //{

        //    Footer Footer = new Footer();
        //    List<Brand> Brands = new List<Brand>();
        //    List<Category> Categories = new List<Category>();

        //    Footer.Brands = Brands;
        //    Footer.Categories = Categories;

        //    Connection();

        //    SqlCommand cmd = new SqlCommand("spGetFooterView", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;


        //    DataSet ds = new DataSet();
        //    conn.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(ds);

        //    DataTable BrandTable = ds.Tables[0];
        //    DataTable CategoryTable = ds.Tables[1];

        //    try
        //    {
        //        cmd.ExecuteNonQuery();


        //        // Brands
        //        for (int i = 0; i < BrandTable.Rows.Count; i++)
        //        {
        //            DataRow dt = BrandTable.Rows[i];

        //            Brand br = new Brand();

        //            br.ID = Int32.Parse(dt["ID"].ToString());
        //            br.Name = dt["Name"].ToString();

        //            Brands.Add(br);
        //        }

        //        //Categories
        //        for (int i = 0; i < CategoryTable.Rows.Count; i++)
        //        {
        //            DataRow dt = CategoryTable.Rows[i];

        //            Category cate = new Category();

        //            if (dt.IsNull("Level2CategoryID"))
        //            {
        //                cate.ID = Int32.Parse(dt["ID"].ToString());
        //                cate.Name = dt["Name"].ToString();

        //                Categories.Add(cate);
        //            }
        //        }

        //        for (int i = 0; i < Categories.Count; i++)
        //        {
        //            List<Category> lv2cates = new List<Category>();

        //            for (int j = 0; j < CategoryTable.Rows.Count; j++)
        //            {
        //                DataRow dr = CategoryTable.Rows[j];

        //                if (!(dr.IsNull("Level2CategoryID")))
        //                {
        //                    if (Int32.Parse(dr["Level2CategoryID"].ToString()) == Categories[i].ID)
        //                    {
        //                        Category lv2cate = new Category();

        //                        lv2cate.ID = Int32.Parse(dr["ID"].ToString());
        //                        lv2cate.Name = dr["Name"].ToString();

        //                        lv2cates.Add(lv2cate);
        //                    }
        //                }
        //                else
        //                {
        //                    continue;
        //                }
        //            }

        //            Categories[i].Level2Category = lv2cates;
        //        }

        //        return Footer;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}


        public bool FindUser(string username, string password, out int ID)
        {

            bool resultreturn;
            Connection();

            SqlCommand cmd = new SqlCommand("spFindUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new SqlParameter("@UserName", username));
            cmd.Parameters.Add(new SqlParameter("@Password", password));

            SqlParameter result = cmd.Parameters.Add("@Result", SqlDbType.Bit);
            result.Direction = ParameterDirection.Output;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable UserTable = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                resultreturn = Convert.ToBoolean(result.Value);
                if (UserTable.Rows.Count > 0)
                {
                    DataRow dr = UserTable.Rows[0];

                    ID = Convert.ToInt32(dr["ID"].ToString());
                }
                else
                {
                    ID = -1;
                }


                return resultreturn;

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

        public User GetUser(int ID)
        {

            Connection();

            User us = new User();

            SqlCommand cmd = new SqlCommand("spGetUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID", ID));

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable UserTable = ds.Tables[0];
            DataTable RoleTable = ds.Tables[1];
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                DataRow dr = UserTable.Rows[0];

                us.ID = ID;
                us.UserName = dr["UserName"].ToString();
                us.Password = dr["Password"].ToString();
                us.FullName = dr["FullName"].ToString();
                us.Age = Convert.ToInt32(dr["Age"].ToString());
                us.Gender = Convert.ToBoolean(dr["Gender"].ToString());
                us.Address = dr["Address"].ToString();

                Ward ward = new Ward();
                ward.ID = Convert.ToInt32(dr["WardID"].ToString());
                ward.Name = dr["WardName"].ToString();
                us.Ward = ward;

                District district = new District();
                district.ID = Convert.ToInt32(dr["DistrictID"].ToString());
                district.Name = dr["DistrictName"].ToString();
                us.Ward.District = district;

                City city = new City();
                city.ID = Convert.ToInt32(dr["CityID"].ToString());
                city.Name = dr["CityName"].ToString();
                us.Ward.District.City = city;

                us.PhoneNumber = dr["PhoneNumber"].ToString();
                us.Email = dr["Email"].ToString();


                List<string> roles = new List<string>();


                for (int i = 0; i < RoleTable.Rows.Count; i++)
                {
                    DataRow dr2 = RoleTable.Rows[i];

                    roles.Add(dr2["Name"].ToString());
                }

                us.Roles = roles;

                return us;
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

        public Brand GetBrandByID(int ID)
        {

            Connection();

            Brand brand = new Brand();

            SqlCommand cmd = new SqlCommand("spGetBrandByID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ID", ID));

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable BrandTable = ds.Tables[0];
        try
            {
                conn.Open();

                cmd.ExecuteNonQuery();

                DataRow dr = BrandTable.Rows[0];

                brand.ID = ID;
                brand.Name = dr["Name"].ToString();

                return brand;
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

        

        public void CreateUser(string UserName, string Password, string FullName, int Age, bool Gender, string PhoneNumber, string Email, string Address, int WardID, int RoleID)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spCreateUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;

           
            cmd.Parameters.Add("@UserName", UserName);
            cmd.Parameters.Add("@Password", Password);
            cmd.Parameters.Add("@FullName", FullName);
            cmd.Parameters.Add("@Age", Age);
            cmd.Parameters.Add("@Gender", Gender);
            cmd.Parameters.Add("@PhoneNumber", PhoneNumber);
            cmd.Parameters.Add("@Email", Email);
            cmd.Parameters.Add("@Address", Address);
            cmd.Parameters.Add("@WardID", WardID);
            cmd.Parameters.Add("@RoleID", RoleID);

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();

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


        public void CreateBrand(string Name)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spCreateBrand", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Name", Name);

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();

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
        public List<Product> GetProductsByModeID(int ModeID, int? LimitRowNumber)
        {

            List<Product> ProductsList = new List<Product>();

            Connection();

            SqlCommand cmd = new SqlCommand("spGetProductsByModeID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ModeID", ModeID));
            cmd.Parameters.Add(new SqlParameter("@LimitRowNumber", LimitRowNumber));

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable ProductsTable = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                ProductsList = SetValueForProductModel(ProductsTable);

                return ProductsList;
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

        public List<Product> GetProductsByCategoryID(int CategoryID, int? LimitRowNumber)
        {

            List<Product> ProductsList = new List<Product>();

            Connection();

            SqlCommand cmd = new SqlCommand("spGetProductsByCategoryID", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@CategoryID", CategoryID));
            cmd.Parameters.Add(new SqlParameter("@LimitRowNumber", LimitRowNumber));

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable ProductsTable = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                ProductsList = SetValueForProductModel(ProductsTable);

                return ProductsList;
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

     

        public void CreateOrder(string RecipientName, string Phone, string Address, int WardID, int UserID, string StrCartItems)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spCreateOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Status", 1);
            cmd.Parameters.Add("@RecipientName", RecipientName);
            cmd.Parameters.Add("@Phone", Phone);
            cmd.Parameters.Add("@Address", Address);
            cmd.Parameters.Add("@WardID", WardID);
            cmd.Parameters.Add("@UserID", UserID);

            StrCartItems = StrCartItems.Remove(StrCartItems.Length - 1);
            cmd.Parameters.Add("@StrCartItems", StrCartItems);         
            
            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
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

        public void UpdateQuantityProduct(int ProductID, int Quantity)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spUpdateQuantityProduct", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ProductID", ProductID);
            cmd.Parameters.Add("@Quantity", Quantity);

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();

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

        public List<City> GetCities()
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spGetCities", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            List<City> Cities = new List<City>();

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    City City = new City();

                    City.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                    City.Name = dt.Rows[i]["Name"].ToString();

                    Cities.Add(City);
                }

                return Cities;
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

        public List<District> GetDistricts(int CityID)
        {
            Connection();
            List<District> Districts = new List<District>();

            SqlCommand cmd = new SqlCommand("spGetDistricts", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CityID", CityID));

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    District District = new District();

                    District.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                    District.Name = dt.Rows[i]["Name"].ToString();

                    Districts.Add(District);
                }

                return Districts;
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

        public List<Ward> GetWards(int DistrictID)
        {
            Connection();
            List<Ward> Wards = new List<Ward>();

            SqlCommand cmd = new SqlCommand("spGetWards", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DistrictID", DistrictID));

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Ward Ward = new Ward();

                    Ward.ID = Convert.ToInt32(dt.Rows[i]["ID"].ToString());
                    Ward.Name = dt.Rows[i]["Name"].ToString();

                    Wards.Add(Ward);
                }

                return Wards;
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

        public List<Order> GetOrders(int? ID, string RecipientName, string Phone, int? UserID, int? StatusID, DateTime? StartDate, DateTime? EndDate, int? WardID, int? ShipperID, int? ProductID, int? CategoryID, out int TotalRecords, out float TotalAmount, int PageSize, int Page = 1)
        {
            Connection();

            List<Order> Orders = new List<Order>();

            SqlCommand cmd = new SqlCommand("spGetOrders03", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            ID = (!ID.HasValue)? 0 : ID.Value;
            UserID = (!UserID.HasValue)? 0 : UserID.Value;
            StatusID = (!StatusID.HasValue)? 0 : StatusID.Value;
            WardID = (!WardID.HasValue)? 0 : WardID.Value;
            ShipperID = (!ShipperID.HasValue)? 0 : ShipperID.Value;
            ProductID = (!ProductID.HasValue)? 0 : ProductID.Value;
            CategoryID = (!CategoryID.HasValue)? 0 : CategoryID.Value;

            cmd.Parameters.Add(new SqlParameter("@ID", ID));
            cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
            cmd.Parameters.Add(new SqlParameter("@StatusID", StatusID));
            cmd.Parameters.Add(new SqlParameter("@WardID", WardID));

            if (String.IsNullOrEmpty(RecipientName))
            {
                cmd.Parameters.Add(new SqlParameter("@RecipientName", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@RecipientName", RecipientName));
            }

            if (String.IsNullOrEmpty(Phone))
            {
                cmd.Parameters.Add(new SqlParameter("@Phone", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@Phone", Phone));
            }



            if (!StartDate.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@StartDate", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@StartDate", StartDate.Value));
            }

            if (!EndDate.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@EndDate", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@EndDate", EndDate.Value));
            }

            cmd.Parameters.Add(new SqlParameter("@ShipperID", ShipperID));
            cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
            cmd.Parameters.Add(new SqlParameter("@CategoryID", CategoryID));

            cmd.Parameters.Add(new SqlParameter("@Page", Page));
            cmd.Parameters.Add(new SqlParameter("@PageSize", PageSize));

            SqlParameter TotalRecordsOutput = cmd.Parameters.Add("@TotalRecords", SqlDbType.Int);
            TotalRecordsOutput.Direction = ParameterDirection.Output;

            SqlParameter TotalAmountOutput = cmd.Parameters.Add("@TotalAmount", SqlDbType.Float);
            TotalAmountOutput.Direction = ParameterDirection.Output;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable OrderTable = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                TotalRecords = Convert.ToInt32(TotalRecordsOutput.Value);
                //TotalAmount = float.Parse(TotalAmountOutput.Value.ToString());
                TotalAmount = Convert.ToSingle(TotalAmountOutput.Value.ToString());

                for (int i = 0; i < OrderTable.Rows.Count; i++)
                {
                    DataRow dr = OrderTable.Rows[i];

                    Order order = new Order();
                    Ward ward = new Ward();
                    District district = new District();
                    City city = new City();
                    User user = new User();
                    User shipper = new User();
                    OrderStatus orderstatus = new OrderStatus();
                    List<OrderProduct> orderproducts = new List<OrderProduct>();

                    order.ID = Convert.ToInt32(dr["OrderID"].ToString());
                    order.RecipientName = dr["RecipientName"].ToString();
                    order.Phone = dr["Phone"].ToString();
                    order.Address = dr["Address"].ToString();
                    order.TotalAmount = float.Parse(dr["TotalAmount"].ToString());
                    order.ShipmentTime = (!dr.IsNull("ShipmentTime")) ? DateTime.Parse(dr["ShipmentTime"].ToString()) : (DateTime?)null;
                    order.ShippedTime = (!dr.IsNull("ShippedTime")) ? DateTime.Parse(dr["ShippedTime"].ToString()) : (DateTime?)null;
                    order.CreationTime = DateTime.Parse(dr["CreationTime"].ToString());


                    orderstatus.ID = Convert.ToInt32(dr["StatusID"].ToString());
                    orderstatus.Description = dr["Status"].ToString();
                    order.OrderStatus = orderstatus;

                    ward.ID = Convert.ToInt32(dr["WardID"].ToString());
                    ward.Name = dr["WardName"].ToString();
                    order.Ward = ward;

                    district.ID = Convert.ToInt32(dr["DistrictID"].ToString());
                    district.Name = dr["DistrictName"].ToString();
                    order.Ward.District = district;

                    city.ID = Convert.ToInt32(dr["CityID"].ToString());
                    city.Name = dr["CityName"].ToString();
                    order.Ward.District.City = city;

                    user.ID = Convert.ToInt32(dr["UserID"].ToString());
                    user.FullName = dr["UserFullName"].ToString();
                    order.User = user;

                    if (!dr.IsNull("ShipperID"))
                    {
                        shipper.ID = Convert.ToInt32(dr["ShipperID"].ToString());
                        shipper.FullName = dr["ShipperName"].ToString();
                        shipper.PhoneNumber = dr["ShipperPhone"].ToString();
                        shipper.Email = dr["ShipperEmail"].ToString();
                    }
                    else
                    {
                        shipper = null;
                    }

                    order.Shipper = shipper;


                    string StrProducts = dr["ProductDetails"].ToString().TrimEnd(new[] { '|' });
                    string[] ArrProducts = Regex.Split(StrProducts, @"\|\|\|");
                    List<string> LstProducts = new List<string>(ArrProducts);

                    for (int k = 0; k < LstProducts.Count; k++)
                    {
                        OrderProduct orderproduct = new OrderProduct();

                        Product product = new Product();
                        List<string> LstProductDetails = new List<string>(Regex.Split(LstProducts[k], @"\|"));

                        product.Name = LstProductDetails[0];
                        product.ID = Convert.ToInt32(LstProductDetails[1]);
                        product.Price = float.Parse(LstProductDetails[2]);
                        product.DefaultImage = LstProductDetails[4];

                        orderproduct.Product = product;
                        orderproduct.Quantity = Convert.ToInt32(LstProductDetails[3]);

                        orderproducts.Add(orderproduct);
                    }

                    order.OrderProducts = orderproducts;

                    Orders.Add(order);
                }

                return Orders;
            }

            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public Order GetOrderDetails(int ID)
        {
            Connection();

            Order order = new Order();

            SqlCommand cmd = new SqlCommand("spGetOrderDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@OrderID", ID));

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);

            DataTable OrderTable = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                DataRow dr = OrderTable.Rows[0];

                Ward ward = new Ward();
                District district = new District();
                City city = new City();
                User user = new User();
                User shipper = new User();
                OrderStatus orderstatus = new OrderStatus();
                List<OrderProduct> orderproducts = new List<OrderProduct>();

                order.ID = Convert.ToInt32(dr["OrderID"].ToString());
                order.RecipientName = dr["RecipientName"].ToString();
                order.Phone = dr["Phone"].ToString();
                order.Address = dr["Address"].ToString();
                order.TotalAmount = float.Parse(dr["TotalAmount"].ToString());
                order.ShipmentTime = (!dr.IsNull("ShipmentTime")) ? DateTime.Parse(dr["ShipmentTime"].ToString()) : (DateTime?)null;
                order.ShippedTime = (!dr.IsNull("ShippedTime")) ? DateTime.Parse(dr["ShippedTime"].ToString()) : (DateTime?)null;
                order.CreationTime = DateTime.Parse(dr["CreationTime"].ToString());


                orderstatus.ID = Convert.ToInt32(dr["StatusID"].ToString());
                orderstatus.Description = dr["Status"].ToString();
                order.OrderStatus = orderstatus;

                ward.ID = Convert.ToInt32(dr["WardID"].ToString());
                ward.Name = dr["WardName"].ToString();
                order.Ward = ward;

                district.ID = Convert.ToInt32(dr["DistrictID"].ToString());
                district.Name = dr["DistrictName"].ToString();
                order.Ward.District = district;

                city.ID = Convert.ToInt32(dr["CityID"].ToString());
                city.Name = dr["CityName"].ToString();
                order.Ward.District.City = city;

                user.ID = Convert.ToInt32(dr["UserID"].ToString());
                user.FullName = dr["UserFullName"].ToString();
                user.Email = dr["Email"].ToString();
                order.User = user;

                if (!dr.IsNull("ShipperID"))
                {
                    shipper.ID = Convert.ToInt32(dr["ShipperID"].ToString());
                    shipper.FullName = dr["ShipperName"].ToString();
                    shipper.PhoneNumber = dr["ShipperPhone"].ToString();
                    shipper.Email = dr["ShipperEmail"].ToString();
                }
                else
                {
                    shipper = null;
                }

                order.Shipper = shipper;

                string StrProducts = dr["ProductDetails"].ToString().TrimEnd(new[] { '|' });
                string[] ArrProducts = Regex.Split(StrProducts, @"\|\|\|");
                List<string> LstProducts = new List<string>(ArrProducts);

                for (int k = 0; k < LstProducts.Count; k++)
                {
                    OrderProduct orderproduct = new OrderProduct();

                    Product product = new Product();
                    List<string> LstProductDetails = new List<string>(Regex.Split(LstProducts[k], @"\|"));

                    product.Name = LstProductDetails[0];
                    product.ID = Convert.ToInt32(LstProductDetails[1]);
                    product.Price = float.Parse(LstProductDetails[2]);
                    product.DefaultImage = LstProductDetails[4];

                    orderproduct.Product = product;
                    orderproduct.Quantity = Convert.ToInt32(LstProductDetails[3]);

                    orderproducts.Add(orderproduct);
                }

                order.OrderProducts = orderproducts;

                return order;
            }

            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public void SetCancelOrder(int ID)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spSetCancelOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderID", ID);

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();
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

        public void SetShippingOrder(int ID, int ShipperID)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spSetShippingOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderID", ID);
            cmd.Parameters.Add("@ShipperID", ShipperID);

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();

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

        public void SetShippedOrder(int ID)
        {
            Connection();

            SqlCommand cmd = new SqlCommand("spSetShippedOrder", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderID", ID);

            conn.Open();

            try
            {
                cmd.ExecuteNonQuery();

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

        public bool CheckExistUserName(string username)
        {

            bool resultreturn;
            Connection();

            SqlCommand cmd = new SqlCommand("spCheckExistUserName", conn);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new SqlParameter("@UserName", username));
            
            SqlParameter result = cmd.Parameters.Add("@Result", SqlDbType.Bit);
            result.Direction = ParameterDirection.Output;

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                resultreturn = Convert.ToBoolean(result.Value);
               
                return resultreturn;

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

        public List<User> GetShippers()
        {
            Connection();
            List<User> Shippers = new List<User>();

            SqlCommand cmd = new SqlCommand("spGetShippers", conn);
            cmd.CommandType = CommandType.StoredProcedure;           

            DataSet ds = new DataSet();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            DataTable dt = ds.Tables[0];

            conn.Open();
            try
            {
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];

                    User Shipper = new User();

                    Shipper.ID = Convert.ToInt32(dr["ID"].ToString());
                    Shipper.FullName = dr["FullName"].ToString();

                    Shippers.Add(Shipper);
                }

                return Shippers;
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
}

    


