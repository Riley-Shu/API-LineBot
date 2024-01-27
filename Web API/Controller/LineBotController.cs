using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample07_Service.Models;

namespace Sample07_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        //Data
        private ServiceURL _serviceURL;
        private IHttpClientFactory _httpClientFactory;
        //建構子注入
        public LineBotController(ServiceURL _serviceURL, IHttpClientFactory httpClientFactory)
        {
            this._serviceURL = _serviceURL; //注意：要加this
            _httpClientFactory = httpClientFactory;
        }

        [Route("hook")]
        [HttpPost]
        [Consumes("application/json")]
        public String Webhook([FromBody] WebHookData webHookData)
        {
            //Part 
            String replyMessage = "";
            Console.WriteLine(replyMessage);
            Event myEvent = webHookData.events[0];
            Console.WriteLine(myEvent);

            Source source = myEvent.source;
            Console.WriteLine(source);
            String userId = null;
            Console.WriteLine(source.type);
            Console.WriteLine(myEvent.type);

            //辨認source類型
            if (source.type.Equals("user"))
            {  //如果soruce是user，是使用者進行操作
                userId = source.userId;
                Console.WriteLine(userId);

                switch (myEvent.type)
                {
                    case "message": //使用者聊天
                        HookMessage eventMessage = myEvent.message; ;
                        switch (eventMessage.type)
                        {
                            case "text": //如果message類型是text
                                //String bodyContent = eventMessage.text;
                                replyMessage = "Hello";
                                Console.WriteLine(replyMessage);
                                break;
                                ;
                            case "image":
                                break;
                        }
                        break;
                    case "follow": //使用者追蹤
                        break;
                    case "unfollow": //使用者退追蹤
                        break;
                }
            }
            else    //如果soruce不是user
            {
                //如果soruce是user，是使用者進行操作
                userId = source.userId;
                Console.WriteLine(userId);

                switch (myEvent.type)
                {

                    case "message": //使用者聊天

                        HookMessage eventMessage = myEvent.message; ;
                        switch (eventMessage.type)
                        {
                            case "text": //如果message類型是text
                                //String bodyContent = eventMessage.text;
                                replyMessage = "你說hello";
                                Console.WriteLine(replyMessage);
                                break;
                                ;
                            case "image":
                                break;
                        }
                        break;
                    case "follow": //使用者追蹤
                        break;
                    case "unfollow": //使用者退追蹤
                        break;
                }
            }

            //Part: 配置send push Message API
            HttpClient httpClient = _httpClientFactory.CreateClient();
            //配置Uri物件，連接 "linesendpush": "https://api.line.me/v2/bot/message/push" 
            httpClient.BaseAddress = new Uri(_serviceURL.linesendpush);
            //配置 'Authorization: Bearer {channel access token}' 
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "(channel access token)");
            //配置Http Body,透過SendPushData物件
            SendPushData sendPushData = new SendPushData()
            {
                to = userId,
                messages = new PushMessage[] {new PushMessage()
                    {
                        type = "text",
                        text = replyMessage
                        //text = "replyMessage"
                    }
                }
            };

            //Part 轉換為JSON
            String jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sendPushData); //Newtonsoft套件
            HttpContent httpContent = new StringContent(jsonString);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            httpClient.PostAsync("", httpContent).GetAwaiter().GetResult();
            //replyMessage = "你說hello";
            return replyMessage;
        }
    }
}
