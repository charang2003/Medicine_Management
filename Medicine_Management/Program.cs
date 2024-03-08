using System;
using MySql.Data.MySqlClient;

class Program
{
    static void Main()
    {
        // Replace "your_connection_string_here" with your actual connection string
        string connectionString = "server=localhost;uid=root;pwd=nikith@2003;database=student;";

        // Create a MySqlConnection object with the connection string
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                // Open the database connection
                connection.Open();
                Console.WriteLine("Connected to the database.");

                // Menu loop
                while (true)
                {
                    Console.WriteLine("\nMenu:");
                    Console.WriteLine("1. Search and Retrieve Data");
                    Console.WriteLine("2. Add New Data");
                    Console.WriteLine("3. Update Data");
                    Console.WriteLine("4. Exit");
                    Console.Write("Enter your choice (1-4): ");

                    if (int.TryParse(Console.ReadLine(), out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                SearchAndRetrieveData(connection);
                                break;
                            case 2:
                                AddNewData(connection);
                                break;
                            case 3:
                                UpdateData(connection);
                                break;
                            case 4:
                                // Exit the program
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // Close the database connection, whether an exception occurred or not
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Connection closed.");
                }
            }
        }
    }

    static void SearchAndRetrieveData(MySqlConnection connection)
    {
        Console.Write("Enter the ID to retrieve data: ");
        if (int.TryParse(Console.ReadLine(), out int inputId))
        {
            string query = "SELECT * FROM medicine WHERE id = @inputId";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@inputId", inputId);

                Console.WriteLine($"Executing query: {query}");

                try
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Display retrieved data
                                int id = reader.GetInt32("id");
                                string name = reader.GetString("name");
                                decimal price = reader.GetDecimal("price");
                                string isDiscontinued = reader.GetString("Is_discontinued");
                                string manufacturerName = reader.GetString("manufacturer_name");
                                string type = reader.GetString("type");
                                string packSizeLabel = reader.GetString("pack_size_label");
                                string shortComposition1 = reader.GetString("short_composition1");
                                string shortComposition2 = reader.GetString("short_composition2");

                                Console.WriteLine($"ID: {id}, Name: {name}, Price: {price}, " +
                                                  $"Discontinued: {isDiscontinued}, Manufacturer: {manufacturerName}, " +
                                                  $"Type: {type}, Pack Size: {packSizeLabel}, " +
                                                  $"Composition 1: {shortComposition1}, Composition 2: {shortComposition2}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No data found for ID {inputId}.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric ID.");
        }
    }

    static void AddNewData(MySqlConnection connection)
    {
        Console.WriteLine("\nEnter new data for the 'medicine' table:");

        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out int newId))
        {
            Console.Write("Enter Name: ");
            string newName = Console.ReadLine();

            Console.Write("Enter Price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice))
            {
                Console.Write("Enter Is Discontinued (Yes/No): ");
                string newIsDiscontinued = Console.ReadLine();

                Console.Write("Enter Manufacturer Name: ");
                string newManufacturerName = Console.ReadLine();

                Console.Write("Enter Type: ");
                string newType = Console.ReadLine();

                Console.Write("Enter Pack Size Label: ");
                string newPackSizeLabel = Console.ReadLine();

                Console.Write("Enter Composition 1: ");
                string newShortComposition1 = Console.ReadLine();

                Console.Write("Enter Composition 2: ");
                string newShortComposition2 = Console.ReadLine();

                // Insert the new data into the 'medicine' table
                string insertQuery = $"INSERT INTO medicine (id, name, price, Is_discontinued, manufacturer_name, type, pack_size_label, short_composition1, short_composition2) " +
                                     $"VALUES (@newId, @newName, @newPrice, @newIsDiscontinued, @newManufacturerName, @newType, @newPackSizeLabel, @newShortComposition1, @newShortComposition2)";

                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@newId", newId);
                    insertCommand.Parameters.AddWithValue("@newName", newName);
                    insertCommand.Parameters.AddWithValue("@newPrice", newPrice);
                    insertCommand.Parameters.AddWithValue("@newIsDiscontinued", newIsDiscontinued);
                    insertCommand.Parameters.AddWithValue("@newManufacturerName", newManufacturerName);
                    insertCommand.Parameters.AddWithValue("@newType", newType);
                    insertCommand.Parameters.AddWithValue("@newPackSizeLabel", newPackSizeLabel);
                    insertCommand.Parameters.AddWithValue("@newShortComposition1", newShortComposition1);
                    insertCommand.Parameters.AddWithValue("@newShortComposition2", newShortComposition2);

                    insertCommand.ExecuteNonQuery();
                    Console.WriteLine("New data added to the 'medicine' table.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Price should be a numeric value.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric ID.");
        }
    }

    static void UpdateData(MySqlConnection connection)
    {
        Console.Write("Enter the ID to update data: ");
        if (int.TryParse(Console.ReadLine(), out int updateId))
        {
            Console.Write("Enter new Name: ");
            string updatedName = Console.ReadLine();

            Console.Write("Enter new Price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal updatedPrice))
            {
                Console.Write("Enter new Is Discontinued (Yes/No): ");
                string updatedIsDiscontinued = Console.ReadLine();

                Console.Write("Enter new Manufacturer Name: ");
                string updatedManufacturerName = Console.ReadLine();

                Console.Write("Enter new Type: ");
                string updatedType = Console.ReadLine();

                Console.Write("Enter new Pack Size Label: ");
                string updatedPackSizeLabel = Console.ReadLine();

                Console.Write("Enter new Composition 1: ");
                string updatedShortComposition1 = Console.ReadLine();

                Console.Write("Enter new Composition 2: ");
                string updatedShortComposition2 = Console.ReadLine();

                // Update the data in the 'medicine' table
                string updateQuery = $"UPDATE medicine SET name = @updatedName, price = @updatedPrice, " +
                                     $"Is_discontinued = @updatedIsDiscontinued, manufacturer_name = @updatedManufacturerName, " +
                                     $"type = @updatedType, pack_size_label = @updatedPackSizeLabel, " +
                                     $"short_composition1 = @updatedShortComposition1, short_composition2 = @updatedShortComposition2 " +
                                     $"WHERE id = @updateId";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                {
                    updateCommand.Parameters.AddWithValue("@updatedName", updatedName);
                    updateCommand.Parameters.AddWithValue("@updatedPrice", updatedPrice);
                    updateCommand.Parameters.AddWithValue("@updatedIsDiscontinued", updatedIsDiscontinued);
                    updateCommand.Parameters.AddWithValue("@updatedManufacturerName", updatedManufacturerName);
                    updateCommand.Parameters.AddWithValue("@updatedType", updatedType);
                    updateCommand.Parameters.AddWithValue("@updatedPackSizeLabel", updatedPackSizeLabel);
                    updateCommand.Parameters.AddWithValue("@updatedShortComposition1", updatedShortComposition1);
                    updateCommand.Parameters.AddWithValue("@updatedShortComposition2", updatedShortComposition2);
                    updateCommand.Parameters.AddWithValue("@updateId", updateId);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"Data updated for ID {updateId}.");
                    }
                    else
                    {
                        Console.WriteLine($"No data found for ID {updateId}.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Price should be a numeric value.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid numeric ID.");
        }
    }
}
