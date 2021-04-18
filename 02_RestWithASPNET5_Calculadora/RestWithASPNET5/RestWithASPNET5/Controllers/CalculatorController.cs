using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
       

        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        //path
        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult sumGet(string firstNumber, string secondNumber)
        {
            // verificar se é numerico
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("sub/{firstNumber}/{secondNumber}")]
        public IActionResult subGet(string firstNumber, string secondNumber)
        {
            //verificar se é número
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(sub.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("multi/{firstNumber}/{secondNumber}")]
        public IActionResult multiGet(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var multi = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(multi.ToString());
            }
            return BadRequest("Invalid Input");
        }

        [HttpGet("div/{firstNumber}/{secondNumber}")]
        public IActionResult divGet(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber)&& IsNumeric(secondNumber))
            {
                var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(div.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("med/{firstNumber}/{secondNumber}")]
        public IActionResult medGet(string firstNumber, string secondNumber)
        {
            if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var div = (ConvertToDecimal(firstNumber) + ConvertToDecimal(firstNumber)) / 2;
                return Ok(div.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("raiz/{firstNumber}")]
        public IActionResult raizGet(string firstNumber)
        {
            if(IsNumeric(firstNumber))
            {
                var raiz = Math.Sqrt((double)ConvertToDecimal(firstNumber));
                return Ok(raiz.ToString());
            }

            return BadRequest("Invalid Input");
        }


        //converte o numero em decimal
        private decimal ConvertToDecimal(string strNumber)
        {
            decimal decimalValue;
            if(decimal.TryParse(strNumber, out decimalValue))
            {
                return decimalValue;
            }

            return 0;
        }

        //validador de número
        private bool IsNumeric(string strNumber)
        {
            double number;
            //Converte a representação de cadeia de caracteres de um número no inteiro com sinal de 32 bits equivalente. Um valor retornado indica se a operação foi bem-sucedida.
            bool IsNumber = double.TryParse(
                strNumber,
                System.Globalization.NumberStyles.Any, 
                System.Globalization.NumberFormatInfo.InvariantInfo,
                out number);
            return IsNumber;               
        }
    }
}
