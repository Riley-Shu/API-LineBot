# Step01 取得Send messages at any time路徑
 - [Sending messages | LINE Developers](https://developers.line.biz/en/docs/messaging-api/sending-messages/#send-messages-at-any-time)
 - 路徑: Line developers > Documentation > Messaging API > Guides > Sending messages > Send messages at any time
 - Exp: 
```json
curl -v -X POST https://api.line.me/v2/bot/message/push \
-H 'Content-Type: application/json' \
-H 'Authorization: Bearer {channel access token}' \
-d '{
    "to": "U4af4980629...",
    "messages":[
        {
            "type":"text",
            "text":"Hello, world1"
        },
        {
            "type":"text",
            "text":"Hello, world2"
        }
    ]
}'
```
# Step02 將路徑加入專案並註冊服務
- appsetting.json加入路徑
- 建立自訂類別 ServiceURL
- 註冊服務
## appsetting.json
```json
"Services": {
	"linesendpush": "https://api.line.me/v2/bot/message/push"
}
```
## ServiceURL.cs
```cs
namespace Sample07_Service.Models
{
    public class ServiceURL
    {
        public String linesedpush { set; get; }
    }
}
```
## Program.cs
- 註冊自訂服務
- 註冊AddHttpClient
```cs
//= = = = = 註冊服務:ServiceURL = = = = = 
ServiceURL serviceURL = new ServiceURL();
ConfigurationManager manager = builder.Configuration;
IConfigurationSection section = manager.GetSection("Services");
section.Bind(serviceURL);
builder.Services.AddSingleton(serviceURL);
//= = = = = = = = = = = = = = = = = = = =

//= = = = = 註冊服務: AddHttpClient = = = = = 
builder.Services.AddHttpClient();
//= = = = = = = = = = = = = = = = = = = =
```
## LinebotController.cs
- 將建構子加入控制器
```cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample07_Service.Models;

namespace Sample07_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private ServiceURL _serviceURL;
        private IHttpClientFactory _httpClientFactory;

        public LineBotController (ServiceURL serviceURL, IHttpClientFactory httpClientFactory)
        {
            _serviceURL = serviceURL;
            _httpClientFactory = httpClientFactory;
        }
```

# Step03  配置send push Message API
- 安裝套件: Newtonsoft.json
![[S07-N02-01.png]]
## SendPushData.cs
- 建立類別
```cs
namespace Sample07_Service.Models
{

    public class SendPushData
    {
        public string to { get; set; }
        public SendPushMessage[] messages { get; set; }
    }

    public class SendPushMessage
    {
        public string type { get; set; }
        public string text { get; set; }
    }
}
```
## LinebotController.cs
- channel access token:   ![[S07-N02-02.png]]
```cs
 //Part: 配置send push Message API
 HttpClient httpClient = _httpClientFactory.CreateClient();
 //配置Uri物件，連接 "linesendpush": "https://api.line.me/v2/bot/message/push" 
 httpClient.BaseAddress = new Uri(_serviceURL.linesendpush);
 //配置 'Authorization: Bearer {channel access token}' 
 httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "(Channel access token)"); 
 //配置Http Body,透過SendPushData物件
 SendPushData sendPushData = new SendPushData()
 {
     to = userId,
     messages = new SendPushMessage[] {new SendPushMessage()
     {
         type = "text",
         text = "replyMessage"
     }
     }
 };

  
 //Part 轉換為JSON
 String jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(sendPushData); //Newtonsoft套件
 HttpContent httpContent = new StringContent(jsonString);
 httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
 httpClient.PostAsync("",httpContent).GetAwaiter().GetResult();
       


 return replyMessage;
```
