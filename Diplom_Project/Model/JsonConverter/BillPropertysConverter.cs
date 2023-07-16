//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace Diplom_Project
//{
//    public class BillPropertysConverter : JsonConverter<List<IBillPropertys>>
//    {
//        public override List<IBillPropertys> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//        {
//            var billPropertysList = new List<IBillPropertys>();

//            while (reader.Read())
//            {
//                if (reader.TokenType == JsonTokenType.StartObject)
//                {
//                    var billPropertys = ReadBillPropertys(ref reader);
//                    billPropertysList.Add(billPropertys);
//                }
//                else if (reader.TokenType == JsonTokenType.EndArray)
//                {
//                    break;
//                }
//            }

//            return billPropertysList;
//        }

//        private IBillPropertys ReadBillPropertys(ref Utf8JsonReader reader)
//        {
//            var billPropertys = new DefaultBillPropertys();

//            while (reader.Read())
//            {
//                if (reader.TokenType == JsonTokenType.PropertyName)
//                {
//                    var propertyName = reader.GetString();

//                    if (propertyName!.Equals("AmountPaid", StringComparison.OrdinalIgnoreCase))
//                    {
//                        reader.Read();
//                        billPropertys.AmountPaid = reader.GetString()!;
//                    }
//                    else if (propertyName.Equals("Member", StringComparison.OrdinalIgnoreCase))
//                    {
//                        reader.Read();
//                        billPropertys.Member = reader.GetString()!;
//                    }
//                }
//                else if (reader.TokenType == JsonTokenType.EndObject)
//                {
//                    break;
//                }
//            }

//            return billPropertys;
//        }

//        public override void Write(Utf8JsonWriter writer, List<IBillPropertys> value, JsonSerializerOptions options)
//        {
//            writer.WriteStartArray();

//            foreach (var billPropertys in value)
//            {
//                writer.WriteStartObject();
//                writer.WriteString("AmountPaid", billPropertys.AmountPaid);
//                writer.WriteString("Member", billPropertys.Member);
//                writer.WriteEndObject();
//            }

//            writer.WriteEndArray();
//        }
//    }
//}
