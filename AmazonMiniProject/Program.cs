using System;
using System.Data.SqlClient;

namespace AmazonMiniProject
{
    class Program
    {
        static string ConString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=TestDBB;Integrated Security=True;Pooling=False";
        SqlConnection con = new SqlConnection(ConString);

        public int shopping()
        {
            int product=0;
            Console.WriteLine("Please Enter\n1 for ordering Electronic Devices\n2 for ordering Food");
            int choice = Convert.ToInt32(Console.ReadLine());
            if(choice == 1)
            {
                Console.WriteLine("Please Enter \n101 for Smart Phone\n102 for Laptop\n103 for Earphones");
                product = Convert.ToInt32(Console.ReadLine());
                
            }
            else if (choice == 2)
            {
                Console.WriteLine("Please Enter \n104 for Coffee\n105 for Tea\n106 for Burger");
                product = Convert.ToInt32(Console.ReadLine());
                
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
            return product;
        }
        public int getCustomer_id(string phone)
        {
            string querystring = "Select customer_id from Customers where customer_phone ='" + phone+"'";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int customer_id = Convert.ToInt32(reader[0].ToString());
            con.Close();
            return customer_id;
        }
        public int getProduct_price(int product_id)
        {
            string querystring = "Select product_price from Products where product_id="+product_id+"";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int product_price = Convert.ToInt32(reader[0].ToString());
            con.Close();
            return product_price;
        }
        public string payNow(int price)
        {
            string date_paid;
            Console.WriteLine("Pay now\n Please pay amount {0}", price);
            int paid = Convert.ToInt32(Console.ReadLine());
            if(paid == price)
            {
                date_paid = DateTime.Now.ToString("M/d/yyyy");
                   
            }
            else
            {
                Console.WriteLine("Please pay the full amount");
                date_paid = null;
            }
            return date_paid;
        }
        public string getProductName(int product_id)
        {
            string querystring = "Select product_type_code from Products where product_id=" + product_id + "";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string product_name = reader[0].ToString();
            con.Close();
            return product_name;
        }
        public void placeOrder(int customer_id,string customer_payment_method_id,string order_status_code,string date_order_placed,string date_order_paid,int der_order_price, string other_order_details)
        {
            string querystring = "insert into Customer_Orders (customer_id,customer_payment_method_id,order_status_code,date_order_placed,date_order_paid,der_order_price,other_order_details) values(" + customer_id + ",'" + customer_payment_method_id + "','" + order_status_code + "','" + date_order_placed + "','"+ date_order_paid + "'," + der_order_price + ",'"+ other_order_details + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.Clear();
                Console.WriteLine("Order Placed");
            }
            con.Close();
        }
        public int getOrderId(int customer_id)
        {
            string querystring = "Select order_id from Customer_Orders where customer_id=" + customer_id + "";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int order_id= Convert.ToInt32(reader[0].ToString());
            con.Close();
            return order_id;
        }
        public void orderProduct(int order_id, int product_id,int quantity,string comment)
        {
            string querystring = "insert into Customer_Order_Products values(" + order_id + "," + product_id + "," + quantity + ",'" + comment + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                
                Console.WriteLine("Your order will be delivered soon");
            }
            con.Close();
        }
        public int getAddressId(string city)
        {
            string querystring = "Select address_id from Addresses where city ='" + city + "'";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int address_id = Convert.ToInt32(reader[0].ToString());
            con.Close();
            return address_id;
        }
        public void fillAddress(int customer_id,int type,string city)
        {
            int address_id = getAddressId(city);
           
            string date = DateTime.Now.ToString("M/d/yyyy");
            string dateto = "not delivered";
            string querystring = "insert into Customer_Addresses  values("+customer_id+","+address_id+",'"+date+"',"+type+ ",'" + dateto + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {

                Console.WriteLine("Address Stored");
            }
            con.Close();

        }
        
        public void customerAddress(int customer_id)
        {
            String buildingName, street, area, city, state, country, message;
            int pincode,type;
          
            Console.WriteLine("Please the Address Details");
            Console.WriteLine("Enter type of address Home/Office\n11 for Home \n12 for Office ");
            type = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Building ");
            buildingName = Console.ReadLine();
            Console.WriteLine("Enter Street ");
            street = Console.ReadLine();
            Console.WriteLine("Enter Area or Locality name ");
            area = Console.ReadLine();
            Console.WriteLine("Enter City");
            city = Console.ReadLine();
            Console.WriteLine("Enter Pincode");
            pincode = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter State");
            state = Console.ReadLine();
            Console.WriteLine("Enter Country");
            country = Console.ReadLine();
            Console.WriteLine("Enter Additional Information");
            message = Console.ReadLine();
            string querystring = "insert into Addresses (line_1_number_building,line_2_number_street,line_3_area_locality,city,zip_postcode,state_province_county,iso_country_code,other_address_details) values('" + buildingName + "','" + street + "','" + area + "','" + city + "'," + pincode + ",'" + state + "','" + country + "','" + message + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {

                Console.WriteLine("Address confirmed");
            }
           
            con.Close();
            fillAddress(customer_id, type,city);
            

        }

        
        public void customerDetails()
        {

            try
            {
                string name, email, message, phone;
                Console.WriteLine("Please Enter your name: ");
                name = Console.ReadLine();
                Console.WriteLine("Please Enter your email: ");
                email = Console.ReadLine();
                Console.WriteLine("Please Enter your phone: ");
                phone = Console.ReadLine();
                Console.WriteLine("Please Enter your message: ");
                message = Console.ReadLine();
           
                string querystring = "insert into Customers (customer_name,customer_phone,customer_email,other_customer_details) values('" + name+ "','" + phone + "','" + email + "','" + message + "')";
                con.Open();
                SqlCommand cmd = new SqlCommand(querystring, con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Console.WriteLine("Registration successful");
                }
                con.Close();
                Console.Clear();
                int product_id = shopping();
                Console.WriteLine("Please enter the quantity you want");
                int quantity = Convert.ToInt32(Console.ReadLine());
                int customer_id= getCustomer_id(phone);
                
                int der_order_price = quantity*getProduct_price(product_id);
                string customer_payment_method=null;
                string date_order_paid;
                string date_order_placed = DateTime.Now.ToString("M/d/yyyy");
                string other_order_details = getProductName(product_id);
                string order_status_code = "Order Placed";
                string comment = "Nice product";

                Console.WriteLine($"The Product You selected is {other_order_details} & has price {der_order_price}");
                Console.WriteLine("Please select payment option");
                Console.WriteLine("Please Enter \n1 for Console Payment\n2 for Cash on Delivery");
                int pay = Convert.ToInt32(Console.ReadLine());
                if (pay==1)
                {
                    customer_payment_method = "Online";
                    date_order_paid=payNow(der_order_price);

                }
                else if(pay == 2)
                {
                    customer_payment_method = "COD";
                    date_order_paid = null;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    date_order_paid = null;
                }
                
                Console.WriteLine("Please Enter \n1 for place order\n2 for main menu\n3 for exit");
                int key = Convert.ToInt32(Console.ReadLine());

                switch (key)
                {
                    case 1:
                        Console.Clear();
                       placeOrder(customer_id, customer_payment_method, order_status_code, date_order_placed, date_order_paid, der_order_price, other_order_details);
                        int order_id = getOrderId(customer_id);
                        orderProduct(order_id,product_id,quantity,comment);
                        customerAddress(customer_id);
                        break;
                    case 2:
                        Console.Clear();
                        callAgain();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Thanks for Your Consern");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid input please try again");
                        
                        break;
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        public string getPaymentMethod(int order_id)
        {
            string querystring = "Select customer_payment_method_id from Customer_Orders where order_id=" + order_id + "";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string payment = reader[0].ToString();
            con.Close();
            return payment;
        }
        public int getPayAmount(int order_id)
        {
            string querystring = "Select der_order_price from Customer_Orders where order_id=" + order_id + "";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int amount = Convert.ToInt32(reader[0].ToString());
            con.Close();
            return amount;
        }
        
        public void updateOrderStatus(int order_id)
        {
            string querystring = "update Customer_Orders SET order_status_code = 'Delivered' where order_id="+order_id+"; ";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Order status updated");
            }
            con.Close();
        }
        
        public void bill(int order_id)
        {
            string querystring = "Select * from Customer_Orders where order_id ="+order_id+"";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("----------------------------BILL-------------------------"+"\n"+"Order Id:" + reader[0].ToString() + " \n" + "Customer Id:" + reader[1].ToString() + " \n" + "Payment Method:" + reader[2].ToString() + " \n" + "Order Status:" + reader[3].ToString() + " \n" + "Date Ordered:" + reader[4].ToString() + " \n" + "Date amout Paid:" + reader[5].ToString() + " \n" + "Total Price:" + reader[6].ToString() + " \n" + "Product Name:" + reader[7].ToString()+"\n"+ "----------------------------BILL-------------------------");
            }
            con.Close();
        }
        public void orderConfirmed(int order_id)
        {
            string date = DateTime.Now.ToString("M/d/yyyy");
            string status = "Delivered";
            string querystring = "insert into Customer_Orders_Delivery values(" + order_id + ",'" + date + "','" + status + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(querystring, con);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Order Delivered");
            }
            con.Close();
            updateOrderStatus(order_id);
            bill(order_id);

        }
        public void deliverySystem()
        {
            try
            {
                Console.WriteLine("Enter the order id for order delivery");
                int order_id = Convert.ToInt32(Console.ReadLine());
                string payment =getPaymentMethod(order_id);
                if (payment =="Online")
                {
                    Console.WriteLine("Payment has done Online please deliver the product");
                    orderConfirmed(order_id);
                }
                else
                {
                    int cash= getPayAmount(order_id);
                    Console.WriteLine($"Please Collect the Cash Amount : {cash} from the customer");
                    Console.WriteLine("Enter the amount customer paid");
                    int paid = Convert.ToInt32(Console.ReadLine());
                    if(paid == cash)
                    {
                        Console.WriteLine("Deliver the Product");
                        orderConfirmed(order_id);

                    }
                    else
                    {
                        Console.WriteLine($"Please pay {cash}");
                        deliverySystem();
                    }
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public void callAgain()
        {
            Main(new string[0]);
        }
        static void Main(string[] args)
        {
            var p = new Program();
            
             Console.WriteLine("Welcome to Console Shopping\n");
             Console.WriteLine("Please Enter\n1 for Shopping \n2 for Delivary \n0 for Exit");
             int key = Convert.ToInt32(Console.ReadLine());

             switch (key)
             {
                 case 1:
                     Console.Clear();
                     p.customerDetails();
                     break;
                 case 2:
                     Console.Clear();
                    p.deliverySystem();
                     break;
                 case 0:
                     Console.Clear();
                     Console.WriteLine("Thanks for Your Consern");
                     break;
                 default:
                     Console.Clear();
                     Console.WriteLine("Invalid input please try again");
                     p.callAgain();
                     break;
             }
           
            


        }
    }
}
