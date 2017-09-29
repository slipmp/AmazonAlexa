using System.Net.Http;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Canalex.AWS.BizRules;
using System;
using Alexa.NET.Response.Ssml;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Canalex.AWS
{
    public class Function
    {

        public const string INVOCATION_NAME = "DTN";

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var skillResponseService = new SkillResponseService();

            var requestType = input.GetRequestType();
            if (requestType == typeof(IntentRequest))
            {
                var alexaRequest = (IntentRequest) input.Request;
                var alexaIntentName = (AlexaIntentEnum) Enum.Parse(typeof(AlexaIntentEnum), alexaRequest.Intent.Name);

                string result = string.Empty;
                var lenderBizRule = new LenderBizRule();

                switch (alexaIntentName)
                {
                    case AlexaIntentEnum.AppStatusForLastName:
                        result = lenderBizRule.GetApplicationStatus("Soares");
                        break;
                    case AlexaIntentEnum.FavLender:
                        var date = alexaRequest.Intent.Slots["DealerDate"];
                        result = lenderBizRule.GetMyFavouriteLender(date.Value);
                        break;
                    case AlexaIntentEnum.DealsStuckInSU:
                        result = lenderBizRule.GetDealsStuckInSu();
                        break;
                    case AlexaIntentEnum.DealsInApprovedStatus:
                        result = lenderBizRule.GetDealsApprovedToday();
                        break;
                    case AlexaIntentEnum.LenderDeclined:
                        var lender = alexaRequest.Intent.Slots["lender"];
                        result = lenderBizRule.GetDealsDeclined(lender.Value);
                        break;

                    
                    default:
                        result = "Cannot find your answer in DTN";
                        break;
                }
              
                
                
                return skillResponseService.MakeSkillResponse(result, true);
            }
            return skillResponseService.MakeSkillResponse(
                $"I don't know how to handle this intent. Please say something like " +
                $"Alexa, Ask DTN what is my favourite lender today?",
                true);
        }


        
    }
}