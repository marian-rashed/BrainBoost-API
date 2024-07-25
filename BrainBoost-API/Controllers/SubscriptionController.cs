using AutoMapper;
using BrainBoost_API.DTOs.Paylink;
using BrainBoost_API.DTOs.Subscription;
using BrainBoost_API.Mapper;
using BrainBoost_API.Models;
using BrainBoost_API.Repositories.Inplementation;
using HelperPlan.DTO.Paylink;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Claims;

namespace BrainBoost_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> user;

        // get section Paylink from appsettings.json by i options
        private readonly EnvironmentPaylink paylink;
        private readonly IMapper mapper;

        public SubscriptionController(IUnitOfWork unitOfWork, IOptions<EnvironmentPaylink> paylink, IMapper mapper, UserManager<ApplicationUser> user)
        {
            this.unitOfWork = unitOfWork;
            this.user = user;
            this.paylink = paylink.Value;
            this.mapper = mapper;
        }
        //Check Subscription Status if it Active Or Not
        [HttpGet("GetStatus")]
        public IActionResult GetSubscriptionStatus()
        {
            int TeacherId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var subscription = this.unitOfWork.SubscriptionRepository.Get(c => c.TeacherId == TeacherId);
            if (subscription != null)
            {
                return Ok(new { IsActive = subscription.IsActive });
            }
            return Ok(new { IsActive = false });
        }

        // create subscription

        [HttpPost("Create")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(SubscriptionDto subscriptionDto)
        {
            if (ModelState.IsValid)
            {
                HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                subscriptionDto.TeacherId = 1;//int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var orderNumber = Guid.NewGuid().ToString();

                var result = await GeneratePaylink(await AuthenticationPaylink(), subscriptionDto, orderNumber);

                var subscribtion = mapper.Map<subscription>(subscriptionDto);
                subscribtion.SubscribtionsStatus = result.OrderStatus;
                subscribtion.TransactionNo = result.TransactionNo;
                subscribtion.CheckUrl = result.CheckUrl;
                subscribtion.orderNumber = orderNumber;
                this.unitOfWork.SubscriptionRepository.add(subscribtion);
                //this.unitOfWork.save();
                return Ok(new { Url = result.Url });

            }
            return BadRequest(ModelState);
        }

        // check subscribtion status
        [HttpGet("CheckStatus/{orderNumber}")]
        public async Task<IActionResult> CheckStatus(string? orderNumber)
        {
            var subscribtion = this.unitOfWork.SubscriptionRepository.Get(x => x.orderNumber == orderNumber);
            int Duration = this.unitOfWork.PlanRepository.Get(x => x.Id == subscribtion.PlanId).Duration;
            if (subscribtion == null)
            {
                return NotFound();
            }
            var options = new RestClientOptions(this.paylink.url + $"/api/getInvoice/{subscribtion.TransactionNo}");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json;charset=UTF-8");
            var token = await AuthenticationPaylink();
            request.AddHeader("Authorization", "Bearer " + token);
            var response = await client.GetAsync(request);
            var rst = response.Content;
            var result = JsonConvert.DeserializeObject<GatewayOrderResponse>(response.Content);

            // using swatch to check status of subscribtion
            switch (result.OrderStatus)
            {
                case "Pending":
                    subscribtion.SubscribtionsStatus = "Pending";
                    break;
                case "Paid":
                    subscribtion.SubscribtionsStatus = "Paid";
                    subscribtion.IsActive = true;
                    subscribtion.EndDate = DateTime.Now.AddDays(Duration);
                    break;
                case "Declined":
                    subscribtion.SubscribtionsStatus = "Declined";
                    break;
                default:
                    subscribtion.SubscribtionsStatus = "Cancelled ";
                    break;
            }

            this.unitOfWork.SubscriptionRepository.update(subscribtion);
            //this.unitOfWork.save();
            return Ok(result);
        }

        [NonAction]
        // Authentication paylink
        public async Task<string> AuthenticationPaylink()
        {
            var options = new RestClientOptions(this.paylink.url + "/api/auth");
            var client = new RestClient(options);
            var request = new RestRequest("");


            request.AddHeader("accept", "*/*");
            request.AddHeader("content-type", "application/json");
            request.AddJsonBody(new
            {
                apiId = this.paylink.apiId,
                secretKey = this.paylink.secretKey,
                persistToken = this.paylink.persistToken,
            });
            var response = await client.PostAsync(request);
            var result = response.Content;

            // i need get id_token desrialize it and return it
            var token = JsonConvert.DeserializeObject<tokenPaylink>(result);

            // return token.id_token;

            return token.id_token;

        }

        [NonAction]
        // genrate paylink
        public async Task<GatewayOrderResponse> GeneratePaylink(string token, SubscriptionDto subscribtionDto, string orderNumber)
        {

            /*var teacherId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var teacherInfo = await user.FindByIdAsync(teacherId);
            // _unitOfWork.EmployeerRepository.Get(x => x.Id == subscribtionDto.EmployerID);

            var plan = this.unitOfWork.PlanRepository.Get(x => x.Id == subscribtionDto.PlanId);*/

            var options = new RestClientOptions(this.paylink.url + "/api/addInvoice");
            var client = new RestClient(options);
            var request = new RestRequest("");

            request.AddHeader("accept", "application/json");
            // add token to header
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddJsonBody(new
            {

                amount = 100,//plan.Price, //100.0,
                clientMobile = 05156325333,// teacherInfo.PhoneNumber,//"0512345678",
                clientName = "poula",//string.Format("{0} {1}", teacherInfo.Fname, teacherInfo.Lname),
                //"Mohammed Ali",
                clientEmail = "poual@gmail.com",//teacherInfo.Email,//"mohammed@test.com",

                orderNumber = "1236566256",//orderNumber,// "123456789",


                callBackUrl = $"http://localhost:4200/success/{orderNumber}",
                cancelUrl = $"http://localhost:4200/fail/{orderNumber}",
                currency = "SAR",
                note = "Test invoice",

                products = new List<object>
                {
                    new
                    {

                        title = "test",//plan.Name,//"test",
                        price =100.0,//plan.Price,//100.0,
                        qty = 1,
                        imageSrc = "",
                        description = "",
                        isDigital = false,
                        specificVat = 0,
                        productCost = 0,
                    }
                },
            });
            var response = await client.PostAsync(request);
            // deserialize response
            var rst = response.Content;
            var result = JsonConvert.DeserializeObject<GatewayOrderResponse>(response.Content);

            return result;
        }

    }
}
