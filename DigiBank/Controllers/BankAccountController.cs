using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace DigiBank.Controllers
{
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountLogic _bankAccountLogic;
        private readonly ITransactionLogic _transactionLogic;
        public BankAccountController(IBankAccountLogic bankAccountLogic, ITransactionLogic transactionLogic)
        {
            _bankAccountLogic = bankAccountLogic;
            _transactionLogic = transactionLogic;
        }

        [HttpGet]
        [Route("api/[controller]/GetAccountBalance")]
        public IActionResult GetAccountBalance()
        {
            try
            {
                var loggedInUserId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
                return Ok(_bankAccountLogic.GetAccountBalance(loggedInUserId));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/[controller]/AdminGetAccountBalance")]
        public IActionResult AdminGetAccountBalance(string userAccountNumber)
        {
            try
            {
                return Ok(_bankAccountLogic.AdminGetAccountBalance(userAccountNumber));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetAccountStatement")]
        public IActionResult GetAccountStatement()
        {
            try
            {
                var loggedInUserId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
                return Ok(_transactionLogic.GetTransactionsStatement(loggedInUserId));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("api/[controller]/AdminGetAccountStatement")]
        public IActionResult AdminGetAccountStatement(string userAccountNumber)
        {
            try
            {
                return Ok(_transactionLogic.AdminGetTransactionsStatement(userAccountNumber));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
