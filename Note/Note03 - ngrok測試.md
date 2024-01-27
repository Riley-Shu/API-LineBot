# Step01 註冊&下載
> **Ngrok** 是一個全球都可以使用的『反向代理』工具，它可以把你在任何裝置中運行的網頁服務轉發到一組公開的網址
> ngrok 通常不是用於真正的網站部署，而是快速地讓我們 demo 網站服務的。你可以把 ngrok 看作是應用程序的前門。ngrok 是獨立於環境的，因為它可以向任何地方運行的服務提供流量，而不會改變您的環境網絡

- Download: [Download (ngrok.com)](https://ngrok.com/download)
- 註冊 (記得驗證信箱)

# Step02 建立token
- 取得Command Line
![image](https://github.com/Riley-Shu/API-LineBot/blob/master/Note/image/S07-N03-01.png)
- 開啟ngrok.exe
- cmd: 建立token
```
ngrok config add-authtoken (My Authtoken)
```
- cmd: 連接到服務
```
ngrok http https://localhost:7108
```
![image](https://github.com/Riley-Shu/API-LineBot/blob/master/Note/image/S07-N03-02.png)
注意: 
- 此頁面需正確離開 (ctrl+c)
- 一旦離開要重新建立token
- - 若需要重啟服務，可直接重啟

# Step03 Postman測試
- 【POST】https://bd76-218-164-140-54.ngrok-free.app/api/linebot/hook
- Body
```json
{
  "destination": "string",
  "events": [
    {
      "replyToken": "string",
      "type": "message",
      "mode": "active",
      "timestamp": 0,
      "source": {
        "type": "user",
        "groupId": "string",
        "userId": "U7ad5f7aa6ea24ed0fe48d7572ab5248c"
      },
      "webhookEventId": "string",
      "deliveryContext": {
        "isRedelivery": true
      },
      "message": {
        "id": "string",
        "type": "text",
        "quoteToken": "string",
        "text": "test",
        "emojis": [
          {
            "index": 0,
            "length": 0,
            "productId": "string",
            "emojiId": "string"
          }
        ],
        "mention": {
          "mentionees": [
            {
              "index": 0,
              "length": 0,
              "type": "string",
              "userId": "string"
            }
          ]
        }
      }
    }
  ]
}
```
![image](https://github.com/Riley-Shu/API-LineBot/blob/master/Note/image/S07-N03-03.png)

# Step04 - 加入Line webhook URL
- 將ngrok路徑加入Line webhook URL
![image](https://github.com/Riley-Shu/API-LineBot/blob/master/Note/image/S07-N03-04.png)
- 取消自動回應
![image](https://github.com/Riley-Shu/API-LineBot/blob/master/Note/image/S07-N03-05.png)
![image](https://github.com/Riley-Shu/API-LineBot/blob/master/Note/image/S07-N03-06.png)

![image](https://github.com/Riley-Shu/API-LineBot/blob/master/Note/image/S07-N03-07.png)

