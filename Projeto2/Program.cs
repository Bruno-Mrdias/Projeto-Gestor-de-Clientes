using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Projeto2
{
    internal class Program
    {
        [System.Serializable]
       struct Client
        {
            public string name;
            public string email;
            public string cpf;
        }

        static List<Client> Clients = new List<Client>();

        enum Menu { Listagem = 1 , Adicionar, Remover , Sair}

        static void Main(string[] args)
        {
            Loading();
            bool escolheuSair = false;

            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de clientes - Bem vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu option = (Menu)intOp;

                switch (option)
                {
                    case Menu.Adicionar:
                        Add();
                        break;
                    case Menu.Remover:
                        Remove();
                        break;
                    case Menu.Sair: 
                        escolheuSair = true;
                        break;
                    case Menu.Listagem:
                        Listt();
                        break;

                }
                Console.Clear();
            }



           
                
        }

        static void Add()
        {
            Client client = new Client();
            Console.WriteLine("Cadastro de Cliente: ");
            Console.WriteLine("Digite o nome do Cliente: ");
            client.name = Console.ReadLine();
            Console.WriteLine("Digite o Email do cliente: ");
            client.email = Console.ReadLine();
            Console.WriteLine("Digite o cpf do cliente: ");
            client.cpf = Console.ReadLine();
            
            Clients.Add(client);
            Save();
            Console.WriteLine("Cadastro realizado com sucesso!, aperte ENTER para sair.");
            Console.ReadLine();
            

        }


        static void Listt()
        {
            if (Clients.Count > 0)
            {
                Console.WriteLine("Lista de Clientes: ");
                int i = 0;
                foreach (Client client in Clients)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome:{client.name} ");
                    Console.WriteLine($"E-mail: {client.email}");
                    Console.WriteLine($"CPF: {client.cpf}");
                    Console.WriteLine("====================");
                    i++;
                    
                }
                
            }

            else 
            {
                Console.WriteLine("Nenhum cliente cadastrado");
            }

            Console.WriteLine("Aperte ENTER para sair!!");
            Console.ReadLine();

        }

        static void Remove()
        {
            Listt();

            if (Clients.Count > 0)
            {
                Console.WriteLine("Digite o ID do cliente no qual quer remover: ");
            }  
                int id = int.Parse(Console.ReadLine());
            


            if (id >= 0 && id < Clients.Count)    
            {
                Clients.RemoveAt(id);
                Save();
                
                
            }

            else
            {
                Console.WriteLine("ID digitado é inválido, tente novamente!!");
                Console.ReadLine();
            }

            


        }



        static void Save()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, Clients);

            stream.Close();
        }

        static void Loading()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);

            try
            {
                
                BinaryFormatter encoder = new BinaryFormatter();

                Clients = (List<Client>)encoder.Deserialize(stream);

                if(Clients == null) 
                {
                    Clients = new List<Client>();
                }
        

                
            }
            catch(Exception) 
            {

                Clients = new List<Client>();

            }
            stream.Close();
        
        }
        

    }
}
