using BusinessLogic.Interfaces;
using DtoMappings.DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DigiBank.Controllers
{

    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionLogic _transactionLogic;
        public TransactionController(ITransactionLogic transactionLogic)
        {
            _transactionLogic = transactionLogic;
        }

        [HttpPost]
        [Route("api/[controller]/Transfer")]
        public IActionResult Transfer(TransactionDepositDTO transactionDepositDTO)
        {
            try
            {
               return Ok(_transactionLogic.Transfer(transactionDepositDTO));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/[controller]/AdminDeposit")]
        public IActionResult AdminDeposit(AdminTransactionDTO adminDepositDTO)
        {
            try
            {
                return Ok(_transactionLogic.AdminDepositAsync(adminDepositDTO));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/[controller]/Withdrawal")]
        public IActionResult Withdrwal(TransactionWithdrawalDTO transactionWithdrawalDTO)
        {
            try
            {
                return Ok(_transactionLogic.Withdrawal(transactionWithdrawalDTO));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("api/[controller]/AdminWithdrawal")]
        public IActionResult AdminWithdrwal(AdminTransactionDTO adminTransactionDTO)
        {
            try
            {
                return Ok(_transactionLogic.AdminWithdrawal(adminTransactionDTO ));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
