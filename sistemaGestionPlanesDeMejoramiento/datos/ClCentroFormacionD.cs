using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClCentroFormacionD
    {
        ClConexion cn = new ClConexion();

        public bool InsertarCentro(ClCentroFormacion centro)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO centro (nombre, direccion, telefono, estado) " +
                    "VALUES (@nombre, @direccion, @telefono, @estado)", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@nombre", centro.nombre);
                cmd.Parameters.AddWithValue("@direccion", centro.direccion);
                cmd.Parameters.AddWithValue("@telefono", centro.telefono);
                cmd.Parameters.AddWithValue("@estado", centro.estado);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public List<ClCentroFormacion> ListarCentros()
        {
            List<ClCentroFormacion> lista = new List<ClCentroFormacion>();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT idCentro, nombre, direccion, telefono, estado FROM centro", cn.MtAbrirConexion());

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ClCentroFormacion centro = new ClCentroFormacion()
                    {
                        idCentro = Convert.ToInt32(dr["idCentro"]),
                        nombre = Convert.ToString(dr["nombre"]),
                        direccion = Convert.ToString(dr["direccion"]),
                        telefono = Convert.ToString(dr["telefono"]),
                        estado = Convert.ToBoolean(dr["estado"])
                    };
                    lista.Add(centro);
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return lista;
        }
        public bool EliminarCentro(int idCentro)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM centro WHERE idCentro = @idCentro", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idCentro", idCentro);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public bool ActualizarCentro(ClCentroFormacion centro)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE centro " +
                    "SET nombre = @nombre, " +
                    "direccion = @direccion, " +
                    "telefono = @telefono, " +
                    "estado = @estado " +
                    "WHERE idCentro = @idCentro", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idCentro", centro.idCentro);
                cmd.Parameters.AddWithValue("@nombre", centro.nombre);
                cmd.Parameters.AddWithValue("@direccion", centro.direccion);
                cmd.Parameters.AddWithValue("@telefono", centro.telefono);
                cmd.Parameters.AddWithValue("@estado", centro.estado);

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