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
        //Inserta de forma automatica el rol = 3(aprendiz)
        public bool InsertarAprendizConUsuario(ClAprendiz aprendiz, string username, string password)
        {
            bool respuesta = false;
            SqlTransaction trans = null;
            SqlConnection conexion = null;
            try
            {
                conexion = cn.MtAbrirConexion();
                trans = conexion.BeginTransaction();

                ValidarCupoFicha(conexion, trans, aprendiz.idFicha, null);

                string sqlUser = "INSERT INTO usuarios (username, password, idRol) VALUES (@u, @p, 3); SELECT SCOPE_IDENTITY();";
                SqlCommand cmdUser = new SqlCommand(sqlUser, conexion, trans);
                cmdUser.Parameters.AddWithValue("@u", username);
                cmdUser.Parameters.AddWithValue("@p", ClUsuarioD.HashPassword(password));
                int idUsuario = Convert.ToInt32(cmdUser.ExecuteScalar());

                SqlCommand cmdIns = new SqlCommand(
                    "INSERT INTO aprendiz (nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, fechaNacimiento, idFicha, idUsuario, estado) " +
                    "VALUES (@nombres, @apellidos, @tipoDocumento, @numeroDocumento, @correo, @telefono, @fechaNacimiento, @idFicha, @idUsuario, @estado)",
                    conexion, trans);

                cmdIns.Parameters.AddWithValue("@nombres", aprendiz.nombres);
                cmdIns.Parameters.AddWithValue("@apellidos", aprendiz.apellidos);
                cmdIns.Parameters.AddWithValue("@tipoDocumento", aprendiz.tipoDocumento);
                cmdIns.Parameters.AddWithValue("@numeroDocumento", aprendiz.numeroDocumento);
                cmdIns.Parameters.AddWithValue("@correo", aprendiz.correo);
                cmdIns.Parameters.AddWithValue("@telefono", aprendiz.telefono ?? (object)DBNull.Value);
                cmdIns.Parameters.AddWithValue("@fechaNacimiento", aprendiz.fechaNacimiento);
                cmdIns.Parameters.AddWithValue("@idFicha", aprendiz.idFicha);
                cmdIns.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmdIns.Parameters.AddWithValue("@estado", "En Formación");//Estado por defecto

                cmdIns.ExecuteNonQuery();
                trans.Commit();
                respuesta = true;
            }
            catch (SqlException ex)
            {
                trans?.Rollback();
                if (ex.Number == 2627)
                {
                    if (ex.Message.Contains("UQ_correoAprendiz"))
                        throw new Exception("Ya existe un aprendiz con ese correo electrónico.");
                    if (ex.Message.Contains("UQ_numeroDocumento"))
                        throw new Exception("Ya existe un aprendiz con ese número de documento.");
                    if (ex.Message.Contains("UQ_Username"))
                        throw new Exception("El nombre de usuario ya existe. Elija otro.");
                }
                throw;
            }
            catch
            {
                trans?.Rollback();
                throw;
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
                SqlConnection conexion = cn.MtAbrirConexion();
                ValidarCupoFicha(conexion, null, aprendiz.idFicha, aprendiz.idAprendiz);

                SqlCommand cmd = new SqlCommand(
                    "UPDATE aprendiz SET nombres=@nombres, apellidos=@apellidos, tipoDocumento=@tipoDocumento, " +
                    "numeroDocumento=@numeroDocumento, correo=@correo, telefono=@telefono, fechaNacimiento=@fechaNacimiento, idFicha=@idFicha " +
                    "WHERE idAprendiz=@idAprendiz",
                    conexion);

                cmd.Parameters.AddWithValue("@idAprendiz", aprendiz.idAprendiz);
                cmd.Parameters.AddWithValue("@nombres", aprendiz.nombres);
                cmd.Parameters.AddWithValue("@apellidos", aprendiz.apellidos);
                cmd.Parameters.AddWithValue("@tipoDocumento", aprendiz.tipoDocumento);
                cmd.Parameters.AddWithValue("@numeroDocumento", aprendiz.numeroDocumento);
                cmd.Parameters.AddWithValue("@correo", aprendiz.correo);
                cmd.Parameters.AddWithValue("@telefono", aprendiz.telefono ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@fechaNacimiento", aprendiz.fechaNacimiento);
                cmd.Parameters.AddWithValue("@idFicha", aprendiz.idFicha);

                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    if (ex.Message.Contains("UQ_correoAprendiz"))
                        throw new Exception("Ya existe otro aprendiz con ese correo.");
                    if (ex.Message.Contains("UQ_numeroDocumento"))
                        throw new Exception("Ya existe otro aprendiz con ese número de documento.");
                }
                throw;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public bool ActualizarEstadoAprendiz(int idAprendiz, string nuevoEstado)
        {
            bool respuesta = false;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE aprendiz SET estado = @estado WHERE idAprendiz = @id",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@id", idAprendiz);
                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public List<ClAprendiz> ListarAprendices()
        {
            List<ClAprendiz> lista = new List<ClAprendiz>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idAprendiz, nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, fechaNacimiento, idFicha, idUsuario, estado " +
                    "FROM aprendiz",
                    cn.MtAbrirConexion());
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
                        idUsuario = Convert.ToInt32(dr["idUsuario"]),
                        estado = dr["estado"].ToString()
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

        public bool EliminarAprendiz(int idAprendiz)
        {
            bool respuesta = false;
            try
            {
                SqlConnection conexion = cn.MtAbrirConexion();
                SqlCommand cmdPlanes = new SqlCommand("SELECT COUNT(1) FROM planMejoramiento WHERE idAprendiz = @idAprendiz", conexion);
                cmdPlanes.Parameters.AddWithValue("@idAprendiz", idAprendiz);
                if (Convert.ToInt32(cmdPlanes.ExecuteScalar()) > 0)
                    throw new InvalidOperationException("No se puede eliminar el aprendiz porque tiene planes de mejoramiento asignados.");

                SqlCommand cmd = new SqlCommand("DELETE FROM aprendiz WHERE idAprendiz = @idAprendiz", conexion);
                cmd.Parameters.AddWithValue("@idAprendiz", idAprendiz);
                respuesta = cmd.ExecuteNonQuery() > 0;
            }
            finally { cn.MtCerrarConexion(); }
            return respuesta;
        }

        public System.Data.DataTable ListarAprendicesPorInstructor(int idInstructor)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT a.idAprendiz, a.nombres, a.apellidos, a.correo, a.estado, f.codigoFicha " +
                    "FROM aprendiz a INNER JOIN ficha f ON a.idFicha = f.idFicha " +
                    "INNER JOIN instructorFicha ifc ON f.idFicha = ifc.idFicha " +
                    "WHERE ifc.idInstructor = @idInstructor " +
                    "ORDER BY a.apellidos, a.nombres",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idInstructor", idInstructor);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            finally { cn.MtCerrarConexion(); }
            return dt;
        }

        public bool AprendizEstaCancelado(int idAprendiz)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT COUNT(1)
                      FROM aprendiz
                      WHERE idAprendiz = @idAprendiz
                        AND LOWER(LTRIM(RTRIM(ISNULL(estado, '')))) IN ('cancelado', 'canselado', 'cancelada', 'canselada')",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idAprendiz", idAprendiz);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }

        public bool AprendizEstaCanceladoPorIdUsuario(int idUsuario)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT COUNT(1)
                      FROM aprendiz
                      WHERE idUsuario = @idUsuario
                        AND LOWER(LTRIM(RTRIM(ISNULL(estado, '')))) IN ('cancelado', 'canselado', 'cancelada', 'canselada')",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
            finally { cn.MtCerrarConexion(); }
        }

        public ClAprendiz ObtenerAprendizPorIdUsuario(int idUsuario)
        {
            ClAprendiz aprendiz = null;
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT idAprendiz, nombres, apellidos, tipoDocumento, numeroDocumento, correo, telefono, fechaNacimiento, idFicha, idUsuario, estado " +
                    "FROM aprendiz WHERE idUsuario = @idUsuario",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    aprendiz = new ClAprendiz
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
                        idUsuario = Convert.ToInt32(dr["idUsuario"]),
                        estado = dr["estado"].ToString()
                    };
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return aprendiz;
        }

        public int ContarAprendicesPorFicha(int idFicha)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(1) FROM aprendiz WHERE idFicha = @idFicha",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idFicha", idFicha);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            finally { cn.MtCerrarConexion(); }
        }

        private void ValidarCupoFicha(SqlConnection conexion, SqlTransaction trans, int idFicha, int? idAprendizExcluir)
        {
            SqlCommand cmd = new SqlCommand(
                @"SELECT COUNT(1)
                  FROM aprendiz
                  WHERE idFicha = @idFicha
                    AND (@idAprendizExcluir IS NULL OR idAprendiz <> @idAprendizExcluir)",
                conexion,
                trans);
            cmd.Parameters.AddWithValue("@idFicha", idFicha);
            cmd.Parameters.AddWithValue("@idAprendizExcluir", idAprendizExcluir.HasValue ? (object)idAprendizExcluir.Value : DBNull.Value);

            if (Convert.ToInt32(cmd.ExecuteScalar()) >= 30)
                throw new InvalidOperationException("No se puede asignar el aprendiz porque la ficha ya tiene 30 aprendices.");
        }

        public List<ClResultado> ObtenerResultadosPendientes(int idAprendiz)
        {
            List<ClResultado> lista = new List<ClResultado>();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT ra.idResultado, ra.codigo, ra.descripcion
              FROM resultadosAprendizaje ra
              INNER JOIN competencias c ON ra.idCompetencias = c.idCompetencias
              INNER JOIN programa p ON c.idPrograma = p.idPrograma
              INNER JOIN centroPrograma cp ON p.idPrograma = cp.idPrograma
              INNER JOIN ficha f ON cp.idCentroPrograma = f.idCentroPrograma
              INNER JOIN aprendiz a ON f.idFicha = a.idFicha
              WHERE a.idAprendiz = @idAprendiz
                AND ra.idResultado NOT IN (
                    SELECT pr.idResultado
                    FROM planMejoramiento pm
                    INNER JOIN planResultado pr ON pm.idPlanMejoramiento = pr.idPlanMejoramiento
                    WHERE pm.idAprendiz = @idAprendiz
                      AND pm.estadoPlan = 'Aprobado'
                )",
                    cn.MtAbrirConexion());
                cmd.Parameters.AddWithValue("@idAprendiz", idAprendiz);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new ClResultado
                    {
                        idResultado = Convert.ToInt32(dr["idResultado"]),
                        codigo = dr["codigo"].ToString(),
                        descripcion = dr["descripcion"].ToString()
                    });
                }
                dr.Close();
            }
            finally { cn.MtCerrarConexion(); }
            return lista;
        }

    }
}
