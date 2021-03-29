using Cs.Application.Interfaces;
using Cs.Common.Models;
using Cs.Domain.Hr;
using Cs.Service.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cs.Service.Admin.Hr
{
    public class PaymentTypeController : ResourceController
    {
        private readonly ICrudOperation<PaymentType> _paymentTypes;

        public PaymentTypeController(ICrudOperation<PaymentType> paymentTypes)
        {
            _paymentTypes = paymentTypes;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Paging paging)
        {
            var result = _paymentTypes.Get(paging);
            
            return Json(ServiceResult<ListResult<PaymentType>>.Ok(result));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var paymentType = _paymentTypes.Get(id);
            if (paymentType == null)
                return NotFound();

            return Json(ServiceResult<PaymentType>.Ok(paymentType));
        }

        [HttpPost]
        public IActionResult Post([FromBody]PaymentType paymentType)
        {
            var id = _paymentTypes.Add(paymentType);

            return Json(ServiceResult<int>.Ok(id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PaymentType paymentType)
        {
            _paymentTypes.Update(paymentType);

            return Json(ServiceResult<bool>.Ok(true));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _paymentTypes.Delete(id);

            return Json(ServiceResult<bool>.Ok(true));
        }
    }
}