using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CP_Entidad;

namespace CP_Datos
{
   public class CNX_Avion
   {
      //Traer la lista de aviones de la tabla avion en la base de datos 
      public List<avion> listaAviones()
      {

         //Instancia de la clase avion en entidades 
         List<avion> listaAvionesBD = new List<avion>();

         try
         {
            //Crear instancia de conexion a la base de datos 
            using (SqlConnection conexionBD = new SqlConnection(BDConexion.conexionBD))
            {
               //String a ejecutar en la base de datos 
               string query = "select * from avion";

               //Crear el envio del sript a ejecutar en la base de datos 
               SqlCommand cmd = new SqlCommand(query, conexionBD);
               cmd.CommandType = CommandType.Text;

               //Abrir conexion con la base de datos
               conexionBD.Open();

               //Ejecutar o enviar el script a la base de datos con la consulta 
               using (SqlDataReader reader = cmd.ExecuteReader())
               {

                  //Leer la respuesta de la base de datos
                  while (reader.Read())
                  {

                     //Lista objetos avion, llenarla  
                     listaAvionesBD.Add(new avion()
                     {
                        numAvion = Convert.ToInt32(reader["numAvion"]),
                        serieAvion = reader["serieAvion"].ToString(),
                        nombreFantasiaAvion = reader["nombreFantasiaAvion"].ToString(),
                        idTipoAvio = Convert.ToInt32(reader["idTipoAvio"]),
                        estadoAvionRetOActivo = Convert.ToBoolean(reader["estadoAvionRetOActivo"])
                     });
                  }
               }

            }
         }
         catch (Exception)
         {

            listaAvionesBD = new List<avion>();
         }

         //Retornar la lista 
         return listaAvionesBD;
      }

      //Registrar una nuevo avion en la tabla avion de la base de datos  
      public int RegistrarNuevoAvion(avion datosAvion)
      {
         int resultado = -1;

         try
         {
            //Crear instancia de conexion a la base de datos 
            using (SqlConnection conexionBD = new SqlConnection(BDConexion.conexionBD))
            {
               //Crear el envio del sript a ejecutar en la base de datos 
               SqlCommand cmd = new SqlCommand("sp_RegistrarNewAvion", conexionBD);
               //Datos del avion
               cmd.Parameters.AddWithValue("serieAvion", datosAvion.serieAvion);
               cmd.Parameters.AddWithValue("nombreFantasiaAvion", datosAvion.nombreFantasiaAvion);
               cmd.Parameters.AddWithValue("idTipoAvio", datosAvion.idTipoAvio);
               cmd.CommandType = CommandType.StoredProcedure;

               //Abrir conexion con la base de datos
               conexionBD.Open();

               //Ejecutar o enviar el script a la base de datos con la consulta 
               cmd.ExecuteReader();

               resultado = 1;
            }
         }
         catch (Exception ex)
         {
            resultado = 0;
         }

         //Retornar si se guardo el registro correctamente 
         return resultado;
      }

      //Incrementar la cantida de aviones 
      public int IncrementarCantidadAviones(int idTipoAvion, int cantidadAvion)
      {
         int resultado = 1;

         try
         {
            //Crear instancia de conexion a la base de datos 
            using (SqlConnection conexionBD = new SqlConnection(BDConexion.conexionBD))
            {
               //Crear el envio del sript a ejecutar en la base de datos 
               SqlCommand cmd = new SqlCommand("sp_AumentarCantidadAvion", conexionBD);
               //Datos de la persona 
               cmd.Parameters.AddWithValue("idLenguajeProgram", idTipoAvion);
               cmd.Parameters.AddWithValue("puntuacionLenguaje", cantidadAvion);
               cmd.CommandType = CommandType.StoredProcedure;

               //Abrir conexion con la base de datos
               conexionBD.Open();

               //Ejecutar o enviar el script a la base de datos con la consulta 
               cmd.ExecuteReader();

               resultado = 1;
            }
         }
         catch (Exception ex)
         {
            resultado = -1;
         }

         //Retornar si se guardo el registro correctamente 
         return resultado;
      }

      //Consultar la cantidad de aviones  
      public int ConsultarCantidadAviones(int idAvion)
      {
         int cantidadAviones = -1;

         try
         {
            using (SqlConnection conexionBD = new SqlConnection(BDConexion.conexionBD))
            {
               //String a ejecutar en la base de datos 
               string query = "select cantidadAviones from tipoAvion where idTipoAvion = " + idAvion;

               //Crear el envio del sript a ejecutar en la base de datos 
               SqlCommand cmd = new SqlCommand(query, conexionBD);
               cmd.CommandType = CommandType.Text;

               //Abrir conexion con la base de datos
               conexionBD.Open();

               //Ejecutar o enviar el script a la base de datos con la consulta 
               using (SqlDataReader reader = cmd.ExecuteReader())
               {
                  //Leer la respuesta de la base de datos
                  while (reader.Read())
                  {
                     //Almacenar cantidad de aviones 
                     cantidadAviones = Convert.ToInt32(reader["cantidadAviones"]);
                  }
               }
            }
         }
         catch (Exception ex)
         {
            cantidadAviones = -1;
         }

         //Retornar la cantidad de aviones 
         return cantidadAviones;
      }


      //Consultar el tipo de avion  
      public int ConsultarTipoAvion(int idMarcaAvion, int idModeloAvion)
      {
         int _tipoAvion = -1;

         try
         {
            using (SqlConnection conexionBD = new SqlConnection(BDConexion.conexionBD))
            {
               //String a ejecutar en la base de datos 
               string query = "select idTipoAvion from tipoAvion where idMarcaAvio = " + idMarcaAvion + " and idMarcaAvio = " + idModeloAvion;

               //Crear el envio del sript a ejecutar en la base de datos 
               SqlCommand cmd = new SqlCommand(query, conexionBD);
               cmd.CommandType = CommandType.Text;

               //Abrir conexion con la base de datos
               conexionBD.Open();

               //Ejecutar o enviar el script a la base de datos con la consulta 
               using (SqlDataReader reader = cmd.ExecuteReader())
               {
                  //Leer la respuesta de la base de datos
                  while (reader.Read())
                  {
                     //Almacenar cantidad de aviones 
                     _tipoAvion = Convert.ToInt32(reader["idTipoAvion"]);
                  }
               }
            }
         }
         catch (Exception ex)
         {
            _tipoAvion = -1;
         }

         //Retornar la cantidad de aviones 
         return _tipoAvion;
      }
   }
}
