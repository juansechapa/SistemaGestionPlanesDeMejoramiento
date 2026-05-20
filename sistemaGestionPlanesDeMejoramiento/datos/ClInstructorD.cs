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
                SqlCommand cmd = new SqlCommand("INSERT INTO instructor (nombres, apellidos, correo, telefono, especialidad, idUsuario) " +
                    "VALUES (@nombres, @apellidos, @correo, @telefono, @especialidad, @idUsuario)", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@nombres", instructor.nombres);
                cmd.Parameters.AddWithValue("@apellidos", instructor.apellidos);
                cmd.Parameters.AddWithValue("@correo", instructor.correo);
                cmd.Parameters.AddWithValue("@telefono", instructor.telefono);
                cmd.Parameters.AddWithValue("@especialidad", instructor.especialidad);
                cmd.Parameters.AddWithValue("@idUsuario", instructor.idUsuario);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public List<ClInstructor> ListarInstructores()
        {
            List<ClInstructor> lista = new List<ClInstructor>();

            try
            {
                SqlCommand cmd = new SqlCommand("SELECT idInstructor, nombres, apellidos, correo, telefono, especialidad, idUsuario FROM instructor", cn.MtAbrirConexion());

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ClInstructor instructor = new ClInstructor()
                    {
                        idInstructor = Convert.ToInt32(dr["idInstructor"]),
                        nombres = Convert.ToString(dr["nombres"]),
                        apellidos = Convert.ToString(dr["apellidos"]),
                        correo = Convert.ToString(dr["correo"]),
                        telefono = Convert.ToString(dr["telefono"]),
                        especialidad = Convert.ToString(dr["especialidad"]),
                        idUsuario = Convert.ToInt32(dr["idUsuario"])
                    };
                    lista.Add(instructor);
                }
                dr.Close();
            }
            finally
            {
                cn.MtCerrarConexion();
            }
            return lista;
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
            finally
            {
                cn.MtCerrarConexion();
            }
            return respuesta;
        }

        public bool ActualizarInstructor(ClInstructor instructor)
        {
            bool respuesta = false;

            try
            {
                SqlCommand cmd = new SqlCommand("UPDATE instructor " +
                    "SET nombres = @nombres, " +
                    "apellidos = @apellidos, " +
                    "correo = @correo, " +
                    "telefono = @telefono, " +
                    "especialidad = @especialidad, " +
                    "idUsuario = @idUsuario " +
                    "WHERE idInstructor = @idInstructor", cn.MtAbrirConexion());

                cmd.Parameters.AddWithValue("@idInstructor", instructor.idInstructor);
                cmd.Parameters.AddWithValue("@nombres", instructor.nombres);
                cmd.Parameters.AddWithValue("@apellidos", instructor.apellidos);
                cmd.Parameters.AddWithValue("@correo", instructor.correo);
                cmd.Parameters.AddWithValue("@telefono", instructor.telefono);
                cmd.Parameters.AddWithValue("@especialidad", instructor.especialidad);
                cmd.Parameters.AddWithValue("@idUsuario", instructor.idUsuario);

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