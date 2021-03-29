using System.Linq;
using Cs.Application.Interfaces;
using Cs.Application.Tools;
using Cs.Common.Models;
using Cs.Domain.Hr;

namespace Cs.Application.Hr
{
    public class PaymentTypeService : ICrudOperation<PaymentType>
    {
        private readonly IHrDbService _db;

        public PaymentTypeService(IHrDbService db)
        {
            _db = db;
        }

        public ListResult<PaymentType> Get(Paging paging)
        {
            var paymentTypes = _db.PaymentTypes.All();
            if (!string.IsNullOrEmpty(paging.Search))
                paymentTypes = paymentTypes.Where(t => t.Name.Contains(paging.Search));
            
            var result = paymentTypes.Skip(paging.Skip).Take(paging.Count).ToList();
            var total = paymentTypes.Count();
            
            return new ListResult<PaymentType>(result, total);
        }

        public PaymentType Get(int id)
        {
            return _db.PaymentTypes.Get(id);
        }

        public int Add(PaymentType t)
        {
            if (_db.PaymentTypes.All().Any(bc => bc.Name == t.Name))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.PaymentTypes.Add(t);
            _db.PaymentTypes.Save();

            return t.Id;
        }

        public void Update(PaymentType t)
        {
            if (_db.PaymentTypes.All().Any(bc => bc.Name == t.Name && bc.Id != t.Id))
                throw new ServiceException(UiConstants.EntityWithSameNameAlreadyExists(t.Name));

            _db.PaymentTypes.Update(t);
            _db.PaymentTypes.Save();
        }

        public void Delete(int id)
        {
            _db.PaymentTypes.Delete(id);
            _db.PaymentTypes.Save();
        }
    }
}