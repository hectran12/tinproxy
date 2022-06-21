using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;
namespace tinproxy
{
    public class tinproxy_
    {

        // get and set
        /**
        
        status	string	active	Trạng thái Proxy:
        - active: Hoạt động
        - expired: Hết hạn
        - delete: Xoá
        - error: Lỗi kết nối
        - waiting: Đang cập nhật Proxy
        public_ip	string	116.109.193.35	Public IPv4 của proxy
        public_ip_ipv6	string	2402:800:6376:60ef:81ea:ea43:4583:e542	Public IPv6 của proxy
        http_ipv4	number	116.109.193.35:4007	Thông tin proxy IPv4
        http_ipv6	number	116.109.193.35:4207	Thông tin proxy IPv6
        socks_ipv4	string	116.109.193.35:4107	Thông tin socks IPv4
        http_ipv6_ipv4	string	116.109.193.35:4307	Thông tin http proxy IPv6 -> v4
        expired_at	string	08-04-2022 11:04:01	Public port IPv6 của proxy
        timeout	number	1467 (giá trị = 0 có nghĩa là không giới hạn)	Thời gian sử dụng Proxy
        next_request	number	33	Thời gian đếm ngược đến lần refresh tiếp theo
        ip_allow	string	116.111.123.45	Danh sách IP được phép truy cập proxy
        authentication	object	{ "username": "a2MGrGNi", "password": "haI1z66f" }	Thông tin tài khoảng xác thực (có thể dùng thay thế cho ip_allow)
        - username: tài khoản
        - password: mật khẩu
        
        **/
        
        public string token { get; set;  }
        public string[] allow_ip { get; set; }
        public Data data = new Data();
        public Root root = new Root();
        
        public JObject json { get; set; }
        public class Authentication
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class Data
        {
            public string http_ipv4 { get; set; }
            public string http_ipv6 { get; set; }
            public string socks_ipv4 { get; set; }
            public string http_ipv6_ipv4 { get; set; }
            public string public_ip { get; set; }
            public string public_ip_ipv6 { get; set; }
            public string expired_at { get; set; }
            public int timeout { get; set; }
            public int next_request { get; set; }
            public Authentication authentication = new Authentication();
            public List<string> ip_allow { get; set; }
            public string your_ip { get; set; }
        }

        public class Root
        {
            public int code { get; set; }
            public string message { get; set; }
            public string status { get; set; }
            public Data data { get; set; }
        }


        // rq http
        public HttpRequest rq_ = new HttpRequest();

        // url
        
        const string Basic_Url = "https://tinproxy.com/api/proxy/";
        const string GetNew_Proxy = Basic_Url + "/get-new-proxy";
        const string GetCurrent_Proxy = Basic_Url + "/get-current-proxy";

        // get url
        public string getUrl (int number)
        {
            string auth = allow_ip != null ? "?api_key=" + token + "&authen_ips=" + string.Join(",", allow_ip) : "?api_key=" + token;
            return number == 1 ? GetNew_Proxy + auth : GetCurrent_Proxy + auth;
        }


        // to get
        public bool getNew_Proxy()
        {
            try
            {
                string content = rq_.Get(getUrl(1)).ToString();
                json = JObject.Parse(content);
                if((string)json["message"] == "Lấy Proxy thành công")
                {
                    if (update_json())
                    {
                       
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                  
                }
            } catch
            {
                return false;
            }
        }

        public bool getCurrent_Proxy()
        {
            try
            {
                string content = rq_.Get(getUrl(2)).ToString();
                json = JObject.Parse(content);
                if ((string)json["message"] == "Lấy Proxy thành công")
                {
                    if (update_json())
                    {
                      
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool update_json()
        {
            try
            {
                data.http_ipv4 = (string)json["data"]["http_ipv4"];
                data.http_ipv6 = (string)json["data"]["http_ipv6"];
                data.socks_ipv4 = (string)json["data"]["socks_ipv4"];
                data.http_ipv6_ipv4 = (string)json["data"]["http_ipv6_ipv4"];
                data.public_ip = (string)json["data"]["public_ip"];
                data.public_ip_ipv6 = (string)json["data"]["public_ip_ipv6"];
                data.expired_at = (string)json["data"]["expired_at"];
                data.timeout = (int)json["data"]["timeout"];
                data.next_request = (int)json["data"]["next_request"];
                List<string> listipallow = new List<string>();
                foreach (string item in json["data"]["ip_allow"])
                {
                    listipallow.Add(item);
                }
                data.ip_allow = listipallow;
                data.authentication.username = (string)json["data"]["authentication"]["username"];
                data.authentication.password = (string)json["data"]["authentication"]["password"];
                data.your_ip = (string)json["data"]["your_ip"];
                return true;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
