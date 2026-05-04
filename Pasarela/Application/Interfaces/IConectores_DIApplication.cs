using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lantik.Pasarela.Application.DTOs
{
    public interface IConectores_DIApplication
    {
        ResponseBaseDTO<IList<Conectores_DIDTO>> ObtenerPorIDyModelo(string ModelName, int DI_ID);
        ResponseBaseDTO<int> Insertar(Conectores_DIDTO business);
        ResponseBaseDTO<bool> Borrar(int IdDiagram);
    }
}
