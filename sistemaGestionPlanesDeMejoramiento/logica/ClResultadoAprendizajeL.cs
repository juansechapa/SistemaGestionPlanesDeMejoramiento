using sistemaGestionPlanesDeMejoramiento.datos;
using sistemaGestionPlanesDeMejoramiento.Modelo;
using System;
using System.Collections.Generic;

namespace sistemaGestionPlanesDeMejoramiento.logica
{
    public class ClResultadoAprendizajeL
    {
        ClResultadoAprendizajeD resultadoD = new ClResultadoAprendizajeD();

        public List<ClResultado> ListarPorCompetencia(int idCompetencias)
        {
            if (idCompetencias <= 0) throw new ArgumentException("Competencia inválida.");
            return resultadoD.ListarPorCompetencia(idCompetencias);
        }

        public bool Insertar(ClResultado resultado)
        {
            if (resultado == null) throw new ArgumentException("Resultado inválido.");
            if (resultado.idCompetencias <= 0) throw new ArgumentException("Competencia inválida.");
            if (string.IsNullOrWhiteSpace(resultado.codigo)) throw new ArgumentException("El código del resultado es obligatorio.");
            if (string.IsNullOrWhiteSpace(resultado.descripcion)) throw new ArgumentException("La descripción del resultado es obligatoria.");
            if (string.IsNullOrWhiteSpace(resultado.estado)) resultado.estado = "Activo";
            return resultadoD.Insertar(resultado);
        }

        public bool Eliminar(int idResultado)
        {
            if (idResultado <= 0) throw new ArgumentException("Resultado inválido.");
            return resultadoD.Eliminar(idResultado);
        }
    }
}
