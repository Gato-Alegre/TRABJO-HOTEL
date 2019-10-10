using System;
using System.Data.SqlClient;

namespace HOTEL
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=GATO_ALEGRE\\SQLEXPRESS;Initial Catalog=HOTEL;Integrated Security=True");
        static bool salir = true;
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("     Bienvenido al hotel");
                Console.WriteLine("*****************************");
                Console.WriteLine("1- Registrar Clientes");
                Console.WriteLine("2- Editar cliente");
                Console.WriteLine("3- Check-in");
                Console.WriteLine("4- Check-Out");
                Console.WriteLine("5- Ver habitaciones");
                Console.WriteLine("6- Modificar BBDD (en la siguiente actualizacion)");
                Console.WriteLine("7- Salir");
                Console.WriteLine("*****************************");

                Console.WriteLine("Selecione opciones");
                string opcion = Console.ReadLine().ToLower();

                if (opcion.Contains("sal") || opcion.Contains("7"))
                {
                    salir = true;
                }
                else
                {
                    Menu(opcion);
                }

            } while (!salir);



        }
        public static bool Menu(string input)
        {

            if (input.Contains("regi") || input.Contains("1"))
            {
                Console.WriteLine("**ESTA EN EL REGSITRO**");
                RegistrarCliente();

            }
            else if (input.Contains("edit") || input.Contains("2"))
            {
                Console.WriteLine("**ESTAS EN EL EDITOR DE USUARIO**");
                EditarCliente();
            }
            else if (input.Contains("in") || input.Contains("3"))
            {
                Console.WriteLine("**ESTAS EN EL CHECK-IN**");
                CheckIn();

            }
            else if (input.Contains("out") || input.Contains("4"))
            {
                Console.WriteLine("**ESTAS EN EL CHEC-OUT**");
                CheckOut();

            }
            else if (input.Contains("habi") || input.Contains("5"))
            {
                Console.Clear();
                VerHabitacion();
            }
            //else if (input.Contains("modi") || input.Contains("6"))
            //{
            //    Console.Clear();
            //    ModificarBase();
            //}
            else
            {
                Console.WriteLine("No te entiendo vulve a introducir la opcion");
                Console.ReadKey();
                Console.Clear();
            }
            return salir = false;

        }
        public static void RegistrarCliente()
        {
            Console.WriteLine("Introduza el nombre");
            string nombre = Console.ReadLine();
            Console.WriteLine("Introduza el apellido");
            string apellido = Console.ReadLine();
            Console.WriteLine("Introduza el DNI");
            string dni = Console.ReadLine();

            string query = $"INSERT INTO CLIENTES(DNI,APELLIDO,NOMBRE) VALUES('{dni}','{apellido}','{nombre}')";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            if (command.ExecuteNonQuery() > 0)
            {
                connection.Close();
                Console.WriteLine($"Se ha registrado correctamente");
            }
            else
            {
                Console.WriteLine($"ERROR");
            }
            connection.Close();
            Console.ReadKey();
            Console.Clear();
        }
        public static void EditarCliente()
        {
            bool exit;
            Console.WriteLine("Que usuario quieres cambiar, introduzca el DNI");
            do
            {
                exit = false;
                string dni = Console.ReadLine();
                string query = $"SELECT * FROM CLIENTES WHERE DNI LIKE '{dni}'";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    connection.Close();
                    bool salirCambio;
                    do
                    {
                        salirCambio = false;
                        Console.WriteLine("Que desea cambiar nombre , apellido o nombre y apellido ");
                        string opcionCambio = Console.ReadLine();

                        if (opcionCambio.Contains("y"))
                        {
                            Console.WriteLine("Introduzca el nuevo nombre");
                            string nombre = Console.ReadLine();
                            Console.WriteLine("Introduzca el nuevo apellido");
                            string apellido = Console.ReadLine();
                            query = $"Update CLIENTES SET APELLIDO ='{apellido}', NOMBRE ='{nombre}' WHERE DNI = '{dni}'";
                            command = new SqlCommand(query, connection);
                            connection.Open();
                            if (command.ExecuteNonQuery() > 0)
                            {
                                connection.Close();
                                query = $"SELECT * FROM CLIENTES WHERE APELLIDO LIKE '{apellido}'";
                                command = new SqlCommand(query, connection);
                                connection.Open();
                                SqlDataReader readerCambio = command.ExecuteReader();
                                while (readerCambio.Read())// miestras el reader lee, o sea miestra haya registros si quisieramos ver solo uno se podria con if
                                {
                                    Console.WriteLine($"{readerCambio[0].ToString()} {readerCambio[1].ToString()} {readerCambio[2].ToString()} {readerCambio[3].ToString()}");
                                }
                                connection.Close();
                            }

                            salirCambio = true;
                        }
                        else if (opcionCambio.Contains("nom"))
                        {
                            Console.WriteLine("Intruduza el nuevo nombre");
                            string nombre = Console.ReadLine();
                            query = $"Update CLIENTES SET NOMBRE ='{nombre}' WHERE DNI = '{dni}'";
                            command = new SqlCommand(query, connection);
                            connection.Open();
                            if (command.ExecuteNonQuery() > 0)
                            {
                                connection.Close();
                                query = $"SELECT * FROM CLIENTES WHERE NOMBRE LIKE '{nombre}'";
                                command = new SqlCommand(query, connection);
                                connection.Open();
                                SqlDataReader readerCambio = command.ExecuteReader();
                                while (readerCambio.Read())// miestras el reader lee, o sea miestra haya registros si quisieramos ver solo uno se podria con if
                                {
                                    Console.WriteLine($"{readerCambio[0].ToString()} {readerCambio[1].ToString()} {readerCambio[2].ToString()} {readerCambio[3].ToString()}");
                                }
                                connection.Close();
                            }

                            salirCambio = true;
                        }
                        else if (opcionCambio.Contains("apell"))
                        {
                            Console.WriteLine("Introduzca el nuevo apellido");
                            string apellido = Console.ReadLine();
                            query = $"Update CLIENTES SET APELLIDO ='{apellido}' WHERE DNI = '{dni}'";
                            command = new SqlCommand(query, connection);
                            connection.Open();
                            if (command.ExecuteNonQuery() > 0)
                            {
                                connection.Close();
                                query = $"SELECT * FROM CLIENTES WHERE APELLIDO LIKE '{apellido}'";
                                command = new SqlCommand(query, connection);
                                connection.Open();
                                SqlDataReader readerCambio = command.ExecuteReader();
                                while (readerCambio.Read())// miestras el reader lee, o sea miestra haya registros si quisieramos ver solo uno se podria con if
                                {
                                    Console.WriteLine($"{readerCambio[0].ToString()} {readerCambio[1].ToString()} {readerCambio[2].ToString()} {readerCambio[3].ToString()}");
                                }
                                connection.Close();
                            }

                            salirCambio = true;
                        }
                        else
                        {
                            Console.WriteLine("no te entinedo");
                        }


                    } while (!salirCambio);

                    exit = true;
                }
                else
                {
                    Console.WriteLine("cliente no existente, debe registrasrse antes gracia");

                    do
                    {
                        exit = false;
                        Console.WriteLine("¿Desea registrarse?");
                        string siNo = Console.ReadLine().ToLower();
                        if (siNo.Contains("si"))
                        {
                            connection.Close();
                            RegistrarCliente();
                            exit = true;
                        }
                        else if (siNo.Contains("no"))
                        {
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("¿Perdon? no te entinedo");
                        }

                    } while (!exit);
                    connection.Close();
                }


            } while (!exit);
            Console.ReadKey();
            Console.Clear();
        }
        public static void CheckIn()
        {
            Console.WriteLine("Introduzca el DNI para hacer la reserva");
            string dni = Console.ReadLine().ToLower();
            string query = $"SELECT ID FROM CLIENTES WHERE DNI LIKE '{dni}'";
            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())//aqui miramos si el usuario esta registrado
            {
                int clienteId = Convert.ToInt32(reader[0]);
                connection.Close();
                query = $"SELECT * FROM HABITACIONES";
                command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader readerCambio = command.ExecuteReader();
                Console.WriteLine($"HABITACIONES/ESTADO");
                while (readerCambio.Read())// miestras el reader lee, o sea miestra haya registros si quisieramos ver solo uno se podria con if
                {
                    Console.WriteLine($"    {readerCambio[0].ToString()}        {readerCambio[1].ToString()}\n");
                }
                connection.Close();
                int numeroHanitacion;
                bool exit;
                do
                {
                    exit = false;
                    Console.WriteLine("Que habitacion desea reservar");
                    numeroHanitacion = Convert.ToInt32(Console.ReadLine()); //solucuinar al meter un valor que no sea int solucionar con un metodo
                    if (numeroHanitacion > 0 && numeroHanitacion <= 8)
                    {

                        Console.WriteLine("¿Estas seguro que quieres esa habitacion?");
                        string confirmar = Console.ReadLine().ToLower();

                        if (confirmar.Contains("si"))
                        {
                            exit = true;
                        }
                        else if (confirmar.Contains("no"))
                        {
                            exit = false;
                        }
                        else
                        {
                            Console.WriteLine("No te entiendo, vuelva a introducir");
                            Console.WriteLine("************************************");
                        }

                    }
                    else
                    {
                        Console.WriteLine("No tenemos tantas habitaciones");
                        Console.WriteLine("Vuelva a introducir otra habitacion");
                        Console.WriteLine("************************************\n");

                    }


                } while (!exit);

                query = $"UPDATE HABITACIONES SET ESTADO = 'OCUPADO' WHERE HabitacionID = {numeroHanitacion}";
                string queryFechaEntrada = $"INSERT INTO RESERVAS (FechaCheckin,ClienteID, HabitacionID) VALUES ('{DateTime.Now}',{clienteId},'{numeroHanitacion}')";
                SqlCommand commandCambio = new SqlCommand(query, connection);
                SqlCommand commandFechaEntrada = new SqlCommand(queryFechaEntrada, connection);
                connection.Open();

                if (commandCambio.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine($"La habitacion {numeroHanitacion} ha sido reservada");
                }
                else
                {
                    Console.WriteLine("ERROR");
                }

                if (commandFechaEntrada.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine($"Se ha regsitrado el:{DateTime.Now}");
                }
                else
                {
                    Console.WriteLine($"ERROR");
                }


            }//aqui miramos si el usuario esta registrado
            else//si no esta registrado
            {
                Console.WriteLine("cliente no existente, debe registrasrse antes gracia");
                bool exit;
                do
                {
                    exit = false;
                    Console.WriteLine("¿Desea registrarse?");
                    string siNo = Console.ReadLine().ToLower();
                    if (siNo.Contains("si"))
                    {
                        connection.Close();
                        RegistrarCliente();
                        exit = true;
                    }
                    else if (siNo.Contains("no"))
                    {
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine("¿Perdon? no te entinedo");
                    }

                } while (!exit);
            }//si no esta registrado

            connection.Close();
            Console.ReadKey();
            Console.Clear();
        }
        public static void CheckOut()
        {
            bool exit;
            Console.WriteLine("introduzca el DNI para fichar salida");
            do
            {
                exit = false;
                string dni = Console.ReadLine();
                string query = $"SELECT ID FROM CLIENTES WHERE DNI LIKE '{dni}'";// mirar esto en casa
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())//si me lee el cliente id pasa y me mete la id del cliente en la variable
                {
                    int clienteID = Convert.ToInt32(reader[0]);
                    connection.Close();
                    string queryHabitacion = $"SELECT HabitacionID FROM RESERVAS WHERE ClienteID = '{clienteID}'";
                    SqlCommand commandHabitacion = new SqlCommand(queryHabitacion, connection);
                    connection.Open();
                    SqlDataReader readerHabitacion = commandHabitacion.ExecuteReader();
                    if (readerHabitacion.Read())//si me lee el ID de la habitacion pasa y me mete la id de la habitacion en la variable
                    {
                        int habitacionId = Convert.ToInt32(readerHabitacion[0]);//importante para dar variable no puede estar delante del readerHabitacion
                        connection.Close();
                        string queryNombre = $"SELECT NOMBRE FROM CLIENTES WHERE DNI LIKE '{dni}'";
                        SqlCommand commandNombre = new SqlCommand(queryNombre, connection);
                        connection.Open();
                        SqlDataReader readerNombre = commandNombre.ExecuteReader();
                        if (readerNombre.Read())//si me lee  el nombre del cliente me mete la el nombre  en la variable
                        {
                            string nombre = readerNombre[0].ToString();
                            connection.Close();
                            query = $"UPDATE RESERVAS SET FechaCheckOut = '{DateTime.Now}' WHERE ClienteID = {clienteID}";
                            string queryLibre = $"UPDATE HABITACIONES SET ESTADO = 'LIBRE' WHERE HabitacionID LIKE '{habitacionId}'";
                            SqlCommand commandLibre = new SqlCommand(queryLibre, connection);
                            command = new SqlCommand(query, connection);
                            connection.Open();

                            if (command.ExecuteNonQuery() > 0)
                            {
                                if (commandLibre.ExecuteNonQuery() > 0)
                                {
                                    Console.WriteLine($"Gracias por su visita {nombre}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR");
                            }
                        }
                        connection.Close();

                    }
                    exit = true;

                }
                else
                {
                    Console.WriteLine("cliente no existente, debe registrasrse antes gracia");

                    do
                    {
                        exit = false;
                        Console.WriteLine("¿Desea registrarse?");
                        string siNo = Console.ReadLine().ToLower();
                        if (siNo.Contains("si"))
                        {
                            RegistrarCliente();
                            exit = true;
                        }
                        else if (siNo.Contains("no"))
                        {
                            exit = true;
                        }
                        else
                        {
                            Console.WriteLine("¿Perdon? no te entinedo");
                        }

                    } while (!exit);
                    connection.Close();
                }


            } while (!exit);
            Console.ReadKey();
            Console.Clear();

        }
        public static void VerHabitacion()
        {
            do
            {
                Console.WriteLine("**ESTAS EN VER HABITACIONES**");
                Console.WriteLine("\n*****************************");
                Console.WriteLine("1- Ver todas las habitaciones");
                Console.WriteLine("2- Ver habitaciones ocupadad");
                Console.WriteLine("3- Ver habitaciones libres");
                Console.WriteLine("4- Salir");
                Console.WriteLine("*****************************");

                Console.WriteLine("Selecione opciones");
                string opcion = Console.ReadLine().ToLower();

                if (opcion.Contains("tod") || opcion.Contains("1"))
                {
                    TodasHabitaciones();
                    salir = false;
                }
                else if (opcion.Contains("ocupa") || opcion.Contains("2"))
                {
                    OcupadoHabitacion();
                    salir = false;
                }
                else if (opcion.Contains("libr") || opcion.Contains("3"))
                {
                    LibreHabitacion();
                    salir = false;
                }
                else if (opcion.Contains("sal") || opcion.Contains("4"))
                {
                    salir = true;
                }
                else
                {
                    Console.WriteLine("No te entiendo vulve a introducir la opcion");
                    Console.ReadKey();
                    Console.Clear();
                    salir = false;
                }

            } while (!salir);

            Console.Clear();
        }
        public static void TodasHabitaciones()
        {
            for (int i = 1; i <= 8; i++)
            {

                int habitacionId = i;
                string queryClienteId = $"SELECT ClienteID FROM RESERVAS WHERE HabitacionID = '{habitacionId}'";
                SqlCommand commandClienteId = new SqlCommand(queryClienteId, connection);
                connection.Open();
                SqlDataReader readerClienteId = commandClienteId.ExecuteReader();
                if (readerClienteId.Read())
                {
                    int clienteId = Convert.ToInt32(readerClienteId[0]);
                    connection.Close();
                    String queryNombre = $"SELECT NOMBRE FROM CLIENTES WHERE ID = '{clienteId}'";
                    SqlCommand commandNombre = new SqlCommand(queryNombre, connection);
                    connection.Open();
                    SqlDataReader readerNombre = commandNombre.ExecuteReader();

                    if (readerNombre.Read())
                    {
                        string nombre = readerNombre[0].ToString();
                        connection.Close();
                        string queryEstado = $"SELECT ESTADO FROM HABITACIONES WHERE HabitacionID = '{habitacionId}'";
                        SqlCommand commandEstado = new SqlCommand(queryEstado, connection);
                        connection.Open();
                        SqlDataReader readerEstado = commandEstado.ExecuteReader();
                        if (readerEstado.Read())
                        {
                            string estado = readerEstado[0].ToString();
                            Console.WriteLine($"{habitacionId} - {nombre} - {estado}");
                        }
                        connection.Close();

                    }
                    connection.Close();

                }
                else//si no detecta clientes
                {
                    connection.Close();
                    string queryEstado = $"SELECT ESTADO FROM HABITACIONES WHERE HabitacionID = '{habitacionId}'";
                    SqlCommand commandEstado = new SqlCommand(queryEstado, connection);
                    connection.Open();
                    SqlDataReader readerEstado = commandEstado.ExecuteReader();
                    if (readerEstado.Read())
                    {
                        string estado = readerEstado[0].ToString();
                        Console.WriteLine($"{habitacionId} - NULL - {estado}");
                    }
                    connection.Close();
                }

            }

            Console.ReadKey();
            Console.Clear();
        }
        public static void OcupadoHabitacion()
        {
            for (int i = 1; i < 8; i++)
            {

                int habitacionId = i;
                string queryClienteId = $"SELECT ClienteID FROM RESERVAS WHERE HabitacionID = '{habitacionId}'";
                SqlCommand commandClienteId = new SqlCommand(queryClienteId, connection);
                connection.Open();
                SqlDataReader readerClienteId = commandClienteId.ExecuteReader();
                if (readerClienteId.Read())
                {
                    int clienteId = Convert.ToInt32(readerClienteId[0]);
                    connection.Close();
                    string queryEstado = $"SELECT ESTADO FROM HABITACIONES WHERE HabitacionID = '{habitacionId}'";
                    SqlCommand commandEstado = new SqlCommand(queryEstado, connection);
                    connection.Open();
                    SqlDataReader readerEstado = commandEstado.ExecuteReader();
                    if (readerEstado.Read())
                    {
                        string estado = readerEstado[0].ToString();
                        if (estado.Contains("OCUPADO"))
                        {
                            connection.Close();
                            String queryNombre = $"SELECT NOMBRE FROM CLIENTES WHERE ID = '{clienteId}'";
                            SqlCommand commandNombre = new SqlCommand(queryNombre, connection);
                            connection.Open();
                            SqlDataReader readerNombre = commandNombre.ExecuteReader();

                            if (readerNombre.Read())
                            {
                                string nombre = readerNombre[0].ToString();
                                connection.Close();
                                Console.WriteLine($"{habitacionId} - {nombre} - {estado}");
                            }
                        }
                        connection.Close();
                    }

                    connection.Close();
                }
                connection.Close();

            }
            Console.ReadKey();
            Console.Clear();

        }
        public static void LibreHabitacion()
        {
            for (int i = 1; i <= 8; i++)
            {
                int habitacionId = i;

                string queryEstado = $"SELECT ESTADO FROM HABITACIONES WHERE HabitacionID = '{habitacionId}'";
                SqlCommand commandEstado = new SqlCommand(queryEstado, connection);
                connection.Open();
                SqlDataReader readerEstado = commandEstado.ExecuteReader();
                if (readerEstado.Read())
                {
                    string estado = readerEstado[0].ToString();
                    connection.Close();
                    if (estado.Contains("LIBRE"))
                    {
                        Console.WriteLine($"{habitacionId} - NULL - {estado}");
                    }

                }
                connection.Close();
            }
            Console.ReadKey();
            Console.Clear();


        }
        //public static void ModificarBase()
        //{
        //    do
        //    {

        //        Console.WriteLine("  **ESTAS EN MODIFICAR BBDD**");
        //        Console.WriteLine("\n*****************************");
        //        Console.WriteLine("1- Eliminar clientes");
        //        Console.WriteLine("2- Eliminar reservas");
        //        Console.WriteLine("3- Salir");
        //        Console.WriteLine("*****************************");

        //        Console.WriteLine("Selecione opciones");
        //        string opcion = Console.ReadLine().ToLower();

        //        if (opcion.Contains("clie") || opcion.Contains("1"))
        //        {
        //            EliminarClientes();
        //            salir = false;
        //        }
        //        else if (opcion.Contains("reser") || opcion.Contains("2"))
        //        {

        //            salir = false;
        //        }
        //        else if (opcion.Contains("sal") || opcion.Contains("3"))
        //        {
        //            salir = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("No te entiendo vulve a introducir la opcion");
        //            Console.ReadKey();
        //            Console.Clear();
        //            salir = false;
        //        }

        //    } while (!salir);
        //    Console.ReadKey();
        //    Console.Clear();
        //}
        //public static void EliminarClientes()
        //{
        //    string query = $"SELECT * FROM CLIENTES";
        //    SqlCommand command = new SqlCommand(query, connection);
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();
        //    bool exit = false;
        //    Console.WriteLine("Esta es la tabla de clientes");
        //    while (reader.Read())
        //    {

        //        Console.WriteLine($"{reader[0]}---{reader[1]}---{reader[2]}---{reader[3]}\n");
        //    }
        //    connection.Close();
        //    do
        //    {
        //        Console.WriteLine("¿Que desea hacer?");
        //        Console.WriteLine("1-Eliminar");
        //        Console.WriteLine("2-Salir");
        //        string opcion = Console.ReadLine().ToLower();

        //        if (opcion.Contains("elim") || opcion.Contains("1"))
        //        {
        //            do
        //            {
        //                Console.WriteLine("Introduzca el DNI del cliente a eliminar");
        //                string dni = Console.ReadLine();

        //                query = $"SELECT * FROM CLIENTES WHERE DNI LIKE '{dni}'";
        //                command = new SqlCommand(query, connection);

        //                connection.Open();
        //                reader = command.ExecuteReader();

        //                if (reader.Read())
        //                {
        //                    connection.Close();
        //                    query = $"SELECT ID FROM CLIENTES WHERE DNI LIKE '{dni}'";
        //                    command = new SqlCommand(query, connection);
        //                    connection.Open();
        //                    reader = command.ExecuteReader();
        //                    if (reader.Read())
        //                    {
        //                        int idCliente = Convert.ToInt32(reader[0]);
        //                        connection.Close();

        //                        query = $"DELETE FROM CLIENTES WHERE DNI LIKE '{dni}'";
        //                        command = new SqlCommand(query, connection);
        //                        connection.Open();
        //                        if (command.ExecuteNonQuery() > 0)
        //                        {
        //                            connection.Close();
        //                            query = $"DELETE FROM RESERVAS WHERE ClienteID LIKE '{idCliente}'";
        //                            command = new SqlCommand(query, connection);
        //                            connection.Open();
        //                            if (command.ExecuteNonQuery() > 0)
        //                            {
        //                                Console.WriteLine("Se ha eliminado correctamente toda la informacion de cliente en la tabla CLIENTE y RESERVAS");
        //                            }   


        //                        }
        //                    }
        //                    salir = true;
        //                }
        //                else//si no existe el DNI
        //                {
        //                    connection.Close();
        //                    Console.WriteLine("DNI incorrecto, vuelve a introducir");
        //                    Console.WriteLine("**************");
        //                    salir = false;
        //                }

        //            } while (!salir);
        //            exit = true;
        //        }
        //        else if (opcion.Contains("sal") || opcion.Contains("2"))
        //        {
        //            exit = true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("No te entindo");
        //            Console.ReadKey();
        //            Console.Clear();
        //            exit = false;
        //        }
        //        connection.Close();
        //    } while (!exit);


        //    Console.ReadKey();
        //    Console.Clear();
        //}




    }
}
