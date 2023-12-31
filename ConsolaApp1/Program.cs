﻿
//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;

using ConsolaApp1;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-12\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=123456";


    static void Main()
    {

       List<Trabajador> trabajadores = ListarTrabajadores() ;
        foreach(var lista in trabajadores)
        {
            Console.WriteLine($"ID: {lista.idTrabajador}," +
                $" Nombre: {lista.Nombres}, " +
                $"+Apellido: {lista.Apellidos} ," +
                $" Sueldo: {lista.Sueldo}, " +
                $"FechaNac: {lista.FechaNacimiento}");
        }

        ListarTrabajadoresDAT();






    }
    private static DataTable  ListarTrabajadoresDAT()
    {
        DataTable dataTable;
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Trabajadores";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            // Crear un DataTable para almacenar los resultados
             dataTable = new DataTable();

            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Mostrar los datos en la consola
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"ID: {row["idTrabajador"]}");
                Console.WriteLine($"Nombres: {row["Nombres"]}");
                Console.WriteLine($"Apellidos: {row["Apellidos"]}");
                Console.WriteLine($"Sueldo: {row["Sueldo"]}");
                Console.WriteLine($"Fecha de Nacimiento: {row["FechaNacimiento"]}");
                Console.WriteLine();
            }

            // Cerrar la conexión
            connection.Close();
        }
        return dataTable;
    }

    //De forma conectada
    private static List<Trabajador> ListarTrabajadores()
    {
        List<Trabajador> trabajadores = new List<Trabajador>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT idTrabajador,Nombres,Apellidos,Sueldo,FechaNacimiento FROM Trabajadores";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        Console.WriteLine("Lista de Trabajadores:");
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            trabajadores.Add(new Trabajador
                            {
                                idTrabajador = (int)reader["idTrabajador"],
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Sueldo = (decimal)reader["Sueldo"],
                                FechaNacimiento = (DateTime)reader["FechaNacimiento"]
                            });

                        }
                    }
                }
            }
            // Cerrar la conexión
            connection.Close();


        }
        return trabajadores;

    }


}
