using System;
using System.Collections.Generic;
using Entities;
using Interfaces;

namespace Business
{
    public class DiagramBusiness
    {
        private readonly IDiagramRepository _diagramRepository;

        public DiagramBusiness(IDiagramRepository diagramRepository)
        {
            if (diagramRepository == null)
            {
                throw new ArgumentNullException("diagramRepository");
            }

            _diagramRepository = diagramRepository;
        }

        public List<DiagramCmDto> ObtenerDiagramasPorModelo(string modelName)
        {
            return _diagramRepository.GetByModel(modelName);
        }

        public List<DiagramCmDto> ObtenerDiagramasPorModeloYCategoria(string modelName, string categoryName)
        {
            return _diagramRepository.GetByModelAndCategory(modelName, categoryName);
        }

        public List<DiagramCmDto> ObtenerProcedimientos(string modelName)
        {
            return _diagramRepository.GetByModelAndCategory(modelName, "Procedimiento");
        }

        public List<DiagramCmDto> ObtenerRamificaciones(string modelName)
        {
            return _diagramRepository.GetByModelAndCategory(modelName, "Ramificación");
        }

        public DiagramCmDto ObtenerDiagramaPorId(string modelName, int diId)
        {
            return _diagramRepository.GetById(modelName, diId);
        }
    }
}
