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

        public bool InsertarInstructor(ClInstructor instructor)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO instructor (nombres, apellidos, correo, telefono, especialidad, idUsuario) " +
                    "VALUES (@nombres, @apellidos, @correo, @telefono, @especialidad, @idUsuario)", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@nombres", instructor.nombres);
                cmd.Parameters.AddWithValue("@apellidos", instructor.apellidos);
                cmd.Parameters.AddWithValue("@correo", instructor.correo);
                cmd.Parameters.AddWithValue("@telefono", instructor.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@especialidad", instructor.especialidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@idUsuario", instructor.idUsuario);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 && ex.Message.Contains("UQ_CorreoInstructor"))
                    throw new Exception("Ya existe un instructor con ese correo electrónico.");
                throw;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public List<ClInstructor> ListarInstructores()
        {
            List<ClInstructor> lista = new List<ClInstructor>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idInstructor, nombres, apellidos, correo, telefono, especialidad, idUsuario FROM instructor", cn.MtAbrirConexion());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClInstructor
                    {
                        idInstructor = Convert.ToInt32(dr["idInstructor"]),
                        nombres = dr["nombres"].ToString(),
                        apellidos = dr["apellidos"].ToString(),
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
                    "UPDATE instructor SET nombres=@nombres, apellidos=@apellidos, correo=@correo, telefono=@telefono, " +
                    "especialidad=@especialidad, idUsuario=@idUsuario WHERE idInstructor=@idInstructor", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idInstructor", instructor.idInstructor);
                cmd.Parameters.AddWithValue("@nombres", instructor.nombres);
                cmd.Parameters.AddWithValue("@apellidos", instructor.apellidos);
                cmd.Parameters.AddWithValue("@correo", instructor.correo);
                cmd.Parameters.AddWithValue("@telefono", instructor.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@especialidad", instructor.especialidad ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@idUsuario", instructor.idUsuario);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 && ex.Message.Contains("UQ_CorreoInstructor"))
                    throw new Exception("Ya existe otro instructor con ese correo electrónico.");
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