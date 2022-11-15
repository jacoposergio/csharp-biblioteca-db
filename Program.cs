using System.Data.SqlClient;
using System.Numerics;

string stringaDiConnessione = "Data Source=localhost;Initial Catalog=db-biblioteca;Integrated Security=True";
SqlConnection connessioneSql = new SqlConnection(stringaDiConnessione);

bool loop = true;
while (loop)
{
    Console.WriteLine("1. Inserisci nuovo documento");
    Console.WriteLine("2. Cerca documento");
    int risposta = Convert.ToInt32(Console.ReadLine());


    switch (risposta)
    {
        case 1:
            try
            {
                InserisciDocumento();
            }
            catch (Exception e)
            {
                Console.WriteLine("error: {0}", e.Message);
            }
            break;
       case 2:
            try
            {
                CercaDocumento();
            }
            catch (Exception e)
            {
                Console.WriteLine("error: {0}", e.Message);
            }
            break;
        case 3:
            loop = false;
            break;
        default:
            Console.WriteLine("Sei capace di premere un tasto?");
            break;

    }
}


 void InserisciDocumento()
{
    Console.Write("inserisci id documento: ");
    int id = Convert.ToInt32(Console.ReadLine());
    Console.Write("inserisci codice documento: ");
    string codice = Console.ReadLine();
    Console.Write("inserisci titolo: ");
    string titolo = Console.ReadLine();
    Console.Write("inserisci anno: ");
    int anno = Convert.ToInt32(Console.ReadLine());
    Console.Write("inserisci settore: ");
    string settore = Console.ReadLine();
    Console.Write("inserisci 0 se è disponibile, 1 se non è disponibile : ");
    int disponibile = Convert.ToInt32(Console.ReadLine());
    Console.Write("inserisci scaffale: ");
    string scaffale = Console.ReadLine();
    Console.Write("inserisci autore: ");
    string autore_nome = Console.ReadLine();
    Console.Write("inserisci tipo (libro o dvd): ");
    string tipo = Console.ReadLine();
    int durata = 0;
    int pagine = 0;

    if (tipo == "libro")
    {
        Console.Write("inserisci numero di pagine: ");
        pagine = Convert.ToInt32(Console.ReadLine());
        if (tipo == "dvd")
    {
        Console.Write("inserisci durata: ");
        durata = Convert.ToInt32(Console.ReadLine());
        }
    else
    {
        pagine = 0;
        durata = 0;
    }
  }

    try
    {
        connessioneSql.Open();
        string insertQuery = "INSERT INTO Documenti (id, codice, titolo, anno, settore, disponibile, scaffale, autore_nome, tipo, durata, pagine ) VALUES (@id, @codice, @titolo, @anno, @settore, @disponibile, @scaffale, @autore_nome, @tipo, @durata, @pagine)";

        SqlCommand insertCommand = new SqlCommand(insertQuery, connessioneSql);

        insertCommand.Parameters.Add(new SqlParameter("@id", id));
        insertCommand.Parameters.Add(new SqlParameter("@codice", codice));
        insertCommand.Parameters.Add(new SqlParameter("@titolo", titolo));
        insertCommand.Parameters.Add(new SqlParameter("@anno", anno));
        insertCommand.Parameters.Add(new SqlParameter("@settore", settore));
        insertCommand.Parameters.Add(new SqlParameter("@disponibile", disponibile));
        insertCommand.Parameters.Add(new SqlParameter("@scaffale", scaffale));
        insertCommand.Parameters.Add(new SqlParameter("@autore_nome", autore_nome));
        insertCommand.Parameters.Add(new SqlParameter("@tipo", tipo));
        insertCommand.Parameters.Add(new SqlParameter("@durata", durata));
        insertCommand.Parameters.Add(new SqlParameter("@pagine", pagine));

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

}


void CercaDocumento()
{
    try
    {
        connessioneSql.Open();

        Console.Write("Inserisci il titolo del documento da cercare: ");
        string documentoCercato = Console.ReadLine();

        string insertQuery = "SELECT * FROM Documenti where titolo=@titolo";

        SqlCommand insertCommand = new SqlCommand(insertQuery, connessioneSql);
        insertCommand.Parameters.Add(new SqlParameter("@titolo", documentoCercato));

        SqlDataReader reader = insertCommand.ExecuteReader();

        while (reader.Read())
        {
            string titolo = reader.GetString(2);
            string autore = reader.GetString(7);
            bool tipo = reader.GetBoolean(5);
            string scaffale = reader.GetString(6);
            string settore = reader.GetString(4);


            Console.WriteLine("il titolo " + titolo + " di "+ autore);
            if (tipo)
            {
                Console.WriteLine("è disponibile al " + settore + ", scaffale " + scaffale);
            }
            else
            {
                Console.WriteLine("non è attualmente disponibile");
            }
        }
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
}
  