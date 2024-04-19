using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using MVCPais.Models.Conexion;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Drawing;


namespace MVCPais.Models.Pais
{
    public class Pais
    {
        public int idpais { get; set; }
        public string paisdesc { get; set; }
        

        MVCPais.Models.Conexion.Conexion xion = new MVCPais.Models.Conexion.Conexion();

        public int AgregaPais() 
        {
            SqlConnection con = new SqlConnection(xion.con);

            SqlCommand cmdInsert = new SqlCommand("sp_paises", con);
            cmdInsert.CommandType = CommandType.StoredProcedure;

            cmdInsert.Parameters.Add("@paisdesc", SqlDbType.NVarChar);
            cmdInsert.Parameters.Add("@idpais", SqlDbType.Int);
            cmdInsert.Parameters.Add("@operador", SqlDbType.NVarChar);

            cmdInsert.Parameters["@paisdesc"].Value = paisdesc;
            cmdInsert.Parameters["@idpais"].Value = 1;
            cmdInsert.Parameters["@operador"].Value = "I";

            con.Open();
            int id = Convert.ToInt32(cmdInsert.ExecuteScalar());
            con.Close();

            return id;
        }

        public void ActualizaPais(int idpais)
        {
            SqlConnection con = new SqlConnection(xion.con);

            SqlCommand cmdActualiza = new SqlCommand("sp_paises", con);
            cmdActualiza.CommandType = CommandType.StoredProcedure;

            cmdActualiza.Parameters.Add("@paisdesc", SqlDbType.NVarChar).Value = paisdesc;
            cmdActualiza.Parameters.Add("@idpais", SqlDbType.Int).Value = idpais;
            cmdActualiza.Parameters.Add("@operador", SqlDbType.NVarChar).Value = "U";
            con.Open();
            cmdActualiza.ExecuteNonQuery();
            con.Close();
        }

        public Pais ConsultaIndividualPais(int idpais)
        {
            Pais pai = new Pais();

            SqlConnection con = new SqlConnection(xion.con);
            SqlCommand cmdSelect = new SqlCommand("sp_paises", con);
            cmdSelect.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter = cmdSelect.Parameters.Add("@idpais", SqlDbType.Int);
            parameter.Value = idpais;
            cmdSelect.Parameters.Add("@operador", SqlDbType.NVarChar).Value = "Q";
            cmdSelect.Parameters.Add("@paisdesc", SqlDbType.NVarChar).Value = "";


            con.Open();
            SqlDataReader dr = cmdSelect.ExecuteReader();

            if (dr.Read())
            {
                pai.idpais = Convert.ToInt32(dr["idpais"].ToString());
                pai.paisdesc = dr["paisdesc"].ToString();
            }
            con.Close();

            return pai;
        }

        public List<Pais> ConsultarPaises()
        {
            SqlConnection con = new SqlConnection(xion.con);
            List<Pais> paiss = new List<Pais>();
            SqlCommand cmdSelect = new SqlCommand("sp_paises", con);

            cmdSelect.CommandType = CommandType.StoredProcedure;

            cmdSelect.Parameters.Add("@paisdesc", SqlDbType.NVarChar).Value = "";
            cmdSelect.Parameters.Add("@operador", SqlDbType.NVarChar).Value = "CT";
            cmdSelect.Parameters.Add("@idpais", SqlDbType.Int).Value = 1;

            con.Open();
            SqlDataReader dr = cmdSelect.ExecuteReader();
            while (dr.Read())
            {
                Pais p = new Pais()
                {
                    idpais = Convert.ToInt32(dr["idpais"].ToString()),
                    paisdesc = dr["paisdesc"].ToString()           
                };
                paiss.Add(p);
            }

            con.Close();  
            return paiss;

        }

        public void EliminarPais(int idpais)
        {
            SqlConnection con = new SqlConnection(xion.con);
            SqlCommand cmdDelete = new SqlCommand("sp_paises", con);
            cmdDelete.CommandType = CommandType.StoredProcedure;

            cmdDelete.Parameters.Add("@paisdesc", SqlDbType.NVarChar).Value = "";
            cmdDelete.Parameters.Add("@idpais", SqlDbType.Int).Value = idpais;
            cmdDelete.Parameters.Add("@operador", SqlDbType.NVarChar).Value = "D";

            con.Open();
            cmdDelete.ExecuteNonQuery();
            con.Close();
        }
    }
}