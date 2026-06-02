using Pasabidea.Core.Entities.XML;
using Pasabidea.Core.Helpers;
using Lantik.Pasabidea.Core.Data.sqlRepository;

namespace Pasabidea.Core.Data.XML
{
    public class XMLRepository
    {
        private readonly XMLsqlRepository _repository = new XMLsqlRepository();

        public int GrabarConfiguracionBorrador(string XML)
        {

            string usuario = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            _repository.BeginTransaction();

            var res = _repository.GrabarConfiguracionBorrador(XML, out int sqlCode, out string sqlMessage);


            if (res.SQLCode == 0)
                _repository.Commit();
            else
                _repository.Rollback();

            return res.SQLCode;
        }
    }
}
