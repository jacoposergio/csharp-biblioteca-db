using System.Data.SqlClient;

string stringaDiConnessione = "Data Source=localhost;Initial Catalog=db-biblioteca;Integrated Security=True";
SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione);

try
{
    connessioneSql.Open();
    string insertQuery = "INSERT INTO Documenti (id, codice, titolo, anno, settore, disponibile, scaffale, autore_nome, tipo, durata, pagine ) VALUES (@id ,@codice, @titolo, @anno, @settore, @disponibile, @scaffale, @autore_nome, @tipo, @durata, @pagine)";

    SqlCommand insertCommand = new SqlCommand(insertQuery, connessioneSql);

    insertCommand.Parameters.Add(new SqlParameter("@id", 2));
    insertCommand.Parameters.Add(new SqlParameter("@codice", "12230"));
    insertCommand.Parameters.Add(new SqlParameter("@titolo", "ciao"));
    insertCommand.Parameters.Add(new SqlParameter("@anno", 1987));
    insertCommand.Parameters.Add(new SqlParameter("@settore", "434 bfv"));
    insertCommand.Parameters.Add(new SqlParameter("@disponibile", 1));
    insertCommand.Parameters.Add(new SqlParameter("@scaffale", "4c destra"));
    insertCommand.Parameters.Add(new SqlParameter("@autore_nome", "Stephen king"));
    insertCommand.Parameters.Add(new SqlParameter("@tipo", "libro"));
    insertCommand.Parameters.Add(new SqlParameter("@durata", 325));

    insertCommand.Parameters.Add(new SqlParameter("@pagine", 325));

    int affectedRows = insertCommand.ExecuteNonQuery();

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    connessioneSql.Close();
}

return;
