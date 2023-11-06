using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Application.Payments.CreateUpdatePaymentIntent;
using SkinetV2.Contracts.Payments;
using SkinetV2.Domain.Orders;
using Stripe;

namespace SkinetV2.Api.Controllers
{
    public class PaymentsController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly ILogger _logger;
        private readonly IPaymentService _paymentService;
        private const string _whSecret = "whsec_fd2bfd47ce095b182cc4fa9f7cb3a2e578c95e9add05769a20f989bbe71f6b6e";
        public PaymentsController(ISender mediator, IMapper mapper, ILogger<IPaymentService> logger, IPaymentService paymentService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdatePaymentIntentAsync([FromQuery] CreateOrUpdatePaymentIntentRequest request)
        {
            var command = _mapper.Map<CreateOrUpdatePaymentIntentCommand>(request);
            var result = await _mediator.Send(command);

            if (result.Value == null)
            {
                return Problem(title: "Payment Intent Failed", statusCode: 409, detail: "Problem while creating or updating a payment intent.");
            }

            return result.Match(
                _ => Ok(result.Value),
                Problem
            );
        }

        // LOCAL STRIPE SETUP FOR DEVELOPING - STRIPE CLI
        // Install stripe with the help of Scoop package
        // Once stripe installed you can use "stripe login"
        // Set up the local stripe listener: stripe listen -f https://localhost:7075/api/payments/webhook -e payment_intent.succeeded,payment_intent.payment_failed
        // Get the web hook secret and paste it in the constant
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhookAsync()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _whSecret);

            PaymentIntent intent;
            Order? order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded:", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received", order!.OrderId.Value);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed:", intent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation("Order updated to payment failed", order!.OrderId.Value);
                    break;
                default:
                    break;
            }

            // An empty result is returned to stripe so that stripe knows we received the event.
            // If we dont do this, stripe will keep sending us that event (prod: 3days, dev: x-hours) until we confirmed we reveiced it by returning an empty result.
            return new EmptyResult();
        }
    }
}