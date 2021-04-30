using RestWithASPNET5.Model;
using System.Collections.Generic;

namespace RestWithASPNET5.Business

{
    //metodos implementados
    public interface IPersonBusiness
    {
        Person Create(Person person);

        Person FindByID(long id);

        List<Person> FindAll();

        Person Update(Person person);

        void Delete(long id);

    }
}
