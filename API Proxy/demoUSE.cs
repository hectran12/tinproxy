using System;
using System.Text;
using tinproxy;
class Program
{
    static void Main (string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        tinproxy_ tin = new tinproxy_();
        tin.token = "colammoicoankhonglammadoicoanthiandaubuoianhihi";

        string[] listip_allow = new string[1]
        {
            "112.94.1.132" // đã pha ke ip
        };

        tin.allow_ip = listip_allow;

        if (tin.getNew_Proxy())
        {
            goto PRINT_INFO_JSON;
        } else
        {
            if (tin.getCurrent_Proxy())
            {
                goto PRINT_INFO_JSON;
            } else
            {
                
                Console.WriteLine("Không có proxy để lấy!");
                Environment.Exit(0);
            }
        }
        

        PRINT_INFO_JSON:
            Console.WriteLine("Http Ipv6 = " + tin.data.http_ipv6);
            Console.WriteLine("Socks Ipv4 = " + tin.data.socks_ipv4);
            Console.WriteLine("Http Ipv6 Ipv4 = " + tin.data.http_ipv6_ipv4);
            Console.WriteLine("Public Ip = " + tin.data.public_ip);
            Console.WriteLine("Public Ip Ipv6 = " + tin.data.public_ip_ipv6);
            Console.WriteLine("Expired At = " + tin.data.expired_at);
            Console.WriteLine("Timeout = " + tin.data.timeout);
            Console.WriteLine("Next Request = " + tin.data.next_request);
            Console.WriteLine("Ip được sử dụng: ");
            foreach (string ip in tin.data.ip_allow)
            {
                Console.WriteLine(ip);
            }
            Console.WriteLine("Authentication:");
            Console.WriteLine("Username = " + tin.data.authentication.username);
            Console.WriteLine("Username = " + tin.data.authentication.password);
            Console.WriteLine("Your Ip = " + tin.data.your_ip);
      
    }
}
