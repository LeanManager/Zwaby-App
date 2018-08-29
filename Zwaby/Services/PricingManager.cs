using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Zwaby.Models;

namespace Zwaby.Services
{
    public class PricingManager
    {
        private string Url = "https://zwaby.azurewebsites.net/api/tca";

        public async Task<List<string>> GeneratePriceAndDuration(string bedrooms, string bathrooms, string residence, string serviceType)
        { 
            PricingVariables variables = new PricingVariables()
            {
                Bedrooms = bedrooms,
                Bathrooms = bathrooms,
                ResidenceType = residence,
                ServiceType = serviceType
            };

            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            // Use the PostAsync method against the base URL to add the book.
            // Create the HttpContent from the JSON string by creating a new StringContent object, 
            // use the constructor which also takes an encoding and media type.
            response = await client.PostAsync(Url, new StringContent(JsonConvert.SerializeObject(variables),
                                                                     Encoding.UTF8, "application/json"));
            
            return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());

        }

        public double CalculatePrice(string bedrooms, string bathrooms, string residenceType, string serviceType)
        {
            double price;

            if (bedrooms.ToString() == "1" && bathrooms.ToString() == "1")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 75.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 150.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 90.0;
                }
                else
                {
                    price = 180.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "2" && bathrooms.ToString() == "1")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 85.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 170.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 100.0;
                }
                else
                {
                    price = 200.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "2" && bathrooms.ToString() == "2")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 95.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 190.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 110.0;
                }
                else
                {
                    price = 220.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "3" && bathrooms.ToString() == "1")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 95.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 190.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 110.0;
                }
                else
                {
                    price = 220.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "3" && bathrooms.ToString() == "2")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 105.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 210.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 120.0;
                }
                else
                {
                    price = 240.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "3" && bathrooms.ToString() == "3")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 115.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 230.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 130.0;
                }
                else
                {
                    price = 260.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "4" && bathrooms.ToString() == "1")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 105.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 210.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 120.0;
                }
                else
                {
                    price = 240.0;
                }
                return price;
            }
            else if (bedrooms.ToString() =="4" && bathrooms.ToString() == "2")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 115.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 230.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 130.0;
                }
                else
                {
                    price = 260.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "4" && bathrooms.ToString() == "3")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 125.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 250.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 140.0;
                }
                else
                {
                    price = 280.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "4" && bathrooms.ToString() == "4")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 135.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 270.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 150.0;
                }
                else
                {
                    price = 300.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "5" && bathrooms.ToString() == "1")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 115.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 230.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 130.0;
                }
                else
                {
                    price = 260.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "5" && bathrooms.ToString() == "2")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 125.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 250.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 140.0;
                }
                else
                {
                    price = 280.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "5" && bathrooms.ToString() == "3")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 135.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 270.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 150.0;
                }
                else
                {
                    price = 300.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "5" && bathrooms.ToString() == "4")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 145.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 290.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 160.0;
                }
                else
                {
                    price = 320.0;
                }
                return price;
            }
            else if (bedrooms.ToString() == "5" && bathrooms.ToString() == "5")
            {
                if (serviceType == "General Cleaning" && residenceType == "Apartment")
                {
                    price = 155.0;
                }
                else if (serviceType == "Deep Cleaning" && residenceType == "Apartment")
                {
                    price = 310.0;
                }
                else if (serviceType == "General Cleaning" && residenceType == "House")
                {
                    price = 170.0;
                }
                else
                {
                    price = 340.0;
                }
                return price;
            }
            // Never gets hit
            else
            {
                return price = 120.0;
            }
        }

        public double GetDuration(string bedrooms, string bathrooms, string residenceType, string serviceType)
        {
            // 40 min per bedroom, 40 min per bathroom (average; use live data to converge on truth)
            // Always 2 cleaners at a time

            double baseDuration = (Double.Parse(bedrooms) * 40) + (Double.Parse(bathrooms) * 40);

            double residenceFactor;
            double serviceTypeFactor;

            if (residenceType == "House")
            {
                residenceFactor = 1.1;
            }
            else // Apartment
            {
                residenceFactor = 1.0;
            }

            if (serviceType == "General Cleaning")
            {
                serviceTypeFactor = 1.0;
            }
            else // Deep
            {
                serviceTypeFactor = 2.0;
            }

            double totalTime = baseDuration * residenceFactor * serviceTypeFactor;

            totalTime = totalTime / 60;

            return totalTime;
        }
    }
}
