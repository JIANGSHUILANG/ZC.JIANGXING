using Newtonsoft.Json;
using org.apache.rocketmq.client.consumer;
using org.apache.rocketmq.client.consumer.listener;
using org.apache.rocketmq.common.message;
using org.apache.rocketmq.common.protocol.heartbeat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZC.DBUtils;

namespace JiangXinService.Utils
{
    public class TestRocketMQ
    {
        static MongodbHelper mongodbHelper = null;

        static DefaultMQPushConsumer consumerQuery = null;

        static DefaultMQPushConsumer consumerUpdateVersion = null;

        public void runtest(Object invokeEntity)
        {
            try
            {
                mongodbHelper = new ZC.DBUtils.MongodbHelper(System.Configuration.ConfigurationManager.AppSettings["mgdwhost"].ToString());
            }
            catch (Exception ex)
            {
                File.AppendAllText("error.txt", ex.Message);
            }

            //// 处理mongodb查询日志
            consumerQuery = RocketMqHelper.CreateDefaultMQPushConsumer<MongoDBQueryListener>("MongoDB_Query", MessageModel.CLUSTERING, "MongoDB", "Query");

            consumerUpdateVersion = RocketMqHelper.CreateDefaultMQPushConsumer<MongoDBUpdateVersionListener>("MongoDB_UpdateVersion", MessageModel.CLUSTERING, "MongoDB", "UpdateVersion");

            while (true)
            {
                Thread.Sleep(2000);
            }
        }

        public class MongoDBQueryListener : MessageListenerConcurrently
        {
            public ConsumeConcurrentlyStatus consumeMessage(java.util.List list, ConsumeConcurrentlyContext ccc)
            {
                for (int i = 0; i < list.size(); i++)
                {
                    var msg = list.get(i) as Message;
                    byte[] body = msg.getBody();
                    var str = Encoding.UTF8.GetString(body);
                    MongodbQueryEntity mongodbQueryEntity = JsonConvert.DeserializeObject<MongodbQueryEntity>(str);
                    mongodbHelper.Add(mongodbQueryEntity.tablename + DateTime.Now.ToString("yyyyMM"), mongodbQueryEntity); ;
                }
                return ConsumeConcurrentlyStatus.CONSUME_SUCCESS;
            }
        }

        public class MongoDBUpdateVersionListener : MessageListenerConcurrently
        {
            public ConsumeConcurrentlyStatus consumeMessage(java.util.List list, ConsumeConcurrentlyContext ccc)
            {
                for (int i = 0; i < list.size(); i++)
                {
                    var msg = list.get(i) as Message;
                    byte[] body = msg.getBody();
                    var str = Encoding.UTF8.GetString(body);
                    MongodbUpdateVersion mongodbUpdateVersion = JsonConvert.DeserializeObject<MongodbUpdateVersion>(str);
                    if (!string.IsNullOrEmpty(mongodbUpdateVersion.item))
                    {
                        mongodbUpdateVersion.item = mongodbUpdateVersion.item.Remove(0, 1);
                        mongodbUpdateVersion.item = mongodbUpdateVersion.item.Remove(mongodbUpdateVersion.item.Length - 1, 1);
                    }
                    mongodbHelper.AddForJson(mongodbUpdateVersion.tablename, mongodbUpdateVersion.item);
                }
                return ConsumeConcurrentlyStatus.CONSUME_SUCCESS;
            }
        }

        class MongodbUpdateVersion
        {
            public string id { get; set; }
            public string tablename { get; set; }

            public string item { get; set; }
        }

        class MongodbQueryEntity
        {
            public string loginid { get; set; }
            public string userid { get; set; }
            public string username { get; set; }
            public string tablename { get; set; }
            public string pagecnt { get; set; }
            public string rownumber { get; set; }
            public string orderby { get; set; }
            public string createtime { get; set; }
            public List<QueryEF> dto { get; set; }
        }

        public class QueryEF
        {
            public string orAnd { get; set; }

            public string symbol { get; set; }

            public string columnname { get; set; }

            public string valuetext { get; set; }

            public List<QueryEF> childef { get; set; }
        }

    }
}
