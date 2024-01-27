# Step01 註冊Line Developers帳號
1. create a new channel > Messaging API
![[S07-N01-01.png]]
# Step02 建立API專案
# Step03 建立Model類別
- 取得Line Messaging API JSON格式
> - [Messaging API reference | LINE Developers](https://developers.line.biz/en/reference/messaging-api/#wh-text)
> - 路徑: Line developers > Documentation > Messaging API > Messaging API reference > Webhook Event Objects > Message event > Text
```json
// When a user sends a text message containing mention and an emoji in a group chat
{
  "destination": "xxxxxxxxxx",
  "events": [
    {
      "replyToken": "nHuyWiB7yP5Zw52FIkcQobQuGDXCTA",
      "type": "message",
      "mode": "active",
      "timestamp": 1462629479859,
      "source": {
        "type": "group",
        "groupId": "Ca56f94637c...",
        "userId": "U4af4980629..."
      },
      "webhookEventId": "01FZ74A0TDDPYRVKNK77XKC3ZR",
      "deliveryContext": {
        "isRedelivery": false
      },
      "message": {
        "id": "444573844083572737",
        "type": "text",
        "quoteToken": "q3Plxr4AgKd...",
        "text": "@All @example Good Morning!! (love)",
        "emojis": [
          {
            "index": 29,
            "length": 6,
            "productId": "5ac1bfd5040ab15980c9b435",
            "emojiId": "001"
          }
        ],
        "mention": {
          "mentionees": [
            {
              "index": 0,
              "length": 4,
              "type": "all"
            },
            {
              "index": 5,
              "length": 8,
              "userId": "U49585cd0d5...",
              "type": "user"
            }
          ]
        }
      }
    }
  ]
}
```

## WebHookData.cs
- 選擇性貼上 > 貼上JSON作為類別
```cs
namespace Sample07_Service.Models
{

    public class WebHookData
    {
        public string destination { get; set; }
        public Event[] events { get; set; }
    }

    public class Event
    {
        public string replyToken { get; set; }
        public string type { get; set; }
        public string mode { get; set; }
        public long timestamp { get; set; }
        public Source source { get; set; }
        public string webhookEventId { get; set; }
        public Deliverycontext deliveryContext { get; set; }
        public HookMessage message { get; set; }
    }

    public class Source
    {
        public string type { get; set; }
        //public string groupId { get; set; } //
        public string userId { get; set; }
    }

    public class Deliverycontext
    {
        public bool isRedelivery { get; set; }
    }

    public class HookMessage
    {
        public string id { get; set; }
        public string type { get; set; }
        public string quoteToken { get; set; } 
        public string text { get; set; }
        //public Emoji[] emojis { get; set; } 
        //public Mention mention { get; set; }
    }

    public class Mention
    {
        public Mentionee[] mentionees { get; set; }
    }

    public class Mentionee
    {
        public int index { get; set; }
        public int length { get; set; }
        public string type { get; set; }
        public string userId { get; set; }
    }

    public class Emoji
    {
        public int index { get; set; }
        public int length { get; set; }
        public string productId { get; set; }
        public string emojiId { get; set; }
    }
}
```
- 注意: 若貼上失敗，要拿掉JSON原始格式的註解
![[S07-N01-02.png]]

# Step04 建立API控制器
- 建立API控制器
- 設定路徑
- Postman測試
## LinebotController.cs
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
        [Route("hook")]
        [HttpPost]
        [Consumes("application/json")]
        public Rootobject rootObject([FromBody] Rootobject rootobject)
        {
            return rootobject;
        }
    }
}
```
![[S07-N01-03.png]]

# Step05 編輯API控制器return內容
- 調整API控制器 return 內容為文字
- Postman測試
## LinebotController.cs
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
        [Route("hook")]
        [HttpPost]
        [Consumes("application/json")]
        public String rootObject([FromBody] Rootobject rootobject)
        {
            String replayMessage = "";
            Console.WriteLine(replayMessage);
            Event myEvent = rootobject.events[0];
            Console.WriteLine(myEvent);
            Source source = myEvent.source;
            Console.WriteLine(source);
            
            Console.WriteLine(source.type);
            if (source.type.Equals("user")) {
                
                String userId = source.userId;
                Console.WriteLine(userId);
                Console.WriteLine(myEvent.type);
                switch (myEvent.type)
                {
                    case "message":
                        Message message = myEvent.message; ;
                        Console.WriteLine(message);
                        Console.WriteLine(message.type);
                        switch (message.type)
                        {
                            case "text":
                                String content = message.text;
                                replayMessage = content;
                                break;
                            case "image":
                                break;
                        }
                        break;
                    case "follow":
                        break;
                    case "unfollow":
                        break;  
                    }
            }
            else
            {
            }
            return replayMessage;
        }
    }
}
```

- POSTMAN body內容
- 注意: 相關的項目body要填進去
- user Id: 
	MyRobot > Basic settings > Your user ID
```json
{
  "destination": "string",
  "events": [
    {
      "replyToken": "string",
      "type": "message", 
      "mode": "string",
      "timestamp": 0,
      "source": {
        "type": "user",
        "groupId": "string",
        "userId": "userId" 
      },
      "webhookEventId": "string",
      "deliveryContext": {
        "isRedelivery": true
      },
      "message": {
        "id": "string",
        "type": "txt",
        "quoteToken": "string",
        "text": "test",
        "emojis": [
          {
            "index": 0,
            "length": 0,
            "productId": "string",
            "emojiId": "string"
          }
        ],
        "mention": {
          "mentionees": [
            {
              "index": 0,
              "length": 0,
              "type": "string",
              "userId": "string"
            }
          ]
        }
      }
    }
  ]
}
```
![[S07-N01-04.png]]
