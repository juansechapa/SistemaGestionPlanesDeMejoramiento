using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClAprendizD
    {
        ClConexion cn = new ClConexion();
        public bool InsertarAprendiz(ClAprendiz aprendiz)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO aprendiz (nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, fechaNacimiento)" +
                    "VALUES (@nombres, @apellidos, @tipoDocumento, @numeroDocumento, @correo, @telefono, @fechaNacimiento)", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@nombres", aprendiz.nombres);
                cmd.Parameters.AddWithValue("@apellidos", aprendiz.apellidos);
                cmd.Parameters.AddWithValue("@tipoDocumento", aprendiz.tipoDocumento);
                cmd.Parameters.AddWithValue("@numeroDocumento", aprendiz.numeroDocumento);
                cmd.Parameters.AddWithValue("@correo", aprendiz.correo);
                cmd.Parameters.AddWithValue("@telefono", aprendiz.telefono);
                cmd.Parameters.AddWithValue("@fechaNacimiento", aprendiz.fechaNacimiento);

               respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;

        }
        public List<ClAprendiz> ListarAprendices()
        {
            List<ClAprendiz> listarTodos = new List<ClAprendiz>();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT idAprendiz, nombres, apellidos, tipoDocumento,numeroDocumento, correo, telefono, fechaNacimiento FROM aprendiz", cn.MtAbrirConexion());

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ClAprendiz aprendiz = new ClAprendiz()
                    {
                        idAprendiz = Convert.ToInt32(dr["idAprendiz"]),
                        nombres = Convert.ToString(dr["nombres"]),
                        apellidos = Convert.ToString(dr["apellidos"]),
                        tipoDocumento = Convert.ToString(dr["tipoDocumento"]),
                        numeroDocumento = Convert.ToString(dr["numeroDocumento"]),
                        correo = Convert.ToString(dr["correo"]),
                        telefono = Convert.ToString(dr["telefono"]),
                        fechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"])
                    };
                    listarTodos.Add(aprendiz);
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return listarTodos;
        }

        public bool EliminarAprendiz(int idAprendiz)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM aprendiz WHERE idAprendiz = @idAprendiz", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idAprendiz", idAprendiz);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public bool ActualizarAprendiz(ClAprendiz aprendiz)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE aprendiz " +
                    "SET nombres = @nombres," +
                    " apellidos = @apellidos, " +
                    "tipoDocumento = @tipoDocumento, " +
                    "numeroDocumento = @numeroDocumento," +
                    "correo = @correo," +
                    "telefono = @telefono, " +
                    "fechaNacimiento = @fechaNacimiento " +
                    "WHERE idAprendiz = @idAprendiz", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idAprendiz", aprendiz.idAprendiz);
                cmd.Parameters.AddWithValue("@nombres", aprendiz.nombres);
                cmd.Parameters.AddWithValue("@apellidos", aprendiz.apellidos);
                cmd.Parameters.AddWithValue("@tipoDocumento", aprendiz.tipoDocumento);
                cmd.Parameters.AddWithValue("@numeroDocumento", aprendiz.numeroDocumento);
                cmd.Parameters.AddWithValue("@correo", aprendiz.correo);
                cmd.Parameters.AddWithValue("@telefono", aprendiz.telefono);
                cmd.Parameters.AddWithValue("@fechaNacimiento", aprendiz.fechaNacimiento);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }
    }
}