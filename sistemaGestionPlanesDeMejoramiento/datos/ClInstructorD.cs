using sistemaGestionPlanesDeMejoramiento.Datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace sistemaGestionPlanesDeMejoramiento.datos
{
    public class ClInstructorD
    {
        ClConexion cn = new ClConexion();

        public bool InsertarInstructorConUsuario(ClInstructor instructor, string username, string password)
        {
            bool respuesta = false;
            SqlConnection conn = null;
            SqlTransaction trans = null;
            try
            {
                conn = cn.MtAbrirConexion();
                trans = conn.BeginTransaction();

                string sqlUser = "INSERT INTO usuarios (username, password, idRol) VALUES (@u, @p, 2); SELECT SCOPE_IDENTITY();";
                SqlCommand cmdUser = new SqlCommand(sqlUser, conn, trans);
                cmdUser.Parameters.AddWithValue("@u", username);
                cmdUser.Parameters.AddWithValue("@p", password);  
                int idUsuario = Convert.ToInt32(cmdUser.ExecuteScalar());

                SqlCommand cmdIns = new SqlCommand(
                    "INSERT INTO instructor (nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, especialidad, idUsuario) " +
                    "VALUES (@nombres, @apellidos, @tipoDocumento, @numeroDocumento, @correo, @telefono, @especialidad, @idUsuario)",
                    conn, trans);

                cmdIns.Parameters.AddWithValue("@nombres", instructor.nombres);
                cmdIns.Parameters.AddWithValue("@apellidos", instructor.apellidos);
                cmdIns.Parameters.AddWithValue("@tipoDocumento", instructor.tipoDocumento);
                cmdIns.Parameters.AddWithValue("@numeroDocumento", instructor.numeroDocumento);
                cmdIns.Parameters.AddWithValue("@correo", instructor.correo);
                cmdIns.Parameters.AddWithValue("@telefono", instructor.telefono ?? (object)DBNull.Value);
                cmdIns.Parameters.AddWithValue("@especialidad", instructor.especialidad ?? (object)DBNull.Value);
                cmdIns.Parameters.AddWithValue("@idUsuario", idUsuario);

                cmdIns.ExecuteNonQuery();
                trans.Commit();
                respuesta = true;
            }
            catch (SqlException ex)
            {
                trans?.Rollback();
                if (ex.Number == 2627)
                {
                    if (ex.Message.Contains("UQ_CorreoInstructor"))
                        throw new Exception("Ya existe un instructor con ese correo electrónico.");
                    else if (ex.Message.Contains("UQ_numeroDocumento_instructor"))
                        throw new Exception("Ya existe un instructor con ese número de documento.");
                    else if (ex.Message.Contains("UQ_Username"))
                        throw new Exception("El nombre de usuario ya existe. Elija otro.");
                }
                throw;
            }
            catch (Exception)
            {
                trans?.Rollback();
                throw;
            }
            finally
            {
                if (conn != null && conn.State == System.Data.ConnectionState.Open)
                    cn.MtCerrarConexion();
            }
            return respuesta;
        }

        

        public List<ClInstructor> ListarInstructores()
        {
            List<ClInstructor> lista = new List<ClInstructor>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idInstructor, nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, especialidad, idUsuario FROM instructor",
                    cn.MtAbrirConexion());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClInstructor
                    {
                        idInstructor = Convert.ToInt32(dr["idInstructor"]),
                        nombres = dr["nombres"].ToString(),
                        apellidos = dr["apellidos"].ToString(),
                        tipoDocumento = dr["tipoDocumento"].ToString(),
                        numeroDocumento = dr["numeroDocumento"].ToString(),
                        correo = dr["correo"].ToString(),
                        telefono = dr["telefono"] != DBNull.Value ? dr["telefono"].ToString() : null,
                        especialidad = dr["especialidad"] != DBNull.Value ? dr["especialidad"].ToString() : null,
                        idUsuario = Convert.ToInt32(dr["idUsuario"])
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public bool ActualizarInstructor(ClInstructor instructor)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE instructor SET nombres=@nombres, apellidos=@apellidos, tipoDocumento=@tipoDocumento, numeroDocumento=@numeroDocumento, " +
                    "correo=@correo, telefono=@telefono, especialidad=@especialidad WHERE idInstructor=@idInstructor",
                    cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idInstructor", instructor.idInstructor);
                cmd.Parameters.AddWithValue("@nombres", instructor.nombres);
                cmd.Parameters.AddWithValue("@apellidos", instructor.apellidos);
                cmd.Parameters.AddWithValue("@tipoDocumento", instructor.tipoDocumento);
                cmd.Parameters.AddWithValue("@numeroDocumento", instructor.numeroDocumento);
                cmd.Parameters.AddWithValue("@correo", instructor.correo);
                cmd.Parameters.AddWithValue("@telefono", instructor.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@especialidad", instructor.especialidad ?? (object)DBNull.Value);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    if (ex.Message.Contains("UQ_CorreoInstructor"))
                        throw new Exception("Ya existe otro instructor con ese correo electrónico.");
                    else if (ex.Message.Contains("UQ_numeroDocumento_instructor"))
                        throw new Exception("Ya existe otro instructor con ese número de documento.");
                }
                throw;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public bool EliminarInstructor(int idInstructor)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM instructor WHERE idInstructor = @idInstructor", cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idInstructor", idInstructor);
                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }
    }
}