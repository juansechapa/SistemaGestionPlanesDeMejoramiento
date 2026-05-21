using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO aprendiz (nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, fechaNacimiento, idFicha, idUsuario) " +
                    "VALUES (@nombres, @apellidos, @tipoDocumento, @numeroDocumento, @correo, @telefono, @fechaNacimiento, @idFicha, @idUsuario)",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@nombres", aprendiz.nombres);
                cmd.Parameters.AddWithValue("@apellidos", aprendiz.apellidos);
                cmd.Parameters.AddWithValue("@tipoDocumento", aprendiz.tipoDocumento);
                cmd.Parameters.AddWithValue("@numeroDocumento", aprendiz.numeroDocumento);
                cmd.Parameters.AddWithValue("@correo", aprendiz.correo);
                cmd.Parameters.AddWithValue("@telefono", aprendiz.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaNacimiento", aprendiz.fechaNacimiento);
                cmd.Parameters.AddWithValue("@idFicha", aprendiz.idFicha);
                cmd.Parameters.AddWithValue("@idUsuario", aprendiz.idUsuario);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                // Número de 2627 sale cuando una unique dececta un valor repetido
                if (ex.Number == 2627)
                {
                    if (ex.Message.Contains("UQ_correoAprendiz"))
                        throw new Exception("Ya existe un aprendiz con ese correo electrónico.");
                    else if (ex.Message.Contains("UQ_numeroDocumento"))
                        throw new Exception("Ya existe un aprendiz con ese número de documento.");
                    else
                        throw new Exception("Error de duplicado en la base de datos.");
                }
                throw;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public List<ClAprendiz> ListarAprendices()
        {
            List<ClAprendiz> lista = new List<ClAprendiz>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idAprendiz, nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, fechaNacimiento, idFicha, idUsuario " +
                    "FROM aprendiz", cn.MtAbrirConexion());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClAprendiz
                    {
                        idAprendiz = Convert.ToInt32(dr["idAprendiz"]),
                        nombres = dr["nombres"].ToString(),
                        apellidos = dr["apellidos"].ToString(),
                        tipoDocumento = dr["tipoDocumento"].ToString(),
                        numeroDocumento = dr["numeroDocumento"].ToString(),
                        correo = dr["correo"].ToString(),
                        telefono = dr["telefono"] != DBNull.Value ? dr["telefono"].ToString() : null,
                        fechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"]),
                        idFicha = Convert.ToInt32(dr["idFicha"]),
                        idUsuario = Convert.ToInt32(dr["idUsuario"])
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public bool ActualizarAprendiz(ClAprendiz aprendiz)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE aprendiz SET nombres=@nombres, apellidos=@apellidos, tipoDocumento=@tipoDocumento, " +
                    "numeroDocumento=@numeroDocumento, correo=@correo, telefono=@telefono, fechaNacimiento=@fechaNacimiento, " +
                    "idFicha=@idFicha, idUsuario=@idUsuario WHERE idAprendiz=@idAprendiz", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idAprendiz", aprendiz.idAprendiz);
                cmd.Parameters.AddWithValue("@nombres", aprendiz.nombres);
                cmd.Parameters.AddWithValue("@apellidos", aprendiz.apellidos);
                cmd.Parameters.AddWithValue("@tipoDocumento", aprendiz.tipoDocumento);
                cmd.Parameters.AddWithValue("@numeroDocumento", aprendiz.numeroDocumento);
                cmd.Parameters.AddWithValue("@correo", aprendiz.correo);
                cmd.Parameters.AddWithValue("@telefono", aprendiz.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaNacimiento", aprendiz.fechaNacimiento);
                cmd.Parameters.AddWithValue("@idFicha", aprendiz.idFicha);
                cmd.Parameters.AddWithValue("@idUsuario", aprendiz.idUsuario);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    if (ex.Message.Contains("UQ_correoAprendiz"))
                        throw new Exception("Ya existe otro aprendiz con ese correo electrónico.");
                    else if (ex.Message.Contains("UQ_numeroDocumento"))
                        throw new Exception("Ya existe otro aprendiz con ese número de documento.");
                }
                throw;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
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
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }
    }
}
