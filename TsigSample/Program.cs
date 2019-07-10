using RestSharp;
using System;
using System.Diagnostics;

namespace TsigSample
{
    class Program
    {
        const string _idGenServer = "http://localhost:8111/";
        const string _idApiSegment = "tsig/gen/id";
        static RestClient _client = new RestClient(_idGenServer);
        const int _retryNumber = 3;
        static RestRequest _request = new RestRequest(_idApiSegment, dataFormat: DataFormat.None)
        {
            Timeout = 2000
        };

        static void Main(string[] args)
        {
            _request.AddHeader("ApiKey", "4E1B875F-E091-4510-81DD-9334FBE98FDC");

            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss fff"));
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 1; i <= 10_000; i++)
            {
                //Console.WriteLine("Creating request...");

                Exception error = null;
                var retryCount = _retryNumber;
                while (retryCount > 0)
                {
                    if (retryCount != _retryNumber)
                    {
                        Console.WriteLine("Retrying...");
                    }

                    try
                    {
                        Stopwatch stopwatch2 = Stopwatch.StartNew();
                        // execute the request
                        //var request = new RestRequest(_idApiSegment)
                        //{
                        //    Timeout = 2000
                        //};
                        var response = _client.Get(_request);
                        //var response = _client.ExecuteGetTaskAsync(_request).Result;
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            long.TryParse(response.Content, out long id);

                            Console.WriteLine($"{id} - {stopwatch2.Elapsed.TotalMilliseconds}");


                            break;
                        }
                        else
                        {
                            throw new Exception($"Has problem: {_idGenServer}{_idApiSegment} - StatusCode({response?.StatusCode}) > {response?.ErrorMessage}");
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex;
                        retryCount--;
                    }
                }

                if (retryCount <= 0 && error != null)
                {
                    Console.WriteLine(error.Message ?? "[NO MESSAGE]");
                }
            }

            Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss fff"));
            Console.ReadKey();
        }
    }
}
