using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DigiBank.Controllers
{
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IBankAccountLogic _bankAccountLogic;
        public BankAccountController(IBankAccountLogic bankAccountLogic)
        {
            _bankAccountLogic = bankAccountLogic;
        }

        [HttpGet]
        [Route("api/[controller]/GetAccountBalance")]
        public IActionResult GetAccountBalance()
        {
            try
            {
                return Ok(_bankAccountLogic.GetAccountBalance());
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
                return Ok(_bankAccountLogic.GetAccountStatement());
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
                return Ok(_bankAccountLogic.AdminGetAccountStatement(userAccountNumber));
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
