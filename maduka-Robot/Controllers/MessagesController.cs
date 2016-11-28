using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using maduka_Robot.Models;
using System.IO;
using System.Web;
using System.Configuration;
using Microsoft.Bot.Builder.Dialogs;

namespace maduka_Robot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// 取得從Bot Framework送進來的訊息
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                // 處理回覆的內容
                int length = (activity.Text ?? string.Empty).Length;
                string strReply = $"你送入的文字是 {activity.Text} 這段文字長 {length} 個字元";

                /* ----------有用到LUIS才需要打開這些部份----------
                string strLuisKey = ConfigurationManager.AppSettings["LUISAPIKey"].ToString();
                string strLuisAppId = ConfigurationManager.AppSettings["LUISAppId"].ToString();
                string strMessage = HttpUtility.UrlEncode(activity.Text);
                string strLuisUrl = $"https://api.projectoxford.ai/luis/v1/application?id={strLuisAppId}&subscription-key={strLuisKey}&q={strMessage}";

                // 找到文字後，往LUIS送
                WebRequest request = WebRequest.Create(strLuisUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                CognitiveModels.LUISResult objLUISRes = JsonConvert.DeserializeObject<CognitiveModels.LUISResult>(json);

                strReply = "無法識別的內容";

                if (objLUISRes.intents.Count > 0)
                {
                    string strIntent = objLUISRes.intents[0].intent;
                    if (strIntent == "詢問")
                    {
                        string strDate = objLUISRes.entities.Find((x => x.type == "日期")).entity;
                        string strAir = objLUISRes.entities.Find((x => x.type == "航空公司")).entity;
                        string strService = objLUISRes.entities.Find((x => x.type == "服務")).entity;

                        strReply = $"您要詢問的航空公司:{strAir}，日期:{strDate}，相關服務是:{strService}。我馬上幫您找出資訊";
                        strReply += ".....這裡加上後續資料的呈現.....";
                    }
                    
                    if (strIntent == "只是打招呼")
                    {
                        strReply = "您好，有什麼能幫得上忙的呢?";
                    }

                    if (strIntent == "None")
                    {
                        strReply = "您在說什麼，我聽不懂~~~(轉圈圈";
                    }
                }
                ---------- End LUIS ---------- */

                Activity reply = activity.CreateReply(strReply);
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }

            var responses = Request.CreateResponse(HttpStatusCode.OK);
            return responses;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}